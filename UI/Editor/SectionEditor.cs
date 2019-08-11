using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor (typeof (Section), true)]
public class SectionEditor : Editor
{

    SerializedProperty m_Parent;
    SerializedProperty m_Settings;
    SerializedProperty m_UseScaled_Time;
    SerializedProperty m_IgnoreParent;
    SerializedProperty m_OverrideDelay;
    SerializedProperty m_Delay;

    SerializedProperty m_CanvasGroup;
    SerializedProperty m_Children;
    ReorderableList m_ReorderableList;

    SerializedProperty m_AutoShowChildren;
    SerializedProperty m_AutoHideChildren;
    SerializedProperty m_UseSequence;
    SerializedProperty m_ChildrenSequence;
    SerializedProperty m_SequenceDelay;


    protected virtual void OnEnable ()
    {
        m_Parent = serializedObject.FindProperty ("_parent");
        m_Settings = serializedObject.FindProperty ("_settings");
        m_UseScaled_Time = serializedObject.FindProperty ("_useScaledTime");
        m_IgnoreParent = serializedObject.FindProperty ("_ignoreParent");
        m_OverrideDelay = serializedObject.FindProperty ("_overrideDelay");
        m_Delay = serializedObject.FindProperty ("_delay");

        m_CanvasGroup = serializedObject.FindProperty ("_canvasGroup");
        m_Children = serializedObject.FindProperty ("_children");

        m_AutoShowChildren = serializedObject.FindProperty ("_autoShowChildren");
        m_AutoHideChildren = serializedObject.FindProperty ("_autoHideChildren");
        m_UseSequence = serializedObject.FindProperty ("_useSequence");
        m_ChildrenSequence = serializedObject.FindProperty ("_childrenSequence");
        m_SequenceDelay = serializedObject.FindProperty ("_sequenceDelay");

        m_ReorderableList = new ReorderableList (serializedObject, m_Children, true, true, false, false);
        m_ReorderableList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                GUI.enabled = false;
                var child = m_ReorderableList.serializedProperty.GetArrayElementAtIndex (index);
                EditorGUI.ObjectField (rect, child);
                GUI.enabled = true;
            };
        m_ReorderableList.drawHeaderCallback =
            (Rect rect) =>
            {
                EditorGUI.LabelField (rect, "Children");
            };
    }


    public override void OnInspectorGUI ()
    {

        serializedObject.Update ();


        EditorGUILayout.Space ();
        EditorGUILayout.PropertyField (m_Settings);
        GUI.enabled = false;
        EditorGUILayout.PropertyField (m_CanvasGroup);
        EditorGUILayout.PropertyField (m_Parent);
        GUI.enabled = true;
        EditorGUILayout.Space ();
        m_ReorderableList.DoLayoutList ();




        Section showable = target as Section;

        EditorGUILayout.Space ();
        EditorGUILayout.Space ();

        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button ("Show"))
            showable.Show ();
        if (GUILayout.Button ("Hide"))
            showable.Hide ();
        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.Space ();
        EditorGUILayout.Space ();




        if (m_Settings.objectReferenceValue != null)
        {
            EditorGUILayout.LabelField ("Override Settings Parameters", EditorStyles.boldLabel);
            ++EditorGUI.indentLevel;
        }

        EditorGUILayout.PropertyField (m_UseScaled_Time);

        if (m_Parent.objectReferenceValue != null)
        {

            EditorGUILayout.PropertyField (m_IgnoreParent);
            EditorGUILayout.PropertyField (m_OverrideDelay);

            if (m_OverrideDelay.boolValue)
                EditorGUILayout.PropertyField (m_Delay);
        }

        if (m_ReorderableList.count > 0)
        {

            EditorGUILayout.PropertyField (m_AutoShowChildren);
            EditorGUILayout.PropertyField (m_AutoHideChildren);
            EditorGUILayout.PropertyField (m_UseSequence);

            if (m_UseSequence.boolValue)
            {
                ++EditorGUI.indentLevel;
                EditorGUILayout.PropertyField (m_ChildrenSequence);
                EditorGUILayout.PropertyField (m_SequenceDelay);
                --EditorGUI.indentLevel;
            }
        }

        if (m_Settings.objectReferenceValue != null)
        {
            --EditorGUI.indentLevel;
        }

        EditorGUILayout.Space ();






        EditorGUILayout.Space ();



        serializedObject.ApplyModifiedProperties ();
        EditorUtility.SetDirty (target);
    }
}
