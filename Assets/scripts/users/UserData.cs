using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    const string PREFAB_PATH = "UserData";
    static UserData mInstance = null;
    public string userID;
    public string username;
    public Sprite sprite;
    public bool RESET_ALL_DATA;
	public string path;
	
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
#if UNITY_EDITOR
        username = "";
        userID = "";
#else
        username = "";
        userID = "";
#endif
        mInstance = this;		
        if (RESET_ALL_DATA)
            PlayerPrefs.DeleteAll();
    }
    private void Start()
    {
        Invoke("LoopTillReady", 0.5f);
    }
    void LoopTillReady()
    {
        if (username != "")
        {
            OnUserReady();
            //UpdateData(OnUserReady);
        }
        else
        {
            Invoke("LoopTillReady", 0.1f);
            username = Telegram.TelegramManager.Instance.userName;
            userID = Telegram.TelegramManager.Instance.id;
        }
    }
    void OnUserReady()
    {
        Events.OnUserReady();
    }
    public void UpdateData(System.Action OnReady)
    {
        print("UpdateData");
        Data.Instance.serverManager.LoadUserData(userID, OnReady);
    }
}
