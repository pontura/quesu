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

        if (!isYetActive()) return;
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
    bool isYetActive()
    {
        if (!isActiveAndEnabled) return false;
        if (anim == null) return false;
        return true;
    }
    public void SetInit()
    {
        if (!isYetActive()) return;
        PlayAnim("off");
    }
    public void SetOn()
    {
        if (!isYetActive()) return;
        PlayAnim("on");
    }
    void Win()
    {
        if (!isYetActive()) return;
        Events.OnAnswer(true, Input.mousePosition);
        PlayAnim("buttonWin");
	}
	void Lose ()
    {
        if (!isYetActive()) return;
        Events.OnAnswer (false, Input.mousePosition);
        PlayAnim("buttonLose");
	}
	public void DisableButton ()
    {
        if (!isYetActive()) return;
        GetComponent<Button> ().interactable = false;
		textField.text = data.year.ToString ();
	}
	public void Ready () {

        if (!isYetActive()) return;
        if (anim == null)
            return;
		if (win) {
            if (Data.Instance.format == Data.formats.STANDALONE)
                PlayAnim("butonWinStandalone");
            else
                Events.OnSoundFX ("correct");
            resultImage.color = Data.Instance.settings.buttonOkColor;
		} else {
            if (Data.Instance.format == Data.formats.STANDALONE)
                PlayAnim("butonLoseStandalone");
            else
                Events.OnSoundFX ("wrong");
            resultImage.color = Data.Instance.settings.buttonWrongColor;
		}
        
		textField.color = textDoneColor;
		resultImage.color = resultImage.color;
        textField.text = data.year.ToString();
    }
    private void PlayAnim(string a)
    {
        try
        {
            if(gameObject.activeSelf)
                anim.Play(a);
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}