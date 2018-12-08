using System.Collections.Generic;
using UnityEngine;
using Framework.Utils;

namespace Framework.Pooling {

    /// <summary>
    /// Pool manager.
    /// By Javier García, 2018.
    /// 
    /// <para>
    /// Manager for pools.
    /// </para>
    /// </summary>
    public sealed class PoolManager : Singleton<PoolManager> {



        #region Class Members

        //  <summary> The pools dictionary.
        private readonly Dictionary<Pooled, Pool> poolsDictionary
        = new Dictionary<Pooled, Pool>();

        #endregion



        #region Class Accessors

        /// <summary> Gets the pool with the specified prefab. </summary>
        public Pool this [Pooled prefab] { get { return GetPool(prefab) ; } }

        #endregion



        #region MonoBehaviour Overrides

        /// <summary> Called on destroy. </summary>
        private void OnApplicationQuit () {
            foreach (Pool pool in poolsDictionary.Values)
                pool.LogOverRequest ();
        }

        #endregion



        #region Class Implementation

        //  Gets the pool.
        private static Pool GetPool(Pooled instance) {

            //  If the instance has not a pool.
            return instance.pool == null ? 
                        GetPoolFromPrefab (instance) :
                        GetPoolFromInstance (instance);
        }

        /// <summary> Gets a pool from a instance. </summary>
        private static Pool GetPoolFromInstance (Pooled instance) {

            //  Look for the pool in the collection.
            if (Instance.poolsDictionary.ContainsValue (instance.pool))
                return instance.pool;

            Debug.LogError (string.Concat(
                "The instance ",
                instance.name,
                " do not belongs to any pool in the Pool Manager."
            ));

            return null;
        }

        /// <summary> Gets a pool from a prefab. </summary>
        private static Pool GetPoolFromPrefab (Pooled prefab) {

            //  If it is a prefab with a pool return its pool.
            if (Instance.poolsDictionary.ContainsKey (prefab))
                return Instance.poolsDictionary [prefab];

            //  Else create a prefab.
            Pool pool = new Pool (prefab);
            Instance.poolsDictionary.Add (prefab, pool);
            return pool;
        }

        /// <summary> Spawn a new instance of the specified prefab. </summary>
        public static Pooled Spawn (Pooled prefab, GameObject spawner = null) {
            return GetPool (prefab).Spawn (spawner);
        }

        /// <summary> Spawn a new instance of the specified prefab. </summary>
        public static Pooled SpawnAt (
            Pooled prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null,
            GameObject spawner = null
        ) {
            return GetPool (prefab).SpawnAt (
                position:   position,
                rotation:   rotation,
                parent:     parent,
                spawner:    spawner
            );
        }

        /// <summary> Despawn the specified intance. </summary>
        public static void Despawn (Pooled instance) {

            if (instance.pool != null) {
                GetPoolFromInstance (instance).Despawn (instance);
                return;
            }

            Debug.LogError (string.Concat (
                "The instance ",
                instance.name,
                " do not belongs to any pool."
            ));
        }

        /// <summary> Despawn the specified gameObject. </summary>
        public static void Despawn(GameObject gameObject){

            Pooled instance = gameObject.GetComponent<Pooled> ();
            if (instance != null) {
                Despawn (instance);
                return;
            }

            Debug.LogError (string.Concat (
                "The Game Object ",
                gameObject.name,
                " has not the Pooled Component."
            ));
        }

        /// <summary> Despawns all pooled objects. </summary>
        public static void DespawnAll () {
            foreach (Pool pool in Instance.poolsDictionary.Values)
                pool.DespawnAll ();
        }

        /// <summary> Deletes the specified pool. </summary>
        public static void DeletePool(Pooled prefab, bool useGC = true){
            GetPool (prefab).Clear ();
            Instance.poolsDictionary.Remove (prefab);
            if (useGC)
                System.GC.Collect ();
        }

        /// <summary> Clear all pools. </summary>
        public static void Clear(bool useGC = true) {
            foreach (Pool pool in Instance.poolsDictionary.Values)
                pool.Clear ();
            Instance.poolsDictionary.Clear ();
            if (useGC)
                System.GC.Collect ();
        }
        #endregion
    }
}

