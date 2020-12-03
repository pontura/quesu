using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
	public Image bar;
	float timer;
	public bool isOn;
	float totalTimer;
	public Text field;
    public GameObject barAnimTimeDanger;

    private void Start()
    {
        Events.OnAnswer += OnAnswer;
    }
    private void OnDestroy()
    {
        Events.OnAnswer += OnAnswer;
    }
    void OnAnswer(bool iscorrect)
    {
        barAnimTimeDanger.SetActive(false);
        if (iscorrect)
            timer -= Data.Instance.settings.timeWin;
        else
            timer += Data.Instance.settings.timeLose;
        UpdateBar();
    }
    public void Init(float _totalTimer)
	{
		this.totalTimer = _totalTimer;
		timer = 0;
	}
	public void SetState(bool _isOn)
	{
		this.isOn = _isOn;
	}
    void Update()
    {
		if (!isOn)
			return;

		float num = (int)(totalTimer - timer);
        string prefix = "";// = "00:";
        if (num < 10)
        {
            prefix += "0";
            if (num < 5)
                barAnimTimeDanger.SetActive(true);
            else
                barAnimTimeDanger.SetActive(false);
        }

        string t = prefix + num; // System.Math.Round(num,2);
        field.text = t.Replace(",", ":");
        timer += Time.deltaTime;
        UpdateBar();
    }
    void UpdateBar()
    {
        if (timer >= totalTimer)
        {
            barAnimTimeDanger.SetActive(false);
            timer = 0;
            isOn = false;
            StopAllCoroutines();
            GetComponent<Trivia>().TimeOver();
        }
        bar.fillAmount = 1 - (timer / totalTimer);
    }
}
