using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingResults : MainScreen
{
	public override void OnInit()
	{
		Invoke ("Done", 2);
	}
	void Done()
	{
		LoadScreen (5, true);
	}
}