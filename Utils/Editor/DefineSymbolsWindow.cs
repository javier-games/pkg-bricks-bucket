using UnityEditor;

namespace BricksBucket
{
    /// <summary>
    ///
    /// ScriptingDefineWindow.cs
    ///
    /// <para>
    /// Editor Window to show scripting defines symbols.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the UnityDefineManager by @karl-.
    /// https://github.com/karl-/UnityDefineManager
    /// </para>
    ///
    /// </summary>
    public class ScriptingDefineWindow : EditorWindow
    {


        #region Class Members

        Editor _editor;
        DefineSymbols _asset;

        const string MenuPath = "Bricks Bucket/Scripting Define Symbol";
        const string Title = "Define Symbol Editor";

        #endregion



        #region EditorWindow Methods

        //  Called on Init.
        [MenuItem (MenuPath)]
        static void Init ()
        {
            GetWindow<ScriptingDefineWindow> (
                utility: true,
                title: Title,
                focus: true
            );
        }

        //  Called on enable.
        void OnEnable ()
        {
            _asset = CreateInstance<DefineSymbols> ();
            _editor = Editor.CreateEditor (_asset);
        }

        //  Called on disable.
        void OnDisable ()
        {
            DestroyImmediate (_editor);
            DestroyImmediate (_asset);
        }

        //  Called on GUI.
        void OnGUI ()
        {
            _editor.OnInspectorGUI ();
        }

        #endregion
    }
}
