using UnityEngine;
using Enum = System.Enum;

[System.Serializable]
[CreateAssetMenu (fileName = "ShowableSettings", menuName = "ScriptableObjects/Showable Settings", order = 1)]
public class ShowableSettings : ScriptableObject
{

    #region Class Members

    [SerializeField]
    private ShowableType _showableType;


    #region Showable

    /// <summary> Flag wether to use scaled time. </summary>
    [SerializeField]
    private bool _useScaledTime;

    /// <summary> Wether to ignore the show sequence of the parent. </summary>
    [SerializeField]
    private bool _ignoreParent;

    /// <summary> Wether to override delay to apply. </summary>
    [SerializeField]
    private bool _overrideDelay;

    [SerializeField]
    private float _delay = 0.25f;

    /// <summary> Properties to animate. </summary>
    [SerializeField]
    protected int _animatableProperties;


    #region Show Parameters

    /// <summary> Duration of transition. </summary>
    [SerializeField, Range (0f, 5f)]
    private float _showDuration = 0.25f;

    /// <summary> Type of transition behaviour. </summary>
    [SerializeField]
    private EaseType _showEaseType;

    /// <summary> Animation curve of transition behaviour. </summary>
    [SerializeField]
    private AnimationCurve _showAnimation;

    /// <summary> Offset for transform's position. </summary>
    [SerializeField]
    private Vector3 _showOffsetPosition = Vector3.zero;

    /// <summary> Offset for transform's rotation. </summary>
    [SerializeField]
    private Vector3 _showOffsetRosition = Vector3.zero;

    /// <summary> Offset for transform's scale. </summary>
    [SerializeField]
    private Vector3 _showScale = Vector3.one;

    /// <summary> Mask for parameters to override animation. </summary>
    [SerializeField]
    private int _showOverrideAnimation;

    /// <summary> Override curves animation for position. </summary>
    [SerializeField]
    private AnimationCurve3 _showOverridePositionAnimation;

    /// <summary> Override curves animation for position. </summary>
    [SerializeField]
    private AnimationCurve3 _showOverrideRotationAnimation;

    /// <summary> Override curves animation for position. </summary>
    [SerializeField]
    private AnimationCurve3 _showScaleAnimation;

    #endregion


    #region Show Parameters

    /// <summary> Duration of transition. </summary>
    [SerializeField, Range (0f, 5f)]
    private float _hideDuration = 0.25f;

    /// <summary> Type of transition behaviour. </summary>
    [SerializeField]
    private EaseType _hideEaseType;

    /// <summary> Animation curve of transition behaviour. </summary>
    [SerializeField]
    private AnimationCurve _hideAnimation;

    /// <summary> Offset for transform's position. </summary>
    [SerializeField]
    private Vector3 _hideOffsetPosition = Vector3.zero;

    /// <summary> Offset for transform's rotation. </summary>
    [SerializeField]
    private Vector3 _hideOffsetRosition = Vector3.zero;

    /// <summary> Offset for transform's scale. </summary>
    [SerializeField]
    private Vector3 _hideScale;

    /// <summary> Mask for parameters to override animation. </summary>
    [SerializeField]
    private int _hideOverrideAnimation;

    /// <summary> Override curves animation for position. </summary>
    [SerializeField]
    private AnimationCurve3 _hideOverridePositionAnimation;

    /// <summary> Override curves animation for rotation. </summary>
    [SerializeField]
    private AnimationCurve3 _hideOverrideRotationAnimation;

    /// <summary> Override curves animation for scale. </summary>
    [SerializeField]
    private AnimationCurve3 _hideScaleAnimation;

    #endregion

    #endregion


    #region Section

    /// <summary> Automatic show settings. </summary>
    [SerializeField]
    protected AutoShowChildrenType _autoShowChildren = AutoShowChildrenType.None;

    /// <summary> Automatic hide settings. </summary>
    [SerializeField]
    protected AutoHideChildrenType _autoHideChildren = AutoHideChildrenType.None;

    /// <summary> Wether to use sequence. </summary>
    [SerializeField]
    protected bool _useSequence;

    /// <summary> Sequence to show children. </summary>
    [SerializeField]
    private SequenceType _childrenSequence = SequenceType.Incremental;

