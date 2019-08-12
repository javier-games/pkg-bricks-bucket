using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;


[CustomPropertyDrawer (typeof (AnimationCurve3), true)]
public class Vector3CurvesDrawer : PropertyDrawer
{

    SerializedProperty m_xProperty;
    SerializedProperty m_yProperty;
    SerializedProperty m_zProperty;

    public override void
    OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        Rect drawRect = position;
        drawRect.height = 2*EditorGUIUtility.singleLineHeight;

        m_xProperty = property.FindPropertyRelative ("x");
        m_yProperty = property.FindPropertyRelative ("y");
        m_zProperty = property.FindPropertyRelative ("z");

        EditorGUI.PropertyField (drawRect, m_xProperty);
        drawRect.y += 2*(EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
        EditorGUI.PropertyField (drawRect, m_yProperty);
        drawRect.y += 2 * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
        EditorGUI.PropertyField (drawRect, m_zProperty);
        drawRect.y += 2 * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
    }

    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        return 6 * EditorGUIUtility.singleLineHeight + 8 * EditorGUIUtility.standardVerticalSpacing;
    }
}


[CustomEditor (typeof (ShowableSettings), true)]
public class ShowableSettingsDrawer : Editor
{



    AnimBool m_Showable = new AnimBool ();
    AnimBool m_Animatable = new AnimBool ();
    AnimBool m_Section = new AnimBool ();
    AnimBool m_Panel = new AnimBool ();
    AnimBool m_Modal = new AnimBool ();


    SerializedProperty m_ShowableTypeProperty;


    SerializedProperty m_UseScaledTimeProperty;
    SerializedProperty m_IgnoreParentProperty;
    SerializedProperty m_OverrideDelayProperty;
    SerializedProperty m_DelayProperty;
    SerializedProperty m_AnimatablePropertiesProperty;


    SerializedProperty m_ShowDurationProperty;
    SerializedProperty m_ShowEaseTypeProperty;
    SerializedProperty m_ShowAnimationProperty;
    SerializedProperty m_ShowOffsetPositionProperty;
    SerializedProperty m_ShowOffsetRositionProperty;
    SerializedProperty m_ShowScaleProperty;
    SerializedProperty m_ShowOverrideAnimationProperty;
    SerializedProperty m_ShowOverridePositionProperty;
    SerializedProperty m_ShowOverrideRotationProperty;
    SerializedProperty m_ShowOverrideScaleProperty;


    SerializedProperty m_HideDurationProperty;
    SerializedProperty m_HideEaseTypeProperty;
    SerializedProperty m_HideAnimationProperty;
    SerializedProperty m_HideOffsetPositionProperty;
    SerializedProperty m_HideOffsetRositionProperty;
    SerializedProperty m_HideScaleProperty;
    SerializedProperty m_HideOverrideAnimationProperty;
    SerializedProperty m_HideOverridePositionProperty;
    SerializedProperty m_HideOverrideRotationProperty;
    SerializedProperty m_HideOverrideScaleProperty;


    SerializedProperty m_AutoShowChildren;
    SerializedProperty m_AutoHideChildren;
    SerializedProperty m_UseSequence;
    SerializedProperty m_ChildrenSequence;
    SerializedProperty m_SequenceDelay;
    SerializedProperty m_ShowAlpha;
    SerializedProperty m_HideAlpha;
    SerializedProperty m_ShowAlphaAnimation;
    SerializedProperty m_HideAlphaAnimation;


    EaseType m_ShowEaseType;
    EaseType m_HideEaseType;

