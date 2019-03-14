using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MainScreen
{
    public GameObject registerButton;

   public override void OnEnabled()
    {
        if (UserData.Instance.username == "")
            registerButton.SetActive(true);
        else
            registerButton.SetActive(false);
    }
    public void PlaySingleMode()
    {
        LoadScreen(1, true);
    }
    public void Retos()
	{
		LoadScreen(8, true);
	}
	public void OnRegisterClicked()
	{
		LoadScreen(6, true);
	}
}
