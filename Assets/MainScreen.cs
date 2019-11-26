using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
	public string backSceneName;
	int id;
	ScreensManager screensManager;
	float screenWidth;
	public int backScreenID;
    public bool ready;

    void Awake()
    {
        Events.OnButtonClicked += OnButtonClickedChecker;
        if (Data.Instance.format == Data.formats.STANDALONE)
            Events.OnStandaloneKeyDown += OnStandaloneKeyDownChecker;
    }
    void OnButtonClickedChecker(ButtonStandard button)
    {
        if(ready)
            OnButtonClicked(button);
    }
    public void Init(ScreensManager screensManager, int id)
	{
        
        screenWidth = Screen.width + 100;
		this.screensManager = screensManager;
		this.id = id;
		gameObject.SetActive (false);
	}
	public void SetCenterPosition()
	{
		gameObject.transform.localPosition = Vector2.zero;
	}
	public void SetInitialPosition(bool toRight)
	{
        ready = false;
        OnEnabled ();
		float destination = screenWidth;
		if (!toRight)
			destination = -screenWidth;
		gameObject.transform.localPosition = new Vector2 (destination, 0);
	}
	public void MoveTo(bool toRight)
	{
        ready = false;
        float destination = gameObject.transform.localPosition.x-screenWidth;
		if (!toRight)
			destination = gameObject.transform.localPosition.x + screenWidth;		

		iTween.MoveTo (gameObject, iTween.Hash (
			"x", destination,
			"islocal", true,
			"time", 1	,
			"oncomplete", "TransitionDone",
			"oncompletetarget", this.gameObject
		));
	}
	public void LoadScreen(int screenID, bool toRight)
	{
		screensManager.LoadScreen (screenID, toRight);
	}
	void TransitionDone()
	{
		screensManager.OnTransitionDone ();
        if (screensManager.activeScreen == this)
            ready = true;
        else
            ready = false;
    }
    void OnDestroy()
    {
        Events.OnButtonClicked -= OnButtonClicked;
        Events.OnStandaloneKeyDown -= OnStandaloneKeyDownChecker;
    }
    void OnStandaloneKeyDownChecker(StandaloneInputManager.types type)
    {
        if (ready)
        {
            print(this.gameObject.name);
            Events.OnSoundFX("ui");
            OnStandaloneKeyDown(type);
        }
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
    public virtual void OnEnabled() { }
	public virtual void OnButtonClicked(ButtonStandard button) { }
	public virtual void OnInit() 	{ }
	public virtual void OnReset() 	{ }
    public virtual void OnStandaloneKeyDown(StandaloneInputManager.types type) { }
}
