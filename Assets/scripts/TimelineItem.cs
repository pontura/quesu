using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineItem : MonoBehaviour
{
	public Image image;
	public Text dateField;
	public Text titleField;
	public Text textField;
	public ItemData data;

	public void Init(ItemData data)
	{		
		this.data = data;	
		dateField.text = data.year.ToString();
		titleField.text = data.text;
		textField.text = data.description;
		LoopUntilReady ();
	}
	public void InitEmpty(int year)
	{			
		dateField.text = year.ToString ();
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
}
