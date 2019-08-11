using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 
/// Animatable.
/// 
/// <para>
/// Behaviour for UI animatable objects.
/// </para>
/// 
/// <para> By Javier García | @jvrgms | 2019 </para>
/// 
/// </summary>
public class Animatable : AbstractShowable, IAnimatable
{

    #region Class Members

    /// <summary> Reference for interactability. </summary>
    [SerializeField]
    private Selectable _selectable;

    /// <summary> Reference to animate color or sprite. </summary>
    [SerializeField]
    private Graphic _targetGraphic;

    /// <summary> Reference to use animator. </summary>
    [SerializeField]
    private Animator _animator;


    /// <summary> Transition to use. </summary>
    [SerializeField]
    private AnimatableTransition _transitionType;

    /// <summary> Block of color to use as animation. </summary>
    [SerializeField]
    private AnimatableColorBlock _colors = AnimatableColorBlock.Default;

    /// <summary> Collection of sprites to use as animation. </summary>
    [SerializeField]
    private AnimatableSpriteState _sprites = new AnimatableSpriteState ();

    /// <summary> Name of Triggers to use with animator. </summary>
    [SerializeField]
    private AnimatableTriggerSet _triggers = AnimatableTriggerSet.Default;

    #endregion



    #region Accessors 

    /// <summary> gets or sets the selectable reference. </summary>
    public Selectable Selectable
    {
        get { return _selectable; }
        set
        {
            if (_selectable != value)
            {
                _selectable = value;
                OnDidApplyAnimationProperties ();
            }
        }
    }

    /// <summary> Gets or sets the image. </summary>
    public Image Image
    {
        get { return _targetGraphic as Image; }
        private set
        {
            if (_targetGraphic != value)
            {
                _targetGraphic = value;
                if (CurrentTransition == AnimatableTransition.SpriteSwap ||
                    CurrentTransition == AnimatableTransition.ColorTint
                )
                    OnDidApplyAnimationProperties ();
            }
        }
    }

    /// <summary> Animator atached to this animatable. </summary>
    public Animator Animator
    {
        get { return _animator; }
        private set
        {
            if (_animator != value)
            {
                _animator = value;
                if (CurrentTransition == AnimatableTransition.Animation)
                    OnDidApplyAnimationProperties ();
            }
        }
    }


    /// <summary> Wether this animatable is interactable. </summary>
    public bool IsInteractable
    {
        get
        {
            return _selectable == null || _selectable.interactable;
        }
    }

    /// <summary> Block of colors to animate. </summary>
    public AnimatableColorBlock Colors
    {
        get { return _colors; }
        private set { _colors = value; }
    }

    /// <summary> Block of sprites to animate. </summary>
    public AnimatableSpriteState Sprites
    {
        get { return _sprites; }
        private set { _sprites = value; }
    }

    /// <summary> Block of trigger nams to animate. </summary>
    public AnimatableTriggerSet Triggers
    {
        get { return _triggers; }
        private set { _triggers = value; }
    }

    /// <summary> Target Graphic to animate. </summary>
    public Graphic TargetGraphic
    {
        get { return _targetGraphic; }
        private set { _targetGraphic = value; }
    }

    /// <summary> Transition to use for animation. </summary>
    protected AnimatableTransition CurrentTransition
    {
        get { return _transitionType; }
        set
        {
            _transitionType = value;
            OnDidApplyAnimationProperties ();
        }
    }

    /// <summary> Current state of animatable. </summary>
    public AnimatableState CurrentAnimationState { get; protected set; }

    /// <summary> Wether the pointer is inside. </summary>
    public bool IsPointerInside { get; protected set; }

    /// <summary> Wether the pointer is down. </summary>
    public bool IsPointerDown { get; protected set; }

    /// <summary> Wether the event system has a selection. </summary>
    public bool HasSelection { get; protected set; }

    /// <summary> Wether the pointer is dragging. </summary>
    public bool IsDragging { get; protected set; }

    /// <summary> Wether is calling to action. </summary>
    public bool IsCallingToAction { get; protected set; }

