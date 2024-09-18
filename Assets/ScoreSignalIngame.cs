using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSignalIngame : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TMPro.TMP_Text field;
    [SerializeField] Image bg;
    [SerializeField] Animation anim;


    void Start()
    {
        Reset();
        Events.OnFinalScoreByRonda += OnFinalScoreByRonda;
    }

    void Update()
    {
        Events.OnFinalScoreByRonda += OnFinalScoreByRonda;
    }

    private void OnFinalScoreByRonda(int score, Vector2 pos)
    {
        CancelInvoke();
        panel.SetActive(true);
        panel.transform.position = pos;
        Color c;
        if (score > 0)
        {
            anim.Play("on");
            field.text = "+" + Mathf.Abs(score);
            c = Data.Instance.settings.buttonOkColor;
        }
        else
        {
            anim.Play("on_wrong");
            field.text = "-" + Mathf.Abs(score);
            c = Data.Instance.settings.buttonWrongColor;
        }
        c.a = 0.8f;
        bg.color = c;
        Invoke("Reset", 1.5f);
    }
    void Reset()
    {
        panel.SetActive(false);

    }
}
