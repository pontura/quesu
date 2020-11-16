using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriviaPairButtons : MonoBehaviour
{
	public TriviaButton button1;
	public TriviaButton button2;
    public TriviaButton button3;
    public TriviaButton button4;

    Trivia trivia;
    public GameObject flechas;
    Animation anim;
    bool win1, win2, win3, win4;

    void Awake()
    {
        anim = GetComponent<Animation>();
    }
    public void InitStandalone(StandaloneTrivia trivia, ItemData data1, ItemData data2)
    {
        bool win1 = false;

        if (data1.year < data2.year)
            win1 = true;

        button1.Init(this, data1, win1);
        button2.Init(this, data2, !win1);
        anim.Play("standaloneButtonsEnter");
    }
    public void Init(Trivia trivia, ItemData data1, ItemData data2)
    {
		this.trivia = trivia;
		bool win1 = false;
		if (data1.year < data2.year)
			win1 = true;
		button1.Init (this, data1, win1);
		button2.Init (this, data2, !win1);
        anim.Play("buttonsEnter");
    }
    
    public void Init(Trivia trivia, ItemData data1, ItemData data2, ItemData data3)
    {
        this.trivia = trivia;

        if (data1.year < data2.year && data1.year < data3.year)
            win1 = true;
        else if (data2.year < data1.year && data2.year < data3.year)
            win2 = true;
        else
            win3 = true;

        button1.Init(this, data1, win1);
        button2.Init(this, data2, win2);
        button3.Init(this, data3, win3);

        anim.Play("buttonsEnter");
    }
    public void Init(Trivia trivia, ItemData data1, ItemData data2, ItemData data3, ItemData data4)
    {
        this.trivia = trivia;

        if (data1.year < data2.year && data1.year < data3.year && data1.year < data4.year)
            win1 = true;
        else if (data2.year < data1.year && data2.year < data3.year && data2.year < data4.year)
            win2 = true;
        else if (data3.year < data1.year && data3.year < data2.year && data3.year < data4.year)
            win2 = true;
        else
            win4 = true;

        button1.Init(this, data1, win1);
        button2.Init(this, data2, win2);
        button3.Init(this, data3, win3);
        button4.Init(this, data4, win4);

        anim.Play("buttonsEnter");
    }
    public void OnButtonSelected(TriviaButton button)
	{
        if(Data.Instance.format == Data.formats.CLASSIC)
            flechas.GetComponent<Animator> ().Play ("flechasOff");
		button.Ready ();
		trivia.PairDone ();
		button1.DisableButton() ;
		button2.DisableButton() ;
        if(button3 != null)
            button3.DisableButton();
        if (button4 != null)
            button4.DisableButton();
    }
    //para el standalone:
    public void SetBothButtonsReady()
    {
        if (Data.Instance.format == Data.formats.CLASSIC)
            flechas.GetComponent<Animator>().Play("flechasOff");

        button1.Ready();
        button2.Ready();

        if (button3 != null)
            button3.Ready();
        if (button4 != null)
            button4.Ready();
        anim.Play("standaloneButtonsExit");
    }
}
