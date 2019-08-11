using UnityEngine;
using UnityEditor;

namespace BricksBucket
{
    /// <summary>
    ///
    /// Layer Attribute Drawer.
    ///
    /// <para>
    /// Editor tool to draw int as LayerMask.
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
    [CustomPropertyDrawer (typeof (LayerAttribute))]
    public class LayerAttributeDrawer : PropertyDrawer
    {
        #region Class Members

        /// <summary> Wether the type has been checked. </summary>
        private bool _checked;

        #endregion



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

        /// <summary> Called on GUI to draw property. </summary>
        /// <param name="position"> Position to draw property. </param>
        /// <param name="property"> Property to draw. </param>
        /// <param name="label"> Label to draw. </param>
        public override void
        OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Integer)
            {
                if (!_checked)
                {
                    var serializedObject = property.serializedObject;
                    DebugEditor.LogWarningFormat (
                        context: serializedObject?.targetObject,
                        format: StringUtils.Concat (
                            "Property {0} in object {1} is of wrong type.",
                            "Type expected: Int"
                        ),
                        data: new object[]{
                            property.name,
                            serializedObject?.targetObject
                        }
                    );
                    _checked = true;
                }
                EditorGUI.PropertyField (position, property, label);
                return;
            }

            property.intValue = EditorGUI.LayerField (
                position: position,
                label: label,
                layer: property.intValue
            );
        }

        #endregion
    }
}
