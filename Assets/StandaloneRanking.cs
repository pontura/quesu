using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class StandaloneRanking : MonoBehaviour
{
    public List<RankingData> all;
    public int MaxUsersInRanking = 40;
    string[] files;
    string pathPreFix;

    [Serializable]
    public class RankingData
    {
        public int score;
        public Texture2D texture;
        public bool lastOne;
    }
    void Start()
    {
        Load();
    }
    public void Refresh()
    {
        all.Clear();
    }
    void Load()
    {
        //Change this to change pictures folder
        //string path = @"C:\Users\Public\Pictures\Sample Pictures\";
        string path = Application.streamingAssetsPath + "/photos/";

        print("levanta imagenes de: " + path);
        pathPreFix = @"file://";

        files = System.IO.Directory.GetFiles(path , "*.png");
        
        StartCoroutine(LoadImages());
    }

    private IEnumerator LoadImages()
    {
        all.Clear();
        foreach (string tstring in files)
        {
            string pathTemp = pathPreFix + tstring;
            WWW www = new WWW(pathTemp);
            yield return www;
            Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            www.LoadImageIntoTexture(texTmp);            
            string[] d =  tstring.Split("."[0]);
            string[] d2 = d[0].Split("/"[0]);
            string fileName = d2[d2.Length - 1];
            string scoreString = fileName.Split("_"[0])[0];
            AddNew( int.Parse(scoreString),  texTmp);
        }
        Reorder();
    }
    public void AddNew(int scoreString, Texture2D texture, bool reorder = false)
    {
        RankingData rankingData = new RankingData();
        rankingData.score = scoreString;
        rankingData.texture = texture;
        all.Add(rankingData);
        if (reorder)
            Reorder();
        rankingData.lastOne = true;

        if (all.Count > MaxUsersInRanking)
            DeleteLast();
    }
    public void Reorder()
    {
        all = all.OrderBy(w => w.score).ToList();
        all.Reverse();
        foreach (RankingData rankingData in all)
            rankingData.lastOne = false;
    }
    void DeleteLast()
    {
        all.RemoveAt(all.Count - 1);
    }


}
