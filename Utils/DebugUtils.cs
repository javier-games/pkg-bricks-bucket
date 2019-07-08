using UnityEngine;

namespace BricksBucket.Utils
{

    /// <summary>
    /// 
    /// Debug Utils.
    /// 
    /// <para>
    /// Usefull utilities for debuging.
    /// - Debug.Log Methods visible only in debug mode and unity editor.
    /// - Filter System to show only desired messages.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// </summary>
    public static class DebugUtils
    {



        #region Filter System

        /// <summary> Gets the debug filter. </summary>
        /// <value>The debug filter.</value>
        public static int Filter { get; set; } = 0;

        /// <summary> Sets completely the debug filter. </summary>
        /// <param name="levels">Filters.</param>
        public static void SetFilter (params LogLayer[] levels)
        {
            Filter = 0;
            foreach (LogLayer level in levels)
                AddLevelToFilter (level);
        }

        /// <summary> Gets the array of levels in filter. </summary>
        /// <returns>The levels.</returns>
        public static LogLayer[] GetFilteredLevels ()
        {

            int levelsCount = 0;
            for (int i = 0; i <= 6; i++)
                if (IsInFilter ((LogLayer) i))
                    levelsCount++;

            LogLayer[] levels = new LogLayer[levelsCount];
            for (int i = 0; i <= 6; i++)
                if (IsInFilter ((LogLayer) i))
                    levels[i] = (LogLayer) i;

            return levels;
        }

        /// <summary> Adds a new filter. </summary>
        /// <param name="level">Filter.</param>
        public static void AddLevelToFilter (LogLayer level)
        {
            Filter |= 1 << (int) level;
        }

        /// <summary> Removes a level from filter. </summary>
        /// <param name="level">Level.</param>
        public static void RemoveLevelFromFilter (LogLayer level)
        {
            if (IsInFilter (level))
                Filter &= ~(1 << (int) level);
        }

        /// <summary> Defines if a filter belongs to the debug filter. </summary>
        /// <returns><c>true</c> if is in debugfilter.</returns>
        /// <param name="filter">Filter.</param>
        public static bool IsInFilter (LogLayer filter)
        {
            return (Filter & (1 << (int) filter)) > 0;
        }

        #endregion



        #region Private Methods

        /// <summary> Convert an object to an understandable string. </summary>
        /// <returns>The string.</returns>
        /// <param name="s">S.</param>
        private static string DebuggableString (object s)
        {
            //For debug purposes the array is checked to evidentiate null objects.
            return s != null ? s.ToString () : "null";
        }

        /// <summary> Convert an array to an understandable string array. </summary>
        /// <returns>The string.</returns>
        /// <param name="array">Array.</param>
        private static string[] DebuggableString (params object[] array)
        {
            string[] stringArray = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
                stringArray[i] = DebuggableString (array[i]);
            return stringArray;
        }

        #endregion



        #region Log Methods

        /// <summary> Logs data to Unity Console. </summary>
        /// <param name="layer"></param>
        /// <param name="type"></param>
        /// <param name="option"></param>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void ExtendedLog (LogLayer layer, LogType type, LogOption option, Object context, string format, params object[] data)
        {
            if (!IsInFilter (layer))
                return;

            string message = string.IsNullOrWhiteSpace (format) ?
                StringUtils.Concat (DebuggableString (data)) :
                StringUtils.ConcatFormat (format, DebuggableString ());

            Debug.LogFormat (type, option, context, message);
        }


        //  Layer for regular debug content.
        #region Debug Layer

        /// <summary> Logs debug data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void Log (params object[] data)
        {
            ExtendedLog (LogLayer.Debug, LogType.Log, LogOption.None, null, null, data);
        }

        /// <summary> Logs debug data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Debug, LogType.Log, LogOption.None, context, format, data);
        }

        /// <summary> Logs debug warning data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogWarning (params object[] data)
        {
            ExtendedLog (LogLayer.Debug, LogType.Warning, LogOption.None, null, null, data);
        }

