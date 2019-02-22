using System.Collections.Generic;
using UnityEngine;

namespace Framework.Pooling {

    /// <summary>
    /// Pool.
    /// By Javier García, 2018.
    /// 
    /// <para>
    /// Pool of instances.
    /// </para>
    /// </summary>
    public class Pool {



        #region Class Members

        //  List of spwaned instances.
        private readonly List<Pooled>   _spawned = new List<Pooled>();

        //  List of instances on stack.
        private readonly List<Pooled>   _stack = new List<Pooled>();

        private int                     _overRequest;    //  Extra instances.

        #endregion



        #region Class Accessors

        /// <summary> Gets the count of instances in the pool. </summary>
        public int InstanceCount {
            get { return _spawned.Count + _stack.Count; }
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

        #endregion



        #region Constructor

        /// <summary> Initializes a new instance. </summary>
        public Pool (Pooled prefab) {
            Prefab = prefab;
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

        // Allocates the indicated amount of instances.
        private void AllocateInstance (int count = 1) {
            for (int i = 0; i < count; i++) {

                Pooled instance = Object.Instantiate (
                    original: Prefab.gameObject,
                    position: Vector3.zero,
                    rotation: Quaternion.identity
                ).GetComponent<Pooled> ();

                _stack.Add (instance);
                instance.AssignPool (this);
                instance.Despawn ();
            }
        }

        /// <summary> Spawn an instance. </summary>
        public Pooled Spawn (GameObject spawner = null) {

            //  Instantiate a new instace if the stack is empty.
            if (_stack.Count == 0) {
                AllocateInstance ();
                _overRequest++;
            }

            //  Spawning the first element in the list.
            Pooled instance = _stack [0];
            _stack.Remove (instance);
            _spawned.Add (instance);
            instance.Spawn (Vector3.zero, Quaternion.identity, null, spawner);

            return instance;
        }

        /// <summary> Spawns at the specified position and rotation. </summary>
        public Pooled SpawnAt (
            Vector3 position,
            Quaternion rotation,
            Transform parent,
            GameObject spawner = null
        ) {

            if (_stack.Count == 0) {
                AllocateInstance ();
                _overRequest++;
            }

            //  Spawning the first element in the list.
            Pooled instance = _stack [0];
            _stack.Remove (instance);
            _spawned.Add (instance);
            instance.Spawn (position, rotation, parent, spawner);

            return instance;
        }

        /// <summary> Despawn the specified gameObject. </summary>
        public void Despawn(GameObject gameObject){

            Pooled instance = gameObject.GetComponent<Pooled> ();

            if (instance != null)
                Despawn (instance);
            else
                Debug.LogError (string.Concat(
                    "The Game Object ", 
                    gameObject.name, 
                    " is not an instance of the pool ",
                    Prefab.name,
                    "."
                ));

        }

        /// <summary> Despawn the specified instance. </summary>
        public void Despawn (Pooled instance) {

            //  Avoid adding missing instances.
            if(instance == null)
                Debug.LogError (string.Concat (
                     "Trying to add a null instance to the ",
                     Prefab.name,
                     " pool."
                ));

            //  Set from Spawned to Despawned.
            else if (DidSpawned (instance)) {
                _spawned.Remove (instance);
                _stack.Add (instance);
                instance.Despawn ();
            }

            //  Trying to add a instance out of the pool.
            else if (!OnStack (instance)) {

                /* All instances from the pool has to be instatiaded by it
                 * self to prevent the developer to add diferent types of
                 * instances in the same pool. To create different types of
                 * instances you must use the Pool Manager.
                */

                Debug.LogError (string.Concat (
                     "Trying to add an external instance to the ",
                     Prefab.name,
                     " pool."
                ));
            }

            //  Verify the spawner becouse it is already on stack.
            else instance.Despawn ();
        }

        /// <summary> Despawns all. </summary>
        public void DespawnAll () {
            while (_spawned.Count > 0)
                Despawn (_spawned [0]);
        }

        /// <summary> Clear this pool. </summary>
        public void Clear (bool useGC = true) {
            DespawnAll ();
            while (_stack.Count > 0) {
                Pooled instance = _stack [0];
                _stack.RemoveAt (0);
                if (instance != null && instance.gameObject != null)
                    Object.Destroy (instance.gameObject);
            }

            if (useGC)
                System.GC.Collect ();
        }

        public void LogOverRequest(){
            if (_overRequest > 0)
                Debug.LogWarning (string.Concat(
                    "Over Request Alert: ",
                    _overRequest,
                    " extra instances of \"",
                    Prefab.name,
                    "\" prefab has been requested during the game."
                ));
        }

        #endregion



    }
}
