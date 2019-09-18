using UnityEngine;
using UnityEditor;

using Object = UnityEngine.Object;

namespace BricksBucket
{
    /// <summary>
    ///
    /// Auto Property Drawer.
    ///
    /// <para>
    /// Property Drawer for the AutoProperty Attribute.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the MyBox project by @deadcows.
    /// https://github.com/Deadcows/MyBox
    /// </para>
    ///
    /// </summary>
    [CustomPropertyDrawer (typeof (AutoFillAttribute))]
    public class AutoPropertyDrawer : PropertyDrawer
    {
        #region Property Drawer Overrides

        /// <summary> Called to return the Height of a property. </summary>
        /// <param name="property"> Property to draw. </param>
        /// <param name="label"> Label to draw. </param>
        /// <returns> Height to draw property.</returns>
        public override float
        GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight (property);
        }

        /// <summary> Called On GUI </summary>
        /// <param name="position"> Position to draw. </param>
        /// <param name="property"> Property to draw. </param>
        /// <param name="label"> Label to draw. </param>
        public override void
        OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField (position, property, label);
            GUI.enabled = true;
        }

        #endregion
    }

    /// <summary>
    ///
    /// Auto Property Handler.
    ///
    /// <para>
    /// Called on will save assets to search for properties and fill them.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the MyBox project by @deadcows.
    /// https://github.com/Deadcows/MyBox
    /// </para>
    ///
    /// </summary>
    [InitializeOnLoad]
    public static class AutoPropertyHandler
    {
        #region Constructor

        /// <summary> Subscribing to editor events. </summary>
        static AutoPropertyHandler () =>
            EditorEvents.OnSaveAssets += CheckComponents;

        #endregion



        #region Class Implementation

        /// <summary> Check for all auto property attributes. </summary>
        private static void CheckComponents ()
        {
            var components =
                SerializedUtils.GetFieldsWithAttribute <AutoFillAttribute> ();

            for (int i = 0; i < components.Length; i++)
                FillProperty (components[i]);
        }

        /// <summary> Fills the property. </summary>
        /// <param name="property"></param>
        private static void FillProperty (ComponentFieldInfo property)
        {
            var propertyType = property.field.FieldType;

            if (propertyType.IsArray)
            {
                var underlyingType = propertyType.GetElementType ();
                Object[] components =
                    property.component.GetComponentsInChildren (
                        t: underlyingType,
                        includeInactive: true
                    );

                if (components != null && components.Length > 0)
                {
                    var serializedObject = new SerializedObject (
                        obj: property.component
                    );

                    var serializedProperty = serializedObject.FindProperty (
                        propertyPath: property.field.Name
                    );

                    serializedProperty.ReplaceArray (components);
                    serializedObject.ApplyModifiedProperties ();
                    serializedObject.Dispose ();
                    return;
                }
            }
            else
            {
                var component = property.component.GetComponentInChildren (
                    t: propertyType,
                    includeInactive: true
                );

                if (component != null)
                {
                    var serializedObject = new SerializedObject (
                        obj: property.component
                    );

                    var serializedProperty = serializedObject.FindProperty (
                        propertyPath: property.field.Name
                    );

                    if (serializedProperty != null)
                    {
                        serializedProperty.objectReferenceValue = component;
                        serializedObject.ApplyModifiedProperties ();
                        serializedObject.Dispose ();
                        return;
                    }
                }
            }

            DebugEditor.LogErrorFormat (
                context: property.component,
                format: StringUtils.Concat (
                    "AutoProperty not found in {0} component.",
                    "Make sure it is also serialized."),
                data: property.component.name
            );

            throw SerializedUtils.FieldNotFoundException (
                type: propertyType,
                field: property.field.Name
            );
        }

        #endregion
    }
}
