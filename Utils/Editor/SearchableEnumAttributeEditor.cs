using UnityEngine;
using UnityEditor;

namespace BricksBucket
{
    /// <summary>
    /// 
    /// Searchable PopUp Attribute.
    /// 
    /// <para>
    /// Draws the custom enum selector popup for enum fields using the
    /// SearchableEnumAttribute.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the UnityEditorJunkie project by @roboryantron.
    /// https://github.com/roboryantron/UnityEditorJunkie
    /// </para>
    /// 
    /// </summary>
    [CustomPropertyDrawer (typeof (SearchableEnumAttribute))]
    public class SearchableEnumAttributeDrawer : PropertyDrawer
    {

        /// <summary>
        /// Cache of the hash to use to resolve the ID for the drawer.
        /// </summary>
        private int idHash;

        public override void
        OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            // If this is not used on an enum, show an error
            if (property.propertyType != SerializedPropertyType.Enum)
            {
                EditorGUILayout.HelpBox (
                    message: StringUtils.Concat (
                        "SearchableEnum attribute is used ",
                        "on non-enum property."),
                    type: MessageType.Warning
                );

                EditorGUI.PropertyField (position, property, true);
                return;
            }

            // By manually creating the control ID, we can keep the ID for the
            // label and button the same. This lets them be selected together
            // with the keyboard in the inspector, much like a normal popup.
            if (idHash == 0)
                idHash = "SearchableEnumAttributeDrawer".GetHashCode ();

            int id = GUIUtility.GetControlID (
                hint: idHash,
                focusType: FocusType.Keyboard,
                rect: position
            );

            label = EditorGUI.BeginProperty (position, label, property);
            position = EditorGUI.PrefixLabel (position, id, label);

            GUIContent buttonText = new GUIContent (
                text: property.enumDisplayNames[property.enumValueIndex]
            );

            if (DropdownButton (id, position, buttonText))
            {
                void onSelect (int i)
                {
                    property.enumValueIndex = i;
                    property.serializedObject.ApplyModifiedProperties ();
                }

                SearchablePopup.Show (
                    activatorRect: position,
                    options: property.enumDisplayNames,
                    current: property.enumValueIndex,
                    onSelectionCallback: onSelect
                );
            }

            EditorGUI.EndProperty ();
        }

        /// <summary>
        /// A custom button drawer that allows for a controlID so that we can
        /// sync the button ID and the label ID to allow for keyboard
        /// navigation like the built-in enum drawers.
        /// </summary>
        /// <param name="id"> Unique ID reference of the control. </param>
        /// <param name="position"> Position where to draw popup. </param>
        /// <param name="content"> Current option. </param>
        /// <returns> Wether the button has been pressed. </returns>
        private static bool
        DropdownButton (int id, Rect position, GUIContent content)
        {
            Event current = Event.current;
            switch (current.type)
            {
                case EventType.MouseDown:
                if (
                    position.Contains (current.mousePosition) &&
                    current.button == 0
                ) {
                    Event.current.Use ();
                    return true;
                }

                break;
                case EventType.KeyDown:
                if (
                    GUIUtility.keyboardControl == id &&
                    current.character == '\n'
                ) {
                    Event.current.Use ();
                    return true;
                }

                break;
                case EventType.Repaint:
                EditorStyles.popup.Draw (position, content, id, false);
                break;
            }

            return false;
        }
    }

}
