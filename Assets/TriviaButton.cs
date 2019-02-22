using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriviaButton : MonoBehaviour
{
	public Image image;
	public Text textField;
	TriviaPairButtons pairButtons;
	public ItemData data;
	bool win;
	public Image background;
	Animation anim;
	public Color textNormalColor;
	public Color textDoneColor;

	public void Init(TriviaPairButtons pairButtons,  ItemData data, bool win)
	{
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
			image.sprite = Sprite.Create (data.texture, new Rect (0, 0, 300, 300), Vector2.zero);
			return;
		}
		Invoke ("LoopUntilReady", 0.1f);
	}
	void Win()
	{		
		anim.Play ("buttonWin");
	}
	void Lose()
	{		
		anim.Play ("buttonLose");
	}
	public void Ready()
	{
		if (win)
			background.color = Data.Instance.settings.buttonOkColor;
		else
			background.color = Data.Instance.settings.buttonWrongColor;
		Invoke ("Reset", 3);
		textField.color = textDoneColor;
		textField.text = data.year;
	}
	void Reset()
	{
		textField.text = data.text;
		background.color = Color.white;
		textField.color = textNormalColor;
	}
}
