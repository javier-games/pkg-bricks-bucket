using System;
using UnityEngine;

namespace BricksBucket.Core
{
    /// <!-- OnSaveAttribute -->
    /// 
    /// <summary>
    /// Custom callback called on save.
    /// </summary>
    ///
    /// <example><code>
    /// using UnityEngine;
    /// using BricksBucket.Core;
    ///
    /// public class AttributesTest : MonoBehaviour
    /// {
    ///   // This method will be called on save.
    ///   [OnSave]
    ///   private void CalledOnSave ()
    ///   {
    ///     Debug.Log ("The Scene has been saved.");
    ///   }
    /// }
    /// </code></example>
    ///
    /// <!-- By Javier García | @jvrgms | 2020 -->
    [AttributeUsage (AttributeTargets.Method)]
    public class OnSaveAttribute : PropertyAttribute { }
}
