using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class WebcamPhotoStandalone : MonoBehaviour
{
    WebCamTexture webcam;
    WebCamDevice[] devices;
    AspectRatioFitter fit;
    RawImage image;
    bool camAvailable;
    Texture defaultBackground;
    public Vector2 photoSize;

    void OnDisable()
    {
        OnUserRegisterCanceled();
    }
    void OnUserRegisterCanceled()
    {
        if (devices == null || devices.Length == 0)
            return;

        StopCamera();
    }
    void StopCamera()
    {
        if (webcam == null)
            return;

        webcam.Stop();
        camAvailable = false;
    }
    public void InitWebcam(RawImage image, AspectRatioFitter fit)
    {

        devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            camAvailable = false;
            return;
        }
        this.image = image;
        this.fit = fit;

        defaultBackground = image.texture;

        for (int a = 0; a < devices.Length; a++)
        {
            if (devices[a].isFrontFacing)
                webcam = new WebCamTexture(devices[a].name, Screen.width, Screen.height);
        }
        if (webcam == null)
            return;

        webcam.Play();
        image.texture = webcam;

        camAvailable = true;

    }
    void Update()
    {
        if (!camAvailable)
            return;

        float ratio = (float)webcam.width / (float)webcam.height;
        fit.aspectRatio = ratio;

        float scaleY = webcam.videoVerticallyMirrored ? -1 : 1f;
        image.rectTransform.localScale = new Vector3(1, scaleY, 1);
        int orient = -webcam.videoRotationAngle;
        image.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
    
    System.Action OnDone;
    public void TakeSnapshot(System.Action OnDone, string fileName)
    {
        Events.OnSoundFX("photo");
        if (devices.Length == 0)
            return;
        SaveImage(fileName);
        this.OnDone = OnDone;
        Invoke("StopCamera", 0.1f);
    }

    void SaveImage(string filename)
    {
        //Create a Texture2D with the size of the rendered image on the screen.
        Texture2D texture = new Texture2D(image.texture.width, image.texture.height, TextureFormat.ARGB32, false);

        //Save the image to the Texture2D
        texture.SetPixels(webcam.GetPixels());
        texture.Apply();

        //Encode it as a PNG.
        byte[] bytes = texture.EncodeToPNG();

        //Save it in a file.
        File.WriteAllBytes(filename, bytes);

        print("grabo: " + filename);

        int score = UserData.Instance.GetComponent<MultiplayerData>().GetWinnerScore();
        Data.Instance.GetComponent<StandaloneRanking>().AddNew(score, texture, true);
    }



}