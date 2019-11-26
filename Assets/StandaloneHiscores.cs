using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneHiscores : MainScreen
{
    public Transform container;
    public RankingBox rankingBox;

    public override void OnEnabled()
    {
        Utils.RemoveAllChildsIn(container);
        StartCoroutine(Routine());
        Invoke("GotoVideo", 10);
    }
    IEnumerator Routine()
    {
        StandaloneRanking ranking = Data.Instance.GetComponent<StandaloneRanking>();
        //ranking.Reorder();
        int id = 1;
        foreach (StandaloneRanking.RankingData data in ranking.all)
        {
            if (id < 41)
            {
                RankingBox newRankingBox = Instantiate(rankingBox);
                newRankingBox.transform.SetParent(container);
                newRankingBox.transform.localScale = Vector3.one;

                newRankingBox.Init(data, id);

                id++;
                yield return new WaitForSeconds(0.1f);
            }
        }
        
    }
    public override void OnStandaloneKeyDown(StandaloneInputManager.types type)
    {
        LoadScreen(0, false);
    }
    void GotoVideo()
    {
        Data.Instance.LoadLevel("Standalone_02Video");
    }
}
