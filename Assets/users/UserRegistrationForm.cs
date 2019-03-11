using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserRegistrationForm : MonoBehaviour
{
    public UserDataUI ui;
    private string secretKey = "pontura";
    public string setUserURL = "http://pontura.com/quesu/setUser.php";
    public string setUserURLUpload = "http://pontura.com/quesu/updateUser.php";
    UserData userData;
    public bool imageUploaded;

    public void Init()
    {
        userData = UserData.Instance;
        LoadUser();
    }
    void LoadUser()
    {
        if (PlayerPrefs.GetString("userID") != "")
        {
            userData.userID = PlayerPrefs.GetString("userID");
            userData.username = PlayerPrefs.GetString("username");
        }
        else
        {
#if UNITY_EDITOR
            userData.userID = Random.Range(0, 10000).ToString();
            userData.SetUserID(userData.userID);
#elif UNITY_ANDROID
				userData.userID = SystemInfo.deviceUniqueIdentifier;
				userData.SetUserID(userData.userID);
#endif
        }
    }

    void UserCreation()
    {
        ui.DebbugText.text = "new User Created " + UserData.Instance.username;
        UserData.Instance.UserCreation();
        UsersEvents.OnRegistartionDone();
    }
    void UserUploaded()
    {
        ui.DebbugText.text = "User uploaded " + UserData.Instance.username;
        UserData.Instance.UserCreation();
        UsersEvents.OnUserUploadDone();
    }

    public void SaveUser(string username)
    {
        ui.DebbugText.text = "Checking data...";
        UserData.Instance.username = username;
        StartCoroutine(SendData(UserData.Instance.username));
    }
    public void UploadUser(string username)
    {
        ui.DebbugText.text = "Uploading data...";
        UserData.Instance.username = username;
        StartCoroutine(UploadData(UserData.Instance.username));

    }
    public void SavePhoto()
    {
        string imagePath = UserData.Instance.path;

        if (UserData.Instance.userID == "")
        {
            Debug.LogError("NO EXISTE EL USUARIO");
            return;
        }

        StartCoroutine(UploadFileCo(imagePath + UserData.Instance.userID + ".png", "http://pontura.com/quesu/uploadPhoto.php"));
    }
    IEnumerator SendData(string username)
    {
        string hash = Md5Sum(UserData.Instance.userID + username + secretKey);
        string post_url = setUserURL + "?userID=" + WWW.EscapeURL(UserData.Instance.userID) + "&username=" + username + "&hash=" + hash;
        print(post_url);
        WWW www = new WWW(post_url);
        yield return www;

        if (www.error != null)
        {
            ui.DebbugText.text = ("There was an error: " + www.error);
        }
        else
        {
            string result = www.text;
            if (result == "exists")
            {
                ui.DebbugText.text = "ya existe";
            }
            else
            {
                UserCreation();
            }
        }
    }
    IEnumerator UploadData(string username)
    {
        string hash = Md5Sum(UserData.Instance.userID + username + secretKey);
        string post_url = setUserURLUpload + "?userID=" + WWW.EscapeURL(UserData.Instance.userID) + "&username=" + username + "&hash=" + hash;
        print(post_url);
        WWW www = new WWW(post_url);
        yield return www;

        if (www.error != null)
        {
            ui.DebbugText.text = ("There was an error: " + www.error);
        }
        else
        {
            string result = www.text;
            if (result == "exists")
            {
                ui.DebbugText.text = "ya existe";
            }
            else
            {
                UserUploaded();
            }
        }
    }
    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }



    IEnumerator UploadFileCo(string localFileName, string uploadURL)
    {
        print("file" + localFileName);
        print("to: " + uploadURL);
        WWW localFile = new WWW("file:///" + localFileName);
        yield return localFile;
        if (localFile.error == null)
            Debug.Log("Loaded file successfully");
        else
        {
            Debug.Log("Open file error: " + localFile.error);
            yield break; // stop the coroutine here
        }
        WWWForm postForm = new WWWForm();
        postForm.AddBinaryData("theFile", localFile.bytes, localFileName, "text/plain");
        WWW upload = new WWW(uploadURL, postForm);
        yield return upload;
        if (upload.error == null)
            Debug.Log("upload done :" + upload.text);
        else
            Debug.Log("Error during upload: " + upload.error);
    }
}
