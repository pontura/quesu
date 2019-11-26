using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneConfirmPlayers : MonoBehaviour
{
    public GameObject p1_confirm;
    public GameObject p1_done;

    public GameObject p2_confirm;
    public GameObject p2_done;

    bool p1;
    bool p2;

    public bool done;
    MainMenu mainMenu;

    void Start()
    {
        mainMenu = GetComponent<MainMenu>();
    }
    void OnDisable()
    {
        if (Data.Instance.format == Data.formats.STANDALONE)
            Events.OnStandaloneKeyDown -= OnStandaloneKeyDown;
    }
    public void OnEnable()
    {
        if (Data.Instance.format == Data.formats.STANDALONE)
            Events.OnStandaloneKeyDown += OnStandaloneKeyDown;

        p1 = p2 = done = false;

        p1_confirm.SetActive(true);
        p1_done.SetActive(false);

        p2_confirm.SetActive(true);
        p2_done.SetActive(false);
    }
    void OnStandaloneKeyDown(StandaloneInputManager.types type)
    {
        if (!mainMenu.ready)
            return;

        
        switch(type)
        {
            case StandaloneInputManager.types.P1_LEFT:
            case StandaloneInputManager.types.P1_RIGHT:
                if(!p1)
                    Invoke("StartSound", 0.1f);
                p1 = true;
                p1_confirm.SetActive(false);
                p1_done.SetActive(true);
                break;

            case StandaloneInputManager.types.P2_LEFT:
            case StandaloneInputManager.types.P2_RIGHT:
                if (!p2)
                    Invoke("StartSound", 0.1f);
                p2_confirm.SetActive(false);
                p2_done.SetActive(true);
                p2 = true;
                break;
        }
        if (done)
            return;
        if (p1 && p2)
        {
            GetComponent<MainMenu>().OnReset();
            Data.Instance.triviaData.RefreshAll();
            done = true;
            GetComponent<MainMenu>().LoadScreen(1, true);
            
        }
    }
    void StartSound()
    {
        Events.OnSoundFX("start");
    }

}
