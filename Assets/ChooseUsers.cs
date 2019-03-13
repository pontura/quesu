using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseUsers : MainScreen
{
    public UserButton button;
    public Transform container;

    public override void OnEnabled()
    {
        WaitForDataLoaded();
        Data.Instance.usersManager.LoadImages();
    }
    void WaitForDataLoaded()
    {
        if (Data.Instance.usersManager.users.all.Length > 0)
            AddButtons();
        else
            Invoke("WaitForDataLoaded", 0.1f);
    }
    void AddButtons()
    {
        int id = 0;
        foreach (UsersManager.UserData data in Data.Instance.usersManager.users.all)
        {
            AddButton(id, data);
            id++;
        }
    }
    void AddButton(int id, UsersManager.UserData  data)
    {
        UserButton b = Instantiate(button);
        b.transform.SetParent(container);
        b.Init(data, OnClicked);
        b.transform.localScale = Vector3.one;
    }
    public override void OnButtonClicked(ButtonStandard button)
    {
        LoadScreen(2, true);
    }
    public void OnClicked(UserButton _button)
    {
        Data.Instance.retosManager.SetNewReto(_button.data.userID, _button.data.username);
    }
}
