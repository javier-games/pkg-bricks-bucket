using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BricksBucket.Core.Editor.Attributes
{
    // ReSharper disable CommentTypo
    /// <!-- ButtonMethodMonoBehaviourEditor -->
    /// <summary>
    ///
    /// <para>
    /// Button Method MonoBehaviour Editor.
    /// </para>
    ///
    /// <para>
    /// Based in the <see href="https://github.com/Deadcows/MyBox">MyBox
    /// project by @deadcows</see> and the original version by <see creh="
    /// https://github.com/Kaynn-Cahya">@Kaynn-Cahya</see>.
    /// </para>
    ///
    /// </summary>
    ///
    /// <seealso href="https://github.com/Deadcows/MyBox">
    /// Deadcows/MyBox</seealso>
    /// <seealso href="https://github.com/Kaynn-Cahya">
    /// @Kaynn-Cahya</seealso>
    ///
    /// <!-- By Javier García | @jvrgms | 2020 -->
    // ReSharper restore CommentTypo
    [CustomEditor (typeof (MonoBehaviour), true), CanEditMultipleObjects]
    public class ButtonMethodMonoBehaviourEditor : UnityEditor.Editor
    {
        #region Properties

        /// <summary>
        /// Collection of valid members.
        /// </summary>
        private List<MethodInfo> _methods;

        /// <summary>
        /// Current target.
        /// </summary>
        private MonoBehaviour _target;

        #endregion


        #region Methods

        /// <summary> Called on enable. </summary>
        private void OnEnable ()
        {
            _target = target as MonoBehaviour;
            if (_target == null) return;

            _methods = ButtonMethodHandler.CollectValidMethods (
                type: _target.GetType ()
            );
        }

        /// <inheritdoc cref="UnityEditor.Editor.OnInspectorGUI"/>
        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI ();
            if (_methods == null) return;

            ButtonMethodHandler.OnInspectorGUI (_target, _methods);
        }

        #endregion
    }
    
    // ReSharper disable CommentTypo
    /// <!-- ButtonMethodScriptableObjectEditor -->
    /// <summary>
    ///
    /// <para>
    /// Custom editor for a scriptable object Button Method Attribute.
    /// </para>
    ///
    /// <para>
    /// Based in the <see href="https://github.com/Deadcows/MyBox">MyBox
    /// project by @deadcows</see> and the original version by <see creh="
    /// https://github.com/Kaynn-Cahya">@Kaynn-Cahya</see>.
    /// </para>
    ///
    /// </summary>
    ///
    /// <seealso href="https://github.com/Deadcows/MyBox">
    /// Deadcows/MyBox</seealso>
    /// <seealso href="https://github.com/Kaynn-Cahya">
    /// @Kaynn-Cahya</seealso>
    ///
    /// <!-- By Javier García | @jvrgms | 2020 -->
    // ReSharper restore CommentTypo
    [CustomEditor (typeof (ScriptableObject), true), CanEditMultipleObjects]
    public class ButtonMethodScriptableObjectEditor : UnityEditor.Editor
    {
        #region Class Members

        private List<MethodInfo> _methods; //  Collection of valid members.
        private ScriptableObject _target; //  Current target.

        #endregion


        #region Editor Methods

        /// <summary> Called on enable. </summary>
        private void OnEnable ()
        {
            _target = target as ScriptableObject;
            if (_target == null) return;

            _methods = ButtonMethodHandler.CollectValidMethods (
                type: _target.GetType ()
            );
        }

        #endregion


        #region Editor Overrides

        /// <summary> Called on inspector GUI </summary>
        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI ();
            if (_methods == null) return;

            ButtonMethodHandler.OnInspectorGUI (_target, _methods);
        }

        #endregion
    }

    // ReSharper disable CommentTypo
    /// <!-- ButtonMethodHandler -->
    /// <summary>
    ///
    /// <para>
    /// Handler for buttons.
    /// </para>
    ///
    /// <para>
    /// Based in the <see href="https://github.com/Deadcows/MyBox">MyBox
    /// project by @deadcows</see> and the original version by <see creh="
    /// https://github.com/Kaynn-Cahya">@Kaynn-Cahya</see>.
    /// </para>
    ///
    /// </summary>
    ///
    /// <seealso href="https://github.com/Deadcows/MyBox">
    /// Deadcows/MyBox</seealso>
    /// <seealso href="https://github.com/Kaynn-Cahya">
    /// @Kaynn-Cahya</seealso>
    ///
    /// <!-- By Javier García | @jvrgms | 2020 -->
    // ReSharper restore CommentTypo
    public static class ButtonMethodHandler
    {
        #region Public Methods

        /// <summary> Collect valid methods. </summary>
        /// <param name="type"> Type of the target. </param>
        /// <returns> Methods collection. </returns>
        public static List<MethodInfo> CollectValidMethods (Type type)
        {
            List<MethodInfo> methods = null;

            var members = type.GetMembers (
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic
            ).Where (IsButtonMethod);

            foreach (var member in members)
            {
                var method = member as MethodInfo;
                if (!IsValidMethod (method, member)) continue;
                if (methods == null) methods = new List<MethodInfo> ();

                methods.Add (method);
            }

            return methods;
        }

        /// <summary> Called oniInspector GUI to draw button. </summary>
        /// <param name="target"> Current Target. </param>
        /// <param name="methods"> List of methods. </param>
        public static void
            OnInspectorGUI (Object target, List<MethodInfo> methods)
        {
            EditorGUILayout.Space ();

            foreach (MethodInfo method in methods)
            {
                if (GUILayout.Button (method.Name.ToUpperInvariant ()))
                    InvokeMethod (target, method);
            }
        }

        #endregion


        #region Private Methods

        /// <summary> Invokes the method. </summary>
        /// <param name="target"> Current target. </param>
        /// <param name="method"> Method to call. </param>
        private static void InvokeMethod (Object target, MethodInfo method)
        {
            var result = method.Invoke (target, null);

            if (result != null)
            {
                // TODO: Implement Log in ButtonMethodAttributeEditor in Invike.
                Debug.Log (
                    $"{result}\nResult of Method '{method.Name}' " +
                    $"called by {target.name}"
                );
            }
        }

        /// <summary> Validates a method. </summary>
        /// <param name="method"> Method to validate. </param>
        /// <param name="member"> Member of the method. </param>
        /// <returns> Whether the method is valid. </returns>
        private static bool IsValidMethod (MethodInfo method, MemberInfo member)
        {
            if (method == null)
            {
                // TODO: Implement Log in ButtonMethodAttributeEditor in Invoke.
                Debug.LogWarning (
                    $"Property {member.Name}  is not a method button. " +
                    "Remove unnecessary EditorButtonAttribute. "
                );
                return false;
            }

            if (method.GetParameters ().Length <= 0) return true;
            
            // TODO: Implement Log in ButtonMethodAttributeEditor in Invoke.
            Debug.LogWarning (
                "Methods with parameters are not supported by " +
                $"EditorButtonAttribute at Method {method.Name}"
            );
            return false;

        }

        /// <summary> Validates whether a member is a button method. </summary>
        /// <param name="memberInfo"> Member info to validate. </param>
        /// <returns> Whether a member is a button method. </returns>
        private static bool IsButtonMethod (MemberInfo memberInfo) =>
            Attribute.IsDefined (memberInfo, typeof (ButtonMethodAttribute));

        #endregion
    }
}