    /// <summary> Returns the state according the states. </summary>
    private AnimatableState GetAnimationStateByEvents
    {
        get
        {
            if (!IsInteractable)
                return AnimatableState.Idle;

            if (IsDragging)
                return AnimatableState.Dragging;

            if (IsPointerDown)
                return AnimatableState.Pressed;

            if (HasSelection)
                return AnimatableState.Selected;

            if (IsPointerInside)
                return AnimatableState.Highlighted;

            if (IsCallingToAction)
                return AnimatableState.CallToAction;

            return AnimatableState.Idle;
        }
    }

    /// <summary> Gets the amount of time for the transition. </summary>
    public override float TransitionDuration
    {
        get
        {
            float transitionDuration = 0;

            switch (_transitionType)
            {
                case AnimatableTransition.Animation:
                return _animator.GetCurrentAnimatorStateInfo (0).length;
                case AnimatableTransition.ColorTint:
                return _colors.fadeDuration;
                case AnimatableTransition.SpriteSwap:
                return 0;
            }

            return transitionDuration;
        }
    }

    #endregion



    #region UIBehaviour Overrides

    /// <summary> Called on awake for initialization.  </summary>
    protected override void Awake ()
    {
        base.Awake ();

        //  Initialize missing references.
        if (_targetGraphic == null)
            _targetGraphic = GetComponent<Graphic> ();

        if (_animator == null)
            _animator = GetComponent<Animator> ();

        if (_selectable == null)
            _selectable = GetComponent<Selectable> ();

        if (Image != null)
            _sprites.idle = Image.sprite;
    }

    /// <summary> Called when properties have changed. </summary>
    protected override void OnDidApplyAnimationProperties ()
    {
        base.OnDidApplyAnimationProperties ();

        #if UNITY_EDITOR
        if (!Application.isPlaying)
            SetAnimationState (CurrentAnimationState);
        else
        #endif
            SetAnimationState (CurrentAnimationState);
    }

    #if UNITY_EDITOR
    /// <summary> Called from editor to validate. </summary>
    protected override void OnValidate ()
    {
        base.OnValidate ();
        // If the transition mode got changed, we need to clear all the
        // transitions, since we don't know what the old transition mode was.
        _colors.fadeDuration = Mathf.Max (_colors.fadeDuration, 0.0f);
        StartColorTween (Color.white);

        // And now go to the right state.
        SetAnimationState (AnimatableState.Idle);
    }

    /// <summary> Called from editor to reset or first set. </summary>
    protected override void Reset ()
    {
        base.Reset ();

        _targetGraphic = GetComponent<Graphic> ();
        _animator = GetComponent<Animator> ();
        _selectable = GetComponent<Selectable> ();

        if (Image != null)
            _sprites.idle = Image.sprite;
        // And now go to the right state.
        SetAnimationState (AnimatableState.Idle);
    }
    #endif

    #endregion



    #region Interface Implementation

    /// <summary> Called on pointer down. </summary>
    /// <param name="eventData"> Data of event. </param>
    public void OnPointerDown (PointerEventData eventData)
    {
        //  Evaluate current state and transition to pressed state.
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        // Selection tracking
        if (IsInteractable && EventSystem.current != null)
        {
            IsPointerDown = true;
            EvaluateState ();
        }
    }

    /// <summary> Called on pointer up. </summary>
    /// <param name="eventData"> Data of event. </param>
    public void OnPointerUp (PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        IsPointerDown = false;
        EvaluateState ();
    }

    /// <summary> Called on pointer enter. </summary>
    /// <param name="eventData"> Data of event. </param>
    public void OnPointerEnter (PointerEventData eventData)
    {
        IsPointerInside = true;
        EvaluateState ();
    }

    /// <summary> Called on pointer exit. </summary>
    /// <param name="eventData"> Data of event. </param>
    public void OnPointerExit (PointerEventData eventData)
    {
        IsPointerInside = false;
        EvaluateState ();
    }

    /// <summary> Called on begin drag. </summary>
    /// <param name="eventData"> Data of event. </param>
    public void OnBeginDrag (PointerEventData eventData)
    {
        IsDragging = true;
        EvaluateState ();
    }

    /// <summary> Called on draw event. </summary>
    /// <param name="eventData"> Data of event. </param>
    public void OnDrag (PointerEventData eventData)
    {
    }

    /// <summary> Called on end dragg. </summary>
    /// <param name="eventData"> Data of event. </param>
    public void OnEndDrag (PointerEventData eventData)
    {
        IsDragging = false;
        EvaluateState ();
    }

