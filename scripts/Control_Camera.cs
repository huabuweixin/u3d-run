using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Camera : MonoBehaviour
{
    //间隔距离
    public float m_DistanceAway = 5f;
    //间隔高度
    public float m_DistanceHeight = 10f;
    //平滑值
    public float smooth = 2f;
    //目标点
    private Vector3 m_TargetPosition;
    //参照点
    Transform m_Follow;

    void Start()
    {
        m_Follow = GameObject.Find("character").transform;
    }

    void LateUpdate()
    {
        m_TargetPosition = m_Follow.position + Vector3.up * m_DistanceHeight - m_Follow.forward * m_DistanceAway;
        transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Time.deltaTime * smooth);
    }
}

