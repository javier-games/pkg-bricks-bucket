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
        private List<Transition> usedTransitions;

        #endregion



        #region MonoBehaviour Overrides

        //  Called on awake.
        private new void Awake () {
            base.Awake ();
            DontDestroyOnLoad (gameObject);
            usedTransitions = new List<Transition> ();
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

            usedTransitions.ForEach (t => t.gameObject.SetActive (false));
            Transition transition = usedTransitions.Find (
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
                usedTransitions.Add (transition);
            }
            else
                transition.gameObject.SetActive (true);

            StartCoroutine (Load(scene, transition));
        }

        //  Coroutine to manipulate the transition.
        private IEnumerator Load (SceneReference scene, Transition transition) {
            yield return StartCoroutine (transition.In());

            AsyncOperation async = SceneManager.LoadSceneAsync (
                sceneName: scene,
                mode: LoadSceneMode.Single
            );
            yield return StartCoroutine (transition.Loading(async));

            yield return StartCoroutine (transition.Out());

            transition.gameObject.SetActive (false);
        }

        #endregion
    }
}