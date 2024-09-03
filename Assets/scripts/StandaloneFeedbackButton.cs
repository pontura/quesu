using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandaloneFeedbackButton : MonoBehaviour
{
    public GameObject empateImage;
    public GameObject player1;
    public GameObject player2;
    public types type;
    public enum types
    {
        LEFT,
        RIGHT
    }

    public void Reset()
    {
        empateImage.SetActive(false);
        player1.SetActive(false);
        player2.SetActive(false);
    }
    public void Init(StandaloneInputManager.types p1, StandaloneInputManager.types p2)
    {
        Reset();
        bool p1Active = false;
        bool p2Active = false;
        if (type == types.LEFT)
        {
            if (p1 == StandaloneInputManager.types.P1_LEFT)
                p1Active = true;
            if (p2 == StandaloneInputManager.types.P2_LEFT)
                p2Active = true;
        }
        else
        {
            if (p1 == StandaloneInputManager.types.P1_RIGHT)
                p1Active = true;
            if (p2 == StandaloneInputManager.types.P2_RIGHT)
                p2Active = true;
        }
      

        if (p1Active && p2Active)
        {
            empateImage.SetActive(true);
        } else if(p1Active)
        {
            player1.SetActive(true);
        }
        else if (p2Active)
        {
            player2.SetActive(true);
        }
    }
}
