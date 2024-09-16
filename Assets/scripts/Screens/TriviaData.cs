using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriviaData : MonoBehaviour
{
    bool loaded;
    public bool reload = false;
	public TriviaContent triviaContent;
	public string triviaName;
    public int tag_id;
    [SerializeField] int tag_id_num;
	[Serializable]
	public class TriviaContent
	{
		public int tagID;
		public List<ItemData> all;
	}
    public bool IsLoaded()
    {
        return loaded;
    }
    void SetId()
    {
        int num = 0;
        foreach(TagData td  in Data.Instance.tagsData.tags.all)
        {
            if (td.id == tag_id)
            {
                this.tag_id_num = num;
                return;
            }
            num++;
        }
    }
    public void SetActualData(int tag_id)
    {
        this.tag_id = tag_id;
        SetId();
        print("Load trivia for tag tag_id:" + tag_id);
        Data.Instance.triviaData.triviaName = Data.Instance.tagsData.GetTitleById(tag_id);
    }
    public void Load(System.Action OnDone)
	{
        SetActualData(tag_id);
        Data.Instance.serverManager.LoadTrivia(tag_id, 200, OnDone);
	}
	public void EmptyData()
	{
        triviaContent.all.Clear();
	}

    int totalImages;
    int imagesLoaded;
    public void SetData(TriviaContent _trivia, int tagID)
	{
        loaded = false;
        imagesLoaded = 0;
        triviaContent = _trivia;
		triviaContent.tagID = tagID;
        totalImages = triviaContent.all.Count;

        foreach (ItemData id in triviaContent.all)
			StartCoroutine(LoadImage (id, id.image));
	}

	IEnumerator LoadImage(ItemData itemData, string url)
	{
		Dictionary<string, string> headers = new Dictionary<string, string>();

#if UNITY_WEBGL
		headers.Add("Access-Control-Allow-Credentials", "true");
		headers.Add("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
		headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
		headers.Add("Access-Control-Allow-Origin", "*");
#endif
        string path = "";
        if(Data.Instance.triviaData.reload)
            path = Data.Instance.serverManager.ServerURL + "images/";
        else
            path = Application.streamingAssetsPath + "/images/";

        Debug.Log("Image: " + path + url);
        using (WWW www = new WWW(path + url, null, headers))
		{			
			yield return www;
			itemData.texture = www.texture;

            imagesLoaded++;
            if (imagesLoaded > totalImages/1.5f)
                loaded = true;

        }
	}
    public void RefreshAll()
    {
        foreach (ItemData itemData in triviaContent.all)
            itemData.usedInGame = false;
        Utils.Shuffle(triviaContent.all);
    }
    public void Next()
    {
        tag_id_num++;
        if (tag_id_num >= Data.Instance.tagsData.tags.all.Length)
            tag_id_num = 0;
        SetActualData(Data.Instance.tagsData.tags.all[tag_id_num].id);
    }
    public void Prev()
    {
        tag_id_num--;
        if (tag_id_num < 0)
            tag_id_num = Data.Instance.tagsData.tags.all.Length-1;
        SetActualData(Data.Instance.tagsData.tags.all[tag_id_num].id);
    }
}