    protected virtual void OnEnable ()
    {

        m_ShowableTypeProperty = serializedObject.FindProperty ("_showableType");

        m_UseScaledTimeProperty = serializedObject.FindProperty ("_useScaledTime");
        m_IgnoreParentProperty = serializedObject.FindProperty ("_ignoreParent");
        m_OverrideDelayProperty = serializedObject.FindProperty ("_overrideDelay");
        m_DelayProperty = serializedObject.FindProperty ("_delay");
        m_AnimatablePropertiesProperty = serializedObject.FindProperty ("_animatableProperties");


        m_ShowDurationProperty = serializedObject.FindProperty ("_showDuration");
        m_ShowEaseTypeProperty = serializedObject.FindProperty ("_showEaseType");
        m_ShowAnimationProperty = serializedObject.FindProperty ("_showAnimation");
        m_ShowOffsetPositionProperty = serializedObject.FindProperty ("_showOffsetPosition");
        m_ShowOffsetRositionProperty = serializedObject.FindProperty ("_showOffsetRosition");
        m_ShowScaleProperty = serializedObject.FindProperty ("_showScale");
        m_ShowOverrideAnimationProperty = serializedObject.FindProperty ("_showOverrideAnimation");
        m_ShowOverridePositionProperty = serializedObject.FindProperty ("_showOverridePositionAnimation");
        m_ShowOverrideRotationProperty = serializedObject.FindProperty ("_showOverrideRotationAnimation");
        m_ShowOverrideScaleProperty = serializedObject.FindProperty ("_showScaleAnimation");


        m_HideDurationProperty = serializedObject.FindProperty ("_hideDuration");
        m_HideEaseTypeProperty = serializedObject.FindProperty ("_hideEaseType");
        m_HideAnimationProperty = serializedObject.FindProperty ("_hideAnimation");
        m_HideOffsetPositionProperty = serializedObject.FindProperty ("_hideOffsetPosition");
        m_HideOffsetRositionProperty = serializedObject.FindProperty ("_hideOffsetRosition");
        m_HideScaleProperty = serializedObject.FindProperty ("_hideScale");
        m_HideOverrideAnimationProperty = serializedObject.FindProperty ("_hideOverrideAnimation");
        m_HideOverridePositionProperty = serializedObject.FindProperty ("_hideOverridePositionAnimation");
        m_HideOverrideRotationProperty = serializedObject.FindProperty ("_hideOverrideRotationAnimation");
        m_HideOverrideScaleProperty = serializedObject.FindProperty ("_hideScaleAnimation");


        m_AutoShowChildren = serializedObject.FindProperty ("_autoShowChildren");
        m_AutoHideChildren = serializedObject.FindProperty ("_autoHideChildren");
        m_UseSequence = serializedObject.FindProperty ("_useSequence");
        m_ChildrenSequence = serializedObject.FindProperty ("_childrenSequence");
        m_SequenceDelay = serializedObject.FindProperty ("_sequenceDelay");
        m_ShowAlpha = serializedObject.FindProperty ("_showAlpha");
        m_HideAlpha = serializedObject.FindProperty ("_hideAlpha");
        m_ShowAlphaAnimation = serializedObject.FindProperty ("_showAlphaAnimation");
        m_HideAlphaAnimation = serializedObject.FindProperty ("_hideAlphaAnimation");




        var trans = (ShowableType) m_ShowableTypeProperty.enumValueIndex;
        m_Showable.value = trans == ShowableType.Showable;
        m_Animatable.value = trans == ShowableType.Animatable;
        m_Section.value = trans == ShowableType.Section;
        m_Panel.value = trans == ShowableType.Panel;
        m_Modal.value = trans == ShowableType.Modal;

        m_Animatable.valueChanged.AddListener (Repaint);
        m_Section.valueChanged.AddListener (Repaint);
        m_Panel.valueChanged.AddListener (Repaint);
        m_Modal.valueChanged.AddListener (Repaint);
    }

    protected virtual void OnDisable ()
    {
        m_Animatable.valueChanged.RemoveListener (Repaint);
        m_Section.valueChanged.RemoveListener (Repaint);
        m_Panel.valueChanged.RemoveListener (Repaint);
        m_Modal.valueChanged.RemoveListener (Repaint);
    }

    public override void OnInspectorGUI ()
    {

        serializedObject.Update ();

        EditorGUILayout.PropertyField (m_ShowableTypeProperty);

        var trans = (ShowableType) m_ShowableTypeProperty.enumValueIndex;
        m_Showable.target = !m_ShowableTypeProperty.hasMultipleDifferentValues && trans == ShowableType.Showable;
        m_Animatable.target = !m_ShowableTypeProperty.hasMultipleDifferentValues && trans == ShowableType.Animatable;
        m_Section.target = !m_ShowableTypeProperty.hasMultipleDifferentValues && trans == ShowableType.Section;
        m_Panel.target = !m_ShowableTypeProperty.hasMultipleDifferentValues && trans == ShowableType.Panel;
        m_Modal.target = !m_ShowableTypeProperty.hasMultipleDifferentValues && trans == ShowableType.Modal;


        if (EditorGUILayout.BeginFadeGroup (m_Showable.faded))
        {
            DrawShowableParams ();
        }
        EditorGUILayout.EndFadeGroup ();


        if (EditorGUILayout.BeginFadeGroup (m_Section.faded))
        {
            DrawSectionParams ();
        }
        EditorGUILayout.EndFadeGroup ();

        serializedObject.ApplyModifiedProperties ();
    }




