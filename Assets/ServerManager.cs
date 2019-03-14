using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ServerManager : MonoBehaviour
{
    public string GetTrivia = "getTrivia.php";
		public string GetReto = "getReto.php";
    public string GetTags = "getTags.php";
    public string serverURL = "http://pontura.com/quesu/";

    void Start()
    {
        StartCoroutine(LoadTags());
    }
    IEnumerator LoadTags()
    {
        string path = serverURL + GetTags;
        WWW www = new WWW(path);
        yield return www;
        if (www.error != null)
            print("There was an error: " + www.error);
        else
        {
            string result = www.text;
            Data.Instance.tagsData.SetData(JsonUtility.FromJson<TagsData.Tags>(result));
            //	LoadTrivia (4, 20);
        }
    }
    public void LoadTriviaByCategory(string categoryName, int limit)
    {
        Data.Instance.triviaData.EmptyData();
        StartCoroutine(LoadTriviaByCategoryR(categoryName, limit));
    }
    IEnumerator LoadTriviaByCategoryR(string categoryName, int limit)
    {
        string path = serverURL + GetTrivia + "?categoria=" + categoryName + "&limit=" + limit;
        WWW www = new WWW(path);
        yield return www;
        if (www.error != null)
            print("There was an error: " + www.error);
        else
        {
            string result = www.text;
            Data.Instance.triviaData.SetData(JsonUtility.FromJson<TriviaData.TriviaContent>(result), 0);
        }
    }
		public void LoadRetos(string userID)
		{
			StartCoroutine(LoadRetosC(userID));
		}
    IEnumerator LoadRetosC(string userID)
    {
        string path = serverURL + GetReto + "?userID=" + userID;
				print("LoadRetosC path: " + path);
        WWW www = new WWW(path);
        yield return www;
        if (www.error != null)
            print("There was an error: " + www.error);
        else
        {
            string result = www.text;
            Data.Instance.retosManager.SetData(JsonUtility.FromJson<RetosManager.RetosContent>(result));
        }
    }
    public void LoadTrivia(int tagID, int limit)
    {
        Data.Instance.triviaData.EmptyData();
        StartCoroutine(LoadTriviaR(tagID, limit));
    }
    IEnumerator LoadTriviaR(int tagID, int limit)
    {
        string path = serverURL + GetTrivia + "?id=" + tagID + "&limit=" + limit;
        WWW www = new WWW(path);
        yield return www;
        if (www.error != null)
            print("There was an error: " + www.error);
        else
        {
            string result = www.text;
            Data.Instance.triviaData.SetData(JsonUtility.FromJson<TriviaData.TriviaContent>(result), tagID);

        }
    }



}
