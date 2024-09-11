using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriviaData : MonoBehaviour
{
    public bool reload = false;
	public TriviaContent triviaContent;
	public bool loaded;
	public string triviaName;
    public int tag_id;
    [SerializeField] int tag_id_num;
	[Serializable]
	public class TriviaContent
	{
		public int tagID;
		public List<ItemData> all;
	}
    //private void Start()
    //{
    //    if (!reload)
    //    {
    //        foreach (ItemData id in triviaContent.all)
    //            StartCoroutine(LoadImage(id, id.image));
    //    } else
    //    if (Data.Instance.format == Data.formats.STANDALONE)
    //        Load(11, null);
    //}
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
	public void SetData(TriviaContent _trivia, int tagID)
	{
		triviaContent = _trivia;
		triviaContent.tagID = tagID;
		foreach (ItemData id in triviaContent.all)
			StartCoroutine(LoadImage (id, id.image));

		loaded = true;
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
