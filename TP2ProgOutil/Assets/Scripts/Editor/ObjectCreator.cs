using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectCreator : EditorWindow
{
    [MenuItem("Tools/ObjectCreator")]
    private static void Init()
    {
        GetWindow<ObjectCreator>().Show();
    }

    private enum Direction
	{
		Up,
		Down,
		Left,
		Right,
		Forward,
		Back
	}

    private Transform m_StartPosition;
    private PrimitiveType m_PrimitiveToCreate;

    private string m_NameOfObjectCreated;
    private bool m_UseColor;
    private Color m_StartColor;
    private Color m_EndColor;
    private int m_NumberToCreate;
    private float m_Spacing;
    private Direction m_DirectionToSpawn;
    private bool m_AutoCenter;
    private bool m_UseLocalRotation;
    private GameObject m_CustomObject = null;
    private bool m_Custom;
    private List<GameObject> m_Instanciated;

    public void OnGUI()
    {
        //Box Position
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Position", EditorStyles.centeredGreyMiniLabel);
        GUI.color = Color.yellow;
        EditorGUILayout.BeginVertical(GUI.skin.box);
        GUI.color = Color.black;
        EditorGUILayout.LabelField("If non is assigned, then the start position will be (0f,0f,0f)");
        GUI.color = Color.white;
        EditorGUILayout.EndVertical();
        m_StartPosition = (Transform)EditorGUILayout.ObjectField("Start Position", m_StartPosition, typeof(Transform), true);
        EditorGUILayout.EndVertical();

        //Box Object Selection
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Object Selection", EditorStyles.centeredGreyMiniLabel);
        GUI.color = Color.cyan;
        if (GUILayout.Button("Use Custom Object"))
        {
            m_Custom = !m_Custom;
            m_CustomObject = null;
        }
        if (m_Custom)
        {
            m_CustomObject = (GameObject)EditorGUILayout.ObjectField("Custom Object", m_CustomObject, typeof(GameObject), true);
        }

        GUI.color = Color.white;
        m_PrimitiveToCreate = (PrimitiveType)EditorGUILayout.EnumPopup("Primitive Type", m_PrimitiveToCreate);
        EditorGUILayout.EndVertical();

        //Creation Parameter
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Creation Parameters", EditorStyles.centeredGreyMiniLabel);
        m_NameOfObjectCreated = EditorGUILayout.TextField("Name", m_NameOfObjectCreated);
        m_UseColor = EditorGUILayout.Toggle("Use Color?", m_UseColor);

        EditorGUI.BeginDisabledGroup(!m_UseColor);
        m_StartColor = EditorGUILayout.ColorField("Start Color", m_StartColor);
        m_EndColor = EditorGUILayout.ColorField("End Color", m_EndColor);
        EditorGUI.EndDisabledGroup();

        m_NumberToCreate = EditorGUILayout.IntField("NB To Create", m_NumberToCreate);
        m_Spacing = EditorGUILayout.FloatField("Spacing", m_Spacing);
        m_DirectionToSpawn = (Direction)EditorGUILayout.EnumPopup("Axis Direction", m_DirectionToSpawn);
        m_AutoCenter = EditorGUILayout.Toggle("Auto-Center", m_AutoCenter);

        EditorGUI.BeginDisabledGroup(m_StartPosition == null);
        m_UseLocalRotation = EditorGUILayout.Toggle("Use Local Rotation?", m_UseLocalRotation);
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.EndVertical();

        //Buton Create
        GUI.color = Color.green;
        if (GUILayout.Button("Create",GUILayout.Height(40f)))
        {
            GameObject Parent = new GameObject("Parent");
            for (int i = 0; i < m_NumberToCreate; i++)
            {
                GameObject CreatedObject;
                if (m_CustomObject != null)
                {
                    CreatedObject = Instantiate(m_CustomObject);

                }
                else
                {
                    CreatedObject = GameObject.CreatePrimitive(m_PrimitiveToCreate);
                }
                CreatedObject.transform.parent = Parent.transform;
                Undo.RegisterCreatedObjectUndo(CreatedObject, "UndoCreatedObject");

                Vector3 temp;

                temp = m_StartPosition == null ? Vector3.zero : m_StartPosition.position;

                switch (m_DirectionToSpawn)
                {
                    case Direction.Up:
                        temp.y += i * m_Spacing;
                        break;
                    case Direction.Down:
                        temp.y -= i * m_Spacing;
                        break;
                    case Direction.Left:
                        temp.x -= i * m_Spacing;
                        break;
                    case Direction.Right:
                        temp.x += i * m_Spacing;
                        break;
                    case Direction.Forward:
                        temp.z += i * m_Spacing;
                        break;
                    case Direction.Back:
                        temp.z -= i * m_Spacing;
                        break;
                    default:
                        break;
                }
                CreatedObject.transform.position = temp;
                CreatedObject.AddComponent<Environment>();

                if (m_StartPosition != null && m_UseLocalRotation)
                {
                    CreatedObject.transform.rotation = m_StartPosition.rotation;
                }

                if (m_NameOfObjectCreated != "")
                {
                    CreatedObject.name = m_NameOfObjectCreated;
                }
                if (m_UseColor)
                {
                    CreatedObject.GetComponent<Renderer>().material.color = Color.Lerp(m_StartColor, m_EndColor, ((float)i / (m_NumberToCreate - 1)));
                }
            }
            Selection.activeGameObject = Parent;
        }
        
        GUI.color = Color.white;

        //Undo.RegisterCreatedObjectUndo( , "UndoCreatedObject");
        //Undo.RecordObject();
    }
}
