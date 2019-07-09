using System;
using System.Collections;
using UnityEngine;

namespace BricksBucket.Pooling
{
    /// <summary>
    /// 
    /// Pooled.
    /// 
    /// <para>
    /// Helps to specified the type and number of instances to be spawned
    /// and be managed by Pool Manager and Pool classes.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2018 </para>
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Pooled : MonoBehaviour
    {



        #region Class members

        [SerializeField]
        private Pooled _source;         //  Original prefab reference.

        [SerializeField]
        private uint _amount = 1;       //  Amount to be spawned.

        [SerializeField]
        private PrefabType _type;       //  Type of prefab.

        [SerializeField]
        private bool _stopCoroutines;   //  Flag to skip coroutines.


        private Rigidbody[] _rigidbody3D;   //  Rigidbodies of a 3D type.
        private Rigidbody2D[] _rigidbody2D; //  Rigidbodies of a 2D type.

        #endregion



        #region Accessors

        /// <summary> Gets the original prefab. </summary>
        public Pooled Source
        {
            get { return _source; }
            private set { _source = value; }
        }

        /// <summary> Gets the amount of prefab to be spawned. </summary>
        public uint Amount
        {
            get { return _amount; }
            private set { _amount = value; }
        }

        /// <summary> Type of prefab.. </summary>
        public PrefabType Type
        {
            get { return _type; }
            private set { _type = value; }
        }

        /// <summary> Flag to stop coroutines. </summary>
        public bool StopCoroutines
        {
            get { return _stopCoroutines; }
            private set { _stopCoroutines = value; }
        }

        /// <summary> Gets the pool reference. </summary>
        public Pool Pool { get; private set; }

        /// <summary> Gets the spawner reference. </summary>
        public GameObject Spawner { get; private set; }

        /// <summary> Gets or sets the parent of this transform.</summary>
        public Transform Parent
        {
            get
            {
                return transform.parent;
            }
            set
            {
                switch (Type)
                {
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



        #region MonoBehaviour Methods

        //  Called on awake.
        private void Awake ()
        {
            switch (_type)
            {
                case PrefabType._3D:
                _rigidbody3D = GetComponentsInChildren<Rigidbody> (true);
                break;

                case PrefabType._2D:
                _rigidbody2D = GetComponentsInChildren<Rigidbody2D> (true);
                break;

                default:
                _rigidbody3D = null;
                _rigidbody2D = null;
                break;
            }
        }

        #endregion



        #region Class Implementation

        /// <summary> Assign the pool for this prefab. </summary>
        /// <param name="pool">New pool.</param>
        public void SetPool (Pool pool)
        {
            Pool = pool;
            name = pool.Prefab.name;
        }

        /// <summary> Sets the source. </summary>
        /// <param name="source">Source.</param>
        public void SetSource (Pooled source)
        {
            Source = source;
        }

        /// <summary> Spawn this instance with specified parameters. </summary>
        /// <param name="position">Position.</param>
        /// <param name="rotation">Rotation.</param>
        /// <param name="parent">Parent.</param>
        /// <param name="spawner">Spawner.</param>
        public void Spawn (
            Vector3 position,
            Quaternion rotation,
            Transform parent,
            GameObject spawner = null
        )
        {
            transform.position = position;
            transform.rotation = rotation;
            Parent = parent;
            Spawner = spawner;
            gameObject.SetActive (true);
            if (OnSpawn != null)
                OnSpawn.Invoke ();
        }

        /// <summary> Coroutine to despawn instance with delay. </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public IEnumerator DespawnCoroutine (float delay)
        {
            yield return new WaitForSeconds (delay);
            Despawn ();
        }

        /// <summary> Despawn this instance with a specified delay. </summary>
        /// <param name="delay">Delay.</param>
        public void Despawn (float delay)
        {
            StartCoroutine (DespawnCoroutine(delay));
        }

        /// <summary> Despawn this instance. </summary>
        public void Despawn ()
        {

            //  Check for pool.
            if (Pool == null)
            {
                Pool = PoolManager.Instance[this];
            }

            //  Move to the stack if it is not there yet.
            if (!Pool.OnStack (this))
            {
                Pool.Despawn (this);
                return;
            }

            //  Disable the game object in scene.
            gameObject.SetActive (false);
            Parent = Pool.Root;

            //  Applying custom preferences for types.
            switch (Type)
            {
                case PrefabType._3D:
                foreach (Rigidbody body in _rigidbody3D)
                {
                    body.velocity = Vector3.zero;
                    body.angularVelocity = Vector3.zero;
                }
                break;

                case PrefabType._2D:
                foreach (Rigidbody2D body in _rigidbody2D)
                {
                    body.velocity = Vector2.zero;
                    body.angularVelocity = 0;
                }
                break;
            }

            //  Stop current calls and coroutines.
            if (StopCoroutines)
            {
                MonoBehaviour[] c = GetComponentsInChildren<MonoBehaviour> ();
                foreach (MonoBehaviour component in c)
                {
                    component.StopAllCoroutines ();
                    component.CancelInvoke ();
                }
            }

            if (OnDespawn != null)
                OnDespawn.Invoke ();
        }

        #endregion
    }
}
