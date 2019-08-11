/// <summary> Enumeration of possible transitions. </summary>
public enum AnimatableTransition
{
    /// <summary> No Transition. </summary>
    None,

    /// <summary> Use a color tint transition. </summary>
    ColorTint,

    /// <summary> Use an animator component. </summary>
    Animation,

    /// <summary> Use swap sprite to animate. </summary>
    SpriteSwap
}

/// <summary> Enumeration for states of animatable. </summary>
public enum AnimatableState
{
    /// <summary> The UI object can be selected. </summary>
    Idle = 0,

    /// <summary> The game object has been enable. </summary>
    Enable = 1,

    /// <summary> The UI object call user to action. </summary>
    CallToAction = 2,

    /// <summary> The UI object is highlighted. </summary>
    Highlighted = 3,

    /// <summary> The UI object is pressed. </summary>
    Pressed = 4,

    /// <summary> The UI object remains selected </summary>
    Selected = 5,

    /// <summary> The UI object is being dragged. </summary>
    Dragging = 6,

    /// <summary> The game object is going to be disable. </summary>
    Disable = 7,
}

/// <summary> Enumeration of possible transitions. </summary>
public enum TransitionType
{
    /// <summary> No Transition. </summary>
    None,

    /// <summary> Use a color tint transition. </summary>
    ColorTint,

    /// <summary> Use an animator component. </summary>
    Animation,

    /// <summary> Use swap sprite to animate. </summary>
    SpriteSwap
}

/// <summary> Defines the types of properties in a transform. </summary>
public enum AnimatableProperty
{
    Position,
    Rotation,
    Scale,
    Alpha
}

public enum ShowableType
{
    Showable,
    Animatable,
    Section,
    Panel,
    Modal,
}


[System.Serializable]
public enum AutoShowChildrenType
{
    None,
    OnBeginShow,
    OnEndShow
}

[System.Serializable]
public enum AutoHideChildrenType
{
    None,
    OnBeginHide,
    OnEndHide
}

[System.Serializable]
public enum SequenceType
{
    None,
    Yoyo,
    Incremental
}

/// <summary> State types for animation. </summary>
public enum ShowableState
{
    None,       //  State for no current animation.
    Showing,    //  State for transition to show.
    Visible,    //  Stete when the game object is active an visible.
    Hiding,      //  State for transition to hide.
    Disabled,    //  Stete when the game object is active an visible.
}

/// <summary> Enumeration of types of ease animations. </summary>
public enum EaseType

{
    None,
    Zero,
    One,
    Linear,

    QuadOut,
    QuadIn,
    QuadInOut,
    QuadOutIn,

    ExpoOut,
    ExpoIn,
    ExpoInOut,
    ExpoOutIn,

    CubicOut,
    CubicIn,
    CubicInOut,
    CubicOutIn,

    QuartOut,
    QuartIn,
    QuartInOut,
    QuartOutIn,

    QuintOut,
    QuintIn,
    QuintInOut,
    QuintOutIn,

    CircOut,
    CircIn,
    CircInOut,
    CircOutIn,

    SineOut,
    SineIn,
    SineInOut,
    SineOutIn,

    ElasticOut,
    ElasticIn,
    ElasticInOut,
    ElasticOutIn,

    BounceOut,
    BounceIn,
    BounceInOut,
    BounceOutIn,

    BackOut,
    BackIn,
    BackInOut,
    BackOutIn,

    Custom
}
