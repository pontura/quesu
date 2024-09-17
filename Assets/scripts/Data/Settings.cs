using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public CategorieData[] categoriesData;
    [Serializable] public class CategorieData
    {
        public int id;
        public Sprite logo;
        public Color color;
    }

    [SerializeField] float d_timeForFeedback;
    [SerializeField] int d_triviaDuration;
    [SerializeField] int d_timeover;
    [SerializeField] float d_timeWin;
    [SerializeField] float d_timeLose;
    [SerializeField] float d_timerForPair;

    [HideInInspector] public float timeForFeedback;
    [HideInInspector] public int triviaDuration;
    [HideInInspector] public int timeover;
    [HideInInspector] public float timeWin;
    [HideInInspector] public float timeLose;
    [HideInInspector] public float timerForPair;

    [HideInInspector] public int scoreWin = 100;
    [HideInInspector] public int scoreLose = -30;
    [HideInInspector] public int scoreCombo = 45;

    public Color buttonOkColor;
	public Color buttonWrongColor;

    private void Awake()
    {
        SetDefaults();
    }
    public void SetDefaults()
    {
        timeForFeedback = d_timeForFeedback;
        triviaDuration = d_triviaDuration;
        timeWin = d_timeWin;
        timeLose = d_timeLose;
        timerForPair = d_timerForPair;
    }
    CategorieData GetDataByCategory(int id)
    {
        foreach(CategorieData c in categoriesData)
        {
            if (c.id == id)
                return c;
        }
        return null;
    }
    public void GetCategoryData(int tag_id, System.Action<CategorieData> OnLoaded)
    {
        CategorieData c = GetDataByCategory(tag_id);
        if (c == null)
        {
            Debug.LogError("No category for: " + tag_id);
            OnLoaded(categoriesData[0]);
        }
        else
            OnLoaded(c);
    }

}
