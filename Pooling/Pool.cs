using System.Collections.Generic;
using UnityEngine;

namespace Framework.Pooling {

    /// <summary>
    /// 
    /// Pool.
    /// 
    /// <para>
    /// Pool of instances managed by the PoolManager Class.
    /// </para>
    /// 
    /// <para>By Javier García, 2018.</para>
    /// </summary>
    public class Pool {



        #region Class Members

        //  List of spwaned instances.
        private readonly List<Pooled>   _spawned = new List<Pooled>();

        //  List of instances on stack.
        private readonly Stack<Pooled>  _stack = new Stack<Pooled>();

        #endregion



        #region Class Accessors

        /// <summary> Gets the count of instances in the pool. </summary>
        public int InstanceCount {
            get { return SpawnedCount + StackCount; }
        }

        /// <summary> Gets the count of instances spawned. </summary>
        public int SpawnedCount {
            get { return _spawned.Count; }
        }

        /// <summary> Gets the count of instances on stack. </summary>
        public int StackCount {
            get { return _stack.Count; }
        }

        /// <summary> Gets the prefab of the pool. </summary>
        public Pooled Prefab { get; private set; }

        /// <summary> Gets the parent. </summary>
        public Transform Root { get; private set; }

        /// <summary> Amount of over requested instances. </summary>
        private uint OverRequestedInstancesAmount { get; set; }

        #endregion



        #region Constructor

        /// <summary> Initializes a new instance. </summary>
        public Pool (Pooled prefab) {
            Prefab = prefab;

            Root = new GameObject (
                name: string.Concat (prefab.name, " Pool")
            ).transform;
            Root.SetParent (PoolManager.Instance.transform);
            Root.position = Vector3.zero;

            AllocateInstance (prefab.Amount);
        }

        #endregion



        #region Class Implementation

        #region Instance Validation

        /// <summary> Idicates if the instance is on the stack. </summary>
        public bool OnStack(Pooled instance){
            return _stack.Contains (instance);
        }

        /// <summary> Idicates if the instance did spawned. </summary>
        public bool DidSpawned(Pooled instance){
            return _spawned.Contains (instance);
        }

        /// <summary> Contains the specified instance. </summary>
        public bool Contains(Pooled instance){
            return OnStack (instance) || DidSpawned (instance);
        }

        #endregion

        /// <summary> Allocates the indicated amount of instances. </summary>
        /// <param name="count">Count.</param>
        private void AllocateInstance (uint count = 1) {
            for (int i = 0; i < count; i++) {

                Pooled instance = Object.Instantiate (
                    original: Prefab.gameObject,
                    position: Vector3.zero,
                    rotation: Quaternion.identity
                ).GetComponent<Pooled> ();

                _stack.Push (instance);
                instance.SetPool (this);
                instance.SetSource (Prefab);
                instance.Despawn ();
            }
        }

        /// <summary> Spawn an instance. </summary>
        /// <returns>The spawn.</returns>
        /// <param name="spawner">Spawner.</param>
        public Pooled Spawn (GameObject spawner = null) {
            return SpawnAt (Vector3.zero, Quaternion.identity, null, spawner);
        }

        /// <summary> Spawns at the specified parameters. </summary>
        /// <returns>The spawned instance.</returns>
        /// <param name="position">Position.</param>
        /// <param name="rotation">Rotation.</param>
        /// <param name="parent">Parent.</param>
        /// <param name="spawner">Spawner.</param>
        public Pooled SpawnAt (
            Vector3 position,
            Quaternion rotation,
            Transform parent,
            GameObject spawner = null
        ) {

            if (_stack.Count == 0) {
                AllocateInstance ();
                OverRequestedInstancesAmount++;
            }

            //  Spawning the first element in the list.
            Pooled instance = _stack.Pop ();
            _spawned.Add (instance);
            instance.Spawn (position, rotation, parent, spawner);

            return instance;
        }

        /// <summary> Despawn the specified instance. </summary>
        /// <param name="instance">Instance.</param>
        public void Despawn (Pooled instance) {

            //  Avoid adding missing instances.
            if (instance == null) {
                Debug.LogError (string.Concat (
                     "Trying to add a null instance to the ",
                     Prefab.name,
                     " pool."
                ));
                return;
            }

            //  Avoid adding instances from other pools.
            if (instance.Pool != this) {
                instance.Despawn ();
                return;
            }

            //  Set from Spawned to Despawned.
            if (DidSpawned (instance)) {
                _spawned.Remove (instance);
                _stack.Push (instance);
                instance.Despawn ();
                return;
            }

            //  Trying to add a instance out of the pool.
            if (!OnStack (instance)) {

                //  A instance of the prefab found.
                _stack.Push (instance);
                instance.Despawn ();
                return;
            }

            //  Verify the spawner becouse it is already on stack.
            instance.Despawn ();
        }

        /// <summary> Despawns all. </summary>
        public void DespawnAll () {
            while (_spawned.Count > 0)
                Despawn (_spawned [0]);
        }

        /// <summary> Clear this pool. </summary>
        /// <param name="useGC">If <c>true</c> Use Garbage Collector.</param>
        public void Clear (bool useGC = true) {
            DespawnAll ();
            while (_stack.Count > 0) {
                Pooled instance = _stack.Pop ();
                if (instance != null && instance.gameObject != null)
                    Object.Destroy (instance.gameObject);
            }

            if (useGC)
                System.GC.Collect ();
        }

        /// <summary> Log the amount of over request instances. </summary>
        public void LogOverRequest(){
            if (OverRequestedInstancesAmount > 0)
                Debug.LogWarning (string.Concat(
                    "Over Request Alert: ",
                    OverRequestedInstancesAmount,
                    " extra instances of \"",
                    Prefab.name,
                    "\" prefab has been requested during the game."
                ));
        }

        #endregion



    }
}
