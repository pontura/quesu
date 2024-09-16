using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingUI : MainScreen
{
    [SerializeField] Ranking ranking;
    [SerializeField] Text field;
    [SerializeField] ButtonUI button;

    public override void OnInit()
    {
        field.text = "RANKING " + Data.Instance.triviaData.triviaName;
        ranking.Init(Data.Instance.triviaData);
        button.Init(OnClicked);
    }
    public void OnClicked(ButtonUI b)
    {
        Back();
    }
}
