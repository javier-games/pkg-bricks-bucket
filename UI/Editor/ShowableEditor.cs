using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (Showable), true)]
public class ShowableEditor : Editor
{

    SerializedProperty m_Parent;
    SerializedProperty m_Settings;
    SerializedProperty m_UseScaled_Time;
    SerializedProperty m_IgnoreParent;
    SerializedProperty m_OverrideDelay;
    SerializedProperty m_Delay;


    protected virtual void OnEnable ()
    {
        m_Parent = serializedObject.FindProperty ("_parent");
        m_Settings = serializedObject.FindProperty ("_settings");
        m_UseScaled_Time = serializedObject.FindProperty ("_useScaledTime");
        m_IgnoreParent = serializedObject.FindProperty ("_ignoreParent");
        m_OverrideDelay = serializedObject.FindProperty ("_overrideDelay");
        m_Delay = serializedObject.FindProperty ("_delay");
    }


    public override void OnInspectorGUI ()
    {

        serializedObject.Update ();


        EditorGUILayout.Space ();
        GUI.enabled = false;
        EditorGUILayout.PropertyField (m_Parent);
        GUI.enabled = true;
        EditorGUILayout.PropertyField (m_Settings);




        Showable showable = target as Showable;

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

            if(m_OverrideDelay.boolValue)
                EditorGUILayout.PropertyField (m_Delay);

        }

        if (m_Settings.objectReferenceValue != null)
        {
            --EditorGUI.indentLevel;
        }

        EditorGUILayout.Space ();



        serializedObject.ApplyModifiedProperties ();
    }
}
