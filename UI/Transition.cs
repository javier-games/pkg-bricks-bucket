using System.Collections;
using UnityEngine;

namespace Framework.Utils {

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
            private set { _minLoadingTime = value; }
        }

        public float OutDuration {
            get { return _outDuration; }
            private set { _outDuration = value; }
        }

        public float InDuration {
            get { return _inDuration; }
            private set { _inDuration = value; }
        }

        protected virtual void TransitionIn (float t) { }

        protected virtual void TransitionLoading (float t) { }

        protected virtual void TransitionOut (float t) { }


        public IEnumerator In () {
            float time = 0;
            if (_inDuration > 0) {
                while (time < _inDuration) {
                    TransitionIn (time / _inDuration);
                    time += Time.deltaTime;
                    yield return null;
                }
                TransitionIn (1);
            }
        }

        public IEnumerator Out () {
            float time = 0;
            if (_outDuration > 0) {
                while (time < _outDuration) {
                    TransitionOut (time / _outDuration);
                    time += Time.deltaTime;
                    yield return null;
                }
                TransitionOut (1);
            }
        }

        public IEnumerator Loading (AsyncOperation asyncLoad) {
            float time = 0;
            while (!asyncLoad.isDone || time < _minLoadingTime) {

                float fakeProgress = _minLoadingTime > 0 ? time / _minLoadingTime : 1;

                TransitionLoading (asyncLoad.progress < fakeProgress ? asyncLoad.progress : fakeProgress);

                time = time + Time.deltaTime < _minLoadingTime ? time + Time.deltaTime : _minLoadingTime;

                yield return null;
            }
        }

    }
}
