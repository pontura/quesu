using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour
{
	public MainScreen[] all;

	public MainScreen activeScreen;

	void Start()
	{
		int id = 0;
		foreach (MainScreen mainScreen in all) {
			mainScreen.Init (this, id);
			id++;
		}
		ResetAll ();
		LoadScreen (0, true);
	}
	public void LoadScreen(int id, bool isRight)
	{
		if (activeScreen != null) {
			activeScreen.SetCenterPosition ();
			activeScreen.MoveTo (isRight);
		}
		
		activeScreen = all [id];
		activeScreen.gameObject.SetActive (true);
		activeScreen.SetInitialPosition (isRight);
		activeScreen.MoveTo (isRight);
	}
	public void ResetAll()
	{
		foreach (MainScreen mainScreen in all) {
			mainScreen.gameObject.SetActive (false);
		}
	}

}
