using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriviaButton : MonoBehaviour
{
	public Image image;
	public Image resultImage;
	public Text textField;
	TriviaPairButtons pairButtons;
	public ItemData data;
	bool win;
	public Image background;
	Animation anim;
	public Color idleBarColor;
	public Color textNormalColor;
	public Color textDoneColor;

	public void Init(TriviaPairButtons pairButtons,  ItemData data, bool win)
	{
		resultImage.enabled = false;
		anim = GetComponent<Animation> ();
		this.win = win;
		this.data = data;
		this.pairButtons = pairButtons;
		textField.text = data.text;
		LoopUntilReady ();
	}
	public void Clicked()
	{
		if (win)
			Win ();
		else
			Lose ();
		pairButtons.OnButtonSelected (this);
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
	void Win()
	{		
		Events.OnAnswer (true);
		anim.Play ("buttonWin");
	}
	void Lose()
	{		
		Events.OnAnswer (false);
		anim.Play ("buttonLose");
	}
	public void DisableButton()
	{
		GetComponent<Button> ().enabled = false;
		textField.text = data.year.ToString();
	}
	public void Ready()
	{
		if (win)
			background.color = Data.Instance.settings.buttonOkColor;
		else
			background.color = Data.Instance.settings.buttonWrongColor;
		Invoke ("Reset", 3);
		textField.color = textDoneColor;
		resultImage.enabled = true;
		resultImage.color = background.color;
	}
	void Reset()
	{
//		textField.text = data.text;
//		background.color = Color.white;
//		textField.color = textNormalColor;
	}
}
