using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingGame : MainScreen
{
    public TMPro.TMP_Text field;
    [SerializeField] Ranking ranking;
    [SerializeField] ButtonUI button;

    public override void OnInit()
    {
        Invoke("Go", 3);
    }
    void Go()
    {
        LoadScreen(screens.GAME, true);
    }
}