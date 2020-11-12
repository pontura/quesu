using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandaloneTimeManager : MonoBehaviour
{
    public Image bar;
    float timer;
    float lastTimer;
    public bool isOn;
    float totalTimer;
    public Text field;
    public int standalonePlayerID;
    public GameObject ready;
    StandaloneTrivia standaloneTrivia;
    public Animation readyAnim;

    private void Start()
    {
        field.text = "";
        ShowReady(false);
        bar.fillAmount = 1;
    }
    public void Init(StandaloneTrivia standaloneTrivia, float _totalTimer)
    {
        field.text = _totalTimer + "\"";
        readyAnim.gameObject.SetActive(false);
        this.standaloneTrivia = standaloneTrivia;
        this.totalTimer = _totalTimer;
        timer = 0;
        lastTimer = 0;
        ShowReady(false);
        readyAnim.gameObject.SetActive(false);
        field.color = Color.white;
    }
    public void SetState(bool _isOn)
    {
       // if (!_isOn)
       //      lastTimer = timer;
        this.isOn = _isOn;  
        if(!isOn)
            readyAnim.gameObject.SetActive(false);
    }
    public void ShowReady(bool isOn)
    {
        ready.SetActive(isOn);
    }
    void Update()
    {
        if (!isOn)
            return;

        float num = (totalTimer - timer);
        string prefix = ""; // "00:";
        if (num < 10)
            prefix += "0";
        field.text = prefix + System.Math.Round(num, 0);
        field.text += "\"";
        if (timer > totalTimer - 3)
        {
            readyAnim.gameObject.SetActive(true);
        }
        else
        {
            readyAnim.gameObject.SetActive(false);
        }

        timer += Time.deltaTime;

        

        if (timer < 0)
            timer = 0;
        if (timer >= totalTimer)
        {
            timer = 1;
            isOn = false;
            StopAllCoroutines();
            standaloneTrivia.TimeOver(standalonePlayerID);
            bar.fillAmount = 0;
            field.text = "¡TIEMPO!";
            readyAnim.gameObject.SetActive(true);
            readyAnim.Play("timeOverDone");
            field.color = Color.black;
        }
        else {
            SetBar();
        }
    }
    public void AddTime(float value)
    {
        timer -= value;
        SetBar();
    }
    void SetBar()
    {
        float v = 1 - (timer / totalTimer);
        if (v < 0)
            v = 0;
        else if (v > 1)
            v = 1;
        bar.fillAmount = v;
    }

    public float GetResult()
    {       
        float result = totalTimer - (timer - lastTimer);
        //print("lastTimer: " + lastTimer + "    timer :" + timer + "    result: " + result);
        lastTimer = timer;        
        return result;
    }
}
