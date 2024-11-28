using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Scenes : MonoBehaviour
{
    public GameObject[] m_ObstacleArray;
    public Transform[] m_ObstaclePosArray;
    public GameObject[] m_RoadArray;
    private bool m_ISFirst;

    void Start()
    {
        Spawn_Obstacle(1);
    }

    public void Change_Road(int index)
    {
        if (m_ISFirst && index == 0)
        {
            m_ISFirst = false;
            return;
        }
        else
        {
            int lastIndex = index - 1;
            if (lastIndex < 0)
                lastIndex = 2;
            // 更新道路位置
            m_RoadArray[lastIndex].transform.position = m_RoadArray[lastIndex].transform.position - new Vector3(150, 0, 0);
            // 更新障碍物位置
            foreach (Transform item in m_ObstaclePosArray[lastIndex])
            {
                item.position = m_RoadArray[lastIndex].transform.position + new Vector3(0, 0, 0); 
            }
            Spawn_Obstacle(lastIndex);
        }
    }

    //生成障碍物
    public void Spawn_Obstacle(int index)
    {
        //销毁原来的对象
        GameObject[] obsPast = GameObject.FindGameObjectsWithTag("Obstacle" + index);
        for (int i = 0; i < obsPast.Length; i++)
        {
            Destroy(obsPast[i]);
        }
        //生成障碍物
        foreach (Transform item in m_ObstaclePosArray[index])
        {
            GameObject prefab = m_ObstacleArray[Random.Range(0, m_ObstacleArray.Length)];
            Vector3 eulerAngle = new Vector3(0, Random.Range(0, 360), 0);
            GameObject obj = Instantiate(prefab, item.position, Quaternion.Euler(eulerAngle));
            obj.tag = "Obstacle" + index;
        }
    }
}
