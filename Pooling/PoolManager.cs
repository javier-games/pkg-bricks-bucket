using System.Collections.Generic;
using UnityEngine;
using Framework.Utils;

namespace Framework.Pooling {

    /// <summary>
    /// 
    /// Pool manager.
    /// 
    /// <para>
    /// Manage instances of prefabs related to the pooled component.
    /// </para>
    /// 
    /// <para> By Javier García, 2018. </para>
    /// </summary>
    public sealed class PoolManager: Singleton<PoolManager> {



        #region Class Members

        /// <summary> The pools dictionary. </summary>
        private readonly Dictionary<Pooled, Pool> _poolsDictionary
        = new Dictionary<Pooled, Pool> ();

        #endregion



        #region Class Accessors

        /// <summary> Gets the pool with the specified instance. </summary>
        public Pool this[Pooled instance] { get { return GetPool (instance); } }

        #endregion



        #region MonoBehaviour Overrides

        /// <summary> Called on destroy. </summary>
        private void OnApplicationQuit () {
            foreach (Pool pool in _poolsDictionary.Values)
                pool.LogOverRequest ();
        }

        #endregion



        #region Class Implementation

        /// <summary> Gets the pool for the passed instance. </summary>
        /// <returns>The pool.</returns>
        /// <param name="instance">Instance.</param>
        private static Pool GetPool(Pooled instance) {

            //  Return if the passed instance is null.
            if (instance == null) {
                Debug.LogWarning ("The pooled instance is null.");
                return null;
            }

            //  If it is a prefab with already a pool return its pool.
            if (Instance._poolsDictionary.ContainsKey (instance))
                return Instance._poolsDictionary[instance];

            //  So it is an Instance. Return the pool if it have one.
            if (instance.Pool != null)
                if(Instance._poolsDictionary.ContainsValue(instance.Pool))
                    return instance.Pool;

            //  Return a pool of its prefab.
            if (instance.Source != null) {

                //  If its source alrady have a prefab return its pool.
                if (Instance._poolsDictionary.ContainsKey (instance.Source))
                    return Instance._poolsDictionary[instance.Source];

                // Create a pool for its source.
                Pool pool = GetPoolFromPrefab (instance.Source);
                instance.SetPool(pool);
                return pool;
            }

            //  Consider the instance as a prefab. Returns its pool.
            return GetPoolFromPrefab (instance);
        }

        //  Gets a pool from a prefab.
        private static Pool GetPoolFromPrefab (Pooled prefab) {
            //  Creates the pool.
            Pool pool = new Pool (prefab);
            Instance._poolsDictionary.Add (prefab, pool);
            return pool;
        }

        /// <summary> Spawn a new instance of the specified prefab. </summary>
        /// <returns>The spawned instance.</returns>
        /// <param name="prefab">Prefab.</param>
        /// <param name="spawner">Spawner.</param>
        public static Pooled Spawn (Pooled prefab, GameObject spawner = null) {

            if(prefab)
                return GetPool (prefab).Spawn (spawner);

            Debug.LogWarning ("Trying to Spawn from a null prefab.");
            return null;
        }

        /// <summary> Spawn a new instance of the specified prefab. </summary>
        /// <returns>The spawned instance.</returns>
        /// <param name="prefab">Prefab.</param>
        /// <param name="position">Position.</param>
        /// <param name="rotation">Rotation.</param>
        /// <param name="parent">Parent.</param>
        /// <param name="spawner">Spawner.</param>
        public static Pooled SpawnAt (
            Pooled prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null,
            GameObject spawner = null
        ) {

            if(prefab)
                return GetPool (prefab).SpawnAt (
                    position: position,
                    rotation: rotation,
                    parent: parent,
                    spawner: spawner
                );

            Debug.LogWarning ("Trying to Spawn from a null prefab.");
            return null;
        }

        /// <summary> Despawn the specified intance. </summary>
        /// <param name="instance">Instance to be despawned.</param>
        public static void Despawn (Pooled instance) {
            if (instance)
                instance.Despawn ();
            else
                Debug.LogWarning ("Trying to despawn a null instance.");
        }

        /// <summary> Despawn the specified gameObject. </summary>
        /// <param name="gameObject">Game object to be despawned.</param>
        public static void Despawn(GameObject gameObject){

            if (gameObject == null) {
                Debug.LogWarning ("Trying to despawn a null instance.");
                return;
            }

            Pooled instance = gameObject.GetComponent<Pooled> ();
            if (instance) {
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
            foreach (Pool pool in Instance._poolsDictionary.Values)
                pool.DespawnAll ();
        }

        /// <summary> Deletes the specified pool. </summary>
        /// <param name="prefab">Prefab.</param>
        /// <param name="useGC">If <c>true</c> Use Garbage Collector.</param>
        public static void DeletePool (Pooled prefab, bool useGC = true) {
            if (Instance._poolsDictionary.ContainsKey (prefab)) {
                Instance._poolsDictionary[prefab].Clear ();
                Instance._poolsDictionary.Remove (prefab);
            }
            if (useGC)
                System.GC.Collect ();
        }

        /// <summary> Clear all pools. </summary>
        /// <param name="useGC">If <c>true</c> Use Garbage Collector.</param>
        public static void Clear(bool useGC = true) {
            foreach (Pool pool in Instance._poolsDictionary.Values)
                pool.Clear ();
            Instance._poolsDictionary.Clear ();
            if (useGC)
                System.GC.Collect ();
        }

        #endregion



    }
}

