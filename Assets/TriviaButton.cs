using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriviaButton : MonoBehaviour {
	public Image image;
	public Image resultImage;
	public Text textField;
	TriviaPairButtons pairButtons;
	public ItemData data;
    public bool win;
	public Image background;
	Animation anim;
	public Color idleBarColor;
	public Color textNormalColor;
	public Color textDoneColor;

	public void Init (TriviaPairButtons pairButtons, ItemData data, bool win) {
        textField.color = Color.black;
        resultImage.enabled = true;
        resultImage.color = idleBarColor;


        resultImage.enabled = false;
		anim = GetComponent<Animation> ();
		this.win = win;
		this.data = data;
		this.pairButtons = pairButtons;
		textField.text = data.text;
		LoopUntilReady ();

        if(Data.Instance.format == Data.formats.STANDALONE)
            anim.Play("buttonStandaloneIdle");
    }
	public void Clicked () {
		if (win)
			Win ();
		else
			Lose ();
		pairButtons.OnButtonSelected (this);
	}
	void LoopUntilReady () {
		if (data.texture != null) {
			Sprite newSprite = Sprite.Create (data.texture, new Rect (0, 0, data.texture.width, data.texture.height), Vector2.zero);
			if (newSprite != null)
				image.sprite = newSprite;
			return;
		}
		Invoke ("LoopUntilReady", 0.1f);
	}
	void Win () {
		
		Events.OnAnswer (true);
		anim.Play ("buttonWin");
	}
	void Lose () {
		
		Events.OnAnswer (false);
		anim.Play ("buttonLose");
	}
	public void DisableButton () {
		GetComponent<Button> ().enabled = false;
		textField.text = data.year.ToString ();
	}
	public void Ready () {
        if (anim == null)
            return;
		if (win) {
            if (Data.Instance.format == Data.formats.STANDALONE)
                anim.Play("butonWinStandalone");
            else
                Events.OnSoundFX ("correct");
			background.color = Data.Instance.settings.buttonOkColor;
		} else {
            if (Data.Instance.format == Data.formats.STANDALONE)
                anim.Play("butonLoseStandalone");
            else
                Events.OnSoundFX ("wrong");
			background.color = Data.Instance.settings.buttonWrongColor;
		}
        
		textField.color = textDoneColor;
		resultImage.enabled = true;
		resultImage.color = background.color;
        textField.text = data.year.ToString();
    }
}