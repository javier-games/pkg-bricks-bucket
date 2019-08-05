using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Object = UnityEngine.Object;

namespace BricksBucket
{
    /// <summary>
    /// 
    /// Button Method MonoBehaviour Editor.
    /// 
    /// <para>
    /// Custom editor for a MonoBehaviour Button Method Attribute.
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
	[CustomEditor (typeof(MonoBehaviour), true), CanEditMultipleObjects]
	public class ButtonMethodMonoBehaviourEditor : Editor
	{

        #region Class Members

        private List<MethodInfo> _methods;  //  Collection of valid members.
		private MonoBehaviour _target;      //  Current target.

        #endregion



        #region  Editor Methods

        /// <summary> Called on enable. </summary>
        private void OnEnable()
		{
			_target = target as MonoBehaviour;
			if (_target == null)
                return;

			_methods = ButtonMethodHandler.CollectValidMethods(
                type: _target.GetType()
            );
		}

        #endregion



        #region Editor Overrides

        /// <summary> Called on inspector GUI </summary>
        public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (_methods == null)
                return;

			ButtonMethodHandler.OnInspectorGUI(_target, _methods);
		}

        #endregion
    }

    /// <summary>
    /// 
    /// Button Method Scriptable Object Editor.
    /// 
    /// <para>
    /// Custom editor for a scriptable object Button Method Attribute.
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
    [CustomEditor (typeof (ScriptableObject), true), CanEditMultipleObjects]
    public class ButtonMethodScriptableObjectEditor : Editor
    {

        #region Class Members

        private List<MethodInfo> _methods;  //  Collection of valid members.
        private ScriptableObject _target;   //  Current target.

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

    /// <summary>
    /// 
    /// Button Method Handler.
    /// 
    /// <para>
    /// Class.
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
                if (IsValidMethod (method, member))
                {
                    if (methods == null)
                        methods = new List<MethodInfo> ();

                    methods.Add (method);
                }
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
                if (GUILayout.Button (method.Name.FromCamelCase()))
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
                DebugEditor.LogFormat (
                    context: target,
                    format: "{0}\nResult of Method '{1}' called by {2}",
                    data: new object[]{ result, method.Name, target.name }
                );
            }
        }

        /// <summary> Validates a method. </summary>
        /// <param name="method"> Method to validate. </param>
        /// <param name="member"> Member of the methot. </param>
        /// <returns> Wether the method is valid. </returns>
        private static bool IsValidMethod (MethodInfo method, MemberInfo member)
        {
            if (method == null)
            {
                DebugEditor.LogWarningFormat (
                    context: null,
                    format: StringUtils.Concat(
                        "Property {0}  is not a method button. ",
                        "Remove unnecesary EditorButtonAttribute. "
                    ),
                    data: member.Name
                );
                return false;
            }

            if (method.GetParameters ().Length > 0)
            {
                DebugEditor.LogWarningFormat (
                    context: null,
                    format: StringUtils.Concat (
                        "Methods with parameters are not supported by ",
                        "EditorButtonAttribute at Method {0}"
                    ),
                    data: method.Name
                );
                return false;
            }

            return true;
        }

        /// <summary> Validates whether a member is a button method. </summary>
        /// <param name="memberInfo"> Member info to validate. </param>
        /// <returns> Whether a member is a button method. </returns>
        private static bool IsButtonMethod(MemberInfo memberInfo) =>
            Attribute.IsDefined(memberInfo, typeof(ButtonMethodAttribute));

        #endregion
    }
}
