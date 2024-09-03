using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingResults : MainScreen
{
    public int nextScreen = 5;

	public override void OnInit()
	{
		Invoke ("Done", 2);
	}
	void Done()
	{
		LoadScreen (nextScreen, true);
	}
}