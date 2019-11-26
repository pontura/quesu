﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserUIRegistrationPanel : MonoBehaviour
{
    public RawImage image;
    public AspectRatioFitter aspectRatioFitter;
    public Image imageTaken;
    public InputField field;
    UserDataUI userDataUI;
    public GameObject PhotoPanel;
    public GameObject PhotoTakenPanel;
    public GameObject[] hideOnScreenshot;
    public Text buttonField;
    bool userExists;

    public void Init(UserDataUI userDataUI, string _username)
    {
        this.userDataUI = userDataUI;
        field.text = _username;
        if (_username == "")
        {
            buttonField.text = "Registrar";
        }
        else
        {
            userExists = true;
            buttonField.text = "Modificar";
        }           
        ShowPhotoTaken();
    }
    void ShowNewPhoto()
    {
        PhotoPanel.SetActive(true);
        PhotoTakenPanel.SetActive(false);
        userDataUI.webcamPhoto.InitWebcam(image, aspectRatioFitter);
    }
    void ShowPhotoTaken()
    {
        PhotoPanel.SetActive(false);
        PhotoTakenPanel.SetActive(true);
        imageTaken.sprite = UserData.Instance.sprite;
    }
    public void TakeSnapshot()
    {
        foreach (GameObject go in hideOnScreenshot)
            go.SetActive(false);

#if UNITY_ANDROID
        string fileName = UserData.Instance.userID + ".png";
#else
        string fileName = UserData.Instance.path + UserData.Instance.userID + ".png";
#endif

        userDataUI.webcamPhoto.TakeSnapshot(OnPhotoTaken, fileName);
    }
    void OnPhotoTaken()
    {
        foreach (GameObject go in hideOnScreenshot)
            go.SetActive(true);
        ShowPhotoTaken();
        userDataUI.userRegistrationForm.SavePhoto();
    }
    public void ClickedNewPhoto()
    {
        ShowNewPhoto();
    }
    public void OnSubmit()
    {
        if (userExists)
        {
            userDataUI.OnUpload(field.text);
        }
        else
        {
         //   if (UserData.Instance.sprite == null)
         //       userDataUI.DebbugText.text = "Falta la foto!";
         //   else 
            if (field.text == "")
                Events.OnPopup( "Falta un nombre de usuario" );
            else
                userDataUI.OnSubmit(field.text);
        }
    }
}
