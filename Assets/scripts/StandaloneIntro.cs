using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneIntro : MonoBehaviour
{
    void Start()
    {
        Invoke("Init", 0.5f);
    }
    
    void Init()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Standalone_02Video");
    }
}
