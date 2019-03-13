using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetosManager : MonoBehaviour
{
    private string secretKey = "pontura";
    string setRetoURL = "http://pontura.com/quesu/setReto.php";
    string setRetoToUserURL = "http://pontura.com/quesu/setRetoToUser.php";

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

}
