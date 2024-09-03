using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerData : MonoBehaviour
{
    public int score_player_1;
    public int score_player_2;
    public wins win;
    public enum wins
    {
        PLAYER1,
        PLAYER2
    }
    private void Start()
    {
        Events.OnResetTrivia += OnResetTrivia;
    }
    private void OnDestroy()
    {
        Events.OnResetTrivia -= OnResetTrivia;
    }
    void OnResetTrivia()
    {
        score_player_1 = score_player_2 = 0;
    }
    public void SetWinner()
    {
        if (score_player_1 > score_player_2)
            win = wins.PLAYER1;
        else
            win = wins.PLAYER2;
    }
    public int GetWinnerScore()
    {
        if (win == wins.PLAYER1)
            return score_player_1;
        else
            return score_player_2;
    }
}
