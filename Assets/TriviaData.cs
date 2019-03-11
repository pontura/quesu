using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriviaData : MonoBehaviour
{
	public TriviaContent triviaContent;
	public bool loaded;
	public string triviaName;

	[Serializable]
	public class TriviaContent
	{
		public int tagID;
		public ItemData[] all;
	}
	public void EmptyData()
	{
		triviaContent.all = new ItemData[0];
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

		#ifUNITY_WEBGL
		headers.Add("Access-Control-Allow-Credentials", "true");
		headers.Add("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
		headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
		headers.Add("Access-Control-Allow-Origin", "*");
		#endif

		using (WWW www = new WWW(Data.Instance.serverManager.serverURL + "images/" + url, null, headers))
		{			
			yield return www;
			itemData.texture = www.texture;
		}
	}
}
