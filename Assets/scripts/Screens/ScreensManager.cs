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
        LoadScreen(MainScreen.screens.MAIN, true);
    }
    void Start()
	{
		int id = 0;
		foreach (MainScreen mainScreen in all) {
			mainScreen.Init (this, id);
			id++;
		}
		ResetAll ();
    }
	public void LoadScreen(MainScreen.screens screen, bool isRight)
	{
        print("Load Screen: " + screen);
		if (activeScreen != null) {
			activeScreen.SetActive (false);
		}

        activeScreen = GetScreen(screen);
        activeScreen.SetActive(true);
        activeScreen.OnInit();
    }
    MainScreen GetScreen(MainScreen.screens screen)
    {
        foreach (MainScreen mainScreen in all)
        {
            if (mainScreen.screen == screen)
                return mainScreen;
        }
        return null;
    }

    public void ResetAll()
	{
		foreach (MainScreen mainScreen in all) {
			mainScreen.gameObject.SetActive (false);
		}
	}

}
