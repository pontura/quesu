using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandalonePlayerInputsUI : MonoBehaviour
{
    public StandalonePlayerInputUI[] buttons;

    void Start()
    {
        if (Data.Instance.format == Data.formats.STANDALONE)
            Events.OnStandaloneKeyDown += OnStandaloneKeyDown;
    }
    void OnDestroy()
    {
        if (Data.Instance.format == Data.formats.STANDALONE)
            Events.OnStandaloneKeyDown -= OnStandaloneKeyDown;
    }
    void OnStandaloneKeyDown(StandaloneInputManager.types type)
    {
        foreach(StandalonePlayerInputUI button in buttons)
        {
            if (button.type == type)
                button.Clicked();
        }
    }
}