    public void DrawSectionParams ()
    {
        EditorGUILayout.Space ();
        EditorGUILayout.Space ();
        EditorGUILayout.PropertyField (m_UseScaledTimeProperty);
        EditorGUILayout.PropertyField (m_IgnoreParentProperty);
        EditorGUILayout.PropertyField (m_OverrideDelayProperty);
        if (m_OverrideDelayProperty.boolValue)
            EditorGUILayout.PropertyField (m_DelayProperty);
        EditorGUILayout.PropertyField (m_AutoShowChildren);
        EditorGUILayout.PropertyField (m_AutoHideChildren);
        EditorGUILayout.PropertyField (m_UseSequence);
        if (m_UseSequence.boolValue)
        {
            EditorGUILayout.PropertyField (m_ChildrenSequence);
            EditorGUILayout.PropertyField (m_SequenceDelay);
        }




        EditorGUILayout.Space ();
        EditorGUILayout.Space ();
        EditorGUILayout.Space ();

        EditorGUILayout.LabelField ("Show State Parameters", EditorStyles.boldLabel);
        ++EditorGUI.indentLevel;
        DrawShowSectionParams ();
        --EditorGUI.indentLevel;




        EditorGUILayout.Space ();
        EditorGUILayout.Space ();
        EditorGUILayout.Space ();

        EditorGUILayout.LabelField ("Hide State Parameters", EditorStyles.boldLabel);
        ++EditorGUI.indentLevel;
        DrawHideSectionParams ();
        --EditorGUI.indentLevel;
    }


