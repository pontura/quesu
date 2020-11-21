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

		float num = (totalTimer - timer);
        string prefix = "";// = "00:";
		if (num < 10)
			prefix += "0";
		string t = prefix + System.Math.Round(num,2);
        field.text = t.Replace(",", ":");
        timer += Time.deltaTime;
        UpdateBar();
    }
    void UpdateBar()
    {
        if (timer >= totalTimer)
        {
            timer = 0;
            isOn = false;
            StopAllCoroutines();
            GetComponent<Trivia>().TimeOver();
        }
        bar.fillAmount = 1 - (timer / totalTimer);
    }
}
