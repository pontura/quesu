using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriviaPairButtons : MonoBehaviour
{
	public TriviaButton button1;
	public TriviaButton button2;
	Trivia trivia;

	public void Init(Trivia trivia, ItemData data1, ItemData data2)
    {
		this.trivia = trivia;
		bool win1 = false;
		if (int.Parse(data1.year) < int.Parse(data2.year))
			win1 = true;
		button1.Init (this, data1, win1);
		button2.Init (this, data2, !win1);
    }
	public void OnButtonSelected(TriviaButton button)
	{
		button1.Ready ();
		button2.Ready ();
		Invoke ("Next", 2);
	}
	void Next()
	{
		trivia.Next ();
	}
}
