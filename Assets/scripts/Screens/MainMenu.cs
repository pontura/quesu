using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MainScreen
{
    [SerializeField] ButtonUI button;

    private void Start()
    {
        button.Init(PlaySingleMode);
        button.SetText("JUGAR");
    }
    public void PlaySingleMode(ButtonUI b)
    {
        LoadScreen(screens.CATEGORIES, true);
        Events.OnSoundFX("ui");
    }
}