    /// <summary> Range of time of the delay between children. </summary>
    [SerializeField, Range (0f, 3f)]
    protected float _sequenceDelay = 0.25f;

    /// <summary> Show alpha parameters. </summary>
    [SerializeField, Range (0f, 1f)]
    private float _showAlpha = 1;

    /// <summary> Hide alpha parameters. </summary>
    [SerializeField, Range (0f, 1f)]
    private float _hideAlpha = 1;

    /// <summary> Override curves animation for alpha. </summary>
    [SerializeField]
    private AnimationCurve _showAlphaAnimation;

    /// <summary> Override curves animation for alpha. </summary>
    [SerializeField]
    private AnimationCurve _hideAlphaAnimation;

    #endregion

    #endregion



    #region Accessors

    /// <summary> Type of showable. </summary>
    public ShowableType ShowableType
    {
        get { return _showableType; }
        set { _showableType = value; }
    }

    /// <summary> Parameters of show animation. </summary>
    public TransformShowableParameters Show
    {
        get {
            return new TransformShowableParameters
            {
                duration = _showDuration,
                ease = _showEaseType,
                animation = _showAnimation,
                offsetPosition = _showOffsetPosition,
                offsetRosition = _showOffsetRosition,
                scale = _showScale,
                alpha = _showAlpha,
                overrideAnimation = _showOverrideAnimation,
                overridePositionAnimation = _showOverridePositionAnimation,
                overrideRotationAnimation = _showOverrideRotationAnimation,
                overrideScaleAnimation = _showScaleAnimation,
                overrideAlphaAnimation = _showAlphaAnimation
            };
        }
    }

    /// <summary> Parameters of hide animation. </summary>
    public TransformShowableParameters Hide
    {
        get
        {
            return new TransformShowableParameters
            {
                duration = _hideDuration,
                ease = _hideEaseType,
                animation = _hideAnimation,
                offsetPosition = _hideOffsetPosition,
                offsetRosition = _hideOffsetRosition,
                scale = _hideScale,
                alpha = _hideAlpha,
                overrideAnimation = _hideOverrideAnimation,
                overridePositionAnimation = _hideOverridePositionAnimation,
                overrideRotationAnimation = _hideOverrideRotationAnimation,
                overrideScaleAnimation = _hideScaleAnimation,
                overrideAlphaAnimation = _hideAlphaAnimation
            };
        }
    }

    /// <summary> Wether to ignore the show sequence of the parent. </summary>
    public bool IgnoreParent
    {
        get { return _ignoreParent; }
        set { _ignoreParent = value; }
    }

    /// <summary> Wether to use scaled time. </summary>
    public bool UseScaledTime
    {
        get { return _useScaledTime; }
        set { _useScaledTime = value; }
    }

    /// <summary> Wether to override delay. </summary>
    public bool OverrideDelay
    {
        get { return _overrideDelay; }
        set { _overrideDelay = value; }
    }

    /// <summary> Overrided delay to use. </summary>
    public float Delay
    {
        get { return _delay; }
        set { _delay = value; }
    }

    /// <summary> Properties to animate. </summary>
    public int AnimatableProperties
    {
        get { return _animatableProperties; }
        set { _animatableProperties = value; }
    }

    /// <summary> Wether or not to animate position. </summary>
    public bool AnimatePosition
    {
        get
        {
            return _animatableProperties.MaskHasLayer (
                AnimatableProperty.Position
            );
        }
    }

    /// <summary> Wether or not to animate rotation. </summary>
    public bool AnimateRotation
    {
        get
        {
            return _animatableProperties.MaskHasLayer (
                AnimatableProperty.Rotation
            );
        }
    }

    /// <summary> Wether or not to animate Scale. </summary>
    public bool AnimateScale
    {
        get
        {
            return _animatableProperties.MaskHasLayer (
                AnimatableProperty.Scale
            );
        }
    }

    /// <summary> Automatic show settings. </summary>
    public AutoShowChildrenType AutoShowChildren
    {
        get { return _autoShowChildren; }
        set { _autoShowChildren = value; }
    }

    /// <summary> Automatic show settings. </summary>
    public AutoHideChildrenType AutoHideChildren
    {
        get { return _autoHideChildren; }
        set { _autoHideChildren = value; }
    }

    /// <summary> Wether to use Sequentce to animate. </summary>
    public bool UseSequence
    {
        get { return _useSequence; }
        set { _useSequence = value; }
    }

