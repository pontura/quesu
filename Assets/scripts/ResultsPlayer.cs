using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsPlayer : MonoBehaviour
{
    public Image winImage;
    public Text scoreField;
    public Text timeField;
    public Animation anim;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Init(int win, int score, float time)
    {
        gameObject.SetActive(true);
        if (win == 2)
            anim.Play("winHand");
        else if (win == 1)
            anim.Play("winHand2");
        else
            anim.Play("loseHand");

        if(score == 0)
            scoreField.text = "";
        else
            scoreField.text = score.ToString();

        string prefix = "00:";
        if (time < 10)
            prefix += "0";
        timeField.text = prefix + System.Math.Round(time, 2);
    }
}
