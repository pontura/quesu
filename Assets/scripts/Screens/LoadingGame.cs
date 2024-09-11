using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingGame : MainScreen
{
    public Text field;
    [SerializeField] Ranking ranking;

    public override void OnEnabled()
    {
        Init();
    }
    void Init()
    {
        field.text = Data.Instance.triviaData.triviaName.ToUpper();
        ranking.Init(Data.Instance.triviaData);
    }
    public void OnStart()
    {
        LoadScreen(3, true);
    }
    public void Next()
    {
        Data.Instance.triviaData.Next(Init);
    }
    public void Prev()
    {
        Data.Instance.triviaData.Prev(Init);
    }
}