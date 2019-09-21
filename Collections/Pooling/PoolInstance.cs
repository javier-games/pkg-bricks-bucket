using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

using Suppress = System.Diagnostics.CodeAnalysis.SuppressMessageAttribute;

namespace BricksBucket.Collections
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
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// 
    /// </summary>
    public sealed class PoolInstance : MonoBehaviour
    {

        #region Class members

        /// <summary>
        /// Reference to the prefab this instance cames from.
        /// If null this instance is consider as the orifginal prefab.
        /// </summary>
        [Tooltip ("Original prefab reference")]
        [Space, SerializeField, ReadOnly, ShowIf ("_prefab")]
        private PoolInstance _prefab;

        /// <summary>
        /// Pool owner of this instance.
        /// </summary>
        [SerializeField, HideInInspector]
        private Pool _pool;

        /// <summary>
        /// Minimum amount of instances to spawn on awake by the pool
        /// manager.
        /// </summary>
        [Title ("Settings")]
        [Tooltip ("Minimum amount of instances to spawn on awake.")]
        [Space, SerializeField]
        private uint _amount = 1;

        /// <summary>
        /// Type of prefab. Changes the behaviour of the instance according to
        /// the selected option.
        /// </summary>
        [Tooltip ("Type of prefab.")]
        [SerializeField]
        private PrefabType _type;

        /// <summary>
        /// Flag to stop the coroutines on all the monobehaviour on the
        /// instance game object.
        /// </summary>
        [Tooltip ("Wether to stop coroutines for all monobehaviors.")]
        [SerializeField]
        private bool _stopCoroutines;

        /// <summary>
        /// Flag to use or ignore scaled time for delayed spawn or dispose.
        /// </summary>
        [Tooltip ("Wether to use scaled time with coroutines.")]
        [SerializeField]
        private bool _useScaledTime;

        /// <summary>
        /// Flag to parent to local or global position.
        /// </summary>
        [Tooltip ("Wether to use local or global position.")]
        [SerializeField]
        private bool _useLocalPosition;

        /// <summary>
        /// Flag to expand the pool after finish all instances.
        /// </summary>
        [Tooltip ("Wether to expand the pool if it is empty.")]
        [SerializeField]
        private bool _isExpandible = true;


        /// <summary>
        /// Event called on spawn.
        /// </summary>
        [Title ("Events")]
        [Tooltip ("Event called on spawn.")]
        [Space, SerializeField, LabelText ("On Spawn")]
        private UnityEvent _onSpawnEvent;

        /// <summary>
        /// Event called on dispose.
        /// </summary>
        [Tooltip ("Event called on dispose.")]
        [SerializeField, LabelText ("On Dispose")]
        private UnityEvent _onDisposeEvent;


        /// <summary>
        /// Collection of related rigid bodies 3D to reset on dispose.
        /// </summary>
        [Title ("Related 3D Components")]
        [Tooltip ("Related Rigid bodies to reset on dispose.")]
        [SerializeField, ReadOnly, ShowIf ("_type", PrefabType._3D)]
        private Rigidbody[] _rigidbodies3D;


        /// <summary>
        /// Collection of related rigid bodies 2D to reset on desapwn.
        /// </summary>
        [Title ("Related 2D Components")]
        [Tooltip ("Related Rigid bodies to reset on dispose.")]
        [SerializeField, ReadOnly, ShowIf ("_type", PrefabType._2D)]
        private Rigidbody2D[] _rigidbodies2D;

        /// <summary>
        /// All children components wich coroutines and invokes will be
        /// stoped on dispose.
        /// </summary>
        [SerializeField, HideInInspector]
        private Component[] _components;


        #endregion



        #region Accessors

        /// <summary> Gets the original prefab. </summary>
        public PoolInstance Prefab
        {
            get => _prefab;
            internal set => _prefab = value;
        }

        /// <summary> Pool owner of this instance. </summary>
        public Pool Pool
        {
            get => _pool;
            internal set => _pool = value;
        }

        /// <summary> Gets the amount of prefab to be spawned. </summary>
        public uint Amount
        {
            get => _amount;
            set => _amount = value;
        }

        /// <summary> Type of prefab.. </summary>
        public PrefabType Type
        {
            get => _type;
            set => _type = value;
        }

        /// <summary> Whether stops all (children include) coroutines
        /// and invokes. </summary>
        public bool StopCoroutines
        {
            get => _stopCoroutines;
            set => _stopCoroutines = value;
        }

        /// <summary> Wether to use scaled time with deleayd spawn and
        /// dispose actions. </summary>
        public bool UseScaledTime
        {
            get => _useScaledTime;
            set => _useScaledTime = value;
        }

        /// <summary> Wether to use global or local positions
        /// on parent. </summary>
        public bool UseLocalPosition
        {
            get => _useLocalPosition;
            set => _useLocalPosition = value;
        }

        /// <summary> Flag to expand the pool after finish all
        /// instances. </summary>
        public bool IsExpandible
        {
            get => _isExpandible;
            set => _isExpandible = value;
        }

        /// <summary> Gets the spawner reference. </summary>
        public Component Spawner { get; private set; }

        /// <summary> Gets or sets the parent of this transform.</summary>
        public Transform Parent
        {
            get => transform.parent;
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

        /// <summary>Unity event called on spawn.</summary>
        public UnityEvent OnSpawnEvent {
            get => _onSpawnEvent;
            private set => _onSpawnEvent = value;
        }

        /// <summary>Unity event called on dispose.</summary>
        public UnityEvent OnDisposeEvent {
            get => _onDisposeEvent;
            private set => _onDisposeEvent = value;
        }

        #endregion



        #region Auto Fill

        /// <summary> Called on Reset by the editor. </summary>
        private void Reset () => AutoFill ();

        /// <summary> Called on validate by editor. </summary>
        private void OnValidate () => AutoFill ();

        /// <summary> Fills serialized read only properties. </summary>
        [OnSave]
        private void AutoFill ()
        {
            GetRelatedComponents ();
            FindSource ();
        }

        /// <summary> Get related components according the type. </summary>
        private void GetRelatedComponents ()
        {
            _rigidbodies2D = null;
            _rigidbodies3D = null;
            _components = null;

            switch (Type)
            {
                case PrefabType._3D:
                _rigidbodies3D = GetComponentsInChildren<Rigidbody> (true);
                break;

                case PrefabType._2D:
                _rigidbodies2D = GetComponentsInChildren<Rigidbody2D> (true);
                break;
            }

            _components = GetComponentsInChildren<Component> (true);
        }

        /// <summary> Finds the prefab source. </summary>
        private void FindSource ()
        {
            if (!Application.isPlaying)
                _prefab = PoolManager.FindSource (this);
        }

        #endregion



        #region Public Methods

        /// <summary> Adds a new listener on spawn. </summary>
        /// <param name="action"> Action to add. </param>
        public void AddListenerOnSpawn (UnityAction action) =>
            OnSpawnEvent.AddListener (action);

        /// <summary> Removes a listener from on spawn. </summary>
        /// <param name="action"> Action to remove. </param>
        public void RemoveListenerOnSpawn (UnityAction action) =>
            OnSpawnEvent.RemoveListener (action);

        /// <summary> Removes all listeners from on spawn. </summary>
        public void RemoveAllListenersOnSpawn () =>
            OnSpawnEvent.RemoveAllListeners ();

        /// <summary> Adds a new listener on dispose. </summary>
        /// <param name="action"> Action to add. </param>
        public void AddListenerOnDispose (UnityAction action) =>
            OnDisposeEvent.AddListener (action);

        /// <summary> Removes a listener from on dispose. </summary>
        /// <param name="action"> Action to add. </param>
        public void RemoveListenerOnDispose (UnityAction action) =>
            OnDisposeEvent.RemoveListener (action);

        /// <summary> Removes all listeners from on dispose. </summary>
        public void RemoveAllListenersOnDispose () =>
            OnDisposeEvent.RemoveAllListeners ();

        /// <summary>Spawn this instance with specified parameters.</summary>
        /// <param name="position">Position to lacate the instance.</param>
        /// <param name="rotation">Rotation to locate the instance.</param>
        /// <param name="parent">Parent of the transform of instace.</param>
        /// <param name="spawner">Spawner who wants order spawn.</param>
        public PoolInstance SpawnAt (
            Vector3 position,
            Quaternion rotation,
            Transform parent,
            Component spawner = null
        ) => PoolManager.SpawnAt (Prefab, position, rotation, parent, spawner);

        /// <summary>Spawn this instance with specified parameters.</summary>
        /// <param name="position">Position to lacate the instance.</param>
        /// <param name="rotation">Rotation to locate the instance.</param>
        /// <param name="parent">Parent of the transform of instace.</param>
        /// <param name="spawner">Spawner who wants order spawn.</param>
        public void SpawnDelayed (
            Vector3 position,
            Quaternion rotation,
            Transform parent,
            float delay,
            System.Action<PoolInstance> callback = null,
            GameObject spawner = null
        )
        {
            //  Up to the manager.
        }

        /// <summary> Disposes this instance with a specified delay. </summary>
        /// <param name="delay">Delay.</param>
        public void Dispose (float delay)
        {
            //  Up to the manager.
        }

        /// <summary> Disposes this instance. </summary>
        public void Dispose () => PoolManager.Dispose (this);

        #endregion



        #region Internal Methods

        /// <summary>Spawn this instance with specified parameters.</summary>
        /// <param name="position">Position to lacate the instance.</param>
        /// <param name="rotation">Rotation to locate the instance.</param>
        /// <param name="parent">Parent of the transform of instace.</param>
        /// <param name="spawner">Spawner who wants order spawn.</param>
        internal void ApplySpawn (
            Vector3 position,
            Quaternion rotation,
            Transform parent,
            Component spawner
        ) {
            Parent = parent;
            Spawner = spawner;

            if (_useLocalPosition)
                transform.localPosition = position;
            else
                transform.position = position;
            transform.rotation = rotation;
            gameObject.SetActive (true);

            if (OnSpawnEvent.GetPersistentEventCount () > 0)
                OnSpawnEvent.Invoke ();
        }

        /// <summary>Dispose this instance.</summary>
        internal void ApplyDispose ()
        {
            gameObject.SetActive (false);

            //  Applying custom preferences for types.
            switch (Type)
            {
                case PrefabType._3D:
                for (int i = 0; i < _rigidbodies3D.Length; i++)
                {
                    _rigidbodies3D[i].velocity = Vector3.zero;
                    _rigidbodies3D[i].angularVelocity = Vector3.zero;
                }
                break;

                case PrefabType._2D:
                for (int i = 0; i < _rigidbodies2D.Length; i++)
                {
                    _rigidbodies2D[i].velocity = Vector2.zero;
                    _rigidbodies2D[i].angularVelocity = 0;
                }
                break;
            }

            //  Stop current calls and coroutines.
            if (StopCoroutines)
            {
                for (int i = 0; i < _components.Length; i++)
                {
                    if (_components[i] is MonoBehaviour)
                    {
                        (_components[i] as MonoBehaviour).StopAllCoroutines ();
                        (_components[i]as MonoBehaviour).CancelInvoke ();
                    }
                }
            }

            Parent = Pool.Root;
            if (OnDisposeEvent.GetPersistentEventCount () > 0)
                OnDisposeEvent.Invoke ();
        }

        #endregion
    }
}