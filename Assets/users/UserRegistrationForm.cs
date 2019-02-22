using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserRegistrationForm : MonoBehaviour
{
	public InputField inputField;
	public Text DebbugText;
	public Image thumb;

	private string secretKey = "pontura";
	public string setUserURL = "http://pontura.com/quesu/setUser.php";

    void Start()
    {
		Texture2D td2;
		Sprite sprite = LoadSprite(Application.persistentDataPath + "/" + UserData.Instance.userID + ".png");
		if(sprite!=null)
			thumb.sprite = sprite;
		if (PlayerPrefs.GetString ("userID") != "") {
			UserData.Instance.userID = PlayerPrefs.GetString ("userID");
			UserData.Instance.username = PlayerPrefs.GetString ("username");
		}
		else {
			#if UNITY_EDITOR
				UserData.Instance.userID = Random.Range (0, 10000).ToString ();
				UserData.Instance.SetUserID (UserData.Instance.userID);
			#elif UNITY_ANDROID
				UserData.Instance.userID = SystemInfo.deviceUniqueIdentifier;
				UserData.Instance.SetUserID(UserData.Instance.userID);
			#endif
		}
    }
	void UserCreation()
	{
		DebbugText.text = "new User Created " + UserData.Instance.username;
		UserData.Instance.UserCreation ();
	}

	public void OnSubmit()
	{
		DebbugText.text = "Checking data...";
		UserData.Instance.username = inputField.text;
		StartCoroutine(SendData (UserData.Instance.username));
	}
	IEnumerator SendData(string username)
	{
		string hash = Md5Sum(UserData.Instance.userID + username + secretKey);
		string post_url = setUserURL + "?userID=" + WWW.EscapeURL(UserData.Instance.userID) + "&username=" + username + "&hash=" + hash;
		print (post_url);
		WWW www = new WWW(post_url);
		yield return www;

		if (www.error != null) {
			DebbugText.text = ("There was an error: " + www.error);
		} else {
			string result = www.text;
			if (result == "exists") {
				DebbugText.text = "ya existe";
			} else {
				UserCreation ();
			}
		}
	}
	public  string Md5Sum(string strToEncrypt)
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

	private Sprite LoadSprite(string path)
	{
		print ("LoadSprite " + path);
		if (string.IsNullOrEmpty(path)) return null;
		if (System.IO.File.Exists(path))
		{
			print ("si");
			byte[] bytes = System.IO.File.ReadAllBytes(path);
			Texture2D texture = new Texture2D(300, 300);
			texture.LoadImage(bytes);
			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			return sprite;
		}
		return null;
	}
}
