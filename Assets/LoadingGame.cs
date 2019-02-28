using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingGame : MainScreen
{
	public Text field;

	public override void OnEnabled()
	{
		field.text = Data.Instance.triviaData.triviaName.ToUpper();
	}
	public override void OnInit()
	{		
		Invoke ("Done", 4);
	}
	void Done()
	{
		LoadScreen (3, true);
	}
}