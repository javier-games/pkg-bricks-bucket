using UnityEditor;
using UnityEngine;

namespace BricksBucket
{
    /// <summary>
    ///
    /// Sprite Layer Attribute Drawer.
    ///
    /// <para>
    /// Editor tool to draw Popup with sprite layers available.
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
	[CustomPropertyDrawer (typeof(SpriteLayerAttribute))]
	public class SpriteLayerAttributeDrawer : PropertyDrawer
	{

        /// <summary> Wether ther type has been checked. </summary>
        private bool _checkedType;

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
        OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType != SerializedPropertyType.Integer)
			{
                if (!_checkedType)
                {
                    var serializedObject = property.serializedObject;
                    DebugEditor.LogWarningFormat (
                        context: serializedObject?.targetObject,
                        format: StringUtils.Concat(
                            "Property {0} in object {1} is of wrong type.",
                            "Type expected: Int"
                        ),
                        data: new object[]{
                            property.name,
                            serializedObject?.targetObject
                        }
                    );
                    _checkedType = true;
                }
				EditorGUI.PropertyField(position, property, label);
				return;
			}

			var spriteLayerNames = GetSpriteLayerNames();

            //  Drawing the pop up.
            EditorGUI.BeginProperty (position, label, property);

            // To show which sprite layer is currently selected.
            bool layerFound = TryGetSpriteLayerIndexFromProperty (
                index: out int currentSpriteLayerIndex,
                spriteLayerNames: spriteLayerNames,
                property: property
            );

            if (!layerFound)
            {
                // Set to default layer. (Previous layer was removed)
                var serializedObject = property.serializedObject;
                DebugEditor.LogWarningFormat (
                    context: serializedObject?.targetObject,
                    format: StringUtils.Concat (
                        "Property {0} in object {1} is set to the default.",
                        "Previously selected layer was removed."
                    ),
                    data: new object[]{
                        property.name,
                        serializedObject?.targetObject
                    }
                );
                property.intValue = 0;
                currentSpriteLayerIndex = 0;
            }

            int selectedSpriteLayerIndex = EditorGUI.Popup (
                position: position,
                label: label.text,
                selectedIndex: currentSpriteLayerIndex,
                displayedOptions: spriteLayerNames
            );

            // Change property value if user selects a new sprite layer.
            if (selectedSpriteLayerIndex != currentSpriteLayerIndex)
                property.intValue = SortingLayer.NameToID (
                    spriteLayerNames[selectedSpriteLayerIndex]
                );

            EditorGUI.EndProperty ();
        }

        #endregion



        #region Class Implementation

        /// <summary> Try to gets the current index sprite. </summary>
        /// <param name="index"> Current index. </param>
        /// <param name="spriteLayerNames"> Sprite layer names. </param>
        /// <param name="property"> Int Property to draw. </param>
        /// <returns> Wether or not gets the current index sprite. </returns>
		private bool TryGetSpriteLayerIndexFromProperty(
            out int index,
            string[] spriteLayerNames,
            SerializedProperty property
        ) {
			string layerName = SortingLayer.IDToName(property.intValue);

			// Return the index where on it matches.
			for (int i = 0; i < spriteLayerNames.Length; ++i)
                if (spriteLayerNames[i].Equals(layerName))
				{
					index = i;
					return true;
				}

			// The current layer was removed.
			index = -1;
			return false;
		}

        /// <summary> Returns the collection of sprite layer names.  </summary>
        /// <returns> Collection of sprite layer names. </returns>
		private string[] GetSpriteLayerNames()
		{
			string[] result = new string[SortingLayer.layers.Length];

			for (int i = 0; i < result.Length; ++i)
				result[i] = SortingLayer.layers[i].name;

			return result;
		}

		#endregion
	}
}
