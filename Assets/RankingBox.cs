using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingBox : MonoBehaviour
{
    public Text scoreField;
    public Text numField;
    public RawImage rawimage;

    public void Init(StandaloneRanking.RankingData data, int num)
    {
        rawimage.texture = data.texture;
        scoreField.text = data.score.ToString();
        numField.text = num.ToString();
        if (data.lastOne)
            GetComponent<Animation>().Play("rankingOn");
    }
}
