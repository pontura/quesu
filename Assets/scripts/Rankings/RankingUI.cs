using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingUI : MainScreen
{
    [SerializeField] Text field;
    [SerializeField] RankingLine line;
    [SerializeField] Transform container;
    [SerializeField] GameObject loading;

    public override void OnInit()
    {
        field.text = "RANKING " + Data.Instance.triviaData.triviaName;
        Data.Instance.rankingsManager.Init(Data.Instance.triviaData.tag_id, OnLoaded);
        loading.SetActive(true);
    }
    private void OnLoaded()
    {
        loading.SetActive(false);
        int id = 1;
        foreach (RankingData l in Data.Instance.rankingsManager.all)
        {
            RankingLine rankingLine = Instantiate(line, container);
            rankingLine.Init(l, id);
            id++;
        }
    }
}
