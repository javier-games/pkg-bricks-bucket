using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

using Action = System.Action;

/// <summary>
/// 
/// Abstract Showable.
/// 
/// <para>
/// Behaviour for UI elements with the hability of show up and hidding.
/// </para>
/// 
/// <para> By Javier García | @jvrgms | 2019 </para>
/// 
/// </summary>
[ExecuteAlways, SelectionBase, DisallowMultipleComponent]
public abstract class AbstractShowable : UIBehaviour, IShowable
{
    #region Class Members

    /// <summary> Parent to whom this container belongs. </summary>
    [SerializeField]
    private AbstractSection _parent;

    /// <summary> Settings for showable animation. </summary>
    [SerializeField]
    private ShowableSettings _settings;

    /// <summary> Flag wether to use scaled time. </summary>
    [SerializeField]
    private bool _useScaledTime;

    /// <summary> Wether to ignore the show sequence of the parent. </summary>
    [SerializeField]
    private bool _ignoreParent;

    /// <summary> Wether override sequence parents delay. </summary>
    [SerializeField]
    private bool _overrideDelay;

    /// <summary> Override to sequence parents delay. </summary>
    [SerializeField]
    private float _delay;


    /// <summary> Wether the container is visible. </summary>
    protected bool _visible;

    /// <summary> Current transition. </summary>
    protected float _transition;

    /// <summary> Wrapper for the float tween animation. </summary>
    protected Tween _tween;

    /// <summary> Current state of animation. </summary>
    protected ShowableState _showableState;

    #endregion



    #region Accessors

    /// <summary> Gets the parent to whom this container belongs. </summary>
    public AbstractSection Parent
    {
        get { return _parent; }
        protected set { _parent = value; }
    }

    public ShowableSettings Settings
    {
        get { return _settings; }
        protected set { _settings = value; }
    }

    /// <summary> Parameters for show state. </summary>
    public TransformShowableParameters ShowParameters
    {
        get
        {
            return Settings ?
                Settings.Show :
                TransformShowableParameters.Zero;
        }
    }

    /// <summary> Parameters for hide state. </summary>
    public TransformShowableParameters HideParameters
    {
        get
        {
            return Settings ?
                Settings.Hide :
                TransformShowableParameters.Zero;
        }
    }

    /// <summary> Wether or not the Container is visible. </summary>
    public bool Visible
    {
        get { return _visible; }
    }

    /// <summary> Returns the current transition duration. </summary>
    public virtual float TransitionDuration
    {
        get
        {
            switch (_showableState)
            {
                case ShowableState.Showing:
                return ShowParameters.duration;
                case ShowableState.Hiding:
                return HideParameters.duration;
                default: return 0;
            }
        }
    }

    /// <summary> Returns the current transition value. </summary>
    public float Transition
    {
        get { return _transition; }
    }

    /// <summary> Gets and Sets wether to use scaled time. </summary>
    public bool UseScaledTime
    {
        get
        {
            return Settings ?
                _useScaledTime || Settings.UseScaledTime:
                _useScaledTime;
        }
        set { _useScaledTime = value; }
    }

    /// <summary> Wether to ignore the show sequence of the parent. </summary>
    public bool IgnoreParent
    {
        get
        {
            return Settings ?
                Settings.IgnoreParent || _ignoreParent:
                _ignoreParent;
        }
        set { _ignoreParent = value; }
    }

    /// <summary> Gets and sets the flag to override delay. </summary>
    public bool OverrideDelay
    {
        get
        {
            return Settings ?
                _overrideDelay || Settings.OverrideDelay:
                _overrideDelay;
        }
        set { _overrideDelay = value; }
    }

    /// <summary> Returns the delay of this showable. </summary>
    public float Delay
    {
        get
        {
            if (_overrideDelay)
                return _delay;

            if (Settings != null && Settings.OverrideDelay)
                return Settings.Delay;

            if (Parent)
                return Parent.SequenceDelay;

            return 0;

        }
        set { _delay = value; }
    }

    /// <summary> Delegate called on begin show. </summary>
    public Action OnBeginShow { get; set; }

    /// <summary> Delegate called on end show. </summary>
    public Action OnEndShow { get; set; }

    /// <summary> Delegate called on begin hide. </summary>
    public Action OnBeginHide { get; set; }

    /// <summary> Delegate called on end hide. </summary>
    public Action OnEndHide { get; set; }


    #endregion



    #region UIBehaviour Overrides

    /// <summary> Called on Awake. </summary>
    protected override void Awake ()
    {
        base.Awake ();

        //  Initializing parameters.
        _visible = false;
        _transition = 0;
        _showableState = ShowableState.None;
    }


    /// <summary> Called on enable. </summary>
    protected override void OnEnable ()
    {
        base.OnEnable ();

        //  Start as Show Up.
        Show ();
    }

    /// <summary> Called on disable. </summary>
    protected override void OnDisable ()
    {
        base.OnDisable ();

        _visible = false;
        _transition = 0;
    }

    /// <summary> Called when the parant transform has changed. </summary>
    protected override void OnTransformParentChanged ()
    {
        base.OnTransformParentChanged ();

        FindParent ();
    }

