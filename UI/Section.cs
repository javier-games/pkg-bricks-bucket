using UnityEngine;

public class Section : AbstractSection
{
    #region Abstract Section Overrides

    /// <summary> Called On Transition Update. </summary>
    /// <param name="t"> Time to transition. </param>
    protected override void OnTransitionUptade (float t)
    {

#if UNITY_EDITOR
        if (!Application.isPlaying)
            return;
#endif
        base.OnTransitionUptade (t);
        switch(_showableState)
        {
            case ShowableState.Showing:
            CanvasGroup.alpha = Mathf.Lerp (
                0,
                1,
                ShowParameters.OverrideAlphaAnimation ?
                ShowParameters.overrideAlphaAnimation.Evaluate (t) :
                ShowParameters.animation.Evaluate (t)
            );
            break;

            case ShowableState.Hiding:
            CanvasGroup.alpha = Mathf.Lerp (
                0,
                1,
                ShowParameters.OverrideAlphaAnimation ?
                HideParameters.overrideAlphaAnimation.Evaluate (t) :
                ShowParameters.animation.Evaluate (t)
            );
            break;
        }

    }


    #endregion
}
