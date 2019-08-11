using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
[RequireComponent (typeof (CanvasGroup))]
public abstract class AbstractSection : Showable, ISection
{


    #region Class Members

    /// <summary> Reference to manage interactivity and raycast. </summary>
    [SerializeField]
    private CanvasGroup _canvasGroup;



    /// <summary> Showable children of this container. </summary>
    [SerializeField]
    protected List<AbstractShowable> _children = new List<AbstractShowable> ();

    /// <summary> Automatic show settings. </summary>
    [SerializeField]
    private AutoShowChildrenType _autoShowChildren;

    /// <summary> Automatic hide settings. </summary>
    [SerializeField]
    private AutoHideChildrenType _autoHideChildren;



    /// <summary> Wether to use sequence. </summary>
    [SerializeField]
    private bool _useSequence;

    /// <summary> Sequence to show children. </summary>
    [SerializeField]
    private SequenceType _childrenSequence;

    /// <summary> Range of time of the delay between children. </summary>
    [SerializeField, Range (0f, 3f)]
    private float _sequenceDelay;

    /// <summary> Coroutine for hide or show coroutine. </summary>
    protected Coroutine _childrenCoroutine;

    /// <summary> Current index to show or hide. </summary>
    protected int _childIndex;

    #endregion



    #region Accessors

    /// <summary> Canvas group to edit. </summary>
    public CanvasGroup CanvasGroup
    {
        get { return _canvasGroup; }
        protected set { _canvasGroup = value; }
    }

    /// <summary> Automatic show settings. </summary>
    public AutoShowChildrenType AutoShowChildren
    {
        get
        {
            if (
                Settings != null &&
                _autoShowChildren == AutoShowChildrenType.None
            )
                return Settings.AutoShowChildren;
            return _autoShowChildren;
        }
        set { _autoShowChildren = value; }
    }

    /// <summary> Automatic show settings. </summary>
    public AutoHideChildrenType AutoHideChildren
    {
        get
        {
            if (
                Settings != null &&
                _autoHideChildren == AutoHideChildrenType.None
            )
                return Settings.AutoHideChildren;
            return _autoHideChildren;
        }
        set { _autoHideChildren = value; }
    }


    /// <summary> Gets the children of this container. </summary>
    public AbstractShowable[] ChildrenCollection
    {
        get { return _children.ToArray (); }
    }

    /// <summary> Wether to use Sequentce to animate. </summary>
    public bool UseSequence
    {
        get
        {
            return Settings ?
                Settings.UseSequence || _useSequence :
                _useSequence;
        }
        set { _useSequence = value; }
    }

    /// <summary> Range of time of the delay between children. </summary>
    public float SequenceDelay
    {
        get
        {
            if(_useSequence)
                return  _sequenceDelay;

            if (Settings && Settings.UseSequence)
                return Settings.SequenceDelay;

            return 0;
        }
        set { _sequenceDelay = value; }
    }

    /// <summary> Sequence to show children. </summary>
    public SequenceType ChildrenSequence
    {
        get
        {
            if (Settings != null && _childrenSequence == SequenceType.None)
                return Settings.ChildrenSequence;

            return _childrenSequence;
        }
        set { _childrenSequence = value; }
    }

    #endregion



    #region UIBehaviour Overrides

    /// <summary> Called on Awake. </summary>
    protected override void Awake ()
    {
        base.Awake ();

        //  Required components validation.
        if (CanvasGroup == null)
            CanvasGroup = GetComponent<CanvasGroup> ();

        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.interactable = false;
    }

#if UNITY_EDITOR

    /// <summary> Validate the parameters showed in inspector. </summary>
    protected override void OnValidate ()
    {
        base.OnValidate ();

        //  Validate Current Children.
        ValidateChildren ();
    }

    /// <summary> Called on reset. </summary>
    protected override void Reset ()
    {
        base.Reset ();

        CanvasGroup = GetComponent<CanvasGroup> ();
        if (CanvasGroup == null)
            CanvasGroup = gameObject.AddComponent<CanvasGroup> ();

        foreach (Transform childTranform in transform)
        {
            var child = childTranform.GetComponent<AbstractShowable> ();
            if (child != null && child.transform.parent == transform)
                child.FindParent ();
        }
    }
#endif


    #endregion



    #region Abstract Shoawable Overrides

    /// <summary> Shows the container and children. </summary>
    public override void Show ()
    {
        if (Visible)
            return;

#if UNITY_EDITOR
        if(Application.isPlaying)
#endif
        if (AutoShowChildren != AutoShowChildrenType.None)
            TryForceHideChildren ();

        base.Show ();
    }

    /// <summary> Called On Begin Show. </summary>
    protected override void BeginShow ()
    {
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.interactable = false;

        if (AutoShowChildren == AutoShowChildrenType.OnBeginShow)
            ShowChildren ();

        base.BeginShow ();
    }

    /// <summary> Called On End Show .</summary>
    protected override void EndShow ()
    {
        CanvasGroup.interactable = true;

        if (AutoShowChildren == AutoShowChildrenType.OnEndShow)
            ShowChildren ();

        base.EndShow ();
    }

    /// <summary> Called On Begin Hide </summary>
    protected override void BeginHide ()
    {

        CanvasGroup.interactable = false;
        
        if (AutoHideChildren == AutoHideChildrenType.OnBeginHide)
            HideChildren (base.BeginHide);

        else
            base.BeginHide ();
    }

    /// <summary> Called On End Hide </summary>
    protected override void EndHide ()
    {
        CanvasGroup.blocksRaycasts = false;

        HideChildren ();

        base.EndHide ();
    }

    #endregion



    #region Class Implementation

