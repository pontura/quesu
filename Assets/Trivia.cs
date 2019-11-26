using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trivia : MainScreen
{
	public TriviaPairButtons pairButton;
	int totalPairs = 1;
	public int serieID = 0;
	int separationY = 176;
	int separation = 370;
	public Transform container;
	public TimerManager timerManager;
	int itemId;
	public FeedbackManager feedbackManager;
    int pairID;
    public int rondaID; 

    public override void OnInit()
	{
		Events.OnResetTrivia ();
		itemId = 0;
		timerManager.Init (Data.Instance.settings.triviaDuration);
		LoopUntilReady ();
		feedbackManager.Init();		
	}
	public override void OnReset()
	{
		Utils.RemoveAllChildsIn (container);
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
		LoadNewSerie ();
		serieID++;
	}
	public void LoadNewSerie()
	{		
		Events.OnSoundFX("swipe");
		Utils.RemoveAllChildsIn (container);
		container.transform.localPosition = new Vector2(0,container.transform.localPosition.y);
		StartCoroutine (LoadRoutine ());
	}
	IEnumerator LoadRoutine()
	{
		pairID = 0;
		for (int a = 0; a < totalPairs; a++) {
			LoadPair ();
			yield return new WaitForSeconds (0.35f);
		}
		timerManager.SetState(true);
		Events.OnMusic("clock");
		yield return null;
	}
    Vector2 diffYears;

    public void LoadPair()
	{
		TriviaPairButtons newPairButton = Instantiate (pairButton);
		newPairButton.transform.SetParent (container);
		newPairButton.transform.localScale = Vector3.one;
		newPairButton.transform.localPosition = new Vector3 (0, -separationY*pairID, 0);
		ItemData data1 = GetNext ();
		ItemData data2 = GetPairFor(data1);
        diffYears = GetDiffYears();
        newPairButton.Init (this, data1, data2);
		Events.OnSoundFX("boing");
		pairID++;
        Debug.Log("pairID " + pairID + "   diffYears: " + diffYears + "     data1.year " + data1.year + "    data2.year " + data2.year);
        itemId++;
        rondaID++;
    }
	ItemData GetNext()
	{
	//	print ("pairID " + pairID + "  itemId " + itemId + " length: "+ Data.Instance.triviaData.triviaContent.all.Length);
		if (Data.Instance.triviaData.triviaContent.all.Count <= itemId-1)
			itemId = 0;

        ItemData data =  Data.Instance.triviaData.triviaContent.all[itemId]; 
		data.usedInGame = true;
		
        return data;
	}
    ItemData GetPairFor(ItemData firstPair)
    {
        int year = firstPair.year;
        int id = 0;
        foreach(ItemData itemData in Data.Instance.triviaData.triviaContent.all)
        {
            id++;
            int year1 = firstPair.year;
            int year2 = itemData.year;
            int difYearsOfThisPair = Mathf.Abs(year1 - year2);
            if (difYearsOfThisPair < diffYears[0] && difYearsOfThisPair > diffYears[1] && itemId < id)
            {
                itemData.usedInGame = true;
                itemId = id;
                return itemData;
            }
        }
        itemId = 0;
        return GetPairFor(firstPair);
    }
    Vector2 GetDiffYears()
    {
        if (rondaID == 0)
            return new Vector2(1000, 30);
        else if (rondaID == 1)
            return new Vector2(30, 20);
        else if (rondaID == 2)
            return new Vector2(20, 10);
        else if (rondaID >= 3 && rondaID < 5)
            return new Vector2(10, 5);
        else if (rondaID >= 5 && rondaID < 7)
            return new Vector2(5, 3);
        else
            return new Vector2(3, 1);
    }
    int pairDone = 0;
	public void PairDone()
	{
		pairDone++;
		if (pairDone >= totalPairs) {
			feedbackManager.Next();
			Events.OnMusic("");
			timerManager.SetState(false);
			Invoke("Next", 1.25f);
			pairDone = 0;
		}
	}
	void Next()
	{		
		iTween.MoveTo(container.gameObject, iTween.Hash(
			"x", -separation,
			"islocal", true,
			"time", 1,
			"oncomplete", "OnAnimationReady",
			"oncompletetarget", this.gameObject
		));
	}
	void OnAnimationReady()
	{
		Invoke("Init", 0.1f);
	}
	public void TimeOver()
	{
		StopAllCoroutines ();
		CancelInvoke ();
		LoadScreen (4, true);
		Events.OnMusic("");
		Events.OnSoundFX("bell");
	}
}
