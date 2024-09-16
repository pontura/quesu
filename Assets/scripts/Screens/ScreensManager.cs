using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour
{
	public MainScreen[] all;

    public MainScreen activeScreen;
	MainScreen lastActiveScreen;


	bool loading;
    private void Awake()
    {
        Events.OnUserReady += OnUserReady;
    }
    private void OnDestroy()
    {
        Events.OnUserReady -= OnUserReady;
    }

    private void OnUserReady()
    {
        print("OnUserReady");
        LoadScreen(0, true);
    }

    void Start()
	{
		int id = 0;
		foreach (MainScreen mainScreen in all) {
			mainScreen.Init (this, id);
			id++;
		}
		ResetAll ();
        LoadScreen(7, true);
    }
	public void LoadScreen(int id, bool isRight)
	{
        print("Load Screen: " + id);
		if (activeScreen != null) {
			activeScreen.SetActive (false);
		}
		
		activeScreen = all [id];
        activeScreen.SetActive(true);
        activeScreen.OnInit();
    }
	public void ResetAll()
	{
		foreach (MainScreen mainScreen in all) {
			mainScreen.gameObject.SetActive (false);
		}
	}

}
