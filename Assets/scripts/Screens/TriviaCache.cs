using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriviaCache : MonoBehaviour
{

    Dictionary<string, ItemData> all;
    private void Awake()
    {
        all = new Dictionary<string, ItemData>();
    }
    List<ItemData> ronda;
    System.Action<List<ItemData>> OnDone;
    int totalImagesToChange;

    public void GetRonda(int rondaID, int qty, System.Action<List<ItemData>> OnDone)
    {
        print("GetRonda " + rondaID);
        this.totalImagesToChange = qty;
        this.OnDone = OnDone;
        Utils.Shuffle(Data.Instance.triviaData.triviaContent.all);

        ronda = new List<ItemData>(qty);

        ItemData dataMaster = GetItemData(Data.Instance.triviaData.triviaContent.all[0].id);
        ronda.Add(dataMaster);
        ronda.Add(GetPairFor(dataMaster));
        LoopForImagesReady();
    }
    void LoopForImagesReady()
    {
        if(OnDone == null)
        {
            Debug.LogError("LoopForImagesReady es null!");
            return;
        }
        if (AllImagesReady())
        {
            OnDone(ronda);
            OnDone = null;
        }
        else
            Invoke("LoopForImagesReady", 0.1f);
    }
    bool AllImagesReady()
    {
        foreach (ItemData itemData in ronda)
        {
            if (!itemData.textureReady)
                return false;
        }
        return true;
    }
    void OnRondaDone()
    {
        OnDone(ronda);
    }
    ItemData GetPairFor(ItemData dataMaster)
    {
        ItemData itemData = Data.Instance.triviaData.triviaContent.all[1];
        return GetItemData(itemData.id);
    }
    public ItemData GetItemData(string itemId)
    {
        print("GetItemData " + itemId + " all: " + all + " count:" + all.Count);
        if (all.ContainsKey(itemId))
            return all[itemId];
        return GetNonCache(itemId);
    }
    ItemData GetNonCache(string itemId)
    {
        ItemData item = Data.Instance.triviaData.triviaContent.GetItem(itemId);
        SetNewItem(item);
        Data.Instance.triviaData.LoadImage(item, OnReady);
        return item;
    }
    void OnReady(Texture2D t)
    {
        print("New image");
    }
    public void SetNewItem(ItemData itemData)
    {
        if (!all.ContainsKey(itemData.id))
            all.Add(itemData.id, itemData);
    }
}
