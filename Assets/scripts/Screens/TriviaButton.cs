using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriviaButton : MonoBehaviour {

	public Image image;
	public Image resultImage;
	public TMPro.TMP_Text textField;
	TriviaPairButtons pairButtons;
	public ItemData data;
    public bool win;
	[SerializeField] Animation anim;
	public Color idleBarColor;
	public Color textNormalColor;
	public Color textDoneColor;

	public void Init (TriviaPairButtons pairButtons, ItemData data, bool win) {

        GetComponent<Button>().interactable = true;
        textField.color = textNormalColor;
        resultImage.enabled = true;
        resultImage.color = idleBarColor;

		this.win = win;
		this.data = data;
		this.pairButtons = pairButtons;
		textField.text = data.text;
        SetSprite();
        SetOn();

    }
	public void Clicked () {
		if (win)
			Win ();
		else
			Lose ();
		pairButtons.OnButtonSelected (this);
	}
	void SetSprite () {
		Sprite newSprite = Sprite.Create (data.texture, new Rect (0, 0, data.texture.width, data.texture.height), Vector2.zero);
		if (newSprite != null)
			image.sprite = newSprite;
	}
    public void SetInit()
    {
        anim.Play("off");
    }
    public void SetOn()
    {
        anim.Play("on");
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
		GetComponent<Button> ().interactable = false;
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
            resultImage.color = Data.Instance.settings.buttonOkColor;
		} else {
            if (Data.Instance.format == Data.formats.STANDALONE)
                anim.Play("butonLoseStandalone");
            else
                Events.OnSoundFX ("wrong");
            resultImage.color = Data.Instance.settings.buttonWrongColor;
		}
        
		textField.color = textDoneColor;
		resultImage.color = resultImage.color;
        textField.text = data.year.ToString();
    }
}