using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StandaloneScores : MainScreen
{

    public GameObject hand_P1;
    public GameObject hand_P2;

    public Text score1;
    public Text score2;

    public Text title;

    public override void OnEnabled()
    {

        MultiplayerData multiplayerData = UserData.Instance.GetComponent<MultiplayerData>();
        multiplayerData.SetWinner();

        hand_P1.SetActive(false);
        hand_P2.SetActive(false);

        int s1=  multiplayerData.score_player_1;
        int s2 = multiplayerData.score_player_2;

        score1.text = Utils.SetFormatedNumber(s1.ToString());
        score2.text = Utils.SetFormatedNumber(s2.ToString());

        if (s1 == s2 && s1 == 0)
        {
            title.text = "No hicieron ni un punto...";
            Invoke("GotoMain", 5);
            return;
        }

        if (s1 == s2)
        {
            title.text = "¡Empate!";
        } else if (multiplayerData.win == MultiplayerData.wins.PLAYER1)
        {
            hand_P1.SetActive(true);
        }
        else
        {
            hand_P2.SetActive(true);
        }
        Invoke("NextScreen", 5);
    }
    void GotoMain()
    {
        LoadScreen(0, false);
    }
    void NextScreen()
    {
        LoadScreen(4, true);
    }
}
