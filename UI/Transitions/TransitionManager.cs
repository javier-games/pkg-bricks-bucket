using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Framework.Utils {

    /// <summary>
    /// Transition Manager.
    /// By Javier García, 2019.
    /// 
    /// <para>
    /// Instance to manage transition instances.
    /// </para>
    /// </summary>
    public class TransitionManager: Singleton<TransitionManager> {



        #region Class Members

        //  Pool of used transition.
        private List<Transition> _usedTransitions;

        public static System.Action<float> In { get; set; }
        public static System.Action<float> Loading { get; set; }
        public static System.Action<float> Out { get; set; }

        #endregion



        #region MonoBehaviour Overrides

        //  Called on awake.
        private new void Awake () {
            base.Awake ();
            DontDestroyOnLoad (gameObject);
            _usedTransitions = new List<Transition> ();
        }

        #endregion


        #region Static Methods

        /// <summary>
        /// Loads the scene on single mode.
        /// </summary>
        /// <param name="scene"> Scene Reference.</param>
        /// <param name="custom"> Optional Custom Transition.</param>
        public static void LoadScene (SceneReference scene, Transition custom = null) {
            Instance.InitLoad (scene, custom);
        }

        #endregion

        #region Class Implementation

        //  Method to instantiate the transition and keep it for future a use.
        private void InitLoad (SceneReference scene, Transition custom) {

            if (custom == null)
                SceneManager.LoadScene (scene, LoadSceneMode.Single);

            _usedTransitions.ForEach (t => t.gameObject.SetActive (false));
            Transition transition = _usedTransitions.Find (
                t => t.ID == custom.ID
            );

            if(transition == null) {
                transition = Instantiate (
                    original:   custom.gameObject,
                    position:   Vector3.zero,
                    rotation:   Quaternion.identity,
                    parent:     transform
                ).GetComponent<Transition>();
                transition.gameObject.name = transition.ID;
                _usedTransitions.Add (transition);
            }

            StartCoroutine (Load(scene, transition));
        }

        //  Coroutine to manipulate the transition.
        private IEnumerator Load (SceneReference scene, Transition transition) {
            transition.gameObject.SetActive (true);
            yield return StartCoroutine (InCoroutine(transition));

            AsyncOperation async = SceneManager.LoadSceneAsync (
                sceneName: scene,
                mode: LoadSceneMode.Single
            );
            yield return StartCoroutine (LoadingCoroutine(transition, async));

            yield return StartCoroutine (OutCoroutine(transition));
            transition.gameObject.SetActive (false);
        }

        private IEnumerator InCoroutine (Transition transition) {
            float time = 0;
            if (transition.InDuration > 0) {
                while (time < transition.InDuration) {
                    if (In != null)
                        In.Invoke(time / transition.InDuration);
                    time += Time.deltaTime;
                    yield return null;
                }
                if (In != null)
                    In.Invoke (1);
            }
        }

        private IEnumerator OutCoroutine (Transition transition) {
            float time = 0;
            if (transition.OutDuration > 0) {
                while (time < transition.OutDuration) {
                    if (Out != null)
                        Out.Invoke (time / transition.OutDuration);
                    time += Time.deltaTime;
                    yield return null;
                }
                if (Out != null)
                    Out.Invoke (0);
            }
        }

        private IEnumerator LoadingCoroutine (Transition transition, AsyncOperation async) {
            float time = 0;
            while (!async.isDone || time < transition.MinLoadingTime) {

                float fakeProgress = transition.MinLoadingTime > 0 ? time / transition.MinLoadingTime : 1;

                if(Loading != null)
                    Loading (async.progress < fakeProgress ? async.progress : fakeProgress);

                time = time + Time.deltaTime < transition.MinLoadingTime ? time + Time.deltaTime : transition.MinLoadingTime;

                yield return null;
            }
        }

        #endregion
    }
}