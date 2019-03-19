using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MainScreen
{
    public GameObject registerButton;

    public override void OnEnabled()
    {
         
        if (UserData.Instance.username == "")
        {
            registerButton.SetActive(true);
        }                           
        else
        {
            registerButton.SetActive(false);
            UserData.Instance.UpdateData();
        }
    }
    public void PlaySingleMode()
    {
        LoadScreen(1, true);
        Events.OnSoundFX("ui");
    }
    public void Retos()
    {
        Events.OnSoundFX("ui");
        if (UserData.Instance.userID == "")
        {
            Events.OnPopup("¡No estás registrado!");
            return;
        }
        LoadScreen(8, true);
    }
    public void OnRegisterClicked()
    {
        LoadScreen(6, true);
        Events.OnSoundFX("ui");
    }
}
