using UnityEngine;
using UnityEditor;

namespace BricksBucket.Utils
{
    /// <summary>
    /// 
    /// Debug Utils Setup Editor.
    /// 
    /// <para>
    /// Displays as a mask the filter on DebugUtilsSetup.cs.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// </summary>
    [CustomEditor (typeof (DebugUtilsSetup))]
    public sealed class DebugUtilsSetupEditor : Editor
    {

        private SerializedProperty filterProperty;  // DebugUtilsSetup.filter.

        private readonly string[] levelArray =
        {
            LogLayer.Debug.ToString(),      //  Layer for regular debug content.
            LogLayer.Physics.ToString(),    //  Layer for game physics.
            LogLayer.Graphics.ToString(),   //  Layer for graphics or shaders.
            LogLayer.Logistics.ToString(),  //  Layer for logistics systems.
            LogLayer.Interface.ToString(),  //  Layer for UI/UX related data.
            LogLayer.Mechanics.ToString(),  //  Layer for game mechanics.
            LogLayer.Services.ToString(),   //  Layer for external services.
            LogLayer.DataBase.ToString(),   //  Layer for data base info.
            LogLayer.Network.ToString(),    //  Layer for network info.
            LogLayer.Internal.ToString()    //  Layer for BrickBucket Scripts.
        };

        /// <summary> Called On Enable </summary>
        public void OnEnable ()
        {
            //  Initialize the property.
            filterProperty = serializedObject.FindProperty ("filter");
        }

        /// <summary> Called on Inspector </summary>
        public override void OnInspectorGUI ()
        {

            serializedObject.Update ();

            EditorGUILayout.Space ();

            //  Draw and get the value for the mask.
            filterProperty.intValue = EditorGUILayout.MaskField (
                label: new GUIContent ("Filter"),
                mask: filterProperty.intValue,
                displayedOptions: levelArray
            );

            serializedObject.ApplyModifiedProperties ();
        }
    }
}
