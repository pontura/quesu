using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingResults : MainScreen
{
	public override void OnInit()
	{
		Invoke ("Done", 3);
    }
	void Done()
	{
        CancelInvoke();
		LoadScreen (screens.RESULTS, true);
	}
}