    /// <summary> Called on select event. </summary>
    /// <param name="eventData"> Data of event. </param>
    public void OnSelect (BaseEventData eventData)
    {
        HasSelection = true;
        EvaluateState ();
    }

    /// <summary> Called on deselect event. </summary>
    /// <param name="eventData"> Data of event. </param>
    public void OnDeselect (BaseEventData eventData)
    {
        HasSelection = false;
        EvaluateState ();
    }

    #endregion


    #region Class Implementation

    /// <summary> Changes the animation state. </summary>
    /// <param name="state"> New state. </param>
    public void SetAnimationState (AnimatableState state)
    {
        if (!IsActive ())
            return;

        if (IsInteractable)
        {
            CurrentAnimationState = state;
            ApplyTransition (state);
        }
        else
        {
            CurrentAnimationState = AnimatableState.Idle;
            ApplyTransition (state);
        }
    }

    /// <summary> Changes animation to Call to Action. </summary>
    public void CallToAction ()
    {
        SetAnimationState (AnimatableState.CallToAction);
    }

    /// <summary> Changes animation to Disable. </summary>
    protected override void BeginShow ()
    {
        base.BeginShow ();

        #if UNITY_EDITOR
        if (!Application.isPlaying)
            SetAnimationState (AnimatableState.Enable);
        #endif

        if (CurrentTransition == AnimatableTransition.ColorTint)
        {
            _targetGraphic.CrossFadeColor (_colors.enable, 0, true, true);
            StartCoroutine (SetAnimationStateCoroutine (
                _colors.fadeDuration,
                AnimatableState.Idle
            ));
        }
        else
        {
            SetAnimationState (AnimatableState.Enable);
        }

    }

