using UnityEngine;

namespace Framework.Utils {

    /// <summary>
    /// Fade alpha transition.
    /// By Javier García, 2019.
    /// 
    /// <para>
    /// Example of how to use the class Transition by modifying the Alpha and
    /// blockRaycasts properties from a CanavasProup.
    /// </para>
    /// </summary>
    [RequireComponent (typeof (CanvasGroup))]
    public class FadeAlphaTransition: Transition {

        CanvasGroup _group;

        //  Called on Awake. Transition inherits from Monobehaviour.
        protected override void Awake () {
            base.Awake ();
            _group = GetComponent<CanvasGroup> ();
        }

        // Called from Transition inhered coroutines, TransitionIn,
        // TransitionLoading and TransitionOut are the places where you can
        // add animations according the percentage of InDuration, OutDuration
        // and the Loading Progress with values from 0 to 1.

        #region Override Methods

        /// <summary>
        /// Transitions In.
        /// </summary>
        /// <param name="t">Transition.</param>
        protected override void In (float t) {
            _group.blocksRaycasts = t <= 0;
            _group.alpha = t;
        }

        /// <summary>
        /// Transitions Out.
        /// </summary>
        /// <param name="t">Transition.</param>
        protected override void Out (float t) {
            _group.alpha = 1 - t;
            _group.blocksRaycasts = t >= 1;
        }

        #endregion
    }
}
