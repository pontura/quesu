using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsData : MonoBehaviour
{
	public int answers_ok;
	public int answers_wrong;
	public int score;
	public int combos;

    void Start()
    {
		Events.OnAnswer += OnAnswer;
		Events.OnResetTrivia += OnResetTrivia;
		Events.OnCombo += OnCombo;
    }
	void OnDestroy()
	{
		Events.OnAnswer -= OnAnswer;
		Events.OnResetTrivia += OnResetTrivia;
		Events.OnCombo -= OnCombo;
	}
	void OnCombo(int value)
	{
		combos++;
		score += value * 10;
	}
	public int GetResults()
	{
		if (score < 1)
			return 0;
		return score;
	}
	void OnResetTrivia()
	{
		answers_ok = 0;
		answers_wrong = 0;
	}
	void OnAnswer(bool isOk)
	{
		if (isOk) {
			answers_ok++;
			score += 10;
		}
		else
		{
			score -= 8;
			answers_wrong++;
		}
	}
	public int GetTotalAnswers()
	{
		return answers_ok + answers_wrong;
	}
}
