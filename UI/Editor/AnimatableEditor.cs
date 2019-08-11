using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.AnimatedValues;

[CustomEditor (typeof (Animatable), true)]
/// <summary>
///   Custom Editor for the Selectable Component.
///   Extend this class to write a custom editor for an Selectable-derived component.
/// </summary>
public class AnimatableEditor : Editor
{
    SerializedProperty m_Script;

    SerializedProperty m_Parent;
    SerializedProperty m_Selectable;

    SerializedProperty m_TargetGraphicProperty;
    SerializedProperty m_AnimatorProperty;
    SerializedProperty m_TransitionProperty;
    SerializedProperty m_ColorBlockProperty;
    SerializedProperty m_SpriteStateProperty;
    SerializedProperty m_AnimTriggerProperty;

    AnimBool m_ShowColorTint = new AnimBool ();
    AnimBool m_ShowSpriteTrasition = new AnimBool ();
    AnimBool m_ShowAnimTransition = new AnimBool ();


    // Whenever adding new SerializedProperties to the Selectable and SelectableEditor
    // Also update this guy in OnEnable. This makes the inherited classes from Selectable not require a CustomEditor.
    private string[] m_PropertyPathToExcludeForChildClasses;

    protected virtual void OnEnable ()
    {
        m_Parent = serializedObject.FindProperty ("_parent");
        m_Selectable = serializedObject.FindProperty ("_selectable");
        m_Script = serializedObject.FindProperty ("m_Script");
        m_TargetGraphicProperty = serializedObject.FindProperty ("_targetGraphic");
        m_AnimatorProperty = serializedObject.FindProperty ("_animator");
        m_TransitionProperty = serializedObject.FindProperty ("_transitionType");
        m_ColorBlockProperty = serializedObject.FindProperty ("_colors");
        m_SpriteStateProperty = serializedObject.FindProperty ("_sprites");
        m_AnimTriggerProperty = serializedObject.FindProperty ("_triggers");

        m_PropertyPathToExcludeForChildClasses = new[]
        {
            m_Parent.propertyPath,
            m_Selectable.propertyPath,
            m_Script.propertyPath,
            m_TransitionProperty.propertyPath,
            m_ColorBlockProperty.propertyPath,
            m_SpriteStateProperty.propertyPath,
            m_AnimTriggerProperty.propertyPath,
            m_TargetGraphicProperty.propertyPath,
            m_AnimatorProperty.propertyPath
        };

        var trans = GetTransition (m_TransitionProperty);
        m_ShowColorTint.value = (trans == AnimatableTransition.ColorTint);
        m_ShowSpriteTrasition.value = (trans == AnimatableTransition.SpriteSwap);
        m_ShowAnimTransition.value = (trans == AnimatableTransition.Animation);

        m_ShowColorTint.valueChanged.AddListener (Repaint);
        m_ShowSpriteTrasition.valueChanged.AddListener (Repaint);
    }

    protected virtual void OnDisable ()
    {
        m_ShowColorTint.valueChanged.RemoveListener (Repaint);
        m_ShowSpriteTrasition.valueChanged.RemoveListener (Repaint);

    }


    static AnimatableTransition GetTransition (SerializedProperty transition)
    {
        return (AnimatableTransition) transition.enumValueIndex;
    }

