using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingsManager : MonoBehaviour
{
    int limit = 100;
    public RankingsData data;
    public class RankingsData
    {
        public List<RankingData> all;
    }

    private void Start()
    {
        Init(8, null);
    }
    public void Init(int id, System.Action OnReady)
    {
        StartCoroutine(Load(id, OnReady) );
    }
    IEnumerator Load(int tag_id, System.Action OnReady)
    {
        string path = Data.Instance.serverManager.ServerURL + "GetRanking.php" + "?tag_id=" + tag_id + "&user_id=" + UserData.Instance.userID + "&username=" + UserData.Instance.username + "&limit=" + limit;
        print("RankingsManager: " + path);
        WWW www = new WWW(path);
        yield return www;
        if (www.error != null)
            print("There was an error: " + www.error);
        else
        {           
            string result = www.text;
            print(result);
            data = JsonUtility.FromJson<RankingsData>(result);
            if (OnReady != null)
                OnReady();
        }
    }
    public void SetHiscore(System.Action OnReady)
    {
        StartCoroutine(SetHiscoreC(OnReady));
    }
    IEnumerator SetHiscoreC(System.Action OnReady)
    { 
        int tag_id = Data.Instance.triviaData.tag_id;
        int score = Data.Instance.resultsData.score;
        string path = Data.Instance.serverManager.ServerURL + "SetHiscore.php" + "?tag_id=" + tag_id + "&user_id=" + UserData.Instance.userID + "&username=" + UserData.Instance.username + "&score=" + score;
        print("RankingsManager: " + path);
        WWW www = new WWW(path);
        yield return www;
        if (www.error != null)
            print("There was an error: " + www.error);
        else
        {
            string result = www.text;
            print(result);
            if (OnReady != null)
                OnReady();
        }
    }
}
