using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingLine : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text field;
    [SerializeField] TMPro.TMP_Text shortName;
    [SerializeField] TMPro.TMP_Text score;
    [SerializeField] Image IconBG;

    public void Init(RankingData data, int id, Color color)
    {
        field.color = color;
        score.color = color;
        IconBG.color = color;

        field.text = id.ToString()+ " - " + data.username;
        score.text = Utils.FormatNumbers(data.score);
        string[] arr = data.username.Split(" ");
        string shortNameText = "";
        if (arr.Length < 2)
            shortNameText += data.username[0];
        else
        {
            int num = 0;
            foreach (string s in arr)
            {
                num++;
                if (num <= 2)
                    shortNameText += s[0];
            }
        }
        shortName.text = shortNameText.ToUpper();
    }
}
