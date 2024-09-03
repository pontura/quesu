using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetosUI : MainScreen
{
    public RetoLine button;
    public Transform container;
    public Text ganados;
    public Text perdidos;

    public override void OnEnabled()
    {
        ganados.text = "Ganados: " + UserData.Instance.retosGanados;
        perdidos.text = "Perdidos: " + UserData.Instance.retosPerdidos;
        Data.Instance.usersManager.LoadImages();
        Data.Instance.serverManager.LoadRetos(UserData.Instance.userID);
        WaitForDataLoaded();
    }
    void WaitForDataLoaded()
    {
        if (Data.Instance.retosManager.retosContent.all.Count > 0)
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
        b.Init(this, data);
        b.transform.localScale = Vector3.one;
    }
    public override void OnButtonClicked(ButtonStandard button)
    {
        Events.OnSoundFX("ui");
        LoadScreen(2, true);
    }
    public void OnClicked(UserButton _button)
    {
        Events.OnSoundFX("ui");
        Data.Instance.retosManager.SetNewReto(_button.data.userID, _button.data.username);
        Events.OnPopup(_button.data.username + " fué retado!");
    }
    public void PlayReto(RetoData data)
    {
        Events.OnSoundFX("ui");
        Data.Instance.retosManager.SetOpenReto(data);
        Data.Instance.triviaData.Load(data.tag_id);
        LoadScreen(2, true);
    }
}
