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

    public formats format;
    public enum formats
    {
        CLASSIC,
        STANDALONE
    }

    public modes mode;
    public enum modes
    {
        CLASSIC,
        TRIPLE,
        CUADROPLE
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
    public UnityEngine.Video.VideoPlayer videoPlayer;

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

        DontDestroyOnLoad(this.gameObject);
		serverManager = GetComponent<ServerManager> ();
		triviaData = GetComponent<TriviaData> ();
		tagsData = GetComponent<TagsData> ();
		settings = GetComponent<Settings> ();
		resultsData = GetComponent<ResultsData> ();

    }
    private void Start()
    {
        if(format == formats.CLASSIC)
            videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "fondo.mp4");        
    }
    public void PlayVideo()
    {
        videoPlayer.Play();
    }

}
