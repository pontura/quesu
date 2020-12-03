using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MainScreen
{
    public InputField totalTime_field;
    public InputField timeWin_field;
    public InputField timeLose_field;
    public InputField delay_field;
    public InputField timePair_field;
    void Start()
    {
        SetData();
    }
    void SetData()
    {
        totalTime_field.text = Data.Instance.settings.triviaDuration.ToString();
        timeWin_field.text = Data.Instance.settings.timeWin.ToString();
        timeLose_field.text = Data.Instance.settings.timeLose.ToString();
        delay_field.text = Data.Instance.settings.timeForFeedback.ToString();
        timePair_field.text = Data.Instance.settings.timerForPair.ToString();
    }
    void SetResults()
    {
        Data.Instance.settings.triviaDuration = int.Parse(totalTime_field.text);
        Data.Instance.settings.timeWin = float.Parse(timeWin_field.text);
        Data.Instance.settings.timeLose = float.Parse(timeLose_field.text);
        Data.Instance.settings.timeForFeedback = float.Parse(delay_field.text);
        Data.Instance.settings.timerForPair = float.Parse(timePair_field.text);
    }
    public override void Back()
    {
        base.Back();
        SetResults();
    }
    public void SetDefaultsValues()
    {
        Data.Instance.settings.SetDefaults();
        SetData();
    }
}
