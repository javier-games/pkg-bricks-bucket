using UnityEditor;
using UnityEngine;

namespace BricksBucket
{
    /// <summary>
    /// 
    /// Positive Value Only Attribute Drawer
    /// 
    /// <para>
    /// Draws and help to not use negative values on numeric properties.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the MyBox project by @deadcows.
    /// https://github.com/Deadcows/MyBox
    /// </para>
    ///
    /// <para>
    /// Original version by @Kaynn-Cahya
    /// https://github.com/Kaynn-Cahya
    /// </para>
    /// 
    /// </summary>
    [CustomPropertyDrawer (typeof(PositiveValueOnlyAttribute))]
	public class PositiveValueOnlyAttributeDrawer : PropertyDrawer
	{
        /// <summary> Called to return the Height of a property. </summary>
        /// <param name="property"> Property to draw. </param>
        /// <param name="label"> Label to draw. </param>
        /// <returns> Height to draw property.</returns>
		public override float
        GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property);
		}

        /// <summary> Called on GUI to draw property. </summary>
        /// <param name="position"> Position to draw property. </param>
        /// <param name="property"> Property to draw. </param>
        /// <param name="label"> Label to draw. </param>
		public override void
        OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
            if (!IsNumerical (property.propertyType))
            {
                EditorGUILayout.HelpBox (
                    message: StringUtils.Concat (
                        "PositiveValueOnly attribute is used ",
                        "on non-numeric property."),
                    type: MessageType.Warning
                );
            }
            else
                if (HandleNegativeValues (property))
                    property.serializedObject.ApplyModifiedProperties ();

			EditorGUI.PropertyField(position, property, true);
		}


		/// <summary> Handle negative values on properties. </summary>
		/// <returns> Wether the value is set to positive. </returns>
		private bool HandleNegativeValues(SerializedProperty property)
		{
			switch (property.propertyType)
			{
				case SerializedPropertyType.Float:
				case SerializedPropertyType.Integer:
					return HandleNegativeNumerics(property);

				case SerializedPropertyType.Vector2:
				case SerializedPropertyType.Vector3:
				case SerializedPropertyType.Vector4:
					return HandleVectors(property);

				case SerializedPropertyType.Vector2Int:
				case SerializedPropertyType.Vector3Int:
					return HandleIntVectors(property);
			}

			return false;
		}

        /// <summary> Handles negative values in numeric properties. </summary>
        /// <param name="property"> Property to handle with. </param>
        /// <returns> Wether the property had to be handle. </returns>
		private bool HandleNegativeNumerics(SerializedProperty property)
		{
            switch (property.propertyType)
            {
                case SerializedPropertyType.Float:
                    if(property.floatValue < 0)
                {
                    property.floatValue = 0;
                    return true;
                }
                return false;

                case SerializedPropertyType.Integer:
                if (property.intValue < 0)
                {
                    property.intValue = 0;
                    return true;
                }
                return false;
            }
			return false;
		}

        /// <summary> Handles negative values in vector properties. </summary>
        /// <param name="property"> Property to handle with. </param>
        /// <returns> Wether the property had to be handle. </returns>
        private bool HandleVectors(SerializedProperty property)
		{
			Vector4 vector = Vector4.zero;
			switch (property.propertyType)
			{
				case SerializedPropertyType.Vector2:
					vector = property.vector2Value;
					break;
				case SerializedPropertyType.Vector3:
					vector = property.vector3Value;
					break;
				case SerializedPropertyType.Vector4:
					vector = property.vector4Value;
					break;
			}

			bool handled = false;
			for (int i = 0; i < 4; ++i)
			{
				if (vector[i] < 0f)
				{
					vector[i] = 0;
					handled = true;
				}
			}

			switch (property.propertyType)
			{
				case SerializedPropertyType.Vector2:
					property.vector2Value = vector;
					break;
				case SerializedPropertyType.Vector3:
					property.vector3Value = vector;
					break;
				case SerializedPropertyType.Vector4:
					property.vector4Value = vector;
					break;
			}

			return handled;
		}

        /// <summary> Handles negative values in int vectors. </summary>
        /// <param name="property"> Property to handle with. </param>
        /// <returns> Wether the property had to be handle. </returns>
		private bool HandleIntVectors (SerializedProperty property)
        {
            switch (property.propertyType)
            {

                case SerializedPropertyType.Vector2Int:
                var vector = property.vector2IntValue;
                if (vector.x > 0 && vector.y > 0)
                    return false;

                property.vector2IntValue = new Vector2Int (
                    vector.x < 0 ? 0 : vector.x,
                    vector.y < 0 ? 0 : vector.y);
                return true;

                case SerializedPropertyType.Vector3Int:
                var vector3 = property.vector3IntValue;
                if (vector3.x > 0 && vector3.y > 0 && vector3.z > 0)
                    return false;
                property.vector3Value = new Vector3 (
                    vector3.x < 0 ? 0 : vector3.x,
                    vector3.y < 0 ? 0 : vector3.y,
                    vector3.z < 0 ? 0 : vector3.z);
                return true;
            }
			return false;
		}

        /// <summary> Defines wether is a valid type. </summary>
        /// <param name="propertyType"> Property type to check. </param>
        /// <returns> Wether is a valid type. </returns>
        private bool IsNumerical(SerializedPropertyType propertyType)
		{
			switch (propertyType)
			{
				case SerializedPropertyType.Float:
				case SerializedPropertyType.Integer:
				case SerializedPropertyType.Vector2:
				case SerializedPropertyType.Vector3:
				case SerializedPropertyType.Vector4:
				case SerializedPropertyType.Vector2Int:
				case SerializedPropertyType.Vector3Int:
				return true;

				default:
                return false;
			}
		}
	}
}
