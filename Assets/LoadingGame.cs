using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingGame : MainScreen
{
    public Text field;
    public GameObject vsPanel;
	public Image vsImage;
	public Text vsUsername;

    public override void OnEnabled()
    {
		RetoData openRetoData = Data.Instance.retosManager.openReto;
        if (openRetoData.userID == "")
        {
            vsPanel.SetActive(false);
        }
        else
        {
			vsPanel.SetActive(true);
			vsUsername.text = openRetoData.username;
        }
        field.text = Data.Instance.triviaData.triviaName.ToUpper();
    }
    public override void OnInit()
    {
        Invoke("Done", 4);
    }
    void Done()
    {
        LoadScreen(3, true);
    }
}