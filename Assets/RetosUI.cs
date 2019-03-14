using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetosUI : MainScreen
{
    public RetoLine button;
    public Transform container;

    public override void OnEnabled()
    {       
        Data.Instance.usersManager.LoadImages();
        Data.Instance.serverManager.LoadRetos(UserData.Instance.userID);
         WaitForDataLoaded();
    }
    void WaitForDataLoaded()
    {
        if (Data.Instance.retosManager.retosContent.all.Length > 0)
            AddButtons();
        else
            Invoke("WaitForDataLoaded", 0.1f);
    }
    void AddButtons()
    {
        int id = 0;
        Utils.RemoveAllChildsIn(container);
        foreach (RetoData data in Data.Instance.retosManager.retosContent.all)
        {
            AddButton(data);
            id++;
        }
    }
    void AddButton(RetoData data)
    {
        RetoLine b = Instantiate(button);
        b.transform.SetParent(container);
        b.Init(data);
        b.transform.localScale = Vector3.one;
    }
    public override void OnButtonClicked(ButtonStandard button)
    {
        LoadScreen(2, true);
    }
    public void OnClicked(UserButton _button)
    {
        Data.Instance.retosManager.SetNewReto(_button.data.userID, _button.data.username);
        Events.OnPopup(_button.data.username + " fué retado!");
    }
}
