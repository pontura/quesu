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

	[Serializable]
	public class TriviaContent
	{
		public int tagID;
		public List<ItemData> all;
	}
    private void Start()
    {
        if (!reload)
        {
            foreach (ItemData id in triviaContent.all)
                StartCoroutine(LoadImage(id, id.image));
        } else
        if (Data.Instance.format == Data.formats.STANDALONE)
            Load(11);
    }
    public void Load(int id)
	{
        print("Load" + id);
        //television y series = 11;
        //tecnologia es id = 3:
       // id = 11;

        if (id == 0)
        {
            Data.Instance.serverManager.LoadTriviaByCategory("all", 200);
        }
        else 
        {
            Data.Instance.serverManager.LoadTrivia(id, 200);
         }
        Data.Instance.triviaData.triviaName = Data.Instance.tagsData.GetTitleById(id);
	}
	public void EmptyData()
	{
        triviaContent.all.Add(new ItemData());
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
            path = Data.Instance.serverManager.serverURL + "images/";
        else
            path = Application.streamingAssetsPath + "/images/";

      //  Debug.Log("Image: " + path);
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
}
