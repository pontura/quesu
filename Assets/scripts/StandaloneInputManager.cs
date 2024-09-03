using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneInputManager : MonoBehaviour
{
    public enum types
    {       
        P1_LEFT,
        P1_RIGHT,
        P2_LEFT,
        P2_RIGHT,
        NONE
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            Events.OnStandaloneKeyDown(types.P1_LEFT);
        else if (Input.GetKeyDown(KeyCode.W))
            Events.OnStandaloneKeyDown(types.P1_RIGHT);

        if (Input.GetKeyDown(KeyCode.O))
            Events.OnStandaloneKeyDown(types.P2_LEFT);
        else if (Input.GetKeyDown(KeyCode.P))
            Events.OnStandaloneKeyDown(types.P2_RIGHT);
    }
}
