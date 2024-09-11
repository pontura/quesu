using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour {

    [SerializeField] Text bienField;
    [SerializeField] Text malField;
    [SerializeField] Text combosField;
    [SerializeField] Text scoreField;
    [SerializeField] Button button;

	void OnEnable () {
		bienField.text = "";
		malField.text = "";
		combosField.text = "";
		scoreField.text = "";

	}
	public void OnInit () {

        button.gameObject.SetActive(false);

        int total = Data.Instance.resultsData.GetResults ();
		bienField.text = "BIEN: " + Data.Instance.resultsData.answers_ok;
		malField.text = "MAL: " + Data.Instance.resultsData.answers_wrong;
		combosField.text = "COMBOS: " + Data.Instance.resultsData.combos;
		scoreField.text = "PUNTOS: " + total;

        Data.Instance.rankingsManager.SetHiscore(Done);
    }
    void Done()
    {
        button.gameObject.SetActive(true);
    }
    

}