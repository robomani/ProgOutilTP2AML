using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField]
    private float m_HP = 50;
    [SerializeField]
    private float m_Offset = 0f;
    [SerializeField]
    private Vector3 m_Scale = Vector3.one;
    [SerializeField]
    private float m_Rotation = 0f;

    Quaternion temp;

    public bool m_ShowBounds = true;
    public Color m_Color = Color.white;

    private void OnDrawGizmos()
    {
        if (m_ShowBounds)
        {
            Gizmos.color = m_Color;
            
            temp.eulerAngles = transform.rotation.eulerAngles + Vector3.one * m_Rotation;
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, temp, transform.lossyScale);
            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(transform.position + m_Offset * Vector3.one, m_Scale);
        }
    }



}
