using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryRanking : MainScreen
{
    public TMPro.TMP_Text field;
    [SerializeField] Ranking ranking;
    [SerializeField] ButtonUI button;
    [SerializeField] ButtonUI next;
    [SerializeField] ButtonUI prev;
    [SerializeField] Image fade;

    private void Start()
    {
        next.Init(Next);
        prev.Init(Prev);
        button.Init(OnClick);
    }
    public override void OnInit()
    {
        field.text = Data.Instance.triviaData.triviaName.ToUpper();
        ranking.Init(Data.Instance.triviaData);
        button.SetText("COMENZAR");
    }
    public void OnClick(ButtonUI b)
    {
        Data.Instance.triviaData.Load(OnDone);
    }
    void OnDone()
    {
        LoadScreen(screens.LOADING_GAME, true);
        Data.Instance.settings.GetCategoryData(Data.Instance.triviaData.tag_id, CategoryDataDone);
    }
    public void CategoryDataDone(Settings.CategorieData data)
    {
        fade.color = data.color;
    }
    public void Next(ButtonUI b)
    {
        Data.Instance.triviaData.Next();
        OnInit();
    }
    public void Prev(ButtonUI b)
    {
        Data.Instance.triviaData.Prev();
        OnInit();
    }
}