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
	public override void OnButtonClicked(int tagID)
	{
		if (tagID == 1) {
			Data.Instance.serverManager.LoadTriviaByCategory ("historia", 40);
		} else if (tagID == 2) {
			Data.Instance.serverManager.LoadTrivia (9, 40);
		} else if (tagID == 3) {
			Data.Instance.serverManager.LoadTrivia (11, 40);
		}
		LoadScreen (0, false);
	}
}
