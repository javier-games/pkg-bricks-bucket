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
    [DisallowMultipleComponent]
    public class Pooled : MonoBehaviour {



        #region Class members

        [SerializeField]
        private Pooled _prefab;         //  Original prefab reference.

        [SerializeField]
        private int _amount = 1;        //  Amount to be spawned.

        [SerializeField]
        private PrefabType _type;       //  Type of prefab.

        [SerializeField]
        private bool _stopCoroutines;   //  Flag to skip coroutines.

        #endregion



        #region Accessors

        /// <summary> Gets the original prefab. </summary>
        public Pooled Prefab {
            get { return _prefab; }
            private set { _prefab = value; }
        }

        /// <summary> Gets the amount of prefab to be spawned. </summary>
        public int Amount{
            get { return _amount; }
            private set { _amount = value; }
        }

        /// <summary> Type of prefab.. </summary>
        private PrefabType Type {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary> Flag to stop coroutines. </summary>
        private bool StopCoroutines {
            get { return _stopCoroutines; }
            set { _stopCoroutines = value; }
        }

        /// <summary> Gets the pool reference. </summary>
        public Pool Pool { get; private set; }

        /// <summary> Gets the spawner reference. </summary>
        public GameObject Spawner { get; private set; }

        /// <summary> Gets or sets the parent of this transform.</summary>
        public Transform Parent{
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
        public Action OnSpawn { get; set; }

        /// <summary> Gets or sets the on despawn action. </summary>
        public Action OnDespawn { get; set; }

        #endregion



        #region Class Implementation

        /// <summary> Assign the pool for this prefab. </summary>
        public void AssignPool(Pool pool){
            Pool = pool;
            this.name = pool.Prefab.name;
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
            Parent = parent;
            Spawner = spawner;
            gameObject.SetActive (true);
            if (OnSpawn != null)
                OnSpawn.Invoke();
        }

        /// <summary> Despawn this instance with a specified delay. </summary>
        public void Despawn (float delay) {
            Invoke ("Despawn", delay);
        }

        /// <summary> Despawn this instance. </summary>
        public void Despawn () {

            //  Move to the stack if it is not there yet.
            if (Pool != null) {
                if (!Pool.OnStack (this)) {
                    Pool.Despawn (this);
                    return;
                }
            }
            else if (Prefab != null) {
                
            }
            else
                Debug.LogError ("From editor assign a prefab reference.");


            //  Disable the game object in scene.
            gameObject.SetActive (false);
            Parent = PoolManager.Instance.transform;

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
            if (_stopCoroutines) {
                MonoBehaviour [] c = GetComponentsInChildren<MonoBehaviour> ();
                foreach (MonoBehaviour component in c) {
                    component.StopAllCoroutines ();
                    component.CancelInvoke ();
                }
            }

            if (OnDespawn != null)
                OnDespawn.Invoke ();
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