using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BricksBucket.Core.Editor.Attributes
{
    // ReSharper disable CommentTypo
    /// <!-- AutoPropertyDrawer -->
    ///
    /// <summary>
    ///
    /// <para>
    /// Property Drawer for the AutoProperty Attribute.
    /// </para>
    /// 
    /// <para>
    /// Based in the <see href="https://github.com/Deadcows/MyBox">MyBox
    /// project by @deadcows</see>.
    /// </para>
    ///
    /// </summary>
    ///
    /// <seealso href="https://github.com/Deadcows/MyBox">
    /// Deadcows/MyBox</seealso>
    /// 
    /// <!-- By Javier García | @jvrgms | 2020 -->
    // ReSharper restore CommentTypo
    [CustomPropertyDrawer (typeof (AutoPropertyAttribute))]
    public class AutoPropertyDrawer : PropertyDrawer
    {
        /// <inheritdoc cref="PropertyDrawer.GetPropertyHeight"/>
        public override float
        GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight (property);
        }

        /// <inheritdoc cref="PropertyDrawer.OnGUI"/>
        public override void
        OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField (position, property, label);
            GUI.enabled = true;
        }
    }

    // ReSharper disable CommentTypo
    /// <!-- AutoPropertyHandler -->
    ///
    /// <summary>
    ///
    /// <para>
    /// Called on will save assets to search for properties and fill them.
    /// </para>
    /// 
    /// <para>
    /// Based in the <see href="https://github.com/Deadcows/MyBox">MyBox
    /// project by @deadcows</see>.
    /// </para>
    ///
    /// </summary>
    ///
    /// <seealso href="https://github.com/Deadcows/MyBox">
    /// Deadcows/MyBox</seealso>
    /// 
    /// <!-- By Javier García | @jvrgms | 2020 -->
    // ReSharper restore CommentTypo
    [InitializeOnLoad]
    public static class AutoPropertyHandler
    {
        /// <summary>
        /// Subscribing to editor events.
        /// </summary>
        static AutoPropertyHandler () =>
            EditorEvents.OnSaveAssets += CheckComponents;

        /// <summary>
        /// Check for all auto property attributes.
        /// </summary>
        private static void CheckComponents ()
        {
            var components =
                SerializedUtils.GetFieldsWithAttribute
                <AutoPropertyAttribute> ();

            for (int i = 0; i < components.Length; i++)
                FillProperty (components[i]);
        }

        /// <summary>
        /// Fills the property.
        /// </summary>
        /// <param name="property">Info of the component.</param>
        private static void FillProperty (ComponentFieldInfo property)
        {
            var propertyType = property.field.FieldType;

            if (propertyType.IsArray)
            {
                var underlyingType = propertyType.GetElementType ();
                // ReSharper disable once CoVariantArrayConversion
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
            
            // TODO: Change Log method in AutoPropertyHandler.FillProperty.
            Debug.LogError ("AutoProperty not found in {0} component.");
            /*
            DebugEditor.LogErrorFormat (
                context: property.component,
                format: string.Concat(
                    "AutoProperty not found in {0} component.",
                    "Make sure it is also serialized."),
                data: property.component.name
            );
            */

            throw SerializedUtils.FieldNotFoundException (
                type: propertyType,
                field: property.field.Name
            );
        }
    }
}
