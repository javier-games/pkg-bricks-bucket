using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace BricksBucket
{
    /// <summary>
    ///
    /// On Save Attribute Handler.
    ///
    /// <para>
    /// Called on will save assets to search for methods and call them.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// </summary>
    [InitializeOnLoad]
    public static class OnSaveAttributeHandler
    {

        #region Constructor

        /// <summary> Subscribing to editor events. </summary>
        static OnSaveAttributeHandler () =>
            EditorEvents.OnSaveAssets += CheckComponents;

        #endregion



        #region Class Implementation

        /// <summary> Check for On Save method attributes. </summary>
        private static void CheckComponents ()
        {
            var components =
                SerializedUtils.GetMethodsWithAttribute<OnSaveAttribute> ();

            for (int i = 0; i < components.Length; i++)
                InvokeMethod (components[i].component, components[i].method);
        }

        #endregion



        #region Private Methods

        /// <summary> Invokes the method. </summary>
        /// <param name="target"> Current target. </param>
        /// <param name="method"> Method to call. </param>
        private static void InvokeMethod (Component target, MethodInfo method)
        {
            if(!IsValidMethod(target, method))
                return;
            
            var result = method.Invoke (target, null);

            if (result != null)
            {
                /*DebugEditor.LogFormat (
                    context: target,
                    format: "{0}\nResult of Method '{1}' called by {2}",
                    data: new object[] { result, method.Name, target.name }
                );*/
            }
        }

        /// <summary> Validates a method. </summary>
        /// <param name="method"> Method to validate. </param>
        /// <returns> Wether the method is valid. </returns>
        private static bool IsValidMethod (Component target, MethodInfo method)
        {
            if (method == null)
            {
                /*DebugEditor.LogWarningFormat (
                    context: null,
                    format: StringUtils.Concat (
                        "Property {0}  is not a method button. ",
                        "Remove unnecesary EditorButtonAttribute. "
                    ),
                    data: member.Name
                );*/
                return false;
            }

            if (method.GetParameters ().Length > 0)
            {
                /*DebugEditor.LogWarningFormat (
                    context: null,
                    format: StringUtils.Concat (
                        "Methods with parameters are not supported by ",
                        "EditorButtonAttribute at Method {0}"
                    ),
                    data: method.Name
                );*/
                return false;
            }

            if (!IsOnSaveMethod (method))
            {
                /*DebugEditor.LogWarningFormat (
                    context: null,
                    format: StringUtils.Concat (
                        "Methods with parameters are not supported by ",
                        "EditorButtonAttribute at Method {0}"
                    ),
                    data: method.Name
                );*/
                return false;
            }

            return true;
        }

        /// <summary> Validates whether a member is a on save method. </summary>
        /// <param name="memberInfo"> Member info to validate. </param>
        /// <returns> Whether a member is a button method. </returns>
        private static bool IsOnSaveMethod (MemberInfo memberInfo) =>
            Attribute.IsDefined (memberInfo, typeof (OnSaveAttribute));

        #endregion
    }
}