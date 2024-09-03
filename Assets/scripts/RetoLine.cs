using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetoLine : MonoBehaviour
{
    public GameObject panel_ToPlay;
    public GameObject panel_played;

    public Text title;
    public Text statusField;

    public Image avatar1;
    public Image avatar2;

    public Text username1;
    public Text username2;

    public Text score1;
    public Text score2;
    RetoData data;
    RetosUI ui;

    public void Init(RetosUI ui, RetoData data)
    {
        this.ui = ui;
        this.data = data;
        LoadData();
    }
    public void LoadData()
    {
        Data.Instance.tagsData.GetTitleById(data.tag_id);

        title.text = Data.Instance.tagsData.GetTitleById(data.tag_id);
        
        username1.text = data.username;
        username2.text = data.username2;

        score1.text = data.score.ToString();
        score2.text = "...";

        LoadImage(avatar1, Data.Instance.usersManager.GetData(data.userID));
        LoadImage(avatar2, Data.Instance.usersManager.GetData(data.userID2));

        if (data.userID2 == UserData.Instance.userID && data.ready == 0)
        {
            panel_ToPlay.SetActive(true);
            panel_played.SetActive(false);
        }
        else
        {
            panel_ToPlay.SetActive(false);
            panel_played.SetActive(true);
        }
    }
    void LoadImage(Image image, UsersManager.UserData data)
    {
        if (data.texture != null)
        {
            Sprite newSprite = Sprite.Create(data.texture, new Rect(0, 0, data.texture.width, data.texture.height), Vector2.zero);
            if (newSprite != null)
                image.sprite = newSprite;
        }
        else
        {
            Invoke("LoadData", 1);
        }
    }
    public void PlayReto()
    {
        ui.PlayReto(data);
    }

}
