using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneVideo : MonoBehaviour
{
    bool canInteract;

    void Start()
    {
        Invoke("Init", 2);
        Invoke("Goto", 20);
    }
    private void Update()
    {
        if (!canInteract)
            return;

        if (Input.GetKeyDown(KeyCode.Q)
            || Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.O)
            || Input.GetKeyDown(KeyCode.P)
            )
            Goto();
    }
    void Init()
    {
        canInteract = true;
    }
    void Goto()
    {
        canInteract = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Standalone_03Game");
    }
}
