using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WebcamPhoto : MonoBehaviour {
	
	public string deviceName;
	WebCamTexture wct;
	public Image image;
	public string path;
	WebCamDevice[] devices;
	// Use this for initialization
	void Start () {
		devices = WebCamTexture.devices;
		deviceName = devices[0].name;
		wct = new WebCamTexture(deviceName, 300, 300, 12);
		image.material.mainTexture = wct;
		wct.Play();
		path = Application.persistentDataPath + "/";
	}
	public void TakeSnapshot()
	{		
		Texture2D snap = new Texture2D(wct.width, wct.height);
		snap.SetPixels(wct.GetPixels());
		snap.Apply();
		System.IO.File.WriteAllBytes(path + UserData.Instance.userID + ".png", snap.EncodeToPNG());

		if (wct != null)
			wct.Stop ();

	}
}