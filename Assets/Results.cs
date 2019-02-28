using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
	public Text bienField;
	public Text malField;
	public Text combosField;
	public Text scoreField;

	void OnEnable()
	{
		bienField.text = "";	
		malField.text = "";	
		combosField.text = "";	
		scoreField.text = "";
	}
	public void OnInit()
	{
		bienField.text = "BIEN: " + Data.Instance.resultsData.answers_ok;	
		malField.text = "MAL: " + Data.Instance.resultsData.answers_wrong;	
		combosField.text = "COMBOS: " + Data.Instance.resultsData.combos;	
		scoreField.text = "PUNTOS: " + Data.Instance.resultsData.GetResults ();	
	}
}
