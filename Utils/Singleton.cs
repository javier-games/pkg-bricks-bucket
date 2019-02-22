using UnityEngine;

namespace Framework.Utils {

    /// <summary>
    /// Singleton.
    /// By Javier García, 2018.
    /// 
    /// <para>
    /// Generic singleton that inherits from monobehaviour.
    /// </para>
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T> {



        #region Class Members

        private static T    instance;       //  Instance of the singleton.
        #pragma warning disable RECS0108    //  Warns about static fields in generic types
        private static bool didAwoken;      //  Flagged true after awake.
        private static bool didDestroyed;   //  Flagged true after on destroy.
        #pragma warning restore RECS0108    //  Warns about static fields in generic types

        #endregion



        #region Class Accessors

        /// <summary> Gets the instance. </summary>
        public static T Instance {
            get {

                //  Return null if the instances has been already destroyed.
                if (didDestroyed)
                    return null;

                //  Return InstanceForced if this has not passed for Awake.
                if (!didAwoken && instance == null){
                    Debug.LogWarning("Singleton " + typeof(T) + " forced.");

                    /* If a singleton has to be forced means that it is called
                     * from another awake in other monobehaviour class before
                     * this singleton is initialized or the execution order is
                     * wrong.
                     * 
                     * Try to call this singleton from Start method or Instance
                     * Forced property instead if it is necessary.
                     * 
                     * Check out the Unity's execution order of events:
                     * https://docs.unity3d.com/Manual/ExecutionOrder.html
                     */

                    return InstanceForced;
                }

                return instance;
            }
        }

        /// <summary> Gets the instance forced to not return null. </summary>
        public static T InstanceForced {
            get {

                //  Return null if the instances has been already destroyed.
                if (didDestroyed)
                    return null;

                //  Return this instance.
                if (instance != null)
                    return instance;

                //  Find class in herarchy.
                instance = FindObjectOfType (typeof (T)) as T;
                if (FindObjectsOfType (typeof (T)).Length > 1)
                    Debug.LogWarning ("Various instances of " + typeof (T));
                if (instance != null)
                    return instance;

                //  Find prefab in resources folder.
                T prefab = Resources.Load<T> (typeof (T).Name);
                if (prefab != null) {
                    instance = (Instantiate (
                        original: prefab.gameObject
                    ) as GameObject).GetComponent<T> ();
                    instance.name = typeof (T).Name;
                    return instance;
                }

                //  Create an instance.
                instance = new GameObject (
                    name:       "Temporal " + typeof (T),
                    components: typeof (T)
                ).GetComponent<T> ();
                return instance;
            }
        }

        /// <summary> Indicates whether this has an instance. </summary>
        public static bool HasInstance{
            get { return instance != null; }
        }

        #endregion



        #region MonoBehaviour Overrides

        //  Called on awake.
        protected virtual void Awake () {
            if (instance != null && this != instance)
                Destroy (this.gameObject);
            else {
                instance = this as T;
                didAwoken = true;
            }
        }

        //  Called on destroy.
        protected virtual void OnDestroy () {
            instance = null;
            didAwoken = false;
            didDestroyed = true;
        }

        #endregion
    }
}