        /// <summary> Logs debug warning data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogWarningFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Debug, LogType.Warning, LogOption.None, context, format, data);
        }

        /// <summary> Logs debug error data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogError (params object[] data)
        {
            ExtendedLog (LogLayer.Debug, LogType.Error, LogOption.None, null, null, data);
        }

        /// <summary> Logs debug error data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogErrorFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Debug, LogType.Error, LogOption.None, context, format, data);
        }

        /// <summary> Logs debug assert data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogAssert (params object[] data)
        {
            ExtendedLog (LogLayer.Debug, LogType.Assert, LogOption.None, null, null, data);
        }

        /// <summary> Logs debug assert data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogAssertFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Debug, LogType.Assert, LogOption.None, context, format, data);
        }

        /// <summary> Logs debug exception data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogException (params object[] data)
        {
            ExtendedLog (LogLayer.Debug, LogType.Exception, LogOption.None, null, null, data);
        }

        /// <summary> Logs debug exception data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogExceptionFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Debug, LogType.Exception, LogOption.None, context, format, data);
        }

        #endregion

        //  Layer for game physics.
        #region Physics Layer

        /// <summary> Logs physics data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void PhysicsLog (params object[] data)
        {
            ExtendedLog (LogLayer.Physics, LogType.Log, LogOption.None, null, null, data);
        }

        /// <summary> Logs physics data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void PhysicsLogFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Physics, LogType.Log, LogOption.None, context, format, data);
        }

        /// <summary> Logs physics warning data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void PhysicsLogWarning (params object[] data)
        {
            ExtendedLog (LogLayer.Physics, LogType.Warning, LogOption.None, null, null, data);
        }

        /// <summary> Logs physics warning data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void PhysicsLogWarningFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Physics, LogType.Warning, LogOption.None, context, format, data);
        }

        /// <summary> Logs physics error data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void PhysicsLogError (params object[] data)
        {
            ExtendedLog (LogLayer.Physics, LogType.Error, LogOption.None, null, null, data);
        }

        /// <summary> Logs physics error data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void PhysicsLogErrorFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Physics, LogType.Error, LogOption.None, context, format, data);
        }

        /// <summary> Logs physics assert data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void PhysicsLogAssert (params object[] data)
        {
            ExtendedLog (LogLayer.Physics, LogType.Assert, LogOption.None, null, null, data);
        }

        /// <summary> Logs physics assert data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void PhysicsLogAssertFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Physics, LogType.Assert, LogOption.None, context, format, data);
        }

        /// <summary> Logs physics exception data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void PhysicsLogException (params object[] data)
        {
            ExtendedLog (LogLayer.Physics, LogType.Exception, LogOption.None, null, null, data);
        }

        /// <summary> Logs physics exception data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void PhysicsLogExceptionFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Physics, LogType.Exception, LogOption.None, context, format, data);
        }

        #endregion

        //  Layer for 2D and 3D graphics or shaders.
        #region Graphics Layer

        /// <summary> Logs graphics data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void GraphicsLog (params object[] data)
        {
            ExtendedLog (LogLayer.Graphics, LogType.Log, LogOption.None, null, null, data);
        }

        /// <summary> Logs graphics data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void GraphicsLogFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Graphics, LogType.Log, LogOption.None, context, format, data);
        }

        /// <summary> Logs graphics warning data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void GraphicsLogWarning (params object[] data)
        {
            ExtendedLog (LogLayer.Graphics, LogType.Warning, LogOption.None, null, null, data);
        }

        /// <summary> Logs graphics warning data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void GraphicsLogWarningFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Graphics, LogType.Warning, LogOption.None, context, format, data);
        }

        /// <summary> Logs graphics error data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void GraphicsLogError (params object[] data)
        {
            ExtendedLog (LogLayer.Graphics, LogType.Error, LogOption.None, null, null, data);
        }

        /// <summary> Logs graphics error data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void GraphicsLogErrorFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Graphics, LogType.Error, LogOption.None, context, format, data);
        }

        /// <summary> Logs graphics assert data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void GraphicsLogAssert (params object[] data)
        {
            ExtendedLog (LogLayer.Graphics, LogType.Assert, LogOption.None, null, null, data);
        }

        /// <summary> Logs graphics assert data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void GraphicsLogAssertFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Graphics, LogType.Assert, LogOption.None, context, format, data);
        }

        /// <summary> Logs graphics exception data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void GraphicsLogException (params object[] data)
        {
            ExtendedLog (LogLayer.Graphics, LogType.Exception, LogOption.None, null, null, data);
        }

        /// <summary> Logs graphics exception data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void GraphicsLogExceptionFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Graphics, LogType.Exception, LogOption.None, context, format, data);
        }

        #endregion

        //  Layer for logistics systems.
        #region Logistics Layer

        /// <summary> Logs logistics data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogisticsLog (params object[] data)
        {
            ExtendedLog (LogLayer.Logistics, LogType.Log, LogOption.None, null, null, data);
        }

        /// <summary> Logs logistics data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogisticsLogFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Logistics, LogType.Log, LogOption.None, context, format, data);
        }

        /// <summary> Logs logistics warning data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogisticsLogWarning (params object[] data)
        {
            ExtendedLog (LogLayer.Logistics, LogType.Warning, LogOption.None, null, null, data);
        }

        /// <summary> Logs logistics warning data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogisticsLogWarningFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Logistics, LogType.Warning, LogOption.None, context, format, data);
        }

        /// <summary> Logs logistics error data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogisticsLogError (params object[] data)
        {
            ExtendedLog (LogLayer.Logistics, LogType.Error, LogOption.None, null, null, data);
        }

        /// <summary> Logs logistics error data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogisticsLogErrorFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Logistics, LogType.Error, LogOption.None, context, format, data);
        }

        /// <summary> Logs logistics assert data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogisticsLogAssert (params object[] data)
        {
            ExtendedLog (LogLayer.Logistics, LogType.Assert, LogOption.None, null, null, data);
        }

        /// <summary> Logs logistics assert data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogisticsLogAssertFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Logistics, LogType.Assert, LogOption.None, context, format, data);
        }

        /// <summary> Logs logistics exception data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogisticsLogException (params object[] data)
        {
            ExtendedLog (LogLayer.Logistics, LogType.Exception, LogOption.None, null, null, data);
        }

        /// <summary> Logs logistics exception data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void LogisticsLogExceptionFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Logistics, LogType.Exception, LogOption.None, context, format, data);
        }

        #endregion

        //  Layer for UI/UX related data.
        #region Interface Layer

        /// <summary> Logs UI / UX data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void InterfaceLog (params object[] data)
        {
            ExtendedLog (LogLayer.Interface, LogType.Log, LogOption.None, null, null, data);
        }

        /// <summary> Logs UI / UX data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void InterfaceLogFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Interface, LogType.Log, LogOption.None, context, format, data);
        }

        /// <summary> Logs UI / UX warning data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void InterfaceLogWarning (params object[] data)
        {
            ExtendedLog (LogLayer.Interface, LogType.Warning, LogOption.None, null, null, data);
        }

        /// <summary> Logs UI / UX warning data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void InterfaceLogWarningFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Interface, LogType.Warning, LogOption.None, context, format, data);
        }

        /// <summary> Logs UI / UX error data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void InterfaceLogError (params object[] data)
        {
            ExtendedLog (LogLayer.Interface, LogType.Error, LogOption.None, null, null, data);
        }

        /// <summary> Logs UI / UX error data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void InterfaceLogErrorFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Interface, LogType.Error, LogOption.None, context, format, data);
        }

        /// <summary> Logs UI / UX assert data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void InterfaceLogAssert (params object[] data)
        {
            ExtendedLog (LogLayer.Interface, LogType.Assert, LogOption.None, null, null, data);
        }

        /// <summary> Logs UI / UX assert data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void InterfaceLogAssertFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Interface, LogType.Assert, LogOption.None, context, format, data);
        }

        /// <summary> Logs UI / UX exception data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void InterfaceLogException (params object[] data)
        {
            ExtendedLog (LogLayer.Interface, LogType.Exception, LogOption.None, null, null, data);
        }

        /// <summary> Logs UI / UX exception data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void InterfaceLogExceptionFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Interface, LogType.Exception, LogOption.None, context, format, data);
        }

        #endregion

        //  Layer for game mechanics and dynamics.
        #region Mechanics Layer

        /// <summary> Logs mechanics data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void MechanicsLog (params object[] data)
        {
            ExtendedLog (LogLayer.Mechanics, LogType.Log, LogOption.None, null, null, data);
        }

        /// <summary> Logs mechanics data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void MechanicsLogFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Mechanics, LogType.Log, LogOption.None, context, format, data);
        }

        /// <summary> Logs mechanics warning data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void MechanicsLogWarning (params object[] data)
        {
            ExtendedLog (LogLayer.Mechanics, LogType.Warning, LogOption.None, null, null, data);
        }

        /// <summary> Logs mechanics warning data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void MechanicsLogWarningFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Mechanics, LogType.Warning, LogOption.None, context, format, data);
        }

        /// <summary> Logs mechanics error data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void MechanicsLogError (params object[] data)
        {
            ExtendedLog (LogLayer.Mechanics, LogType.Error, LogOption.None, null, null, data);
        }

        /// <summary> Logs mechanics error data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void MechanicsLogErrorFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Mechanics, LogType.Error, LogOption.None, context, format, data);
        }

        /// <summary> Logs mechanics assert data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void MechanicsLogAssert (params object[] data)
        {
            ExtendedLog (LogLayer.Mechanics, LogType.Assert, LogOption.None, null, null, data);
        }

        /// <summary> Logs mechanics assert data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void MechanicsLogAssertFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Mechanics, LogType.Assert, LogOption.None, context, format, data);
        }

        /// <summary> Logs mechanics exception data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void MechanicsLogException (params object[] data)
        {
            ExtendedLog (LogLayer.Mechanics, LogType.Exception, LogOption.None, null, null, data);
        }

        /// <summary> Logs mechanics exception data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void MechanicsLogExceptionFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Mechanics, LogType.Exception, LogOption.None, context, format, data);
        }

        #endregion

        //  Layer for external services implementations.
        #region Services Layer

        /// <summary> Logs services data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void ServicesLog (params object[] data)
        {
            ExtendedLog (LogLayer.Services, LogType.Log, LogOption.None, null, null, data);
        }

        /// <summary> Logs services data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void ServicesLogFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Services, LogType.Log, LogOption.None, context, format, data);
        }

        /// <summary> Logs services warning data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void ServicesLogWarning (params object[] data)
        {
            ExtendedLog (LogLayer.Services, LogType.Warning, LogOption.None, null, null, data);
        }

        /// <summary> Logs services warning data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void ServicesLogWarningFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Services, LogType.Warning, LogOption.None, context, format, data);
        }

        /// <summary> Logs services error data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void ServicesLogError (params object[] data)
        {
            ExtendedLog (LogLayer.Services, LogType.Error, LogOption.None, null, null, data);
        }

        /// <summary> Logs services error data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void ServicesLogErrorFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Services, LogType.Error, LogOption.None, context, format, data);
        }

        /// <summary> Logs services assert data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void ServicesLogAssert (params object[] data)
        {
            ExtendedLog (LogLayer.Services, LogType.Assert, LogOption.None, null, null, data);
        }

        /// <summary> Logs services assert data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void ServicesLogAssertFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Services, LogType.Assert, LogOption.None, context, format, data);
        }

        /// <summary> Logs services exception data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void ServicesLogException (params object[] data)
        {
            ExtendedLog (LogLayer.Services, LogType.Exception, LogOption.None, null, null, data);
        }

        /// <summary> Logs services exception data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void ServicesLogExceptionFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Services, LogType.Exception, LogOption.None, context, format, data);
        }

        #endregion

        //  Layer for data base info.
        #region DataBase Layer

        /// <summary> Logs data base data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void DataBaseLog (params object[] data)
        {
            ExtendedLog (LogLayer.DataBase, LogType.Log, LogOption.None, null, null, data);
        }

        /// <summary> Logs data base data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void DataBaseLogFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.DataBase, LogType.Log, LogOption.None, context, format, data);
        }

        /// <summary> Logs data base warning data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void DataBaseLogWarning (params object[] data)
        {
            ExtendedLog (LogLayer.DataBase, LogType.Warning, LogOption.None, null, null, data);
        }

        /// <summary> Logs data base warning data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void DataBaseLogWarningFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.DataBase, LogType.Warning, LogOption.None, context, format, data);
        }

        /// <summary> Logs data base error data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void DataBaseLogError (params object[] data)
        {
            ExtendedLog (LogLayer.DataBase, LogType.Error, LogOption.None, null, null, data);
        }

        /// <summary> Logs data base error data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void DataBaseLogErrorFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.DataBase, LogType.Error, LogOption.None, context, format, data);
        }

        /// <summary> Logs data base assert data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void DataBaseLogAssert (params object[] data)
        {
            ExtendedLog (LogLayer.DataBase, LogType.Assert, LogOption.None, null, null, data);
        }

        /// <summary> Logs data base assert data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void DataBaseLogAssertFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.DataBase, LogType.Assert, LogOption.None, context, format, data);
        }

        /// <summary> Logs data base exception data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void DataBaseLogException (params object[] data)
        {
            ExtendedLog (LogLayer.DataBase, LogType.Exception, LogOption.None, null, null, data);
        }

        /// <summary> Logs data base exception data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void DataBaseLogExceptionFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.DataBase, LogType.Exception, LogOption.None, context, format, data);
        }

        #endregion

        //  Layer for network info.
        #region Network Layer

        /// <summary> Logs network data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void NetworkLog (params object[] data)
        {
            ExtendedLog (LogLayer.Network, LogType.Log, LogOption.None, null, null, data);
        }

        /// <summary> Logs network data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void NetworkLogFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Network, LogType.Log, LogOption.None, context, format, data);
        }

        /// <summary> Logs network warning data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void NetworkLogWarning (params object[] data)
        {
            ExtendedLog (LogLayer.Network, LogType.Warning, LogOption.None, null, null, data);
        }

        /// <summary> Logs network warning data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void NetworkLogWarningFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Network, LogType.Warning, LogOption.None, context, format, data);
        }

        /// <summary> Logs network error data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void NetworkLogError (params object[] data)
        {
            ExtendedLog (LogLayer.Network, LogType.Error, LogOption.None, null, null, data);
        }

        /// <summary> Logs network error data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void NetworkLogErrorFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Network, LogType.Error, LogOption.None, context, format, data);
        }

        /// <summary> Logs network assert data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void NetworkLogAssert (params object[] data)
        {
            ExtendedLog (LogLayer.Network, LogType.Assert, LogOption.None, null, null, data);
        }

        /// <summary> Logs network assert data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void NetworkLogAssertFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Network, LogType.Assert, LogOption.None, context, format, data);
        }

        /// <summary> Logs network exception data to Unity Console. </summary>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void NetworkLogException (params object[] data)
        {
            ExtendedLog (LogLayer.Network, LogType.Exception, LogOption.None, null, null, data);
        }

        /// <summary> Logs network exception data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        public static void NetworkLogExceptionFormat (Object context, string format, params object[] data)
        {
            ExtendedLog (LogLayer.Network, LogType.Exception, LogOption.None, context, format, data);
        }

        #endregion

        //  Layer for BrickBucket Scripts.
        #region Internal Layer

        /// <summary> Logs internal data to Unity Console. </summary>
        /// <param name="layer"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        internal static void InternalLog (LogLayer layer, params object[] data)
        {
            InternalExtendedLog (layer, LogType.Log, LogOption.None, null, null, data);
        }

        /// <summary> Logs internal data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="layer"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        internal static void InternalLogFormat (LogLayer layer, Object context, string format, params object[] data)
        {
            InternalExtendedLog (layer, LogType.Log, LogOption.None, context, format, data);
        }

        /// <summary> Logs internal warning data to Unity Console. </summary>
        /// <param name="layer"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        internal static void InternalLogWarning (LogLayer layer, params object[] data)
        {
            InternalExtendedLog (layer, LogType.Warning, LogOption.None, null, null, data);
        }

        /// <summary> Logs internal warning data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="layer"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        internal static void InternalLogWarningFormat (LogLayer layer, Object context, string format, params object[] data)
        {
            InternalExtendedLog (layer, LogType.Warning, LogOption.None, context, format, data);
        }

        /// <summary> Logs internal error data to Unity Console. </summary>
        /// <param name="layer"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        internal static void InternalLogError (LogLayer layer, params object[] data)
        {
            InternalExtendedLog (layer, LogType.Error, LogOption.None, null, null, data);
        }

        /// <summary> Logs internal error data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="layer"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        internal static void InternalLogErrorFormat (LogLayer layer, Object context, string format, params object[] data)
        {
            InternalExtendedLog (layer, LogType.Error, LogOption.None, context, format, data);
        }

        /// <summary> Logs internal assert data to Unity Console. </summary>
        /// <param name="layer"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        internal static void InternalLogAssert (LogLayer layer, params object[] data)
        {
            InternalExtendedLog (layer, LogType.Assert, LogOption.None, null, null, data);
        }

        /// <summary> Logs internal assert data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="layer"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        internal static void InternalLogAssertFormat (LogLayer layer, Object context, string format, params object[] data)
        {
            InternalExtendedLog (layer, LogType.Assert, LogOption.None, context, format, data);
        }

        /// <summary> Logs internal exception data to Unity Console. </summary>
        /// <param name="layer"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        internal static void InternalLogException (LogLayer layer, params object[] data)
        {
            InternalExtendedLog (layer, LogType.Exception, LogOption.None, null, null, data);
        }

        /// <summary> Logs internal exception data to Unity Console. </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="layer"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        internal static void InternalLogExceptionFormat (LogLayer layer, Object context, string format, params object[] data)
        {
            InternalExtendedLog (layer, LogType.Exception, LogOption.None, context, format, data);
        }

        /// <summary> Logs internal data to Unity Console. </summary>
        /// <param name="layer"></param>
        /// <param name="type"></param>
        /// <param name="option"></param>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
        [System.Diagnostics.Conditional ("DEBUG")]
        internal static void InternalExtendedLog (LogLayer layer, LogType type, LogOption option, Object context, string format, params object[] data)
        {
            if (IsInFilter (LogLayer.Internal))
                ExtendedLog (layer, type, option, context, format, data);
        }

        #endregion

        #endregion
    }
}