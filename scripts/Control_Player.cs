using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Control_Player : MonoBehaviour
{
    //前进速度
    public float m_ForwardSpeeed = 7.0f;
    //动画组件
    private Animator m_Anim;
    //动画现在状态
    private AnimatorStateInfo m_CurrentBaseState;

    //动画状态参照
    static int m_jumpState = Animator.StringToHash("Base Layer.jump");
    static int m_slideState = Animator.StringToHash("Base Layer.slide");
    bool m_IsEnd = false;
    bool end = false;
    Control_Scenes m_ControlScenes;
    public  GameObject road;
    Vector3 startPosition;
    private float m_Score;
    private float m_LastTime;
    // Use this for initialization
    void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_ControlScenes = GameObject.Find("controlscene").GetComponent<Control_Scenes>();
        m_Score = 0;
        m_LastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * m_ForwardSpeeed * Time.deltaTime;
        m_CurrentBaseState = m_Anim.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_Anim.SetBool("jump", true);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            m_Anim.SetBool("slide", true);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Change_PlayerZ(true);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Change_PlayerZ(false);
        }
        if (m_CurrentBaseState.fullPathHash == m_jumpState)
        {
            m_Anim.SetBool("jump", false);
        }
        else if (m_CurrentBaseState.fullPathHash == m_slideState)
        {
            m_Anim.SetBool("slide", false);
        }
        if (!m_IsEnd&&!end)
        {
            if (Time.time - m_LastTime >= 1.0f)
            {
                m_Score++;
                m_LastTime = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); // 退出游戏
        }
    }

    public void Change_PlayerZ(bool IsAD)
    {
        if (IsAD)
        {
            if (transform.position.x == -6.22f)
                return;
            else if (transform.position.x == -2.61f)
            {
                transform.position = new Vector3(-6.22f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(-2.61f, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (transform.position.x == 1.41f)
                return;
            else if (transform.position.x == -2.61f)
            {
                transform.position = new Vector3(1.41f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(-2.61f, transform.position.y,transform.position.z);
            }

        }
    }
    void OnGUI()
    {
        if (m_IsEnd)
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 40;
            style.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "你输了~", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 50), "重新开始"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
            }
        }
        if (end)
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 40;
            style.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "恭喜你到达终点！", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 50), "选择关卡"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
            }
        }
   
        // 显示分数
        GUIStyle scoreStyle = new GUIStyle();
        scoreStyle.alignment = TextAnchor.MiddleLeft;
        scoreStyle.fontSize = 30;
        scoreStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 30), "分数: " + m_Score, scoreStyle);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Vehicle_DumpTruck" || other.gameObject.name == "Vehicle_MixerTruck")
        {
            m_IsEnd = true;
            m_ForwardSpeeed = 0;
            m_Anim.SetBool("idle", true);
        }
        if (other.gameObject.name == "MonitorPos0")
        {
            end = true;
            m_ForwardSpeeed = 0;
            m_Anim.SetBool("idle", true);
            m_ControlScenes.Change_Road(0);
        }
        else if (other.gameObject.name == "MonitorPos1")
        {
            startPosition = other.transform.position;
            GenerateRoad(startPosition);
            m_ControlScenes.Change_Road(1);
        }
        else if (other.gameObject.name == "MonitorPos2")
        {
            m_ControlScenes.Change_Road(2);
        }
    }
    void GenerateRoad(Vector3 startPosition)
    {
        Vector3 roadPosition = startPosition + Vector3.forward * 0; 
        Quaternion roadRotation = Quaternion.identity; 
        Instantiate(road, roadPosition, roadRotation);
    }
}