    /// <summary> Attempts to force children to hide. </summary>
    /// <returns></returns>
    public bool TryForceHideChildren ()
    {
        bool allChildrenHiden = true;

        _children.ForEach (
            child =>
            {
                if (!child.enabled || child.IgnoreParent)
                    allChildrenHiden = false;
                else
                    child.ForceHide ();
            }
        );

        return allChildrenHiden;
    }

    /// <summary> Tries to add  a child if it deserve it. </summary>
    /// <param name="child"> Chil to try to add. </param>
    /// <returns> Returns wether the child was added. </returns>
    public bool TryAddChild (AbstractShowable child)
    {
        if (child == null)
            return false;

        if (child.transform.parent != transform)
            return false;

        if (_children.Contains (child))
            return true;

        _children.Add (child);
        ValidateChildren ();

        return _children.Contains (child);
    }

    /// <summary> Tries to remove a child if it is found. </summary>
    /// <param name="child"> Child to remove. </param>
    public bool TryRemoveChild (AbstractShowable child)
    {
        if (child == null)
            return true;

        if (!_children.Contains (child))
            return true;

        _children.Remove (child);
        ValidateChildren ();

        return !_children.Contains (child);
    }

    /// <summary> Validates the current list of childre. </summary>
    private void ValidateChildren ()
    {
        List<AbstractShowable> childrenToRemove = new List<AbstractShowable> ();

        for (int i = 0; i < _children.Count; i++)
        {
            if (
                _children[i] == null ||
                _children[i].transform == null ||
                _children[i].transform.parent != transform
            )
                childrenToRemove.Add (_children[i]);
        }
        childrenToRemove.ForEach (children => _children.Remove (children));
    }

    /// <summary> Starts sequence to show children. </summary>
    public void ShowChildren (Action callback = null)
    {

#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            _children.ForEach (
                child =>
                {
                    if (child.enabled && !child.IgnoreParent)
                        child.Show ();
                }
            );
            return;
        }
#endif

        //  Show children with out sequence.
        if (!UseSequence)
        {
            _children.ForEach (
                child =>
                {
                    if (child.enabled && !child.IgnoreParent)
                        child.Show ();
                }
            );
            callback?.Invoke ();
            return;
        }


        //  Reseting variables.
        if (_childrenCoroutine != null)
            StopCoroutine (_childrenCoroutine);
        _childIndex = 0;

        //  Start Show sequence.
        _childrenCoroutine = StartCoroutine (ShowChildrenCoroutine (callback));
    }

    /// <summary> Show children with delay. </summary>
    private IEnumerator ShowChildrenCoroutine (Action callback)
    {
        yield return null;
        do
        {
            var child = _children[_childIndex];

            if (!child.enabled || child.IgnoreParent)
            {
                _childIndex++;
                continue;
            }

            float delay = child.OverrideDelay ?
                child.Delay :
                SequenceDelay;

            if (delay > 0)
            {
                if (UseScaledTime)
                    yield return new WaitForSeconds (delay);
                else
                    yield return new WaitForUnscaledSeconds (delay);
            }

            child.Show ();
            _childIndex++;

        } while (_childIndex < _children.Count);
        callback?.Invoke ();
    }

    /// <summary> Starts sequence to show children. </summary>
    public void HideChildren (Action callback = null)
    {

#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            _children.ForEach (
                child =>
                {
                    if (child.enabled && !child.IgnoreParent)
                        child.Hide ();
                }
            );
            callback?.Invoke ();
            return;
        }
#endif

        //  Hide children with out sequence.
        if (!UseSequence)
        {
            _children.ForEach (
                child =>
                {
                    if (child.enabled && !child.IgnoreParent)
                        child.Hide ();
                }
            );
            callback?.Invoke ();
            return;
        }

        //  Reseting variables.
        if (_childrenCoroutine != null)
            StopCoroutine (_childrenCoroutine);

        if (_children.Count <= 0)
        {
            _childIndex = 0;
            return;
        }

        if (_childIndex >= _children.Count)
            _childIndex = _children.Count - 1;

        if (ChildrenSequence != SequenceType.Yoyo)
            _childIndex = 0;

        //  Starting sequence..
        _childrenCoroutine = StartCoroutine (
            HideChildrenCoroutine (callback)
        );
    }


    /// <summary> Hide children with delay. </summary>
    private IEnumerator HideChildrenCoroutine (Action callback)
    {
        yield return null;

        //  Yoyo Mode
        if (ChildrenSequence == SequenceType.Yoyo)
        {
            do
            {
                var child = _children[_childIndex];

                if (child.IgnoreParent || !child.enabled)
                {
                    _childIndex--;
                    continue;
                }

                child.Hide ();

                var delay = child.OverrideDelay ? child.Delay : SequenceDelay;

                if (delay > 0)
                {
                    if (UseScaledTime)
                        yield return new WaitForSeconds (delay);
                    else
                        yield return new WaitForUnscaledSeconds (delay);
                }

                _childIndex--;
            } while (_childIndex >= 0);
        }

        //  Reset and incremental Mode.
        else
        {
            do
            {
                var child = _children[_childIndex];

                if (child.IgnoreParent || !child.enabled)
                {
                    _childIndex++;
                    continue;
                }

                child.Hide ();

                var delay = child.OverrideDelay ? child.Delay : SequenceDelay;

                if (delay > 0)
                {
                    if (UseScaledTime)
                        yield return new WaitForSeconds (delay);
                    else
                        yield return new WaitForUnscaledSeconds (delay);
                }

                _childIndex++;

            } while (_childIndex < _children.Count);

        }

        callback?.Invoke ();
    }

    #endregion
}