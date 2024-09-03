using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackManager : MonoBehaviour {

    public GameObject panel;
    public Text field;
    public int combos;
    bool timeOut;
    public Text scoreField;

    void Start () {
        panel.SetActive (false);
        Events.OnAnswer += OnAnswer;

        if (scoreField != null)
            Invoke("AddScore", 0.1f);
    }
    public void Init () {
        timeOut = false;
        panel.SetActive (false);
    }
    public void Next () {
        
        Invoke ("Delayed", 0.1f);
    }
    void Delayed () {        
        panel.SetActive (true);
        if (timeOut)
        {
            field.text = "¡Tiempo!";
            Invoke("Reset", 2);
            timeOut = false;
        } else if (combos == 3) {
            field.text = "¡Perfecto!";
            Invoke ("Reset", 2);
        } else if (combos == 2) {
            field.text = "Bien!";
            Invoke ("Reset", 2);
        } else {
            panel.SetActive (false);
        }
        combos = 0;
    }
    public void SetTimeOut()
    {
        timeOut = true;
    }
    void Reset () {
        if (panel != null)
            panel.SetActive (false);
    }
    void OnDestroy () {
        Events.OnAnswer -= OnAnswer;
    }

    void OnAnswer (bool isOk) {
        if (isOk)
            combos++;
        else
            combos--;
        if(scoreField != null)
        {
            Invoke("AddScore", 0.1f);
        }
    }
    void AddScore()
    {
        int score = Data.Instance.resultsData.score;
        scoreField.text = "PUNTOS: " + score;
    }
}