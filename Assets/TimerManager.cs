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
		string prefix = "00:";
		if (num < 10)
			prefix += "0";
		field.text = prefix + System.Math.Round(num,2);

		timer += Time.deltaTime;
		if(timer>=totalTimer)
		{
			timer = 0;
			isOn = false;
			StopAllCoroutines ();
			GetComponent<Trivia> ().TimeOver ();
		}
		bar.fillAmount = 1-(timer / totalTimer);

    }
}
