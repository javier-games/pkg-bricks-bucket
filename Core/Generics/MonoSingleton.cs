using UnityEngine;

namespace BricksBucket.Core.Generics
{
    /// <!-- MonoSingleton -->
    /// 
    /// <summary>
    /// Generic singleton that inherits from MonoBehaviour.
    /// </summary>
    /// 
    /// <typeparam name="T">Heiress Class Type.</typeparam>
    /// 
    /// <!-- By Javier García | @jvrgms | 2020 -->
    [DisallowMultipleComponent]
    public abstract class MonoSingleton<T> : MonoBehaviour
    where T : MonoSingleton<T>
    {
        #region Class Members

        private static T _instance;         //  Instance of the singleton.

        #pragma warning disable RECS0108
        // ReSharper disable StaticMemberInGenericType
        private static bool _didAwoken;     //  Flagged true after awake.
        private static bool _didDestroyed;  //  Flagged true after on destroy.
        // ReSharper restore StaticMemberInGenericType
        #pragma warning restore RECS0108

        #endregion


        #region Class Accessors

        /// <summary> Gets the instance. </summary>
        public static T Instance
        {
            get
            {
                //  Return null if the instances has been already destroyed.
                if (_didDestroyed)
                {
                    // TODO: Add Log on MonoSingleton.Instance property.
                    /*
                    DebugUtils.InternalExtendedLog (
                        layer: LogLayer.Logistics,
                        type: LogType.Warning,
                        context: null,
                        format: "{0} singleton has been already destroyed.",
                        data: typeof(T)
                    );
                    */
                    return null;
                }

                //  Return InstanceForced if this has not passed for Awake.
                if (!_didAwoken && !InstanceExist)
                    return InstanceForced;
                
                return _instance;
            }
        }

        /// <summary> Gets the instance forced to not return null. </summary>
        public static T InstanceForced
        {
            get
            {
                //  Return this instance.
                if (InstanceExist)
                    return _instance;
                
                // TODO: Add Log on MonoSingleton.InstanceForced property.
                /*
                DebugUtils.InternalExtendedLog (
                    layer: LogLayer.Logistics,
                    type: LogType.Warning,
                    context: null,
                    format: "{0} singleton has been forced.",
                    data: typeof (T)
                );
                */

                /* If a singleton has to be forced means that it is called
                 * from another awake in other MonoBehaviour class before
                 * this singleton is initialized or the execution order is
                 * wrong.
                 * 
                 * Try to call this singleton from Start method or use
                 * Instance Forced property instead if it is necessary.
                 * 
                 * Check out the Unity's execution order of events:
                 * https://docs.unity3d.com/Manual/ExecutionOrder.html
                 */

                //  Find class in hierarchy.
                _instance = FindObjectOfType (typeof (T)) as T;

                if (FindObjectsOfType (typeof (T)).Length > 1)
                {
                    // TODO: Add Log on MonoSingleton.InstanceForced property.
                    /*
                    DebugUtils.InternalExtendedLog (
                        layer: LogLayer.Logistics,
                        type: LogType.Warning,
                        context: _instance,
                        format: "Various instances of {0} singleton found.",
                        data: typeof (T)
                    );
                    */
                }

                if (InstanceExist)
                    return _instance;

                //  Find prefab in resources folder.
                var prefab = Resources.Load<T> (typeof (T).Name);
                if (prefab != null)
                {
                    _instance = Instantiate (
                        original: prefab.gameObject
                    ).GetComponent<T> ();
                    _instance.name = typeof (T).Name;
                    return _instance;
                }

                //  Create an instance.
                _instance = new GameObject (
                    name: typeof (T).Name,
                    components: typeof (T)
                ).GetComponent<T> ();
                return _instance;
            }
        }

        /// <summary> Indicates whether this has an instance or not. </summary>
        public static bool InstanceExist => _instance != null;

        #endregion


        #region MonoBehaviour Methods

        /// <summary> Called on awake. </summary>
        protected virtual void Awake ()
        {
            if (_instance != null && this != _instance)
                Destroy (gameObject);
            else
            {
                _instance = this as T;
                _didAwoken = true;
                _didDestroyed = false;
            }
        }

        /// <summary> Called on destroy. </summary>
        protected virtual void OnDestroy ()
        {
            _instance = null;
            _didAwoken = false;
            _didDestroyed = true;
        }

        #endregion
    }
}