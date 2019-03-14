using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : MainScreen
{
    public UserDataUI ui;

    public override void OnEnabled()
    {
        ui.Init();
        UsersEvents.OnRegistartionDone += GotoScene;
        UsersEvents.OnUserUploadDone += GotoScene;
        UsersEvents.OnUserRegisterCanceled += GotoScene;
    }
    void OnDestroy()
    {
        UsersEvents.OnRegistartionDone -= GotoScene;
        UsersEvents.OnUserUploadDone -= GotoScene;
        UsersEvents.OnUserRegisterCanceled -= GotoScene;
    }
    void GotoScene()
    {
        if(UserData.Instance.username == "")
            UserData.Instance.userID = "";
        LoadScreen(0, false);
    }
}
