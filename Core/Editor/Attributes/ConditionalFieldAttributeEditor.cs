using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BricksBucket.Core.Editor.Attributes
{
    /// <summary>
    ///
    /// Conditional Field Attribute Drawer.
    ///
    /// <para>
    /// Drawer for properties to hide or show the property according to
    /// other property.
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
	[CustomPropertyDrawer (typeof(ConditionalFieldAttribute))]
	public class ConditionalFieldAttributeDrawer : PropertyDrawer
	{

        #region Class Members

        /// <summary> History of warnings. </summary>
        private readonly HashSet<object> _warnings = new HashSet<object> ();

        /// <summary> Reference attribute. </summary>
        private ConditionalFieldAttribute _attribute;

        /// <summary> Flag for visibility. </summary>
        private bool _isVisible = true;

        /// <summary> Access referenced attribute.</summary>
        private ConditionalFieldAttribute Attribute
        {
            get
            {
                return _attribute ?? (
                    _attribute = attribute as ConditionalFieldAttribute
                );
            }
        }

        #endregion



        #region Property Drawer Overrides

        /// <summary> Returns the height of the property. </summary>
        /// <param name="property"> Property to draw. </param>
        /// <param name="label"> Label to show. </param>
        /// <returns> Height of the property. </returns>
        public override float
        GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			_isVisible = CheckPropertyVisibility(property);

			return _isVisible ? EditorGUI.GetPropertyHeight(property) : 0;
		}

        /// <summary> Called on GUI to draw property. </summary>
        /// <param name="position"> Position to draw property. </param>
        /// <param name="property"> Property to draw. </param>
        /// <param name="label"> Label to show on draw. </param>
		public override void
        OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (_isVisible)
                EditorGUI.PropertyField(position, property, label, true);
		}

        #endregion



        #region Class Implementation

        /// <summary> Checks wether a property is visible. </summary>
        /// <param name="property"> Property to draw. </param>
        /// <returns> Wether a property is visible. </returns>
        public bool CheckPropertyVisibility (SerializedProperty property)
        {

            SerializedProperty condition = FindRelativeProperty (
                property: property,
                toFind: Attribute.propertyToCheck
            );

            if (condition == null)
                return true;

            bool isBoolMatch =
                condition.propertyType == SerializedPropertyType.Boolean &&
                condition.boolValue;

            string compareStringValue = Attribute.compareValue != null ?
                Attribute.compareValue.ToString ().ToUpper () :
                "NULL";

            if (isBoolMatch && compareStringValue == "FALSE")
                isBoolMatch = false;

            string conditionStringValue = condition.ToStringValue ().ToUpper ();
            bool objectMatch = compareStringValue == conditionStringValue;
            bool isVisible = !(!isBoolMatch && !objectMatch);
            if (Attribute.inverse)
                isVisible = !isVisible;
            return isVisible;
        }

        /// <summary> Finds the relative property. </summary>
        /// <param name="property"> Property to draw. </param>
        /// <param name="toFind"> Name of the property to find. </param>
        /// <returns> Relative property. </returns>
        protected SerializedProperty
        FindRelativeProperty (SerializedProperty property, string toFind)
        {
            if (property.depth == 0)
                return property.serializedObject.FindProperty (toFind);

            var parent = property.FindParentProperty ();
            var path = property.GetValuePath ();

            // If nested property is equals to null hit an array property.
            if (parent == null)
            {
                var cleanPath = path.Substring (0, path.IndexOf ('['));

                var propertySerializedObject = property.serializedObject;
                if (propertySerializedObject == null)
                    return null;

                var arrayProperty = propertySerializedObject.FindProperty (
                    propertyPath: cleanPath
                );
                if (arrayProperty == null)
                    return null;

                if (_warnings.Contains (arrayProperty.exposedReferenceValue))
                    return null;

                var arraySerializedObject = arrayProperty.serializedObject;
                if (arraySerializedObject == null)
                    return null;

                var target = arraySerializedObject.targetObject;
                if (target == null)
                    return null;

                /*
                DebugEditor.LogWarningFormat (
                    context: target,
                    format: StringUtils.Concat(
                        "Property {0} in object {1} are not supported ",
                        "by  [ConditionalFieldAttribute]"
                    ),
                    data: new object[] { arrayProperty.name, target.name }
                );
                */
                _warnings.Add (arrayProperty.exposedReferenceValue);
                return null;
            }

            return parent.FindPropertyRelative (toFind);
        }

        #endregion
    }
}
