using UnityEngine;
using UnityEditor;

namespace BricksBucket
{
    /// <summary>
    ///
    /// DefineSymbols.cs
    ///
    /// <para>
    /// Stores the information of defined symbols.
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
    public class DefineSymbols : ScriptableObject
    {
        #region Class Members

        [SerializeField]
        protected Compiler _compiler;               //  Compiler.

        [SerializeField]
        protected BuildTargetGroup _buildTarget;    //  Platform.

        [SerializeField]
        protected string[] _defines;                //  Symbols.

        [SerializeField]
        protected bool _isApplied;                  //  Flag to apply.

        #endregion
    }
}