    /// <summary> Range of time of the delay between children. </summary>
    public float SequenceDelay
    {
        get { return _sequenceDelay; }
        set { _sequenceDelay = value; }
    }

    /// <summary> Sequence to show children. </summary>
    public SequenceType ChildrenSequence
    {
        get { return _childrenSequence; }
        set { _childrenSequence = value; }
    }

    #endregion
}


[System.Serializable]
public struct TransformShowableParameters
{
    #region Struct Parameters

    /// <summary> Duration of transition. </summary>
    [Range (0f, 5f)]
    public float duration;

    /// <summary> Type of transition behaviour. </summary>
    [SerializeField]
    public EaseType ease;

    /// <summary> Animation curve of transition behaviour. </summary>
    [SerializeField]
    public AnimationCurve animation;

    /// <summary> Offset for transform's position. </summary>
    public Vector3 offsetPosition;

    /// <summary> Offset for transform's rotation. </summary>
    public Vector3 offsetRosition;

    /// <summary> Transform's scale. </summary>
    public Vector3 scale;

    /// <summary> Canvas Group's alpha. </summary>
    public float alpha;

    /// <summary> Mask for parameters to override animation. </summary>
    public int overrideAnimation;

    /// <summary> Override curves animation for position. </summary>
    public AnimationCurve3 overridePositionAnimation;

    /// <summary> Override curves animation for rotation. </summary>
    public AnimationCurve3 overrideRotationAnimation;

    /// <summary> Override curves animation for scale. </summary>
    public AnimationCurve3 overrideScaleAnimation;

    /// <summary> Override curve animation for alpha. </summary>
    public AnimationCurve overrideAlphaAnimation;

    #endregion



    #region Accessors

    /// <summary> Zero preset to use on absence. </summary>
    public static TransformShowableParameters Zero
    {
        get
        {
            return new TransformShowableParameters
            {
                duration = 0,
                ease = EaseType.One,
                animation = EaseAnimationCurves.One,
                offsetPosition = Vector3.zero,
                offsetRosition = Vector3.zero,
                scale = Vector3.one,
                alpha = 1,
                overrideAnimation = 0,
                overridePositionAnimation = AnimationCurve3.One,
                overrideRotationAnimation = AnimationCurve3.One,
                overrideScaleAnimation = AnimationCurve3.One,
                overrideAlphaAnimation = EaseAnimationCurves.One
            };
        }
    }

    /// <summary> Wether to select position animation curves. </summary>
    public bool OverridePositionAnimation
    {
        get
        {
            return overrideAnimation.MaskHasLayer (AnimatableProperty.Position);
        }
    }

    /// <summary> Wether to select rotation animation curves. </summary>
    public bool OverrideRotationAnimation
    {
        get
        {
            return overrideAnimation.MaskHasLayer (AnimatableProperty.Rotation);
        }
    }

    /// <summary> Wether to select scale animation curves. </summary>
    public bool OverrideScaleAnimation
    {
        get
        {
            return overrideAnimation.MaskHasLayer (AnimatableProperty.Scale);
        }
    }

    /// <summary> Wether to select scale animation curves. </summary>
    public bool OverrideAlphaAnimation
    {
        get
        {
            return overrideAnimation.MaskHasLayer (AnimatableProperty.Alpha);
        }
    }

    #endregion


}


[System.Serializable]
public struct AnimationCurve3
{
    #region Class Member

    /// <summary> Curve X. </summary>
    public AnimationCurve x;

    /// <summary> Curve Y. </summary>
    public AnimationCurve y;

    /// <summary> Curve Z. </summary>
    public AnimationCurve z;

    #endregion



    #region Accessors

    /// <summary> Curve Zero Constant Behabour. </summary>
    public static AnimationCurve3 Zero
    {
        get
        {
            return new AnimationCurve3
            {
                x = EaseAnimationCurves.Zero,
                y = EaseAnimationCurves.Zero,
                z = EaseAnimationCurves.Zero
            };
        }
    }

    /// <summary> Curve One Constant Behabour. </summary>
    public static AnimationCurve3 One
    {
        get
        {
            return new AnimationCurve3
            {
                x = EaseAnimationCurves.Zero,
                y = EaseAnimationCurves.Zero,
                z = EaseAnimationCurves.Zero
            };
        }
    }