    public void DrawShowSectionParams ()
    {


        var mask = m_AnimatablePropertiesProperty.intValue;
        bool usePosition = mask.MaskHasLayer (AnimatableProperty.Position);
        bool useRotation = mask.MaskHasLayer (AnimatableProperty.Rotation);
        bool useScale = mask.MaskHasLayer (AnimatableProperty.Scale);
        bool useAlpha = mask.MaskHasLayer (AnimatableProperty.Alpha);

        var overrideMask = m_ShowOverrideAnimationProperty.intValue;
        bool overridePosition = overrideMask.MaskHasLayer (AnimatableProperty.Position);
        bool overrideRotation = overrideMask.MaskHasLayer (AnimatableProperty.Rotation);
        bool overrideScale = overrideMask.MaskHasLayer (AnimatableProperty.Scale);
        bool overrideAlpha = overrideMask.MaskHasLayer (AnimatableProperty.Alpha);



        EditorGUILayout.PropertyField (m_ShowDurationProperty, new GUIContent ("Duration"));



        EditorGUILayout.PropertyField (m_ShowEaseTypeProperty, new GUIContent ("Ease"));
        var currentEaseType = (EaseType) m_ShowEaseTypeProperty.enumValueIndex;
        if (m_ShowEaseType != currentEaseType && currentEaseType != EaseType.Custom)
        {
            m_ShowAnimationProperty.animationCurveValue = EaseCurve[currentEaseType];
            m_ShowEaseType = currentEaseType;
        }
        GUI.enabled = currentEaseType == EaseType.Custom;
        EditorGUILayout.PropertyField (m_ShowAnimationProperty, new GUIContent ("Animation"), GUILayout.Height (3 * EditorGUIUtility.singleLineHeight));
        GUI.enabled = true;
        EditorGUILayout.Space ();



        int showableMaskProperties = 0;
        if (usePosition)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Position);
        if (useRotation)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Rotation);
        if (useScale)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Scale);
        if (useAlpha)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Alpha);
        showableMaskProperties = EditorGUILayout.MaskField ("Properties to Animate", showableMaskProperties, new string[] { "Position", "Rotation", "Scale", "Alpha" });
        m_AnimatablePropertiesProperty.intValue = showableMaskProperties;



        showableMaskProperties = 0;
        if (overridePosition)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Position);
        if (overrideRotation)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Rotation);
        if (overrideScale)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Scale);
        if (overrideAlpha)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Alpha);
        showableMaskProperties = EditorGUILayout.MaskField ("Overwrite Animation", showableMaskProperties, new string[] { "Position", "Rotation", "Scale", "Alpha" });
        m_ShowOverrideAnimationProperty.intValue = showableMaskProperties;



        GUI.enabled = usePosition;
        EditorGUILayout.PropertyField (m_ShowOffsetPositionProperty, new GUIContent ("Offset Position"));
        if (overridePosition)
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField (m_ShowOverridePositionProperty);
            --EditorGUI.indentLevel;
        }



        GUI.enabled = useRotation;
        EditorGUILayout.PropertyField (m_ShowOffsetRositionProperty, new GUIContent ("Offset Rotation"));
        if (overrideRotation)
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField (m_ShowOverrideRotationProperty);
            --EditorGUI.indentLevel;
        }


        GUI.enabled = useScale;
        EditorGUILayout.PropertyField (m_ShowScaleProperty, new GUIContent ("Scale"));
        if (overrideScale)
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField (m_ShowOverrideScaleProperty);
            --EditorGUI.indentLevel;
        }


        GUI.enabled = useAlpha;
        EditorGUILayout.PropertyField (m_ShowAlpha, new GUIContent ("Alpha"));
        if (overrideAlpha)
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField (m_ShowAlphaAnimation, new GUIContent("a"), GUILayout.Height (2 * EditorGUIUtility.singleLineHeight));
            --EditorGUI.indentLevel;
        }
        GUI.enabled = true;
    }

    public void DrawHideSectionParams ()
    {


        var mask = m_AnimatablePropertiesProperty.intValue;
        bool usePosition = mask.MaskHasLayer (AnimatableProperty.Position);
        bool useRotation = mask.MaskHasLayer (AnimatableProperty.Rotation);
        bool useScale = mask.MaskHasLayer (AnimatableProperty.Scale);
        bool useAlpha = mask.MaskHasLayer (AnimatableProperty.Alpha);

        var overrideMask = m_HideOverrideAnimationProperty.intValue;
        bool overridePosition = overrideMask.MaskHasLayer (AnimatableProperty.Position);
        bool overrideRotation = overrideMask.MaskHasLayer (AnimatableProperty.Rotation);
        bool overrideScale = overrideMask.MaskHasLayer (AnimatableProperty.Scale);
        bool overrideAlpha = overrideMask.MaskHasLayer (AnimatableProperty.Alpha);



        EditorGUILayout.PropertyField (m_HideDurationProperty, new GUIContent ("Duration"));



        EditorGUILayout.PropertyField (m_HideEaseTypeProperty, new GUIContent ("Ease"));
        var currentEaseType = (EaseType) m_HideEaseTypeProperty.enumValueIndex;
        if (m_HideEaseType != currentEaseType && currentEaseType != EaseType.Custom)
        {
            m_HideAnimationProperty.animationCurveValue = EaseCurve[currentEaseType];
            m_HideEaseType = currentEaseType;
        }
        GUI.enabled = currentEaseType == EaseType.Custom;
        EditorGUILayout.PropertyField (m_HideAnimationProperty, new GUIContent ("Animation"), GUILayout.Height (3 * EditorGUIUtility.singleLineHeight));
        EditorGUILayout.Space ();
        GUI.enabled = true;



        int showableMaskProperties = 0;
        if (usePosition)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Position);
        if (useRotation)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Rotation);
        if (useScale)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Scale);
        if (useAlpha)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Alpha);
        showableMaskProperties = EditorGUILayout.MaskField ("Properties to Animate", showableMaskProperties, new string[] { "Position", "Rotation", "Scale", "Alpha" });
        m_AnimatablePropertiesProperty.intValue = showableMaskProperties;



        showableMaskProperties = 0;
        if (overridePosition)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Position);
        if (overrideRotation)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Rotation);
        if (overrideScale)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Scale);
        if(overrideAlpha)
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Alpha);
        showableMaskProperties = EditorGUILayout.MaskField ("Overwrite Animation", showableMaskProperties, new string[] { "Position", "Rotation", "Scale", "Alpha" });
        m_HideOverrideAnimationProperty.intValue = showableMaskProperties;



        GUI.enabled = usePosition;
        EditorGUILayout.PropertyField (m_HideOffsetPositionProperty, new GUIContent ("Offset Position"));
        if (overridePosition)
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField (m_HideOverridePositionProperty);
            --EditorGUI.indentLevel;
        }


        GUI.enabled = useRotation;
        EditorGUILayout.PropertyField (m_HideOffsetRositionProperty, new GUIContent ("Offset Rotation"));
        if (overrideRotation)
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField (m_HideOverrideRotationProperty);
            --EditorGUI.indentLevel;
        }

        GUI.enabled = useScale;
        EditorGUILayout.PropertyField (m_HideScaleProperty, new GUIContent ("Scale"));
        if (overrideScale)
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField (m_HideOverrideScaleProperty);
            --EditorGUI.indentLevel;
        }


        GUI.enabled = useAlpha;
        EditorGUILayout.PropertyField (m_HideAlpha, new GUIContent ("Alpha"));
        if (overrideAlpha)
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField (m_HideAlphaAnimation, new GUIContent ("a"), GUILayout.Height (2 * EditorGUIUtility.singleLineHeight));
            --EditorGUI.indentLevel;
        }
        GUI.enabled = true;
    }















    public void DrawShowableParams ()
    {
        EditorGUILayout.Space ();
        EditorGUILayout.Space ();
        EditorGUILayout.PropertyField (m_UseScaledTimeProperty);
        EditorGUILayout.PropertyField (m_IgnoreParentProperty);
        EditorGUILayout.PropertyField (m_OverrideDelayProperty);
        if(m_OverrideDelayProperty.boolValue)
            EditorGUILayout.PropertyField (m_DelayProperty);

        EditorGUILayout.Space ();
        EditorGUILayout.Space ();
        EditorGUILayout.Space ();

        EditorGUILayout.LabelField ("Show State Parameters", EditorStyles.boldLabel);
        ++EditorGUI.indentLevel;
        DrawShowShowableParams (m_AnimatablePropertiesProperty.intValue);
        --EditorGUI.indentLevel;

        EditorGUILayout.Space ();
        EditorGUILayout.Space ();
        EditorGUILayout.Space ();

        EditorGUILayout.LabelField ("Hide State Parameters", EditorStyles.boldLabel);
        ++EditorGUI.indentLevel;
        DrawHideShowableParams (m_AnimatablePropertiesProperty.intValue);
        --EditorGUI.indentLevel;
    }




    public void DrawShowShowableParams (int mask)
    {

        var overrideMask = m_ShowOverrideAnimationProperty.intValue;
        bool overridePosition = overrideMask.MaskHasLayer (AnimatableProperty.Position);
        bool overrideRotation = overrideMask.MaskHasLayer (AnimatableProperty.Rotation);
        bool overrideScale = overrideMask.MaskHasLayer (AnimatableProperty.Scale);

        EditorGUILayout.PropertyField (m_ShowDurationProperty, new GUIContent ("Duration"));


        EditorGUILayout.PropertyField (m_ShowEaseTypeProperty, new GUIContent ("Ease"));
        var currentEaseType = (EaseType) m_ShowEaseTypeProperty.enumValueIndex;

        if (m_ShowEaseType != currentEaseType && currentEaseType != EaseType.Custom)
        {
            m_ShowAnimationProperty.animationCurveValue = EaseCurve[currentEaseType];
            m_ShowEaseType = currentEaseType;
        }

        GUI.enabled = currentEaseType == EaseType.Custom;
        EditorGUILayout.PropertyField (m_ShowAnimationProperty, new GUIContent ("Animation"), GUILayout.Height (3 * EditorGUIUtility.singleLineHeight));

        EditorGUILayout.Space ();

        GUI.enabled = true;


        int showableMaskProperties = 0;
        if (mask.MaskHasLayer (AnimatableProperty.Position))
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Position);
        if (mask.MaskHasLayer (AnimatableProperty.Rotation))
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Rotation);
        if (mask.MaskHasLayer (AnimatableProperty.Scale))
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Scale);
        showableMaskProperties = EditorGUILayout.MaskField ("Properties to Animate", showableMaskProperties, new string[] { "Position", "Rotation", "Scale" });
        m_AnimatablePropertiesProperty.intValue = showableMaskProperties;


        int maskOverride = m_ShowOverrideAnimationProperty.intValue;
        showableMaskProperties = 0;
        if (maskOverride.MaskHasLayer (AnimatableProperty.Position))
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Position);
        if (maskOverride.MaskHasLayer (AnimatableProperty.Rotation))
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Rotation);
        if (maskOverride.MaskHasLayer (AnimatableProperty.Scale))
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Scale);
        showableMaskProperties = EditorGUILayout.MaskField ("Overwrite Animation", showableMaskProperties, new string[] { "Position", "Rotation", "Scale" });
        m_ShowOverrideAnimationProperty.intValue = showableMaskProperties;


        GUI.enabled = mask.MaskHasLayer (AnimatableProperty.Position);
        EditorGUILayout.PropertyField (m_ShowOffsetPositionProperty, new GUIContent ("Offset Position"));
        if (overridePosition)
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField (m_ShowOverridePositionProperty);
            --EditorGUI.indentLevel;
        }


        GUI.enabled = mask.MaskHasLayer (AnimatableProperty.Rotation);
        EditorGUILayout.PropertyField (m_ShowOffsetRositionProperty, new GUIContent ("Offset Rotation"));
        if (overrideRotation)
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField (m_ShowOverrideRotationProperty);
            --EditorGUI.indentLevel;
        }

        GUI.enabled = mask.MaskHasLayer (AnimatableProperty.Scale);
        EditorGUILayout.PropertyField (m_ShowScaleProperty, new GUIContent ("Scale"));
        if (overrideScale)
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField (m_ShowOverrideScaleProperty);
            --EditorGUI.indentLevel;
        }

        GUI.enabled = true;
    }

    public void DrawHideShowableParams (int mask)
    {
        var overrideMask = m_HideOverrideAnimationProperty.intValue;
        bool overridePosition = overrideMask.MaskHasLayer (AnimatableProperty.Position);
        bool overrideRotation = overrideMask.MaskHasLayer (AnimatableProperty.Rotation);
        bool overrideScale = overrideMask.MaskHasLayer (AnimatableProperty.Scale);

        EditorGUILayout.PropertyField (m_HideDurationProperty, new GUIContent ("Duration"));


        EditorGUILayout.PropertyField (m_HideEaseTypeProperty, new GUIContent ("Ease"));
        var currentEaseType = (EaseType) m_HideEaseTypeProperty.enumValueIndex;

        if (m_HideEaseType != currentEaseType && currentEaseType != EaseType.Custom)
        {
            m_HideAnimationProperty.animationCurveValue = EaseCurve[currentEaseType];
            m_HideEaseType = currentEaseType;
        }

        GUI.enabled = currentEaseType == EaseType.Custom;
        EditorGUILayout.PropertyField (m_HideAnimationProperty, new GUIContent ("Animation"), GUILayout.Height (3 * EditorGUIUtility.singleLineHeight));

        EditorGUILayout.Space ();

        GUI.enabled = true;




        int showableMaskProperties = 0;
        if (mask.MaskHasLayer (AnimatableProperty.Position))
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Position);
        if (mask.MaskHasLayer (AnimatableProperty.Rotation))
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Rotation);
        if (mask.MaskHasLayer (AnimatableProperty.Scale))
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Scale);
        showableMaskProperties = EditorGUILayout.MaskField ("Properties to Animate", showableMaskProperties, new string[] { "Position", "Rotation", "Scale" });
        m_AnimatablePropertiesProperty.intValue = showableMaskProperties;


        int maskOverride = m_HideOverrideAnimationProperty.intValue;
        showableMaskProperties = 0;
        if (maskOverride.MaskHasLayer (AnimatableProperty.Position))
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Position);
        if (maskOverride.MaskHasLayer (AnimatableProperty.Rotation))
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Rotation);
        if (maskOverride.MaskHasLayer (AnimatableProperty.Scale))
            showableMaskProperties.AddLayerToMask (AnimatableProperty.Scale);
        showableMaskProperties = EditorGUILayout.MaskField ("Overwrite Animation", showableMaskProperties, new string[] { "Position", "Rotation", "Scale" });
        m_HideOverrideAnimationProperty.intValue = showableMaskProperties;



        GUI.enabled = mask.MaskHasLayer (AnimatableProperty.Position);
        EditorGUILayout.PropertyField (m_HideOffsetPositionProperty, new GUIContent ("Offset Position"));
        if (overridePosition)
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField (m_HideOverridePositionProperty);
            --EditorGUI.indentLevel;
        }


        GUI.enabled = mask.MaskHasLayer (AnimatableProperty.Rotation);
        EditorGUILayout.PropertyField (m_HideOffsetRositionProperty, new GUIContent ("Offset Rotation"));
        if (overrideRotation)
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField (m_HideOverrideRotationProperty);
            --EditorGUI.indentLevel;
        }

        GUI.enabled = mask.MaskHasLayer (AnimatableProperty.Scale);
        EditorGUILayout.PropertyField (m_HideScaleProperty, new GUIContent ("Scale"));
        if (overrideScale)
        {
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField (m_HideOverrideScaleProperty);
            --EditorGUI.indentLevel;
        }

        GUI.enabled = true;
    }









    /// <summary> Dictionary of ease curve animations. </summary>
    private static readonly Dictionary<EaseType, AnimationCurve>
    EaseCurve = new Dictionary<EaseType, AnimationCurve> {
        { EaseType.None, EaseAnimationCurves.Zero },
        { EaseType.Zero, EaseAnimationCurves.Zero },
        { EaseType.One, EaseAnimationCurves.One },
        { EaseType.Linear, EaseAnimationCurves.Linear },
        { EaseType.QuadOut, EaseAnimationCurves.QuadOut },
        { EaseType.QuadIn, EaseAnimationCurves.QuadIn },
        { EaseType.QuadInOut, EaseAnimationCurves.QuadInOut },
        { EaseType.QuadOutIn, EaseAnimationCurves.QuadOutIn },
        { EaseType.ExpoOut, EaseAnimationCurves.ExpoOut },
        { EaseType.ExpoIn, EaseAnimationCurves.ExpoIn },
        { EaseType.ExpoInOut, EaseAnimationCurves.ExpoInOut },
        { EaseType.ExpoOutIn, EaseAnimationCurves.ExpoOutIn },
        { EaseType.CubicOut, EaseAnimationCurves.CubicOut },
        { EaseType.CubicIn, EaseAnimationCurves.CubicIn },
        { EaseType.CubicInOut, EaseAnimationCurves.CubicInOut },
        { EaseType.CubicOutIn, EaseAnimationCurves.CubicOutIn },
        { EaseType.QuartOut, EaseAnimationCurves.QuartOut },
        { EaseType.QuartIn, EaseAnimationCurves.QuartIn },
        { EaseType.QuartInOut, EaseAnimationCurves.QuartInOut },
        { EaseType.QuartOutIn, EaseAnimationCurves.QuartOutIn },
        { EaseType.QuintOut, EaseAnimationCurves.QuintOut },
        { EaseType.QuintIn, EaseAnimationCurves.QuintIn },
        { EaseType.QuintInOut, EaseAnimationCurves.QuintInOut },
        { EaseType.QuintOutIn, EaseAnimationCurves.QuintOutIn },
        { EaseType.CircOut, EaseAnimationCurves.CircOut },
        { EaseType.CircIn, EaseAnimationCurves.CircIn },
        { EaseType.CircInOut, EaseAnimationCurves.CircInOut },
        { EaseType.CircOutIn, EaseAnimationCurves.CircOutIn },
        { EaseType.SineOut, EaseAnimationCurves.SineOut },
        { EaseType.SineIn, EaseAnimationCurves.SineIn },
        { EaseType.SineInOut, EaseAnimationCurves.SineInOut },
        { EaseType.SineOutIn, EaseAnimationCurves.SineOutIn },
        { EaseType.ElasticOut, EaseAnimationCurves.ElasticOut },
        { EaseType.ElasticIn, EaseAnimationCurves.ElasticIn },
        { EaseType.ElasticInOut, EaseAnimationCurves.ElasticInOut },
        { EaseType.ElasticOutIn, EaseAnimationCurves.ElasticOutIn },
        { EaseType.BounceOut, EaseAnimationCurves.BounceOut },
        { EaseType.BounceIn, EaseAnimationCurves.BounceIn },
        { EaseType.BounceInOut, EaseAnimationCurves.BounceInOut },
        { EaseType.BounceOutIn, EaseAnimationCurves.BounceOutIn },
        { EaseType.BackOut, EaseAnimationCurves.BackOut },
        { EaseType.BackIn, EaseAnimationCurves.BackIn },
        { EaseType.BackInOut, EaseAnimationCurves.BackInOut },
        { EaseType.BackOutIn, EaseAnimationCurves.BackOutIn },
        { EaseType.Custom, null }
    };

}