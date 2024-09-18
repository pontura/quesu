using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
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
        foreach (RankingData l in Data.Instance.rankingsManager.data.all)
        {
            RankingLine rankingLine = Instantiate(line, container);
            rankingLine.Init(l, id, categoryData.color);
            id++;
        }
    }
}
