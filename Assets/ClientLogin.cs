using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientLogin : MainScreen
{
    public InputField inputField;
    public Text responseField;

    public void OnSubmitFieldClicked()
    {
        CancelInvoke();
        string text = inputField.text;
        if(text.Length<2)
        {
            responseField.text = "Ingresar un password";
            return;
        }
        Data.Instance.serverManager.GetClientByPassword(inputField.text, OnDone);
    }
    public void OnDone(ServerManager.ClientDataJson data)
    {
        if (data == null)
            SetDebugText("No existe este usuario");
        else if (data.all.Count == 0)
            SetDebugText("Usuario sin categorías");
        else
        {
            foreach(TagData tagData in Data.Instance.tagsData.tags.all)
                tagData.lockedByClient = true;

            foreach(ServerManager.ClientDataTagsJson a in data.all)
            {
                foreach (TagData tagData in Data.Instance.tagsData.tags.all)
                {
                    if(tagData.id == a.tag_id)
                        tagData.lockedByClient = false;
                }
            }
            Data.Instance.PlayVideo();
            LoadScreen(0, true);
        }
    }
    void SetDebugText(string text)
    {
        responseField.text = text;
        Invoke("Reset", 3);
    }
    void Reset()
    {
        responseField.text = "";
    }
}
