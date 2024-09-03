using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserUI : MonoBehaviour
{
    public GameObject panel;
    public Text field;
    public Image image;
    public MainScreen mainScreen;
    
    void OnEnable()
    {
        if (UserData.Instance.userID == "")
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
            field.text = UserData.Instance.username;
            image.sprite = UserData.Instance.sprite;
        }
    }
    public void EditUser()
    {
        mainScreen.LoadScreen(6, true);
    }
}
