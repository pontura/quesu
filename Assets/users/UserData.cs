using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
	static UserData mInstance = null;
	public string userID;
	public string username;

	public static UserData Instance
	{
		get
		{
			if (mInstance == null)
			{
				mInstance = FindObjectOfType<UserData>();
			}
			return mInstance;
		}
	}
	void Awake()
	{
		if (!mInstance)
			mInstance = this;

		username = PlayerPrefs.GetString ("username");
		userID = PlayerPrefs.GetString ("userID");
	}
	public void SetUserID(string userID)
	{
		this.userID = userID;
		PlayerPrefs.SetString ("userID", userID);
	}

	public void UserCreation()
	{
		
		PlayerPrefs.SetString ("username", username);
		PlayerPrefs.SetString ("userID", userID);
	}
}
