using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    public screens screen;
    public enum screens
    {
        MAIN,
        CATEGORIES,
        CATEGORY_RANKING,
        LOADING_GAME,
        GAME,
        RESULTS,
        RANKING,
        LOADING,
        PROCESSING_RESULTS
    }
	public screens backScene;
	int id;
	ScreensManager screensManager;
	public int backScreenID;
    public bool ready;

    public void Init(ScreensManager screensManager, int id)
	{
		this.screensManager = screensManager;
		this.id = id;
        SetActive(false);
    }
    public void SetActive(bool isOn)
    {
        if (!isOn)
            OnReset();
        gameObject.SetActive(isOn);
    }
	public void LoadScreen(MainScreen.screens screen, bool toRight)
	{
		screensManager.LoadScreen (screen, toRight);
	}
    public virtual void Back()
	{		
		screensManager.LoadScreen (backScene, false);
	}
    void OnDisable()
    {
        CancelInvoke();
        ready = false;
    }
	public virtual void OnInit() 	{ }
	public virtual void OnReset() 	{ }
}