    public override void OnInspectorGUI ()
    {
        serializedObject.Update ();
        Animatable animatable = target as Animatable;

        GUI.enabled = false;
        //EditorGUILayout.PropertyField (m_Script);
        EditorGUILayout.Space ();
        EditorGUILayout.PropertyField (m_Parent);
        EditorGUILayout.PropertyField (m_Selectable);
        //EditorGUILayout.Space ();
        //animatable.Visible = EditorGUILayout.Toggle ("Visible", animatable.Visible);
        EditorGUILayout.Space ();
        GUI.enabled = true;

        var trans = GetTransition (m_TransitionProperty);

        var graphic = m_TargetGraphicProperty.objectReferenceValue as Graphic;
        if (graphic == null)
            graphic = (target as Animatable).GetComponent<Graphic> ();

        var animator = m_AnimatorProperty.objectReferenceValue as Animator;
        if (animator == null)
            animator = (target as Animatable).GetComponent<Animator> ();

        m_ShowColorTint.target = (!m_TransitionProperty.hasMultipleDifferentValues && trans == AnimatableTransition.ColorTint);
        m_ShowSpriteTrasition.target = (!m_TransitionProperty.hasMultipleDifferentValues && trans == AnimatableTransition.SpriteSwap);
        m_ShowAnimTransition.target = (!m_TransitionProperty.hasMultipleDifferentValues && trans == AnimatableTransition.Animation);

        EditorGUILayout.PropertyField (m_TransitionProperty);

        ++EditorGUI.indentLevel;
        {
            if (trans == AnimatableTransition.ColorTint || trans == AnimatableTransition.SpriteSwap)
            {
                EditorGUILayout.PropertyField (m_TargetGraphicProperty);
            }

            if (trans == AnimatableTransition.Animation)
            {
                EditorGUILayout.PropertyField (m_AnimatorProperty);
            }

            switch (trans)
            {
                case AnimatableTransition.ColorTint:
                if (graphic == null)
                    EditorGUILayout.HelpBox ("You must have a Graphic target in order to use a color transition.", MessageType.Warning);
                break;

                case AnimatableTransition.SpriteSwap:
                if (graphic as Image == null)
                    EditorGUILayout.HelpBox ("You must have a Image target in order to use a sprite swap transition.", MessageType.Warning);
                break;
            }

            if (EditorGUILayout.BeginFadeGroup (m_ShowColorTint.faded))
            {
                EditorGUILayout.PropertyField (m_ColorBlockProperty);
            }
            EditorGUILayout.EndFadeGroup ();

            if (EditorGUILayout.BeginFadeGroup (m_ShowSpriteTrasition.faded))
            {
                EditorGUILayout.PropertyField (m_SpriteStateProperty);
            }
            EditorGUILayout.EndFadeGroup ();

            if (EditorGUILayout.BeginFadeGroup (m_ShowAnimTransition.faded))
            {
                EditorGUILayout.PropertyField (m_AnimTriggerProperty);

                if (animator == null || animator.runtimeAnimatorController == null)
                {
                    Rect buttonRect = EditorGUILayout.GetControlRect ();
                    buttonRect.xMin += EditorGUIUtility.labelWidth;
                    if (GUI.Button (buttonRect, "Auto Generate Animation", EditorStyles.miniButton))
                    {
                        var controller = GenerateSelectableAnimatorContoller ((animatable).Triggers, animatable);
                        if (controller != null)
                        {
                            if (animator == null)
                                animator = (animatable).gameObject.AddComponent<Animator> ();

                            AnimatorController.SetAnimatorController (animator, controller);
                        }
                    }
                }
            }
            EditorGUILayout.EndFadeGroup ();
        }
        --EditorGUI.indentLevel;

        EditorGUILayout.Space ();
        EditorGUILayout.Space ();

        // We do this here to avoid requiring the user to also write a Editor for their Selectable-derived classes.
        // This way if we are on a derived class we dont draw anything else, otherwise draw the remaining properties.
        ChildClassPropertiesGUI ();







        serializedObject.ApplyModifiedProperties ();
    }

    // Draw the extra SerializedProperties of the child class.
    // We need to make sure that m_PropertyPathToExcludeForChildClasses has all the Selectable properties and in the correct order.
    // TODO: find a nicer way of doing this. (creating a InheritedEditor class that automagically does this)
    private void ChildClassPropertiesGUI ()
    {
        if (IsDerivedSelectableEditor ())
            return;

        DrawPropertiesExcluding (serializedObject, m_PropertyPathToExcludeForChildClasses);
    }

    private bool IsDerivedSelectableEditor ()
    {
        return GetType () != typeof (AnimatableEditor);
    }

