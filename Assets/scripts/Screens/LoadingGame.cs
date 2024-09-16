using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingGame : MainScreen
{
    public TMPro.TMP_Text field;
    [SerializeField] Ranking ranking;
    [SerializeField] ButtonUI button;

    public override void OnInit()
    {
        field.text = Data.Instance.triviaData.triviaName.ToUpper();
        ranking.Init(Data.Instance.triviaData);
        button.Init(OnClick);
        button.SetText("COMENZAR");
    }
    public void OnClick(ButtonUI b)
    {
        Data.Instance.triviaData.Load(OnDone);
    }
    void OnDone()
    {
        LoadScreen(3, true);
    }
    public void Next()
    {
        Data.Instance.triviaData.Next();
        OnInit();
    }
    public void Prev()
    {
        Data.Instance.triviaData.Prev();
        OnInit();
    }
}