using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetoLine : MonoBehaviour
{
    public Text title;
    public Text statusField;

    public Image avatar1;
    public Image avatar2;

    public Text username1;
    public Text username2;

    public Text score1;
    public Text score2;
    RetoData data;

    public void Init(RetoData data)
    {
        this.data = data;
        LoadData();
    }
    public void LoadData()
    {
        Data.Instance.tagsData.GetTitleById(data.tag_id);

        username1.text = data.username;
        username2.text = data.username2;

        score1.text = data.score.ToString();
        score2.text = data.score2.ToString();

        LoadImage(avatar1, Data.Instance.usersManager.GetData(data.userID));
        LoadImage(avatar2, Data.Instance.usersManager.GetData(data.userID2));
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

}
