using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UsersManager : MonoBehaviour
{
    public string GetUsers = "getUsers.php";
    public Users users;
    public bool loaded;
    public bool imagesLoaded;
    [Serializable]
    public class Users
    {
        public UserData[] all;
    }
    [Serializable]
    public class UserData
    {
        public string username;
        public string userID;
        public Texture2D texture;
    }

    public void SetData(Users data)
    {
        users = data;
        loaded = true;
    }

    void Start()
    {
        if(Data.Instance.format == Data.formats.CLASSIC)
            StartCoroutine(LoadUsers(50));
    }
    IEnumerator LoadUsers(int limit)
    {
        string path = Data.Instance.serverManager.serverURL + GetUsers + "?limit=" + limit;
        print("LoadUsers: " + path);
        WWW www = new WWW(path);
        yield return www;
        if (www.error != null)
            print("There was an error: " + www.error);
        else
        {
            string result = www.text;
            SetData(JsonUtility.FromJson<Users>(result));
            //	LoadTrivia (4, 20);
        }
    }
    public void LoadImages()
    {
        if (!loaded)
            return;
        if (imagesLoaded)
            return;
        imagesLoaded = true;
        foreach (UserData data in users.all)
            StartCoroutine(LoadImageCoroutine(data, data.userID + ".png"));
    }
    IEnumerator LoadImageCoroutine(UserData data, string url)
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();

# if UNITY_WEBGL
        headers.Add("Access-Control-Allow-Credentials", "true");
        headers.Add("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
        headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        headers.Add("Access-Control-Allow-Origin", "*");
#endif

        string completeUrl = Data.Instance.serverManager.serverURL + "users/" + url;
        Debug.Log("loading image: " + completeUrl);
        using (WWW www = new WWW(completeUrl, null, headers))
        {
            yield return www;
            data.texture = www.texture;
        }
    }
    public UserData GetData(string _userID)
    {
        foreach (UserData d in users.all)
        {
            if (d.userID == _userID)
                return d;
        }
        return null;
    }
}
