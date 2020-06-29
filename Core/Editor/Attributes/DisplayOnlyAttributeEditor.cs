using UnityEditor;
using UnityEngine;

namespace BricksBucket.Core.Editor.Attributes
{
    /// <!-- DisplayOnlyAttributeDrawer -->
    /// 
    /// <summary>
    /// Draws property but disables its edition.
    /// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[CustomPropertyDrawer (typeof(DisplayOnlyAttribute))]
	public class DisplayOnlyAttributeDrawer : PropertyDrawer
	{
        /// <inheritdoc cref="PropertyDrawer.GetPropertyHeight"/>
		public override float
        GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, true);
		}
		
		/// <inheritdoc cref="PropertyDrawer.OnGUI"/>
        public override void
        OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			GUI.enabled = false;
			EditorGUI.PropertyField(position, property, label, true);
			GUI.enabled = true;
		}
	}
}
