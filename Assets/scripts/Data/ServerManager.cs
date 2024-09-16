using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ServerManager : MonoBehaviour
{
    public string GetTrivia = "getTrivia.php";
    public string GetReto = "getReto.php";
    public string GetUser = "getUser.php";
    public string GetClient = "getClient.php";
    public string GetTags = "getTags.php";
    public string GetClientTags = "getClientTags.php";

    public string ServerURL
    {
        get
        {
            return "https://quesu.xyz/";
        }
    }

    void Start()
    {
        StartCoroutine(LoadTags());
    }
    IEnumerator LoadTags()
    {
        string path = ServerURL + GetTags;
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
        string path = ServerURL + GetTrivia + "?categoria=" + categoryName + "&limit=" + limit;
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
    public void LoadTrivia(int tagID, int limit, System.Action OnDone)
    {
        Data.Instance.triviaData.EmptyData();
        StartCoroutine(LoadTriviaR(tagID, limit, OnDone));
    }
    IEnumerator LoadTriviaR(int tagID, int limit, System.Action OnDone)
    {
        string path = ServerURL + GetTrivia + "?id=" + tagID + "&limit=" + limit;
        Debug.Log(path);
        WWW www = new WWW(path);
        yield return www;
        if (www.error != null)
            print("There was an error: " + www.error);
        else
        {
            string result = www.text;
            Data.Instance.triviaData.SetData(JsonUtility.FromJson<TriviaData.TriviaContent>(result), tagID);

        }
        if(OnDone != null)
            OnDone();
    }
    public ClientDataJson clientDataJson;
    [Serializable]
    public class ClientDataJson
    {
        public int id;
        public string username;
        public List<ClientDataTagsJson> all;
    }
    [Serializable]
    public class ClientDataTagsJson
    {
        public int tag_id;
    }
    public class UserDataJson
    {
        public int retosGanados;
        public int retosPerdidos;
    }
    public void LoadUserData(string userID, System.Action OnReady)
    {
        StartCoroutine(LoadUserDataC(userID, OnReady));
    }
    IEnumerator LoadUserDataC(string userID, System.Action OnReady)
    {
        string path = ServerURL + GetUser + "?userID=" + userID;
        print("path" + path);
        WWW www = new WWW(path);
        yield return www;
        if (www.error != null)
            print("There was an error: " + www.error);
        else
        {
            string result = www.text;
            print(result);
            UserDataJson userData = JsonUtility.FromJson<UserDataJson>(result);
            if(OnReady != null)
                OnReady();
           // UserData.Instance.UpdateRetosValue(userData.retosGanados,  userData.retosPerdidos);
        }
    }
    System.Action<ClientDataJson> OnClientDataDone;

    public void GetClientByPassword(string password, System.Action<ClientDataJson> OnClientDataDone)
    {
        this.OnClientDataDone = OnClientDataDone;
        StartCoroutine(LoadUserDataC2(password));
    }
    IEnumerator LoadUserDataC2(string password)
    {
        string path = ServerURL + GetClient + "?password=" + password;
        print("path" + path);
        WWW www = new WWW(path);
        yield return www;
        if (www.error != null || www.text == "false")
            OnClientDataDone(null);
        else
        {
            string result = www.text;
            print(result);
            clientDataJson = JsonUtility.FromJson<ClientDataJson>(result);
            GetClientByPassword(clientDataJson.id);
        }
    }
    public void GetClientByPassword(int client_id)
    {
        StartCoroutine(LoadUserDataC3(client_id));
    }
    IEnumerator LoadUserDataC3(int client_id)
    {
        string path = ServerURL + GetClientTags + "?id=" + client_id;
        print("path" + path);
        WWW www = new WWW(path);
        yield return www;
        if (www.error != null)
        {
            OnClientDataDone(null);
            print("error  " + www.error);
        }
        else
        {
            string result = www.text;
            print("result " + result);
            clientDataJson = JsonUtility.FromJson<ClientDataJson>(result);
            if (clientDataJson == null || clientDataJson.all.Count == 0)
                OnClientDataDone(null);
            else
            {
                OnClientDataDone(clientDataJson);
            }
        }
    }


}
