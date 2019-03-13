using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class Data : MonoBehaviour
{
	public builds build;
	public enum builds
	{
		RELEASE,
		DEBUG
	}

    const string PREFAB_PATH = "Data";    
    static Data mInstance = null;
	public ServerManager serverManager;
	public TriviaData triviaData;
	public TagsData tagsData;
	public UsersManager usersManager;
	public RetosManager retosManager;
	public Settings settings;
	public ResultsData resultsData;
	public string triviaName;

	public static Data Instance
	{
		get
		{
			if (mInstance == null)
			{
				mInstance = FindObjectOfType<Data>();

				if (mInstance == null)
				{
					GameObject go = Instantiate(Resources.Load<GameObject>(PREFAB_PATH)) as GameObject;
					mInstance = go.GetComponent<Data>();
				}
			}
			return mInstance;
		}
	}
    public string currentLevel;
    public void LoadLevel(string aLevelName)
    {
		print ("goto " + aLevelName);
        this.currentLevel = aLevelName;
        SceneManager.LoadScene(aLevelName);
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
        DontDestroyOnLoad(this.gameObject);
		serverManager = GetComponent<ServerManager> ();
		triviaData = GetComponent<TriviaData> ();
		tagsData = GetComponent<TagsData> ();
		settings = GetComponent<Settings> ();
		resultsData = GetComponent<ResultsData> ();
    }

}
