using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsData : MonoBehaviour
{
	public int answers_ok;
	public int answers_wrong;
	public int score;
	public int combos;

    int scoreWin;
    int scoreLose;
    int scoreByTimeLostMult;

    void Start()
    {
        Events.OnInitRonda += OnInitRonda;
        Events.OnAnswer += OnAnswer;
		Events.OnResetTrivia += OnResetTrivia;
		Events.OnCombo += OnCombo;

        scoreWin = Data.Instance.settings.scoreWin;
        scoreLose = Data.Instance.settings.scoreLose;
        scoreByTimeLostMult = Data.Instance.settings.scoreByTimeLostMult;
    }
	void OnDestroy()
    {
        Events.OnInitRonda -= OnInitRonda;
        Events.OnAnswer -= OnAnswer;
		Events.OnResetTrivia += OnResetTrivia;
		Events.OnCombo -= OnCombo;
	}
    float timer;
    void OnInitRonda()
    {
        timer = 0;
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    void OnCombo(int value)
	{
		combos++;
		score += value * Data.Instance.settings.scoreCombo;
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
        score = 0;
        combos = 0;
    }
	void OnAnswer(bool isOk, Vector2 pos)
	{
        int newScore = 0;
		if (isOk) {
			answers_ok++;
            newScore = scoreWin;
        }
		else
		{
            newScore = scoreLose;
            answers_wrong++;
		}
        int lostTime = (int)((float)scoreByTimeLostMult * timer);
        newScore -= (int)lostTime;

        Events.OnFinalScoreByRonda(newScore, pos);

        score += newScore;

        if (score < 0)
            score = 0;
    }
	public int GetTotalAnswers()
	{
		return answers_ok + answers_wrong;
	}
}
