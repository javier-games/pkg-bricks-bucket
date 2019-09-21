#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

using EditorProcessor = UnityEditor.AssetModificationProcessor;

namespace BricksBucket.Editor
{
    /// <summary>
    ///
    /// Editor Events.
    ///
    /// <para>
    /// Events to manages editor scripts.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// </summary>
    [InitializeOnLoad]
    public class EditorEvents : EditorProcessor, IPreprocessBuildWithReport
    {
        /// <summary> Delegate called on save assets. </summary>
        public static Action OnSaveAssets;

        /// <summary> Delegate called on entered edit mode. </summary>
        public static Action OnEnteredEditMode;

        /// <summary> Delegate called on entered play mode. </summary>
        public static Action OnEnteredPlayMode;

        /// <summary> Delegate called on exiting edit mode. </summary>
        public static Action OnExitingEditMode;

        /// <summary> Delegate called on exiting play mode. </summary>
        public static Action OnExitingPlayMode;


        /// <summary> Order to execute. </summary>
        public int callbackOrder
        {
            get { return 0; }
        }

        /// <summary> Called on will save assets. </summary>
        /// <param name="paths"></param>
        /// <returns> Collection of Paths to save. </returns>
        private static string[] OnWillSaveAssets (string[] paths)
        {
            OnSaveAssets?.Invoke ();
            return paths;
        }

        /// <summary> Called on playmode state changed. </summary>
        /// <param name="state"> Current state. </param>
        private static void OnPlayModeStateChanged (PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.EnteredEditMode:
                OnEnteredEditMode?.Invoke ();
                break;
                case PlayModeStateChange.EnteredPlayMode:
                OnEnteredPlayMode?.Invoke ();
                break;
                case PlayModeStateChange.ExitingEditMode:
                OnExitingEditMode?.Invoke ();
                break;
                case PlayModeStateChange.ExitingPlayMode:
                OnExitingPlayMode?.Invoke ();
                break;
            }
        }

        /// <summary> </summary>
        /// <param name="report"></param>
        public void OnPreprocessBuild (BuildReport report)
        {

        }
    }
}

#endif