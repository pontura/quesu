using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Timeline : MainScreen
{
	public ItemData[] all;
	public TimelineItem item;
	public TimelineItem emptyItem;
	public TimelineItem emptyIinitialItem;
	int totalPairs = 3;
	public int serieID = 0;
	int separationY = 148;
	int separation = 370;
	public Transform container;
	int itemId;

	public override void OnInit()
	{
		LoopUntilReady ();
		GetComponent<Results> ().OnInit ();
	}
	public override void OnReset()
	{
		Utils.RemoveAllChildsIn (container);
		all = new ItemData[0];
	}
	void LoopUntilReady()
	{
		if (Data.Instance.triviaData.loaded) {
			Init ();
			return;
		}
		Invoke ("LoopUntilReady", 0.1f);
	}
	public void Init()
	{
		all = Data.Instance.triviaData.triviaContent.all;
		all = all.OrderBy (go => go.year).ToArray ();
		LoadItems ();
	}
	void LoadItems()
	{
		int id = 0;
		AddEmptyInitialItem ();
		bool startedDisplaying = false;
		foreach(ItemData data in all)
		{
			if (data.usedInGame) {
				startedDisplaying = true;
				AddItem (data);
			}			
			id++;
			if (startedDisplaying) {
				if (id >= all.Length - 1)
					return;
				else {
					ItemData nextItem = all [id];
					if (nextItem.year != data.year)
						AddYears (data.year + 1, nextItem.year - 1);
				}
			}
		}
		AddEmptyInitialItem ();
	}
	void AddYears(int year1, int year2)
	{
		for (int a = year1; a < year2; a++) {
			AddEmptyItem (a);
		}
	}
	void AddItem(ItemData data)
	{
		TimelineItem newPairButton = Instantiate (item);
		newPairButton.transform.SetParent (container);
		newPairButton.transform.localScale = Vector3.one;
		newPairButton.Init (data);
	}
	void AddEmptyItem(int year)
	{
		TimelineItem newPairButton = Instantiate (emptyItem);
		newPairButton.transform.SetParent (container);
		newPairButton.transform.localScale = Vector3.one;
		newPairButton.InitEmpty (year);
	}
	void AddEmptyInitialItem()
	{
		TimelineItem newPairButton = Instantiate (emptyIinitialItem);
		newPairButton.transform.SetParent (container);
		newPairButton.transform.localScale = Vector3.one; 
	}

}
