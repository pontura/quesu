using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserButton : MonoBehaviour
{
    public int id;
    public Image image;
    public Text field;
    public UsersManager.UserData data;
    System.Action<UserButton> OnClicked;

    public void Init(UsersManager.UserData data, System.Action<UserButton> _OnClicked)
    {
        OnClicked = _OnClicked;
        this.data = data;
        field.text = data.username;
        LoopUntilReady();
    }
    void LoopUntilReady()
	{
		if (data.texture != null) {
			Sprite newSprite = Sprite.Create (data.texture, new Rect (0, 0, data.texture.width, data.texture.height), Vector2.zero);
			if(newSprite != null)
				image.sprite = newSprite;
			return;
		}
		Invoke ("LoopUntilReady", 0.1f);
	}
    public void OnClick()
    {
        GetComponent<Button>().interactable = false;
        OnClicked(this);
    }
}
