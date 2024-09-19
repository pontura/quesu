using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static RankingsManager;

public class Ranking : MonoBehaviour
{
    [SerializeField] RankingLine myLine;
    [SerializeField] RankingLine line;
    [SerializeField] Transform container;
    [SerializeField] GameObject loading;
    [SerializeField] TMPro.TMP_Text title;
    [SerializeField] Image image;
    Settings.CategorieData categoryData;
    private void OnEnable()
    {
        loading.SetActive(true);
    }
    public void Init(TriviaData triviaData)
    {
        Utils.RemoveAllChildsIn(container);
        loading.SetActive(true);
        Data.Instance.settings.GetCategoryData(triviaData.tag_id, OnDone);
    }
    void OnDone(Settings.CategorieData data)
    {
        this.categoryData = data;
        title.text = data.rankingTitle;
        title.color = data.color;
        image.color = data.color;
        Data.Instance.rankingsManager.Init(data.id, OnLoaded);
    }
    private void OnLoaded()
    {
        loading.SetActive(false);
        int id = 1;
        if (Data.Instance.rankingsManager.data.myranking.Count > 0 && Data.Instance.rankingsManager.data.myranking[0].rank > 0)
        {
            RankingLine rankingLine = Instantiate(myLine, container);
            RankingData rd = new RankingData();
            MyRanking mr = Data.Instance.rankingsManager.data.myranking[0];
            rd.num = mr.rank;
            rd.score = mr.score;
            rd.username = UserData.Instance.username;
            rankingLine.Init(rd, mr.rank, categoryData.color, true);
        }
        foreach (RankingData l in Data.Instance.rankingsManager.data.all)
        {
            RankingLine rankingLine = Instantiate(line, container);
            rankingLine.Init(l, id, categoryData.color, false);
            id++;
        }
       
    }
}
