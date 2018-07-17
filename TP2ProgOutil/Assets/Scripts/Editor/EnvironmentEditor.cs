using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Environment))]
public class EnvironmentEditor : Editor 
{
    private Environment m_Environment;


    private void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {

        //serializedObject.ApplyModifiedProperties;
    }

    // Pour Afficher l'inspecteur par defaut sauf certains parametres, veuillez utiliser DrawPropertiesExcluding(serailizedObject, "m_MyVariable");
}
