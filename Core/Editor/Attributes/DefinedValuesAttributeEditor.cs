using System;
using UnityEditor;
using UnityEngine;

namespace BricksBucket.Core.Editor.Attributes
{
    /// <summary>
    ///
    /// Defined Values Attribute Drawer.
    ///
    /// <para>
    /// Helps to draw a pop up of defined value on editor.
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
    [CustomPropertyDrawer (typeof (DefinedValuesAttribute))]
    public class DefinedValuesAttributeDrawer : PropertyDrawer
    {
        #region Class Memebers

        /// <summary> Reference to attribute to draw. </summary>
        private DefinedValuesAttribute _attribute;

        /// <summary> Type of variable. </summary>
        private Type _variableType;

        /// <summary> Values to show. </summary>
        private string[] _values;

        /// <summary> Index selected. </summary>
        private int _selectedIndex = -1;

        #endregion



        #region Accessors

        /// <summary> Returns wether the type Unicode or a string. </summary>
        private bool IsString
        {
            get
            {
                return
                    _variableType.IsString () ||
                    _variableType.IsChar ();
            }
        }

        /// <summary> Returns wether the type is an integral type. </summary>
        private bool IsInt
        {
            get
            {
                return
                    _variableType.IsSignedByte () ||
                    _variableType.IsByte () ||
                    _variableType.IsShort () ||
                    _variableType.IsUnsignedShort () ||
                    _variableType.IsInt () ||
                    _variableType.IsUnsignedInt () ||
                    _variableType.IsUnsignedLong () ||
                    _variableType.IsLong ();
            }
        }

        /// <summary> Returns wether the type is a floating type. </summary>
        private bool IsFloat
        {
            get
            {
                return
                    _variableType.IsFloat () ||
                    _variableType.IsDouble () ||
                    _variableType.IsDecimal ();
            }
        }


        #endregion



        #region Class Implementation.

        /// <summary> Drawa attribute on GUI. </summary>
        /// <param name="position"> Position to draw attributte. </param>
        /// <param name="property"> Property to draw. </param>
        /// <param name="label"> Label of the property. </param>
        public override void
        OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            if (_attribute == null)
                Initialize (property);

            if (_values == null || _values.Length == 0 || _selectedIndex < 0)
            {
                EditorGUI.PropertyField (position, property, label);
                return;
            }

            EditorGUI.BeginChangeCheck ();

            _selectedIndex = EditorGUI.Popup (
                position: position,
                label: label.text,
                selectedIndex: _selectedIndex,
                displayedOptions: _values
            );

            if (EditorGUI.EndChangeCheck ())
            {
                property.SetObjectValue (
                    _variableType,
                    _values[_selectedIndex]
                );
                property.serializedObject.ApplyModifiedProperties ();
            }
        }

        /// <summary> Initialize a property. </summary>
        /// <param name="property"> Property to draw. </param>
        private void Initialize (SerializedProperty property)
        {
            _attribute = (DefinedValuesAttribute) attribute;

            object[] values = _attribute.valuesArray;

            if (values == null || values.Length == 0)
                return;

            _variableType = property.GetObjectType ();

            if (!IsValidType ())
                return;

            _values = new string[values.Length];
            for (int i = 0; i < values.Length; i++)
                _values[i] = values[i].ToString ();

            _selectedIndex = GetSelectedIndex (property);
        }

        /// <summary> Returns wether the type is valid. </summary>
        /// <returns> Wether the type is valid. </returns>
        private bool IsValidType () => IsInt || IsFloat || IsString;

        /// <summary> Returns the index acording to the value. </summary>
        /// <param name="property"> Property to change. </param>
        /// <returns> Index of the chosen value. </returns>
        private int GetSelectedIndex (SerializedProperty property)
        {
            for (var i = 0; i < _values.Length; i++)
            {
                switch (property.propertyType)
                {
                    case SerializedPropertyType.String:
                    if (property.stringValue == _values[i])
                        return i;
                    break;

                    case SerializedPropertyType.Integer:
                    if(property.intValue == Convert.ToInt32(_values[i]))
                        return i;
                    break;

                    case SerializedPropertyType.Float:
                    if (property.floatValue.Approximately (
                        Convert.ToSingle (_values[i])
                    ))
                        return i;
                    break;

                    default:
                    return 0;
                }
            }
            return 0;
        }

        #endregion
    }
}
