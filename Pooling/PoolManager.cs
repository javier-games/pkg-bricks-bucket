using System.Collections.Generic;
using UnityEngine;
using BricksBucket.Utils;
using BricksBucket.Generics;

namespace BricksBucket.Pooling
{
    /// <summary>
    /// 
    /// Pool manager.
    /// 
    /// <para>
    /// Manage instances of prefabs related to the pooled component.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2018 </para>
    /// </summary>
    public sealed class PoolManager : Singleton<PoolManager>
    {



        #region Class Members

        /// <summary> The pools dictionary. </summary>
        private readonly Dictionary<Pooled, Pool> _poolsDictionary
            = new Dictionary<Pooled, Pool> ();

        #endregion



        #region Class Accessors

        /// <summary> Gets the pool with the specified instance. </summary>
        public Pool this [Pooled reference]
        {
            get { return GetPool (reference); }
        }

        #endregion



        #region MonoBehaviour Overrides

        /// <summary> Called on destroy. </summary>
        private void OnApplicationQuit ()
        {
            foreach (Pool pool in _poolsDictionary.Values)
                pool.LogOverRequest ();
        }

        #endregion



        #region Class Implementation

        /// <summary> Gets the pool for the passed instance. </summary>
        /// <returns>The pool.</returns>
        /// <param name="reference">Instance reference.</param>
        private static Pool GetPool (Pooled reference)
        {
            //  Return if the passed instance is null.
            if (reference == null)
            {
                DebugUtils.LogisticsLogWarningFormat (
                    context: null,
                    format: string.Empty,
                    data: "The pooled instance is null."
                );
                return null;
            }

            //  If it is a prefab with already a pool return its pool.
            if (Instance._poolsDictionary.ContainsKey (reference))
                return Instance._poolsDictionary[reference];

			//  So... it is an Instance. Return the pool if it have one.
			if (reference.Pool != null)
                if (Instance._poolsDictionary.ContainsValue (reference.Pool))
                    return reference.Pool;

			//  Return a pool of its prefab.
			if (reference.Source != null)
            { 
                //  If its source alrady have a prefab return its pool.
                if (Instance._poolsDictionary.ContainsKey (reference.Source))
                    return Instance._poolsDictionary[reference.Source];

                // Create a pool for its source.
                Pool pool = GetPoolFromPrefab (reference.Source);
                reference.SetPool (pool);
                return pool;
            }

			//  Consider the instance as a prefab. Returns its pool.
			return GetPoolFromPrefab (reference);
        }

        //  Gets a pool from a prefab.
        private static Pool GetPoolFromPrefab (Pooled prefab)
        {
            //  Creates the pool.
            Pool pool = new Pool (prefab);
            Instance._poolsDictionary.Add (prefab, pool);
            return pool;
        }

        /// <summary> Spawn a new instance of the specified prefab. </summary>
        /// <returns>The spawned instance.</returns>
        /// <param name="prefab">Prefab.</param>
        /// <param name="spawner">Spawner.</param>
        public static Pooled Spawn (Pooled prefab, GameObject spawner = null)
        {

            if (prefab)
                return GetPool (prefab).Spawn (spawner);

            DebugUtils.LogisticsLogWarningFormat (
                context: null,
                format: string.Empty,
                data: "Trying to spawn from a null prefab."
            );
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
        )
        {

            if (prefab)
                return GetPool (prefab).SpawnAt (
                    position: position,
                    rotation: rotation,
                    parent: parent,
                    spawner: spawner
                );

            DebugUtils.LogisticsLogWarningFormat (
                context: null,
                format: string.Empty,
                data: "Trying to spawn from a null prefab."
            );
            return null;
        }

		/// <summary> Despawn the specified intance. </summary>
		/// <param name="reference">Instance to be despawned.</param>
		/// <returns> True if the gameObject has been despawned. </returns>
		public static bool Despawn (Pooled reference)
        {
            if (reference)
			{
				reference.Despawn();
				return true;
			}
			DebugUtils.LogisticsLogWarningFormat (
                context: null,
                format: string.Empty,
                data: "Trying to despawn from a null prefab."
            );
			return false;
        }

		/// <summary> Despawn the specified gameObject. </summary>
		/// <param name="gameObject">Game object to be despawned.</param>
		/// <returns> True if the gameObject has been despawned. </returns>
		public static bool Despawn (GameObject gameObject)
        {

            if (gameObject == null)
            {
                DebugUtils.LogisticsLogWarningFormat (
                    context: null,
                    format: string.Empty,
                    data: "Trying to despawn a null instance."
                );
                return false;
            }

            Pooled reference = gameObject.GetComponent<Pooled> ();
            if (reference)
                return Despawn (reference);

            DebugUtils.LogisticsLogErrorFormat (
                context: gameObject,
                format: "The game object {0} has not a pooled component.",
                data: gameObject.name
            );
			return false;
        }

        /// <summary> Despawns all pooled objects. </summary>
        public static void DespawnAll ()
        {
            foreach (Pool pool in Instance._poolsDictionary.Values)
                pool.DespawnAll ();
        }

        /// <summary> Deletes the specified pool. </summary>
        /// <param name="prefab">Prefab.</param>
        /// <param name="useGC">If <c>true</c> Use Garbage Collector.</param>
        public static void DeletePool (Pooled prefab, bool useGC = true)
        {
            if (Instance._poolsDictionary.ContainsKey (prefab))
            {
                Instance._poolsDictionary[prefab].Clear ();
                Instance._poolsDictionary.Remove (prefab);
            }
            if (useGC)
                System.GC.Collect ();
        }

        /// <summary> Clear all pools. </summary>
        /// <param name="useGC">If <c>true</c> Use Garbage Collector.</param>
        public static void Clear (bool useGC = true)
        {
            foreach (Pool pool in Instance._poolsDictionary.Values)
                pool.Clear ();
            Instance._poolsDictionary.Clear ();
            if (useGC)
                System.GC.Collect ();
        }

        #endregion



    }
}
