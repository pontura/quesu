using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trivia : MainScreen
{
	public TriviaPairButtons[] pairButton;
	int totalPairs = 1;
	public int serieID = 0;
	int separationY = 176;
	int separation = 370;
	public Transform container;
	public TimerManager timerManager;
    public PairTimer pairTimer;
    [SerializeField] int itemId;
	public FeedbackManager feedbackManager;
    int pairID;
    public int rondaID; 
    [HideInInspector] public List<ItemData> usedItemsData;
    [HideInInspector] public TriviaPairButtons newPairButton;

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
			yield return new WaitForSeconds (0.45f);
		}
		timerManager.SetState(true);
        pairTimer.SetState(true);

        Events.OnMusic("clock");
		yield return null;
	}
    Vector2 diffYears;
    ItemData data1;
    public void LoadPair()
	{
        recursiveTimes = 0;
        usedItemsData.Clear();
        diffYears = GetDiffYears();
        print(diffYears);
        

        if(Data.Instance.mode == Data.modes.TRIPLE)
            newPairButton = Instantiate (pairButton[1]);
        else if (Data.Instance.mode == Data.modes.CUADROPLE)
            newPairButton = Instantiate(pairButton[2]);
        else
            newPairButton = Instantiate(pairButton[0]);

        newPairButton.transform.SetParent (container);
		newPairButton.transform.localScale = Vector3.one;
		newPairButton.transform.localPosition = new Vector3 (0, -separationY*pairID, 0);
		GetNext ();
        ItemData data2 = GetPairFor(data1);

        if (Data.Instance.mode == Data.modes.TRIPLE)
        {
            ItemData data3 = GetPairFor(data1);
           // Debug.Log("TRIPLE pairID " + pairID + "   diffYears: " + diffYears + "  data1.year " + data1.year + "    data2.year " + data2.year + "  data3.year " + data3.year);
            newPairButton.Init(this, data1, data2, data3);
        }
        else if (Data.Instance.mode == Data.modes.CUADROPLE)
        {
            ItemData data3 = GetPairFor(data1);
            ItemData data4 = GetPairFor(data1);
           // Debug.Log("CUADROPLE pairID " + pairID + "   diffYears: " + diffYears + "  data1.year " + data1.year + "    data2.year " + data2.year + "  data3.year " + data3.year + " data4.year " + data4.year);
            newPairButton.Init(this, data1, data2, data3, data4);
        }
        else
        {
            //Debug.Log("pairID " + pairID + "   diffYears: " + diffYears + "  data1.year " + data1.year + "    data2.year " + data2.year);
            newPairButton.Init(this, data1, data2);
        }
            

		Events.OnSoundFX("boing");
		pairID++;
        
        itemId++;
        rondaID++;
    }
	ItemData GetNext()
	{
		print ("___GetNext pairID " + pairID + "  itemId " + itemId );
		if (itemId >= Data.Instance.triviaData.triviaContent.all.Count-1)
			itemId = 0;

        data1 =  Data.Instance.triviaData.triviaContent.all[itemId];
        data1.usedInGame = true;
		
        return data1;
	}
    int yearLater;
    int recursiveTimes = 0;
    ItemData GetPairFor(ItemData firstPair)
    {
        if (usedItemsData.Count == 0)
        {
            yearLater = firstPair.year;
            usedItemsData.Add(firstPair);
        }
        int year = firstPair.year;
        int id = 0;

        Utils.Shuffle(Data.Instance.triviaData.triviaContent.all);
        foreach (ItemData itemData in Data.Instance.triviaData.triviaContent.all)
        {
            id++;

            int year2 = itemData.year;
            int difYearsOfThisPair = Mathf.Abs(yearLater - year2);

             print("id: " + id + "  itemId: " + itemId + "  1 year: " + year +  "  2d: " + itemData.year + " diff: "+ difYearsOfThisPair + "  diffYears:[ " + diffYears[0] + "/" + diffYears[1] + " ]");

            if (year2 != year && difYearsOfThisPair < diffYears[0] && difYearsOfThisPair > diffYears[1] && firstPair != itemData && itemId < id && !IsUsed(itemData))
            {
                if (yearLater > itemData.year)
                    yearLater = itemData.year;
                itemData.usedInGame = true;
                itemId = id;
                usedItemsData.Add(itemData);
                return itemData;
            }
        }
        diffYears[0] *= 2;
        diffYears[1] /= 1.5f;
        itemId = 0;
        recursiveTimes++;
         print("_________________ recursiveTimes: " + recursiveTimes + " year: " +  year);
        if (recursiveTimes > 10)
        {
             print("_________________ <firstPair>");
            return firstPair;
        }

        ///GetNext();
        return GetPairFor(firstPair);
    }

    bool IsUsed(ItemData id)
    {
        foreach (ItemData usedItemData in usedItemsData)
            if (usedItemData == id)
                return true;
        return false;
    }
    Vector2 GetDiffYears()
    {
        return new Vector2(20, 1);
    }
    //Vector2 GetDiffYears()
    //{
    //    if (rondaID == 0)
    //        return new Vector2(1000, 25);
    //    else if (rondaID <3)
    //        return new Vector2(50, 22);
    //    else if (rondaID <7)
    //        return new Vector2(30, 20);
    //    else if (rondaID < 10)
    //        return new Vector2(20, 10);
    //    else if (rondaID < 15)
    //        return new Vector2(15, 8);
    //    else if (rondaID < 21)
    //        return new Vector2(10, 6);
    //    else
    //        return new Vector2(7, 3);
    //}
    int pairDone = 0;
	public void PairDone()
	{
		pairDone++;
		if (pairDone >= totalPairs) {
			feedbackManager.Next();
			Events.OnMusic("");
			timerManager.SetState(false);
            pairTimer.SetState(false);
            Invoke("Next", Data.Instance.settings.timeForFeedback);
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
