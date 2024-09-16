using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
	public string backSceneName;
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
        if (isOn)
            OnInit();
        else
            OnReset();
        gameObject.SetActive(isOn);
    }
	public void LoadScreen(int screenID, bool toRight)
	{
		screensManager.LoadScreen (screenID, toRight);
	}
    public virtual void Back()
	{		
		screensManager.LoadScreen (backScreenID, false);
	}
    void OnDisable()
    {
        CancelInvoke();
        ready = false;
    }
	public virtual void OnInit() 	{ }
	public virtual void OnReset() 	{ }
}