    private static AnimatorController GenerateSelectableAnimatorContoller (Animatable.AnimatableTriggerSet animationTriggers, Animatable target)
    {
        if (target == null)
            return null;

        // Where should we create the controller?
        var path = GetSaveControllerPath (target);
        if (string.IsNullOrEmpty (path))
            return null;

        // figure out clip names
        var enableName = string.IsNullOrEmpty (animationTriggers.enable) ? "Enable" : animationTriggers.enable;
        var idleName = string.IsNullOrEmpty (animationTriggers.idle) ? "Idle" : animationTriggers.idle;
        var callToActionName = string.IsNullOrEmpty (animationTriggers.callToAction) ? "Call To Action" : animationTriggers.callToAction;
        var highlightedName = string.IsNullOrEmpty (animationTriggers.highlighted) ? "Highlighted" : animationTriggers.highlighted;
        var pressedName = string.IsNullOrEmpty (animationTriggers.pressed) ? "Pressed" : animationTriggers.pressed;
        var selectedName = string.IsNullOrEmpty (animationTriggers.selected) ? "Selected" : animationTriggers.selected;
        var draggingName = string.IsNullOrEmpty (animationTriggers.dragging) ? "Dragging" : animationTriggers.dragging;
        var disabledName = string.IsNullOrEmpty (animationTriggers.disable) ? "Disable" : animationTriggers.disable;

        // Create controller and hook up transitions.
        var controller = AnimatorController.CreateAnimatorControllerAtPath (path);

        var enableState = GenerateTriggerableTransition (enableName, controller);
        var idleState = GenerateTriggerableTransition (idleName, controller);
        GenerateTriggerableTransition (highlightedName, controller);
        GenerateTriggerableTransition (pressedName, controller);
        GenerateTriggerableTransition (selectedName, controller);
        GenerateTriggerableTransition (disabledName, controller);
        GenerateTriggerableTransition (callToActionName, controller);
        GenerateTriggerableTransition (draggingName, controller);

        MakeTransition (enableState, idleState);

        AssetDatabase.ImportAsset (path);

        return controller;
    }

    private static string GetSaveControllerPath (Animatable target)
    {
        var defaultName = target.gameObject.name;
        var message = string.Format ("Create a new animator for the game object '{0}':", defaultName);
        return EditorUtility.SaveFilePanelInProject ("New Animation Contoller", defaultName, "controller", message);
    }

    private static void SetUpCurves (AnimationClip highlightedClip, AnimationClip pressedClip, string animationPath)
    {
        string[] channels = { "m_LocalScale.x", "m_LocalScale.y", "m_LocalScale.z" };

        var highlightedKeys = new[] { new Keyframe (0f, 1f), new Keyframe (0.5f, 1.1f), new Keyframe (1f, 1f) };
        var highlightedCurve = new AnimationCurve (highlightedKeys);
        foreach (var channel in channels)
            AnimationUtility.SetEditorCurve (highlightedClip, EditorCurveBinding.FloatCurve (animationPath, typeof (Transform), channel), highlightedCurve);

        var pressedKeys = new[] { new Keyframe (0f, 1.15f) };
        var pressedCurve = new AnimationCurve (pressedKeys);
        foreach (var channel in channels)
            AnimationUtility.SetEditorCurve (pressedClip, EditorCurveBinding.FloatCurve (animationPath, typeof (Transform), channel), pressedCurve);
    }

    private static string BuildAnimationPath (Animatable target)
    {
        // if no target don't hook up any curves.
        var highlight = target.TargetGraphic;
        if (highlight == null)
            return string.Empty;

        var startGo = highlight.gameObject;
        var toFindGo = target.gameObject;

        var pathComponents = new Stack<string> ();
        while (toFindGo != startGo)
        {
            pathComponents.Push (startGo.name);

            // didn't exist in hierarchy!
            if (startGo.transform.parent == null)
                return string.Empty;

            startGo = startGo.transform.parent.gameObject;
        }

        // calculate path
        var animPath = new StringBuilder ();
        if (pathComponents.Count > 0)
            animPath.Append (pathComponents.Pop ());

        while (pathComponents.Count > 0)
            animPath.Append ("/").Append (pathComponents.Pop ());

        return animPath.ToString ();
    }

    private static AnimatorState GenerateTriggerableTransition (string name, AnimatorController controller)
    {
        // Create the clip
        var clip = AnimatorController.AllocateAnimatorClip (name);
        AssetDatabase.AddObjectToAsset (clip, controller);

        // Create a state in the animatior controller for this clip
        var state = controller.AddMotion (clip);

        // Add a transition property
        controller.AddParameter (name, AnimatorControllerParameterType.Trigger);

        // Add an any state transition
        var stateMachine = controller.layers[0].stateMachine;
        var transition = stateMachine.AddAnyStateTransition (state);
        transition.AddCondition (AnimatorConditionMode.If, 0, name);
        return state;
    }

