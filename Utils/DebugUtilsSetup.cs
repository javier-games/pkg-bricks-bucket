using UnityEngine;

namespace BricksBucket.Utils
{
    /// <summary>
    /// 
    /// Debug Utils Setup.
    /// 
    /// <para>
    /// Sets the filter in Debug Utils on awake.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// </summary>
    public class DebugUtilsSetup : MonoBehaviour
    {

        [SerializeField]
        protected int filter;     //  Filter assigned from editor.

        /// <summary> Called on awake. </summary>
        private void Awake ()
        {
            //  Assining the filter to DebugUtils.
            DebugUtils.Filter = filter;
        }
    }
}
