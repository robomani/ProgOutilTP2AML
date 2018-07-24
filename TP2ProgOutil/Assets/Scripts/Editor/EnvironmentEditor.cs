using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Environment))]
public class EnvironmentEditor : Editor 
{
    private Environment m_Environment;
    private SerializedProperty m_HP;
    private SerializedProperty m_Offset;
    private SerializedProperty m_Rotation;
    private SerializedProperty m_Scale;

    private float m_MinOffset;
    private float m_MaxOffset;
    private float m_MinRotation;
    private float m_MaxRotation;
    private Vector3 m_MinScale;
    private Vector3 m_MaxScale;


    private void OnEnable()
    {
        m_HP = serializedObject.FindProperty("m_HP");
        m_Offset = serializedObject.FindProperty("m_Offset");
        m_Rotation = serializedObject.FindProperty("m_Rotation");
        m_Scale = serializedObject.FindProperty("m_Scale");
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Random Position Offset", EditorStyles.centeredGreyMiniLabel);
        m_Offset.floatValue = EditorGUILayout.FloatField("Current Offset", m_Offset.floatValue);
        m_MinOffset = EditorGUILayout.FloatField("Min Offset", m_MinOffset);
        m_MaxOffset = EditorGUILayout.FloatField("Max Offset", m_MaxOffset);
        if (GUILayout.Button("Random Offset", GUI.skin.button))
        {
            m_Offset.floatValue = Random.Range(m_MinOffset, m_MaxOffset);
        }
        GUILayout.EndVertical();

        GUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Random Rotation", EditorStyles.centeredGreyMiniLabel);
        m_Rotation.floatValue = EditorGUILayout.FloatField("Current Rotation", m_Rotation.floatValue);
        m_MinRotation = EditorGUILayout.FloatField("Min Rotation", m_MinRotation);
        m_MaxRotation = EditorGUILayout.FloatField("Max Rotation", m_MaxRotation);
        if (GUILayout.Button("Random Rotation", GUI.skin.button))
        {
            m_Rotation.floatValue = Random.Range(m_MinRotation, m_MaxRotation);
        }
        GUILayout.EndVertical();

        GUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Random Scale", EditorStyles.centeredGreyMiniLabel);
        m_Scale.vector3Value = EditorGUILayout.Vector3Field("Current Scale", m_Scale.vector3Value);
        m_MinScale = EditorGUILayout.Vector3Field("Min Scale", m_MinScale);
        m_MaxScale = EditorGUILayout.Vector3Field("Max Scale", m_MaxScale);
        if (GUILayout.Button("Random Scale", GUI.skin.button))
        {
            float tempX = Random.Range(m_MinScale.x, m_MaxScale.x); 
            float tempY = Random.Range(m_MinScale.y, m_MaxScale.y); 
            float tempZ = Random.Range(m_MinScale.z, m_MaxScale.z); 
            m_Scale.vector3Value = new Vector3(tempX, tempY, tempZ);
        }
        GUILayout.EndVertical();

        m_HP.floatValue = EditorGUILayout.Slider("Health", m_HP.floatValue, 0f, 100f);
        DrawPropertiesExcluding(serializedObject, "m_HP", "m_Scale", "m_Rotation", "m_Offset");
        serializedObject.ApplyModifiedProperties();
    }

    // Pour Afficher l'inspecteur par defaut sauf certains parametres, veuillez utiliser DrawPropertiesExcluding(serailizedObject, "m_MyVariable");
}
