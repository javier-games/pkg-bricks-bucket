using UnityEngine.UI;
using UnityEngine.EventSystems;

using Action = System.Action;

/// <summary>
/// 
/// IShowable
/// 
/// <para>
/// Interface for showable objects.
/// </para>
/// 
/// <para> By Javier García | @jvrgms | 2019 </para>
/// 
/// </summary>
public interface IShowable
{
    /// <summary> Returns the current transition duration. </summary>
    float TransitionDuration { get; }

    /// <summary> Returns the current transition value. </summary>
    float Transition { get; }

    /// <summary> Gets and Sets wether to use scaled time. </summary>
    bool UseScaledTime { get; set; }

    /// <summary> Wether or not the Container is visible. </summary>
    bool Visible { get; }

    /// <summary> Shows up the object. </summary>
    void Show ();

    /// <summary> Hides the object. </summary>
    void Hide ();

    /// <summary> Delegate called on begin show. </summary>
    Action OnBeginShow { get; set; }

    /// <summary> Delegate called on end show. </summary>
    Action OnEndShow { get; set; }

    /// <summary> Delegate called on begin hide. </summary>
    Action OnBeginHide { get; set; }

    /// <summary> Delegate called on end hide. </summary>
    Action OnEndHide { get; set; }
}

/// <summary>
/// 
/// IAnimatable.
/// 
/// <para>
/// Interface for animatable objects..
/// </para>
/// 
/// <para> By Javier García | @jvrgms | 2019 </para>
/// 
/// </summary>
public interface IAnimatable : IShowable,
IPointerDownHandler, IPointerUpHandler,
IPointerEnterHandler, IPointerExitHandler,
ISelectHandler, IDeselectHandler,
IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary> Reference to get interactivity. </summary>
    Selectable Selectable { get; set; }

    /// <summary> Returns the current state of the animatable. </summary>
    AnimatableState CurrentAnimationState { get; }

    /// <summary> Wether the transition is animatable. </summary>
    bool IsInteractable { get; }

    /// <summary> Wether the pointer is inside. </summary>
    bool IsPointerInside { get; }

    /// <summary> Wether the pointer is down. </summary>
    bool IsPointerDown { get; }

    /// <summary> Wether the event system has a selection. </summary>
    bool HasSelection { get; }

    /// <summary> Wether the pointer is dragging. </summary>
    bool IsDragging { get; }

    /// <summary> Wether is calling to action. </summary>
    bool IsCallingToAction { get; }

    /// <summary> Changes the animation state. </summary>
    /// <param name="state"> New state. </param>
    void SetAnimationState (AnimatableState state);

    /// <summary> Changes animation to Call to Action. </summary>
    void CallToAction ();
}

/// <summary>
/// 
/// IContainer.
/// 
/// <para>
/// Interface for container objects.
/// </para>
/// 
/// <para> By Javier García | @jvrgms | 2019 </para>
/// 
/// </summary>
public interface ISection : IShowable
{
    /// <summary> Gets the children of this container. </summary>
    AbstractShowable[] ChildrenCollection { get; }

    /// <summary> Starts sequence to show children. </summary>
    /// <param name="callback"> Called on end show children. </param>
    void ShowChildren (Action callback);

    /// <summary> Starts sequence to show children. </summary>
    /// <param name="callback"> Called on end hide children. </param>
    void HideChildren (Action callback);

    /// <summary> Tries to add  a child if it deserve it. </summary>
    /// <param name="child"> Chil to try to add. </param>
    /// <returns> Returns wether the child was added. </returns>
    bool TryAddChild (AbstractShowable child);

    /// <summary> Tries to remove a child if it is found. </summary>
    /// <param name="child"> Child to remove. </param>
    bool TryRemoveChild (AbstractShowable child);

}