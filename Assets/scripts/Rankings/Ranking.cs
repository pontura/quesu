using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [SerializeField] RankingLine line;
    [SerializeField] Transform container;
    [SerializeField] GameObject loading;

    private void OnEnable()
    {
        loading.SetActive(true);
    }
    public void Init(TriviaData triviaData)
    {
        loading.SetActive(true);
        Data.Instance.rankingsManager.Init(triviaData.tag_id, OnLoaded);       

    }
    private void OnLoaded()
    {
        loading.SetActive(false);
        int id = 1;
        foreach (RankingData l in Data.Instance.rankingsManager.data.all)
        {
            RankingLine rankingLine = Instantiate(line, container);
            rankingLine.Init(l, id);
            id++;
        }
    }
}