    /// <summary> Called when the canvas hierarchy has changed. </summary>
    protected override void OnCanvasHierarchyChanged ()
    {
        base.OnCanvasHierarchyChanged ();

        FindParent ();
    }

#if UNITY_EDITOR
    /// <summary> Called on save to validate. </summary>
    protected override void OnValidate ()
    {
        base.OnValidate ();

        //  Validate parent.
        FindParent ();
    }

    /// <summary> Called on reset. </summary>
    protected override void Reset ()
    {
        base.Reset ();

        FindParent ();
    }


#endif

    #endregion



    #region Class Implementation

    /// <summary> Show up the container. </summary>
    public virtual void Show ()
    {

        //  If the game object is not visible.
        if (!_visible)
        {
            _visible = true;
            _showableState = ShowableState.Showing;

            //  Active game object if it is hidded.
            if (!gameObject.activeSelf)
                gameObject.SetActive (true);


#if UNITY_EDITOR
            //  Show without teening.
            if (!Application.isPlaying)
            {
                OnTransitionUptade (1);
                return;
            }
#endif

            //  Call to subscribers.
            BeginShow ();
        }
    }

    /// <summary> Initializes the show animation. </summary>
    protected virtual void BeginShow ()
    {
        OnBeginShow?.Invoke ();

        //  Clean tween and reseting transition.
        if (_tween != null)
        {
            _tween.Kill ();
            _tween = null;
        }
        else
            _transition = 0;

        OnTransitionUptade (0);

        // Start Tweening.
        _tween = DOTween.To (
            getter: () => _transition,
            setter: x => _transition = x,
            endValue: 1,
            duration: TransitionDuration
        ).Pause ();
        _tween.OnUpdate (() => OnTransitionUptade (_transition));
        _tween.OnComplete (EndShow);
        _tween.SetUpdate (!UseScaledTime);
        _tween.Play ();
    }

    /// <summary> Called On End Show </summary>
    protected virtual void EndShow ()
    {
        _showableState = ShowableState.Visible;
        _tween.Kill ();
        _tween = null;
        OnEndShow?.Invoke ();
    }

    /// <summary> Hides the container. </summary>
    public virtual void Hide ()
    {
        //  If the game object is visible.
        if (_visible)
        {
            _visible = false;
            _showableState = ShowableState.Hiding;


#if UNITY_EDITOR
            //  Show without teening.
            if (!Application.isPlaying)
            {
                OnTransitionUptade (0);
                gameObject.SetActive (false);
                return;
            }
#endif

            //  Call to subscribers.
            BeginHide ();
        }
    }

    /// <summary> Called On Begin Hide </summary>
    protected virtual void BeginHide ()
    {
        OnBeginHide?.Invoke ();

        //  Clean tween and reseting transition.
        if (_tween != null)
        {
            _tween.Kill ();
            _tween = null;
        }
        else
            _transition = 1;

        // Start Tweening.
        _tween = DOTween.To (
            getter: () => _transition,
            setter: x => _transition = x,
            endValue: 0,
            duration: TransitionDuration
        ).Pause ();
        _tween.OnUpdate (() => OnTransitionUptade (_transition));
        _tween.OnComplete (EndHide);
        _tween.SetUpdate (!UseScaledTime);
        _tween.Play ();
    }


    /// <summary> Called On End Hide </summary>
    protected virtual void EndHide ()
    {
        
        OnTransitionUptade (0);
        _showableState = ShowableState.Disabled;

        _tween.Kill ();
        _tween = null;

        OnEndHide?.Invoke ();
        gameObject.SetActive (false);
    }

    /// <summary> Forces this showable to hide instatly.. </summary>
    public virtual void ForceHide ()
    {
        _tween.Kill ();
        _tween = null;
        _transition = 0;
        _visible = false;
        gameObject.SetActive (false);
        _showableState = ShowableState.Disabled;
    }

    /// <summary> Called On Transition Update. </summary>
    protected virtual void OnTransitionUptade (float t) { }

    /// <summary> Returns a parent container if any exists.  </summary>
    /// <returns> Parent container. Null if any is found. </returns>
    public void FindParent ()
    {
        //  Return inmediatelly if nothing has changed.
        if(Parent != null && Parent.transform == transform.parent)
            return;
        if (Parent == null && transform.parent == null)
            return;

        //  Find the nearest container. If no one is found fixit.
        AbstractSection parent = GetComponentInParent<AbstractSection> ();
        if (parent == null)
        {
            if (Parent != null)
            {
                ReplaceParent (null);
                return;
            }
            return;
        }

        //  If the parent found is this then found vía transform.parent.
        if (parent.transform == transform)
        {
            if (transform.parent == null)
            {
                ReplaceParent (null);
                return;
            }

            parent =
                transform.parent.GetComponentInParent<AbstractSection> ();
        }

        ReplaceParent (parent);
    }

    /// <summary> Replaces the parent. </summary>
    /// <param name="parent"></param>
    private void ReplaceParent (AbstractSection parent)
    {
        if (Parent == parent)
            return;

        if (Parent != null)
            Parent.TryRemoveChild (this);

        Parent = null;
        Parent = parent;

        if (Parent != null)
            Parent.TryAddChild (this);
    }

    #endregion
}