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
		float destination = screenWidth;
		if (!toRight)
			destination = -screenWidth;
		gameObject.transform.localPosition = new Vector2 (destination, 0);
	}
	public void MoveTo(bool toRight)
	{		
		float destination = gameObject.transform.localPosition.x-screenWidth;
		if (!toRight)
			destination = gameObject.transform.localPosition.x + screenWidth;		

		iTween.MoveTo (gameObject, iTween.Hash (
			"x", destination,
			"islocal", true,
			"time", 3	,
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

	}
	void OnEnable()
	{		
		Events.OnButtonClicked += OnButtonClicked;
	}
	void OnDisable()
	{
		Events.OnButtonClicked -= OnButtonClicked;
	}
	public virtual void OnButtonClicked(int tagID)
	{
	}
	public void Back()
	{		
		screensManager.LoadScreen (backScreenID, false);
	}
}
