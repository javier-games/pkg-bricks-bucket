using UnityEngine;

namespace BricksBucket.Generics
{

    /// <summary>
    /// MonoSingleton.
    /// 
    /// <para>
    /// Generic singleton that inherits from monobehaviour.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2018 </para>
    /// 
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {



        #region Class Members

        private static T _instance;         //  Instance of the singleton.

        #pragma warning disable RECS0108    //  Warns about static fields in generic types
        private static bool _didAwoken;     //  Flagged true after awake.
        private static bool _didDestroyed;  //  Flagged true after on destroy.
        #pragma warning restore RECS0108    //  Warns about static fields in generic types

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
                    DebugUtils.InternalExtendedLog (
                        layer: LogLayer.Logistics,
                        type: LogType.Warning,
                        context: null,
                        format: "{0} singleton has been already destroyed.",
                        data: typeof(T)
                    );
                    return null;
                }

                //  Return InstanceForced if this has not passed for Awake.
                if (!_didAwoken && _instance == null)
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
                if (_instance != null)
                    return _instance;

                DebugUtils.InternalExtendedLog (
                    layer: LogLayer.Logistics,
                    type: LogType.Warning,
                    context: null,
                    format: "{0} singleton has been forced.",
                    data: typeof (T)
                );

                /* If a singleton has to be forced means that it is called
                 * from another awake in other monobehaviour class before
                 * this singleton is initialized or the execution order is
                 * wrong.
                 * 
                 * Try to call this singleton from Start method or use
                 * Instance Forced property instead if it is necessary.
                 * 
                 * Check out the Unity's execution order of events:
                 * https://docs.unity3d.com/Manual/ExecutionOrder.html
                 */

                //  Find class in herarchy.
                _instance = FindObjectOfType (typeof (T)) as T;

                if (FindObjectsOfType (typeof (T)).Length > 1)
                    DebugUtils.InternalExtendedLog (
                        layer: LogLayer.Logistics,
                        type: LogType.Warning,
                        context: _instance,
                        format: "Various instances of {0} singleton found.",
                        data: typeof (T)
                    );

                if (_instance != null)
                    return _instance;

                //  Find prefab in resources folder.
                T prefab = Resources.Load<T> (typeof (T).Name);
                if (prefab != null)
                {
                    _instance = (Instantiate (
                        original: prefab.gameObject
                    ) as GameObject).GetComponent<T> ();
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
        public static bool InstanceExist
        {
            get { return _instance != null; }
        }

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