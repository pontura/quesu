using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RetosManager : MonoBehaviour
{
    public RetosContent retosContent;

    public RetoData openReto;

    [Serializable]
    public class RetosContent
    {
        public List<RetoData> all;
    }

    private string secretKey = "pontura";
    string setRetoURL = "http://pontura.com/quesu/setReto.php";
    string closeRetoURL = "http://pontura.com/quesu/closeReto.php";

    public void SetOpenReto(RetoData openReto)
    {
        this.openReto = openReto;
    }
    public void SetData(RetosContent _retosContent)
    {
        retosContent.all.Clear();
        retosContent = _retosContent;
        foreach (RetoData d in retosContent.all)
        {
            if (d.userID == UserData.Instance.userID)
            {
                d.username = "Vos";
            }
        }
    }
    public void SetNewReto(string userID, string username)
    {
        StartCoroutine(SetReto(userID, username));
    }
    IEnumerator SetReto(string userID2, string username2)
    {
        string userID = UserData.Instance.userID;
        string username = UserData.Instance.username;
        int tag_id = Data.Instance.triviaData.triviaContent.tagID;

        string hash = Utils.Md5Sum(userID + username + tag_id + secretKey);
        string post_url = setRetoURL + "?userID=" + WWW.EscapeURL(userID) + "&username=" + username;
        post_url += "&tag_id=" + tag_id + "&hash=" + hash;
        post_url += "&score=" + Data.Instance.resultsData.score;
        post_url += "&userID2=" + WWW.EscapeURL(userID2) + "&username2=" + username2;
        print(post_url);
        WWW www = new WWW(post_url);
        yield return www;

        if (www.error != null)
        {
            Events.OnServerResponse("There was an error: " + www.error);
        }
        else
        {
            string result = www.text;
            if (result == "exists")
            {
                Events.OnServerResponse("ya existe");
            }
            else
            {
                Events.OnServerResponse("Reto creado: " + www.text);
            }
        }
        yield return null;
    }
    public void CloseReto(int total)
    {
        openReto.score2 = total;
        openReto.userID2 = UserData.Instance.userID;
        openReto.username2 = UserData.Instance.username;
        openReto.ready = 1;
        StartCoroutine(CloseRetoC());
    }
    IEnumerator CloseRetoC()
    {
        string id = openReto.id.ToString();

        string userID = openReto.userID.ToString();
        string userID2 = openReto.userID2.ToString();

        string score = openReto.score.ToString();
        string score2 = openReto.score2.ToString();

        string hash = Utils.Md5Sum(id + secretKey);
        string post_url = closeRetoURL + "?id=" + id;
        post_url += "&userID=" + userID + "&userID2=" + userID2;
        post_url += "&score=" + score + "&score2=" + score2;
        post_url += "&hash=" + hash;
        print(post_url);
        WWW www = new WWW(post_url);
        yield return www;

        if (www.error != null)
        {
            Events.OnPopup("There was an error: " + www.error);
        }
        else
        {
            string result = www.text;
            Events.OnPopup("Reto cerrado " + www.text);
            retosContent.all.Remove(openReto);
            openReto = new RetoData();
            UserData.Instance.UpdateData();
        }
        yield return null;
    }
}
