using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupDown : MonoBehaviour
{
    public GameObject panel;
    public Text field;

    void Start()
    {
        Reset();
        Events.OnPopup += OnPopup;
    }
    void OnPopup(string text)
    {
        panel.SetActive(false);
        CancelInvoke();
        panel.SetActive(true);
        field.text = text;
        Invoke("Reset", 2);
    }
    void Reset()
    {
        panel.SetActive(false);
    }
}
