using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriviaData : MonoBehaviour
{
	public TriviaContent triviaContent;
	public bool loaded;

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
		using (WWW www = new WWW(Data.Instance.serverManager.serverURL + "images/" + url))
		{
			yield return www;
			itemData.texture = www.texture;
		}
	}
}
