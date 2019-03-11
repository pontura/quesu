using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MainScreen
{
    public GameObject registerButton;

   public override void OnEnabled()
    {
        if (UserData.Instance.userID == "")
            registerButton.SetActive(true);
        else
            registerButton.SetActive(false);
    }
    public void PlaySingleMode()
    {
        LoadScreen(1, true);
    }
	public void OnRegisterClicked()
	{
		LoadScreen(6, true);
	}
}
