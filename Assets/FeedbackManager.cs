using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackManager : MonoBehaviour {
    public GameObject panel;
    public Text field;
    public int combos;

    void Start () {
        panel.SetActive (false);
        Events.OnAnswer += OnAnswer;
    }
    public void Init () {
        panel.SetActive (false);
    }
    public void Next () {
        Invoke ("Delayed", 0.1f);
    }
    void Delayed () {
        panel.SetActive (true);
        if (combos == 3) {
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
    }
}