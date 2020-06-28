using System;
using UnityEditor;
using UnityEngine;

namespace BricksBucket.Core.Editor.Attributes
{
    /// <summary>
    ///
    /// Layer Attribute Drawer.
    ///
    /// <para>
    /// Editor tool to draw an int as mask.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// </summary>
    [CustomPropertyDrawer (typeof (CustomMaskAttribute))]
    public class CustomMaskAttributeDrawer : PropertyDrawer
    {
        #region Class Members

        /// <summary> Wether the type has been checked. </summary>
        private bool _checked;

        #endregion

        private CustomMaskAttribute _attribute;

        private CustomMaskAttribute Attribute
        {
            get
            {
                return _attribute ?? (
                    _attribute = attribute as CustomMaskAttribute
                );
            }
        }



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
                    /*
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
                    */
                    _checked = true;
                }
                EditorGUI.PropertyField (position, property, label);
                return;
            }

            string[] enumNames = Enum.GetNames (Attribute.enumType);

            if(enumNames == null || enumNames.Length == 0)
            {
                if (!_checked)
                {
                    var serializedObject = property.serializedObject;
                    /*
                    DebugEditor.LogWarningFormat (
                        context: serializedObject?.targetObject,
                        format: "Enum Type {0} has not elements.",
                        data: Attribute.EnumType
                    );
                    */
                    _checked = true;
                }
                EditorGUI.PropertyField (position, property, label);
                return;
            }

            property.intValue = EditorGUI.MaskField (
                position: position,
                label: label,
                mask: property.intValue,
                displayedOptions: Enum.GetNames (Attribute.enumType)
            );
        }

        #endregion
    }
}
