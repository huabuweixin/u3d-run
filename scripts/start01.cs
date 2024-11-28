using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start01 : MonoBehaviour
{
    public void OnClickLoadScene()
    {
        SceneManager.LoadScene("Guanqia01Scene");
    }
}
