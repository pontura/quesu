using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriviaPairButtons : MonoBehaviour
{
	public TriviaButton button1;
	public TriviaButton button2;
	Trivia trivia;
	public GameObject flechas;

	public void Init(Trivia trivia, ItemData data1, ItemData data2)
    {
		this.trivia = trivia;
		bool win1 = false;
		if (data1.year < data2.year)
			win1 = true;
		button1.Init (this, data1, win1);
		button2.Init (this, data2, !win1);
    }
	public void OnButtonSelected(TriviaButton button)
	{
		flechas.GetComponent<Animator> ().Play ("flechasOff");
		button.Ready ();
		trivia.PairDone ();
		button1.DisableButton() ;
		button2.DisableButton() ;
	}
}
