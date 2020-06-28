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
    /// <!-- By Javier García | @jvrgms | 2020 -->
    [AttributeUsage (AttributeTargets.Method)]
    public class OnSaveAttribute : PropertyAttribute { }
}
