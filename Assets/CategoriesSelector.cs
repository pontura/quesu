using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoriesSelector : MainScreen
{
	public ButtonStandard button;
	public Transform container;

    void Start()
    {
		WaitForTags ();
    }
	void WaitForTags()
	{
		if (Data.Instance.tagsData.tagsSimple.all.Length > 0) 
			AddButtons ();
		else
			Invoke ("WaitForTags", 0.1f);
	}
	void AddButtons()
	{
		foreach (TagData data in Data.Instance.tagsData.tagsSimple.all) {
			ButtonStandard b = Instantiate (button);
			b.transform.SetParent (container);
			b.Init (data.id, data.name);
			b.transform.localScale = Vector3.one;
		}
	}
	public override void OnButtonClicked(ButtonStandard button)
	{
		if (button.id == 1) {
			Data.Instance.serverManager.LoadTriviaByCategory ("historia", 40);
		} else if (button.id == 2) {
			Data.Instance.serverManager.LoadTrivia (9, 40);
		} else if (button.id == 3) {
			Data.Instance.serverManager.LoadTrivia (11, 40);
		}
		Data.Instance.triviaData.triviaName = button.field.text;
		LoadScreen (2, true);
	}
}
