using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour {

    [SerializeField] ButtonUI button;

    [SerializeField] SummaryLine good;
    [SerializeField] SummaryLine bad;
    [SerializeField] SummaryLine combos;
    [SerializeField] SummaryLine score;
    [SerializeField] TMPro.TMP_Text scoreField;

    void OnEnable () {

	}
	public void OnInit () {

        button.Init(Next);
        button.gameObject.SetActive(false);

        int total = Data.Instance.resultsData.GetResults ();
        scoreField.text = Utils.FormatNumbers(total);

        good.Init("Correctas", Data.Instance.resultsData.answers_ok);
        bad.Init("Incorrectas", Data.Instance.resultsData.answers_wrong);
        combos.Init("Combos", Data.Instance.resultsData.combos);
        score.Init("Total", total);

        Data.Instance.rankingsManager.SetHiscore(Done);
    }
    void Done()
    {
        button.gameObject.SetActive(true);
    }
    public void Next(ButtonUI b)
    {
        GetComponent<Timeline>().Next();
    }
    

}