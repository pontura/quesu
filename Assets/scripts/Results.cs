using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour {
	public GameObject retarButton;
	public Text bienField;
	public Text malField;
	public Text combosField;
	public Text scoreField;

	void OnEnable () {
		bienField.text = "";
		malField.text = "";
		combosField.text = "";
		scoreField.text = "";

	}
	public void OnInit () {
		int total = Data.Instance.resultsData.GetResults ();
		bienField.text = "BIEN: " + Data.Instance.resultsData.answers_ok;
		malField.text = "MAL: " + Data.Instance.resultsData.answers_wrong;
		combosField.text = "COMBOS: " + Data.Instance.resultsData.combos;
		scoreField.text = "PUNTOS: " + total;

		if (Data.Instance.retosManager.openReto.userID != "") {
			GetComponent<RetoResultUI> ().Init (total);
			retarButton.SetActive (false);

			//si no es empate lo cierra:
			if (total != Data.Instance.retosManager.openReto.score)
				Data.Instance.retosManager.CloseReto (total);

		}
	}

}