using UnityEngine;

public class Showable : AbstractShowable
{
    #region Class Members

    /// <summary> Reference to the original position. </summary>
    private Vector3 _originalPosition;

    /// <summary> Reference to the original rotation. </summary>
    private Vector3 _originalRotation;

    #endregion



    #region Abstract Showable Overrides

    /// <summary> Called on awake to initialize variables. </summary>
    protected override void Awake ()
    {
        base.Awake ();
        _originalPosition = transform.localPosition;
        _originalRotation = transform.eulerAngles;
    }

    /// <summary> Called On Transition Update. </summary>
    /// <param name="t"> Time to transition. </param>
    protected override void OnTransitionUptade (float t)
    {

#if UNITY_EDITOR
        if (!Application.isPlaying)
            return;
#endif

        base.OnTransitionUptade (t);

        if (Settings == null)
            return;

        switch (_showableState)
        {
            case ShowableState.Showing:
            LerpTransform (ShowParameters, t);
            return;

            case ShowableState.Hiding:
            LerpTransform (HideParameters, t);
            return;
        }
    }

    /// <summary> Return the showable to its original position. </summary>
    protected override void OnDisable ()
    {
        #if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            base.OnDisable ();
            return;
        }
        #endif

        base.OnDisable ();
        transform.localPosition = _originalPosition;
        transform.eulerAngles = _originalRotation;
    }

    #endregion



    #region Class Implementation

    /// <summary> Lerps properties on a transform. </summary>
    /// <param name="param"> Animations parameters. </param>
    /// <param name="time"> Time for transition. </param>
    private void LerpTransform (TransformShowableParameters param, float time)
    {
        if (Settings == null)
            return;

        if (Settings.AnimatePosition)
        {
            if (param.OverridePositionAnimation)
                LerpPosition (param.overridePositionAnimation, time);
            else
                LerpPosition (param.animation, time);
        }

        if (Settings.AnimateRotation)
        {
            if (param.OverrideRotationAnimation)
                LerpRotation (param.overrideRotationAnimation, time);
            else
                LerpRotation (param.animation, time);
        }

        if (Settings.AnimateScale)
        {
            if (param.OverrideScaleAnimation)
                LerpScale (param.overrideScaleAnimation, time);
            else
                LerpScale (param.animation, time);
        }
    }

    /// <summary> Lerps the transform Position. </summary>
    /// <param name="curve"> Behaviour of animation. </param>
    /// <param name="time"> Time for transition. </param>
    private void LerpPosition (AnimationCurve curve, float time)
    {
        transform.localPosition = LerpVector (
            a: _originalPosition + HideParameters.offsetPosition,
            b: _originalPosition + ShowParameters.offsetPosition,
            curve: curve,
            time: time
        );
    }

    /// <summary> Lerps the transform Position. </summary>
    /// <param name="curve"> Behaviour of animation. </param>
    /// <param name="time"> Time for transition. </param>
    private void LerpPosition (AnimationCurve3 curve, float time)
    {
        transform.localPosition = LerpVector (
            a: _originalPosition + HideParameters.offsetPosition,
            b: _originalPosition + ShowParameters.offsetPosition,
            curve: curve,
            time: time
        );
    }

    /// <summary> Lerps the transform Rotation. </summary>
    /// <param name="curve"> Behaviour of animation. </param>
    /// <param name="time"> Time for transition. </param>
    private void LerpRotation (AnimationCurve curve, float time)
    {
        transform.eulerAngles = LerpVector (
            a: _originalRotation + HideParameters.offsetRosition,
            b: _originalRotation + ShowParameters.offsetRosition,
            curve: curve,
            time: time
        );
    }

    /// <summary> Lerps the transform Rotation. </summary>
    /// <param name="curve"> Behaviour of animation. </param>
    /// <param name="time"> Time for transition. </param>
    private void LerpRotation (AnimationCurve3 curve, float time)
    {
        transform.eulerAngles = LerpVector (
            a: _originalRotation + HideParameters.offsetRosition,
            b: _originalRotation + ShowParameters.offsetRosition,
            curve: curve,
            time: time
        );
    }

    /// <summary> Lerps the transform Scale. </summary>
    /// <param name="curve"> Behaviour of animation. </param>
    /// <param name="time"> Time for transition. </param>
    private void LerpScale (AnimationCurve curve, float time)
    {
        transform.localScale = LerpVector (
            a: HideParameters.scale,
            b: ShowParameters.scale,
            curve: curve,
            time: time
        );
    }

    /// <summary> Lerps the transform Scale. </summary>
    /// <param name="curve"> Behaviour of animation. </param>
    /// <param name="time"> Time for transition. </param>
    private void LerpScale (AnimationCurve3 curve, float time)
    {
        transform.localScale = LerpVector (
            a: HideParameters.scale,
            b: ShowParameters.scale,
            curve: curve,
            time: time
        );
    }

    /// <summary> Lerps vector a to vector b with the curve by t. </summary>
    /// <param name="a"> Vector from. </param>
    /// <param name="b"> Vector to. </param>
    /// <param name="curve"> Animation curve. </param>
    /// <param name="time"> Time for transition. </param>
    /// <returns> Lerped Vector. </returns>
    private Vector3
    LerpVector (Vector3 a, Vector3 b, AnimationCurve curve, float time)
    {
        return Vector3.LerpUnclamped (a, b, curve.Evaluate (time));
    }

    /// <summary> Lerps vector a to vector b with the curve vector. </summary>
    /// <param name="a"> Vector from. </param>
    /// <param name="b"> Vector to. </param>
    /// <param name="curve"> Animation curve. </param>
    /// <param name="time"> Time for transition. </param>
    /// <returns> Lerped Vector. </returns>
    private Vector3
    LerpVector (Vector3 a, Vector3 b, AnimationCurve3 curve, float time)
    {
        Vector3 evaluation = curve.Evaluate (time);
        return new Vector3
        {
            x = Mathf.LerpUnclamped (a.x, b.x, evaluation.x),
            y = Mathf.LerpUnclamped (a.y, b.y, evaluation.y),
            z = Mathf.LerpUnclamped (a.z, b.z, evaluation.z)
        };
    }

    #endregion
}
