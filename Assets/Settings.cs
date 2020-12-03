using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
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

}
