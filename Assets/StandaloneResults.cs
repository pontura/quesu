using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StandaloneResults : MainScreen
{
    public GameObject flash;
    public AspectRatioFitter aspectRatioFitter;
    public RawImage rawImage;
    public GameObject p1_arrow;
    public GameObject p2_arrow;
    public Text countDown;
    int countDownNum;
    int countDownNumInit = 3;
    MultiplayerData multiplayerData;
    public states state;

    public enum states
    {
        WAITING_TO_CLICK,
        COUNTDOWN,
        DONE
    }

    public GameObject hand_P1;
    public GameObject hand_P2;

    public Text score1;
    public Text score2;
    WebcamPhotoStandalone webcamPhotoStandalone;

    public override void OnEnabled()
    {
        flash.SetActive(false);
        webcamPhotoStandalone = GetComponent<WebcamPhotoStandalone>();
        countDown.text = "";
        multiplayerData = UserData.Instance.GetComponent<MultiplayerData>();
        multiplayerData.SetWinner();
        ResetArrows();

        hand_P1.SetActive(false);
        hand_P2.SetActive(false);

        score1.text = Utils.SetFormatedNumber(multiplayerData.score_player_1.ToString());
        score2.text = Utils.SetFormatedNumber(multiplayerData.score_player_2.ToString());

        if (multiplayerData.win == MultiplayerData.wins.PLAYER1)
        {
            hand_P1.SetActive(true);
            p1_arrow.SetActive(true);
        }
        else
        {
            hand_P2.SetActive(true);
            p2_arrow.SetActive(true);
        }

        webcamPhotoStandalone.InitWebcam(rawImage, aspectRatioFitter);
       
    }
    void ResetArrows()
    {
        p1_arrow.SetActive(false);
        p2_arrow.SetActive(false);
    }
    public override void OnStandaloneKeyDown(StandaloneInputManager.types type)
    {
        if (state != states.WAITING_TO_CLICK)
            return;

        if (multiplayerData.win == MultiplayerData.wins.PLAYER1 && (type == StandaloneInputManager.types.P1_LEFT || type == StandaloneInputManager.types.P1_RIGHT))
            Clicked();
        else 
        if (multiplayerData.win == MultiplayerData.wins.PLAYER2 && (type == StandaloneInputManager.types.P2_LEFT || type == StandaloneInputManager.types.P2_RIGHT))
            Clicked();
    }   
    void Clicked()
    {
        ResetArrows();
        countDownNum = countDownNumInit;
        state = states.COUNTDOWN;
        LoopCountDown();
    }
    void LoopCountDown()
    {       
        countDown.text = countDownNum.ToString();
        countDownNum--;
        if (countDownNum < 0)
            Done();
        else
            Invoke("LoopCountDown", 1);
    }
    void Done()
    {
        flash.SetActive(true);
        state = states.DONE;
        string score = multiplayerData.GetWinnerScore().ToString();

        score += "_" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" +  DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" +  DateTime.Now.Second;

        string filename = Application.streamingAssetsPath + "/photos/" + score + ".png";
        webcamPhotoStandalone.TakeSnapshot(null, filename);
        Invoke("OnDone", 2);
    }
    void OnDone()
    {
        LoadScreen(5, true);
        state = states.WAITING_TO_CLICK;
    }
}
