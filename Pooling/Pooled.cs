using System;
using UnityEngine;

namespace Framework.Pooling {

    /// <summary>
    /// Pool manager.
    /// By Javier García, 2018.
    /// 
    /// <para>
    /// Manager for pools.
    /// </para>
    /// </summary>
    public class Pooled : MonoBehaviour {



        #region Class members

        [SerializeField]
        private int _amount = 1;        //  Amount to be spawned.

        [SerializeField]
        private PrefabType _type;       //  Type of prefab.

        [SerializeField]
        private bool _stopCorutines;    //  Flag to stop coroutines.

        private Pool        _pool;      //  Reference to the pool.
        private GameObject  _spawner;   //  Reference to the spawner.

        private Action _onSpawn;        //  Action called on spawn.
        private Action _onDespawn;      //  Action called on despawn.

        #endregion



        #region Accessors

        /// <summary> Gets the amount of prefab to be spawned. </summary>
        public int amount{
            get { return _amount; }
        }

        /// <summary> Gets the pool reference. </summary>
        public Pool pool{
            get { return _pool; }
        }

        /// <summary> Gets the spawner reference. </summary>
        public GameObject spawner {
            get { return _spawner; }
        }

        /// <summary> Gets or sets the parent of this transform.</summary>
        public Transform parent{
            get{
                return transform.parent;
            }
            set{
                switch (_type) {

                    case PrefabType.NONE:
                        transform.SetParent (value);
                        break;

                    case PrefabType._3D:
                    case PrefabType._2D:
                        transform.SetParent (value, true);
                        break;

                    case PrefabType.UI:
                        transform.SetParent (value, false);
                        break;

                    default:
                        transform.SetParent (value);
                        break;
                }
            }
        }

        /// <summary> Gets or sets the on spawn action. </summary>
        public Action OnSpawn {
            get { return _onSpawn; }
            set { _onSpawn = value; }
        }

        /// <summary> Gets or sets the on despawn action. </summary>
        public Action OnDespawn {
            get { return _onDespawn; }
            set { _onDespawn = value; }
        }

        #endregion



        #region Class Implementation

        /// <summary> Assign the pool for this prefab. </summary>
        public void Instatiate(Pool pool){
            _pool = pool;
            this.name = pool.prefab.name;
            Despawn (); 
        }

        /// <summary> Specifies position, rotation and parent. </summary>
        public void Spawn(
            Vector3 position,
            Quaternion rotation,
            Transform parent,
            GameObject spawner = null
        ){
            transform.position = position;
            transform.rotation = rotation;
            this.parent = parent;
            _spawner = spawner;
            gameObject.SetActive (true);
            if (_onSpawn != null)
                _onSpawn.Invoke();
        }

        /// <summary> Despawn this instance with a specified delay. </summary>
        public void Despawn (float delay) {
            Invoke ("Despawn", delay);
        }

        /// <summary> Despawn this instance. </summary>
        public void Despawn () {

            //  Move to the stack if it is not there yet.
            if(pool != null)
            if (!pool.OnStack (this)) {
                pool.Despawn (this);
                return;
            }

            //  Disable the game object in scene.
            gameObject.SetActive (false);
            parent = PoolManager.Instance.transform;

            //  Applying custom preferences for types.
            switch (_type){
                case PrefabType.NONE: break;

                case PrefabType._3D:
                    Rigidbody [] b3D = GetComponentsInChildren<Rigidbody> (
                        includeInactive: true
                    );
                    foreach (Rigidbody body in b3D) {
                        body.velocity = Vector3.zero;
                        body.angularVelocity = Vector3.zero;
                    }
                    break;

                case PrefabType._2D:
                    Rigidbody2D [] b2D = GetComponentsInChildren<Rigidbody2D> (
                        includeInactive: true
                    );
                    foreach (Rigidbody2D body in b2D) {
                        body.velocity = Vector2.zero;
                        body.angularVelocity = 0;
                    }
                    break;
            }

            //  Stop current calls and coroutines.
            if (_stopCorutines) {
                MonoBehaviour [] c = GetComponentsInChildren<MonoBehaviour> ();
                foreach (MonoBehaviour component in c) {
                    component.StopAllCoroutines ();
                    component.CancelInvoke ();
                }
            }

            if (_onDespawn != null)
                _onDespawn.Invoke ();
        }

        #endregion



        #region Nested Classes

        private enum PrefabType {
            NONE,
            _3D,
            _2D,
            UI
        }

        #endregion
    }
}