using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Timeline : MainScreen
{
    public List<ItemData> all;
    public TimelineItem item;
    public TimelineItem emptyItem;
    public TimelineItem emptyIinitialItem;
    int totalPairs = 3;
    public int serieID = 0;
    int separationY = 148;
    int separation = 370;
    public Transform container;
    int itemId;
    
    public void Retar()
    {
        if(UserData.Instance.userID == "")
        {
            Events.OnPopup("¡No estás registrado!");
            return;
        }
        LoadScreen(7, true);
    }
    public override void OnInit()
    {
        LoopUntilReady();
        GetComponent<Results>().OnInit();
    }
    public override void OnReset()
    {
        Utils.RemoveAllChildsIn(container);
        all = new List<ItemData>();
    }
    void LoopUntilReady()
    {
        if (Data.Instance.triviaData.loaded)
        {
            Init();
            return;
        }
        Invoke("LoopUntilReady", 0.1f);
    }
    public void Init()
    {
        ItemData[] allItemsOrdered = Data.Instance.triviaData.triviaContent.all.OrderBy(go => go.year).ToArray();
        all = new List<ItemData>();
        foreach (ItemData itemData in allItemsOrdered)
        {
            if (itemData.usedInGame)
                all.Add(itemData);
        }
        LoadItems();
    }
    void LoadItems()
    {
        int id = 0;
        AddEmptyInitialItem();
        bool startedDisplaying = false;
        foreach (ItemData data in all)
        {
            if (data.usedInGame)
            {
                startedDisplaying = true;
                AddItem(data);
            }
            id++;
            if (startedDisplaying)
            {
                if (id >= all.Count - 1)
                    return;
                else
                {
                    ItemData nextItem = all[id];
                    if (nextItem.year != data.year)
                        AddYears(data.year + 1, nextItem.year - 1);
                }
            }
        }
        AddEmptyInitialItem();
    }
    void AddYears(int year1, int year2)
    {
        for (int a = year1; a < year2; a++)
        {
            AddEmptyItem(a);
        }
    }
    void AddItem(ItemData data)
    {
        TimelineItem newPairButton = Instantiate(item);
        newPairButton.transform.SetParent(container);
        newPairButton.transform.localScale = Vector3.one;
        newPairButton.Init(data);
    }
    void AddEmptyItem(int year)
    {
        TimelineItem newPairButton = Instantiate(emptyItem);
        newPairButton.transform.SetParent(container);
        newPairButton.transform.localScale = Vector3.one;
        newPairButton.InitEmpty(year);
    }
    void AddEmptyInitialItem()
    {
        TimelineItem newPairButton = Instantiate(emptyIinitialItem);
        newPairButton.transform.SetParent(container);
        newPairButton.transform.localScale = Vector3.one;
    }

}
