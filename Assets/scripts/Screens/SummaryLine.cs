using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryLine : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text field;
    [SerializeField] TMPro.TMP_Text score;

    public void Init(string key, int value)
    {
        field.text = key;
        score.text = Utils.FormatNumbers(value);
    }
}
