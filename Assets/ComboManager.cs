using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
	public GameObject panel;
	public Text field;
	public int combos;

    void Start()
    {
		panel.SetActive (false);
		Events.OnAnswer += OnAnswer;
		Events.OnResetTrivia += OnResetTrivia;
	}
	void OnDestroy()
	{
		Events.OnAnswer -= OnAnswer;
		Events.OnResetTrivia += OnResetTrivia;
	}
	void OnResetTrivia()
	{
		combos = 0;
	}
	void OnAnswer(bool isOk)
	{
		panel.SetActive (false);
		if (isOk)
			combos++;
		else
			combos = 0;

		if (combos > 2) {
			panel.SetActive (true);
			field.text = "COMBO x" + (combos - 2).ToString ();
			Events.OnCombo (combos);
		}
	}
}
