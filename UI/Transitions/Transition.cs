using System.Collections;
using UnityEngine;

namespace Framework.Utils {

    /// <summary>
    /// Transition.
    /// By Javier García, 2019.
    /// 
    /// <para>
    /// Class controlled by Transition Manager. Manage the time 
    /// </para>
    /// </summary>
    [DisallowMultipleComponent]
    public class Transition: MonoBehaviour {

        [SerializeField]
        private string _ID;

        [SerializeField]
        private float _inDuration;

        [SerializeField]
        private float _outDuration;

        [SerializeField]
        private float _minLoadingTime;

        public string ID {
            get { return _ID; }
            private set { _ID = value; }
        }

        public float MinLoadingTime {
            get { return _minLoadingTime; }
            protected set { _minLoadingTime = value; }
        }

        public float OutDuration {
            get { return _outDuration; }
            protected set { _outDuration = value; }
        }

        public float InDuration {
            get { return _inDuration; }
            protected set { _inDuration = value; }
        }

        protected virtual void Awake () {
            TransitionManager.In += In;
            TransitionManager.Loading += Loading;
            TransitionManager.Out += Out;
        }

        protected virtual void OnDestroy () {
            TransitionManager.In -= In;
            TransitionManager.Loading -= Loading;
            TransitionManager.Out -= Out;
        }

        protected virtual void In (float t) { }

        protected virtual void Loading (float t) { }

        protected virtual void Out (float t) { }

    }
}
