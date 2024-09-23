using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingLine : MonoBehaviour
{
    [SerializeField] GameObject win;
    [SerializeField] TMPro.TMP_Text field;
    [SerializeField] TMPro.TMP_Text shortName;
    [SerializeField] TMPro.TMP_Text score;
    [SerializeField] Image IconBG;

    public void Init(RankingData data, int id, Color color, bool isYou)
    {
        field.color = color;
        score.color = color;
        IconBG.color = color;

        win.SetActive(id == 1);

        if (isYou)
            field.text = data.username + " (Puesto " + id.ToString() + ")";
        else
            field.text = id.ToString() + "- " + data.username;

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
                if (num <= 2 && s.Length>0)
                    shortNameText += s[0];
            }
        }
        shortName.text = shortNameText.ToUpper();
    }
}
