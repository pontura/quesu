using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MainScreen
{
    public void PlaySingleMode()
    {
        LoadScreen(screens.CATEGORIES, true);
        Events.OnSoundFX("ui");
    }
}
