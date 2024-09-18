using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trivia : MainScreen
{
    public TriviaPairButtons[] pairButton;
    int totalPairs = 1;
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
    [HideInInspector] TriviaCache cache;
    [SerializeField] ButtonUI closeBtn;
    [SerializeField] Image bg;

    private void Awake()
    {
        cache = GetComponent<TriviaCache>();
        closeBtn.Init(OnClose);
    }

    private void OnClose(ButtonUI obj)
    {
        Events.OnConfirmationPopup("¿Seguro deseas salir?", Exit);
    }
    void Exit(bool isOk)
    {
        if (isOk)
            Exit();
    }

    public override void OnInit()
    {
        print("OnInit");
        Events.OnResetTrivia();
        itemId = 0;
        timerManager.Init(Data.Instance.settings.triviaDuration);
        LoopUntilReady();
        feedbackManager.Init();
        Data.Instance.settings.GetCategoryData(Data.Instance.triviaData.tag_id, SetBG);
    }
    void SetBG(Settings.CategorieData data)
    {
        Color c = data.color;
        c.a = 0.8f;
        bg.color = c;
    }
    public override void OnReset()
    {
        Utils.RemoveAllChildsIn(container);
    }
    void LoopUntilReady()
    {
        print("___LoopUntilReady");
        if (Data.Instance.triviaData.IsLoaded()) {
            CancelInvoke();
            Init();
        } else
            Invoke("LoopUntilReady", 0.1f);
    }
    void Init()
    {
        Events.OnSoundFX("swipe");
        container.transform.localPosition = new Vector2(0, container.transform.localPosition.y);

        pairID = rondaID = 0;
       

        Utils.RemoveAllChildsIn(container);
        newPairButton = Instantiate(pairButton[0]);
        newPairButton.transform.SetParent(container);
        newPairButton.transform.localScale = Vector3.one;
        newPairButton.transform.localPosition = new Vector3(0, -separationY * pairID, 0);
        newPairButton.SetInit();
        SetRonda();
    }

    void SetRonda()
    {
        print("SetRonda");
        rondaID++;
        cache.GetRonda(rondaID, 2, OnRondaReady);
    }
    void OnRondaReady(List<ItemData> rondaItems)
    {
        print("OnRondaReady");
        newPairButton.Init(this, rondaItems[0], rondaItems[1]);
        timerManager.SetState(true);
        pairTimer.SetState(true);
        Events.OnInitRonda();
        Events.OnMusic("clock");
    }


    public void Exit()
    {
        StopAllCoroutines();
        CancelInvoke();
        LoadScreen(screens.CATEGORY_RANKING, true);
        Events.OnMusic("");
    }
    //Ends:
    public void TimeOver()
    {
        StopAllCoroutines();
        CancelInvoke();
        LoadScreen(screens.PROCESSING_RESULTS, true);
        Events.OnMusic("");
        Events.OnSoundFX("bell");
        Events.CloseConfirmationPopup();
    }
    public void PairDone()
    {
        print("PairDone");
        feedbackManager.Next();
        Events.OnMusic("");
        timerManager.SetState(false);
        pairTimer.SetState(false);
        Invoke("SetRonda", Data.Instance.settings.timeForFeedback);
        Events.CloseConfirmationPopup();
    }
    ////////////






    //   Vector2 diffYears;
    //   ItemData data1;
    //   void LoadPair()
    //   {
    //       print("Load Pair");
    //       recursiveTimes = 0;
    //       usedItemsData.Clear();
    //       diffYears = GetDiffYears();

    //       if (Data.Instance.mode == Data.modes.TRIPLE)
    //           newPairButton = Instantiate(pairButton[1]);
    //       else if (Data.Instance.mode == Data.modes.CUADROPLE)
    //           newPairButton = Instantiate(pairButton[2]);
    //       else

    //       GetNext();
    //       ItemData data2 = GetPairFor(data1);

    //       if (Data.Instance.mode == Data.modes.TRIPLE)
    //       {
    //           ItemData data3 = GetPairFor(data1);
    //           // Debug.Log("TRIPLE pairID " + pairID + "   diffYears: " + diffYears + "  data1.year " + data1.year + "    data2.year " + data2.year + "  data3.year " + data3.year);
    //           newPairButton.Init(this, data1, data2, data3);
    //       }
    //       else if (Data.Instance.mode == Data.modes.CUADROPLE)
    //       {
    //           ItemData data3 = GetPairFor(data1);
    //           ItemData data4 = GetPairFor(data1);
    //           // Debug.Log("CUADROPLE pairID " + pairID + "   diffYears: " + diffYears + "  data1.year " + data1.year + "    data2.year " + data2.year + "  data3.year " + data3.year + " data4.year " + data4.year);
    //           newPairButton.Init(this, data1, data2, data3, data4);
    //       }
    //       else
    //       {
    //           //Debug.Log("pairID " + pairID + "   diffYears: " + diffYears + "  data1.year " + data1.year + "    data2.year " + data2.year);
    //           newPairButton.Init(this, data1, data2);
    //       }


    //       Events.OnSoundFX("boing");
    //       pairID++;

    //       itemId++;
    //       rondaID++;
    //   }
    //   ItemData GetNext()
    //   {
    //       print("___GetNext pairID " + pairID + "  itemId " + itemId);
    //       if (itemId >= Data.Instance.triviaData.triviaContent.all.Count - 1)
    //           itemId = 0;

    //       data1 = cache.GetItemData(Data.Instance.triviaData.triviaContent.all[0].id);
    //       data1.usedInGame = true;

    //       return data1;
    //   }


    //   int yearLater;
    //   int recursiveTimes = 0;
    //   ItemData GetPairFor(ItemData firstPair)
    //   {
    //       if (usedItemsData.Count == 0)
    //       {
    //           yearLater = firstPair.year;
    //           usedItemsData.Add(firstPair);
    //       }
    //       int year = firstPair.year;
    //       int id = 0;

    //       Utils.Shuffle(Data.Instance.triviaData.triviaContent.all);
    //       foreach (ItemData itemData in Data.Instance.triviaData.triviaContent.all)
    //       {
    //           id++;

    //           int year2 = itemData.year;
    //           int difYearsOfThisPair = Mathf.Abs(yearLater - year2);

    //           // print("id: " + id + "  itemId: " + itemId + "  1 year: " + year +  "  2d: " + itemData.year + " diff: "+ difYearsOfThisPair + "  diffYears:[ " + diffYears[0] + "/" + diffYears[1] + " ]");

    //           if (year2 != year && difYearsOfThisPair < diffYears[0] && difYearsOfThisPair > diffYears[1] && firstPair != itemData && itemId < id && !IsUsed(itemData))
    //           {
    //               if (yearLater > itemData.year)
    //                   yearLater = itemData.year;
    //               itemData.usedInGame = true;
    //               itemId = id;
    //               cache.SetNewItem(itemData);
    //               usedItemsData.Add(itemData);
    //               return itemData;
    //           }
    //       }
    //       diffYears[0] *= 2;
    //       diffYears[1] /= 1.5f;
    //       itemId = 0;
    //       recursiveTimes++;
    //        print("_________________ recursiveTimes: " + recursiveTimes + " year: " +  year);
    //       if (recursiveTimes > 10)
    //       {
    //            print("_________________ <firstPair>");
    //           return firstPair;
    //       }

    //       ///GetNext();
    //       return GetPairFor(firstPair);
    //   }

    //   bool IsUsed(ItemData id)
    //   {
    //       foreach (ItemData usedItemData in usedItemsData)
    //           if (usedItemData == id)
    //               return true;
    //       return false;
    //   }
    //   Vector2 GetDiffYears()
    //   {
    //       return new Vector2(20, 1);
    //   }
    //   //Vector2 GetDiffYears()
    //   //{
    //   //    if (rondaID == 0)
    //   //        return new Vector2(1000, 25);
    //   //    else if (rondaID <3)
    //   //        return new Vector2(50, 22);
    //   //    else if (rondaID <7)
    //   //        return new Vector2(30, 20);
    //   //    else if (rondaID < 10)
    //   //        return new Vector2(20, 10);
    //   //    else if (rondaID < 15)
    //   //        return new Vector2(15, 8);
    //   //    else if (rondaID < 21)
    //   //        return new Vector2(10, 6);
    //   //    else
    //   //        return new Vector2(7, 3);
    //   //}
    //   int pairDone = 0;

    //void Next()
    //{		
    //	iTween.MoveTo(container.gameObject, iTween.Hash(
    //		"x", -separation,
    //		"islocal", true,
    //		"time", 1,
    //		"oncomplete", "OnAnimationReady",
    //		"oncompletetarget", this.gameObject
    //	));
    //}
    //void OnAnimationReady()
    //{
    //	Invoke("Init", 0.1f);
    //}
    //public void TimeOver()
    //{
    //	StopAllCoroutines ();
    //	CancelInvoke ();
    //	LoadScreen (4, true);
    //	Events.OnMusic("");
    //	Events.OnSoundFX("bell");
    //}
}