    private IEnumerator
    SetAnimationStateCoroutine (float delay, AnimatableState state)
    {
        #if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            float currentTime = 0;
            while (currentTime < delay)
            {
                yield return null;
                currentTime += Time.deltaTime;
            }
        }
        else
        #endif
            yield return new WaitForSeconds (delay);
        SetAnimationState (state);
    }

    /// <summary> Changes animation to Disable. </summary>
    protected override void BeginHide ()
    {
        SetAnimationState (AnimatableState.Disable);
    }

    // Change the button to the correct state
    protected void EvaluateState ()
    {
        if (!IsActive ())
            return;

        CurrentAnimationState = GetAnimationStateByEvents;
        ApplyTransition (CurrentAnimationState);
    }

    /// <summary> Applies the animation tranisition. </summary>
    /// <param name="state"></param>
    protected virtual void ApplyTransition (AnimatableState state)
    {
        if (!gameObject.activeInHierarchy)
            return;

        Color tintColor = Color.white;
        string triggerName = string.Empty;
        Sprite sprite = null;

        switch (state)
        {
            case AnimatableState.Enable:
            tintColor = _colors.enable;
            triggerName = _triggers.enable;
            sprite = _sprites.enable;
            break;

            case AnimatableState.Idle:
            tintColor = _colors.idle;
            triggerName = _triggers.idle;
            sprite = _sprites.idle;
            break;

            case AnimatableState.CallToAction:
            tintColor = _colors.callToAction;
            triggerName = _triggers.callToAction;
            sprite = _sprites.callToAction;
            break;

            case AnimatableState.Highlighted:
            tintColor = _colors.highlighted;
            triggerName = _triggers.highlighted;
            sprite = _sprites.highlighted;
            break;

            case AnimatableState.Pressed:
            tintColor = _colors.pressed;
            triggerName = _triggers.pressed;
            sprite = _sprites.pressed;
            break;

            case AnimatableState.Selected:
            tintColor = _colors.selected;
            triggerName = _triggers.selected;
            sprite = _sprites.selected;
            break;

            case AnimatableState.Dragging:
            tintColor = _colors.dragging;
            triggerName = _triggers.dragging;
            sprite = _sprites.dragging;
            break;

            case AnimatableState.Disable:
            tintColor = _colors.disable;
            triggerName = _triggers.disable;
            sprite = _sprites.disable;
            break;
        }

        switch (CurrentTransition)
        {
            case AnimatableTransition.ColorTint:
            StartColorTween (tintColor * _colors.colorMultiplier);
            break;

            case AnimatableTransition.Animation:
            TriggerAnimation (triggerName);
            break;

            case AnimatableTransition.SpriteSwap:
            SpriteSwap (sprite);
            break;
        }


    }

    /// <summary> Applies color Transition. </summary>
    /// <param name="targetColor"></param>
    private void StartColorTween (Color targetColor)
    {
        if (_targetGraphic == null)
            return;
        _targetGraphic.CrossFadeColor (
            targetColor: targetColor,
            duration: _colors.fadeDuration,
            ignoreTimeScale: true,
            useAlpha: true
        );
    }

    /// <summary> Swaps the sprites. </summary>
    /// <param name="newSprite"></param>
    private void SpriteSwap (Sprite newSprite)
    {
        if (Image == null)
            return;

        Image.overrideSprite = newSprite;
    }

    /// <summary> Animate with the specified trigger. </summary>
    /// <param name="triggerName"> Name of the trigger. </param>
    private void TriggerAnimation (string triggerName)
    {
        bool mustReturn =
            CurrentTransition != AnimatableTransition.Animation ||
            Animator == null ||
            !Animator.isActiveAndEnabled ||
            !Animator.hasBoundPlayables ||
            string.IsNullOrEmpty (triggerName);

        if (mustReturn)
            return;

        Animator.ResetTrigger (_triggers.enable);
        Animator.ResetTrigger (_triggers.idle);
        Animator.ResetTrigger (_triggers.callToAction);
        Animator.ResetTrigger (_triggers.highlighted);
        Animator.ResetTrigger (_triggers.pressed);
        Animator.ResetTrigger (_triggers.selected);
        Animator.ResetTrigger (_triggers.dragging);
        Animator.ResetTrigger (_triggers.disable);
        Animator.SetTrigger (triggerName);
    }

    #endregion



    #region Nested Attributes

    /// <summary> Block of colors for animation. </summary>
    [System.Serializable]
    public struct AnimatableColorBlock
    {
        public Color idle;          //  Idle color.
        public Color enable;        //  Enable color.
        public Color callToAction;  //  Call to action color.
        public Color highlighted;   //  Highlighted color.
        public Color pressed;       //  Pressed color.
        public Color selected;      //  Selected color.
        public Color dragging;      //  Dragging color.
        public Color disable;       //  Diable color.

        [Range (1f, 5f)]
        public float colorMultiplier;   //  Color multiplier.
        public float fadeDuration;      //  Duration of animation.

        /// <summary> Default value of animatable color block. </summary>
        public static AnimatableColorBlock Default
        {
            get
            {
                return new AnimatableColorBlock
                {
                    enable = Color.white,
                    idle = Color.white,
                    callToAction = Color.white,
                    highlighted = Color.white,
                    pressed = Color.gray,
                    selected = Color.white,
                    dragging = Color.white,
                    disable = new Color (1, 1, 1, 0),
                    colorMultiplier = 1,
                    fadeDuration = 0.25f
                };
            }
        }
    }

    /// <summary> Set of tragger on animation. </summary>
    [System.Serializable]
    public struct AnimatableTriggerSet
    {
        public string idle;          //  Idle color.
        public string enable;        //  Enable color.
        public string callToAction;  //  Call to action color.
        public string highlighted;   //  Highlighted color.
        public string pressed;       //  Pressed color.
        public string selected;      //  Selected color.
        public string dragging;      //  Dragging color.
        public string disable;       //  Diable color.

        /// <summary> Default value of animatable triggers. </summary>
        public static AnimatableTriggerSet Default
        {
            get
            {
                return new AnimatableTriggerSet
                {
                    enable = "Enable",
                    idle = "Idle",
                    callToAction = "Call To Action",
                    highlighted = "Highlighted",
                    pressed = "Pressed",
                    selected = "Selected",
                    dragging = "Dragging",
                    disable = "Disable"
                };
            }
        }
    }

    /// <summary> Collection of sprites to set. </summary>
    [System.Serializable]
    public struct AnimatableSpriteState
    {
        public Sprite idle;          //  Idle Sprite.
        public Sprite enable;        //  Enable Sprite.
        public Sprite callToAction;  //  Call to action Sprite.
        public Sprite highlighted;   //  Highlighted Sprite.
        public Sprite pressed;       //  Pressed Sprite.
        public Sprite selected;      //  Selected Sprite.
        public Sprite dragging;      //  Dragging Sprite.
        public Sprite disable;       //  Diable Sprite.
    }

    #endregion
}


