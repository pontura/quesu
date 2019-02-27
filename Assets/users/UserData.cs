using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
	const string PREFAB_PATH = "UserData";   
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

				if (mInstance == null)
				{
					GameObject go = Instantiate(Resources.Load<GameObject>(PREFAB_PATH)) as GameObject;
					mInstance = go.GetComponent<UserData>();
				}
			}
			return mInstance;
		}
	}
	void Awake()
	{
		if (!mInstance)
			mInstance = this;
		else
		{
			Destroy(this.gameObject);
			return;
		}

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
