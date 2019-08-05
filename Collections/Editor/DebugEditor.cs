using UnityEngine;

namespace BricksBucket.Collections
{
    /// <summary>
    /// 
    /// Debug Editor.
    /// 
    /// <para>
    /// Internal version for editor debuging.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// 
    /// </summary>
    internal static class DebugEditor
    {

        /// <summary> Log layer to use. </summary>
        private const LogLayer Layer = LogLayer.Editor;

        /// <summary> Logs internal data to Unity Console. </summary>
        /// <param name="data"></param>
        internal static void Log (params object[] data) =>
            DebugUtils.ExtendedLog (Layer, LogType.Log, null, null, data);

        /// <summary> Logs internal data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        internal static void LogFormat (Object context, string format, params object[] data) =>
            DebugUtils.ExtendedLog (Layer, LogType.Log, context, format, data);

        /// <summary> Logs internal warning data to Unity Console. </summary>
        /// <param name="data"></param>
        internal static void LogWarning (params object[] data) =>
            DebugUtils.ExtendedLog (Layer, LogType.Warning, null, null, data);

        /// <summary> Logs internal warning data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        internal static void LogWarningFormat (Object context, string format, params object[] data) =>
            DebugUtils.ExtendedLog (Layer, LogType.Warning, context, format, data);

        /// <summary> Logs internal error data to Unity Console. </summary>
        /// <param name="data"></param>
        internal static void LogError (params object[] data) =>
            DebugUtils.ExtendedLog (Layer, LogType.Error, null, null, data);

        /// <summary> Logs internal error data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        internal static void LogErrorFormat (Object context, string format, params object[] data) =>
            DebugUtils.ExtendedLog (Layer, LogType.Error, context, null, format, data);

        /// <summary> Logs internal assert data to Unity Console. </summary>
        /// <param name="data"></param>
        internal static void LogAssert (params object[] data) =>
            DebugUtils.ExtendedLog (Layer, LogType.Assert, null, null, data);

        /// <summary> Logs internal assert data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        internal static void LogAssertFormat (Object context, string format, params object[] data) =>
            DebugUtils.ExtendedLog (Layer, LogType.Assert, context, format, data);

        /// <summary> Logs internal exception data to Unity Console. </summary>
        /// <param name="data"></param>
        internal static void LogException (params object[] data) =>
            DebugUtils.ExtendedLog (Layer, LogType.Exception, null, null, data);

        /// <summary> Logs internal exception data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        internal static void LogExceptionFormat (Object context, string format, params object[] data) =>
            DebugUtils.ExtendedLog (Layer, LogType.Exception, context, format, data);

    }
}
