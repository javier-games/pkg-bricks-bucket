using UnityEngine;
using UnityEditor;

namespace BricksBucket
{
    /// <summary>
    /// 
    /// Read Only Attribute Drawer.
    /// 
    /// <para>
    /// Draws property but disables editablity.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// </summary>
	[CustomPropertyDrawer (typeof(DisplayOnlyAttribute))]
	public class DisplayOnlyAttributeDrawer : PropertyDrawer
	{
        /// <summary> Called to return the Height of a property. </summary>
        /// <param name="property"> Property to draw. </param>
        /// <param name="label"> Label to draw. </param>
        /// <returns> Height to draw property.</returns>
		public override float
        GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, true);
		}

        /// <summary> Called on GUI to draw property. </summary>
        /// <param name="position"> Position to draw property. </param>
        /// <param name="property"> Property to draw. </param>
        /// <param name="label"> Label to draw. </param>
        public override void
        OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			GUI.enabled = false;
			EditorGUI.PropertyField(position, property, label, true);
			GUI.enabled = true;
		}
	}
}
