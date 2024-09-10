using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingLine : MonoBehaviour
{
    [SerializeField] Text field;
    [SerializeField] Text score;
    [SerializeField] Text num;

    public void Init(RankingData data, int id)
    {
        num.text = id.ToString();
        field.text = data.username;
        score.text = Utils.FormatNumbers(data.score);
    }
}
