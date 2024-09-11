using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PairTimer : MonoBehaviour
{
    bool isOn;
    public GameObject panel;
    public Image bar;
    float value;
    float totalTime;

    void Start()
    {
        totalTime = Data.Instance.settings.timerForPair;
        SetState(false);
    }
    public void SetState(bool _isOn)
    {
        if (Data.Instance.mode == Data.modes.CLASSIC)
            panel.transform.localPosition = new Vector3(0, 10, 0);
        else if (Data.Instance.mode == Data.modes.TRIPLE)
            panel.transform.localPosition = new Vector3(0, -20, 0);
        else if (Data.Instance.mode == Data.modes.CUADROPLE)
            panel.transform.localPosition = new Vector3(0, -17, 0);
        value = 0;
        this.isOn = _isOn;
        panel.SetActive(isOn);
    }
    void Update()
    {
        if (!isOn)
            return;
        value += Time.deltaTime / totalTime;

        if (value >= 1)
        {
            Events.OnSoundFX("timeOver");
            SetState(false);
            GetComponent<FeedbackManager>().SetTimeOut();
            GetComponent<Trivia>().newPairButton.SetTimeOut();
        }
      
        bar.fillAmount = 1-value;
    }
}