    /// <summary> Linear Curve Behabour. </summary>
    public static AnimationCurve3 Linear
    {
        get
        {
            return new AnimationCurve3
            {
                x = EaseAnimationCurves.Linear,
                y = EaseAnimationCurves.Linear,
                z = EaseAnimationCurves.Linear
            };
        }
    }

    #endregion





    #region Struct Implementation

    /// <summary> Returns a vector with the evaluation of each curve. </summary>
    /// <param name="time"> Time to evaluate. </param>
    /// <returns> Vector with the evaluation of each curve. </returns>
    public Vector3 Evaluate (float time)
    {
        return new Vector3
        {
            x = x != null ? x.Evaluate (time) : 0,
            y = y != null ? y.Evaluate (time) : 0,
            z = z != null ? z.Evaluate (time) : 0
        };
    }

    #endregion
}


public static class MissingUtils
{
    /// <summary> Add as layer to this numbers an int value. </summary>
    /// <param name="mask"> Int value as mask. </param>
    /// <param name="layer"> Int value as Layer. </param>
    public static void AddLayerToMask (this ref int mask, Enum layer)
    {
        mask.AddLayerToMask (GetIntFromEnum (layer));
    }

    /// <summary> Add as layer to this numbers an int value. </summary>
    /// <param name="mask"> Int value as mask. </param>
    /// <param name="layer"> Int value as Layer. </param>
    public static void AddLayerToMask (this ref int mask, int layer)
    {
        mask |= 1 << layer;
    }

    /// <summary> Wether an int value layer is in mask. </summary>
    /// <param name="mask"> Int value as mask. </param>
    /// <param name="layer"> Int value as Layer. </param>
    public static bool MaskHasLayer (this int mask, Enum layer)
    {
        return mask.MaskHasLayer (GetIntFromEnum (layer));
    }

    /// <summary> Wether an int value layer is in mask. </summary>
    /// <param name="mask"> Int value as mask. </param>
    /// <param name="layer"> Int value as Layer. </param>
    public static bool MaskHasLayer (this int mask, int layer)
    {
        return (mask & (1 << layer)) > 0;
    }

    /// <summary> Removes an int layer reference from mask. </summary>
    /// <param name="mask"> Int value as mask. </param>
    /// <param name="layer"> Int value as Layer. </param>
    public static void RemoveLayerFromMask (this ref int mask, Enum layer)
    {
        mask.RemoveLayerFromMask (GetIntFromEnum (layer));
    }

    /// <summary> Removes an int layer reference from mask. </summary>
    /// <param name="mask"> Int value as mask. </param>
    /// <param name="layer"> Int value as Layer. </param>
    public static void RemoveLayerFromMask (this ref int mask, int layer)
    {
        if (mask.MaskHasLayer (layer))
            mask &= ~(1 << layer);
    }

    /// <summary> Converts an Enum Constant to its int value. </summary>
    /// <param name="enum"> Enum Constant to convert</param>
    /// <returns> Returns int value of an Enum constant. </returns>
    public static int GetIntFromEnum (Enum @enum)
    {
        return (int) Enum.Parse (@enum.GetType (), @enum.ToString ());
    }

}

/// <summary>
/// 
/// Wait for Unscaled Seconds.
/// 
/// <para>
/// Suspends the coroutine execution for the given amount of seconds
/// without using scaled time.
/// </para>
/// 
/// <para> By Javier García | @jvrgms | 2019 </para>
/// 
/// </summary>
public class WaitForUnscaledSeconds : CustomYieldInstruction
{
    #region Class Members

    /// <summary> Time to wait. </summary>
    private readonly float _waitTime;

    #endregion

    #region Accessors

    /// <summary> Wether keep waiting or stops. </summary>
    public override bool keepWaiting
    {
        get { return Time.realtimeSinceStartup < _waitTime; }
    }

    #endregion

    #region Constructor

    /// <summary> Suspends the coroutine execution for the given
    /// amount of seconds without using scaled time. </summary>
    /// <param name="waitTime"> Time in seconds to wait. </param>
    public WaitForUnscaledSeconds (float waitTime)
    {
        _waitTime = Time.realtimeSinceStartup + waitTime;
    }

    #endregion
}