    private static void MakeTransition (AnimatorState from, AnimatorState to)
    {
        var transition = from.AddTransition (to);
        transition.hasExitTime = true;
    }
}

[CustomPropertyDrawer (typeof (Animatable.AnimatableColorBlock), true)]
public class AnimatableColorBlockDrawer : PropertyDrawer
{
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        Rect drawRect = position;
        drawRect.height = EditorGUIUtility.singleLineHeight;

        SerializedProperty idle = property.FindPropertyRelative ("idle");
        SerializedProperty enable = property.FindPropertyRelative ("enable");
        SerializedProperty callToAction = property.FindPropertyRelative ("callToAction");
        SerializedProperty highlighted = property.FindPropertyRelative ("highlighted");
        SerializedProperty pressed = property.FindPropertyRelative ("pressed");
        SerializedProperty selected = property.FindPropertyRelative ("selected");
        SerializedProperty dragging = property.FindPropertyRelative ("dragging");
        SerializedProperty disable = property.FindPropertyRelative ("disable");


        SerializedProperty colorMultiplier = property.FindPropertyRelative ("colorMultiplier");
        SerializedProperty fadeDuration = property.FindPropertyRelative ("fadeDuration");

        EditorGUI.PropertyField (drawRect, idle);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, enable);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, callToAction);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, highlighted);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, pressed);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, selected);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, dragging);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, disable);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        EditorGUI.PropertyField (drawRect, colorMultiplier);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, fadeDuration);
    }

    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        return 10 * EditorGUIUtility.singleLineHeight + 6 * EditorGUIUtility.standardVerticalSpacing;
    }
}

[CustomPropertyDrawer (typeof (Animatable.AnimatableTriggerSet), true)]
public class AnimatableTriggerSetEditor : PropertyDrawer
{
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        Rect drawRect = position;
        drawRect.height = EditorGUIUtility.singleLineHeight;

        SerializedProperty idle = property.FindPropertyRelative ("idle");
        SerializedProperty enable = property.FindPropertyRelative ("enable");
        SerializedProperty callToAction = property.FindPropertyRelative ("callToAction");
        SerializedProperty highlighted = property.FindPropertyRelative ("highlighted");
        SerializedProperty pressed = property.FindPropertyRelative ("pressed");
        SerializedProperty selected = property.FindPropertyRelative ("selected");
        SerializedProperty dragging = property.FindPropertyRelative ("dragging");
        SerializedProperty disable = property.FindPropertyRelative ("disable");

        EditorGUI.PropertyField (drawRect, idle);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, enable);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, callToAction);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, highlighted);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, pressed);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, selected);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, dragging);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, disable);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
    }

    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        return 8 * EditorGUIUtility.singleLineHeight + 4 * EditorGUIUtility.standardVerticalSpacing;
    }
}

[CustomPropertyDrawer (typeof (Animatable.AnimatableSpriteState), true)]
public class AnimatableSpriteStateEditor : PropertyDrawer
{
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        Rect drawRect = position;
        drawRect.height = EditorGUIUtility.singleLineHeight;

        SerializedProperty idle = property.FindPropertyRelative ("idle");
        SerializedProperty enable = property.FindPropertyRelative ("enable");
        SerializedProperty callToAction = property.FindPropertyRelative ("callToAction");
        SerializedProperty highlighted = property.FindPropertyRelative ("highlighted");
        SerializedProperty pressed = property.FindPropertyRelative ("pressed");
        SerializedProperty selected = property.FindPropertyRelative ("selected");
        SerializedProperty dragging = property.FindPropertyRelative ("dragging");
        SerializedProperty disable = property.FindPropertyRelative ("disable");

        EditorGUI.PropertyField (drawRect, idle);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, enable);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, callToAction);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, highlighted);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, pressed);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, selected);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, dragging);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField (drawRect, disable);
        drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
    }

    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        return 8 * EditorGUIUtility.singleLineHeight + 4 * EditorGUIUtility.standardVerticalSpacing;
    }
}
