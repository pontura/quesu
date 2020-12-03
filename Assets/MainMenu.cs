using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MainScreen
{
    public GameObject registerButton;

    public override void OnEnabled()
    {
        if (Data.Instance.format == Data.formats.CLASSIC)
        {
            if (UserData.Instance.username == "")
            {
               /// registerButton.SetActive(true);
            }
            else
            {
               // registerButton.SetActive(false);
                UserData.Instance.UpdateData();
            }
        }
        else
        {
            Invoke("ShowHiscores", 8);
        }
    }
    public void PlaySingleMode()
    {
        //  LoadScreen(1, true);

        // clients login si no bajo sus categorías dadas de alta en el admin:
        if(Data.Instance.serverManager.clientDataJson.all.Count == 0)
            LoadScreen(9, true);
        else
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
    void ShowHiscores()
    {
        LoadScreen(5, true);
    }
    public override void OnReset()
    {
        CancelInvoke();
    }
    public void ShoeSettings()
    {
        LoadScreen(10, true);
    }
}
