﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelector : MainScreen
{
    //public ButtonStandard button;
    //public Transform container;

    void Start()
    {
        WaitForTags();
    }
    void WaitForTags()
    {
        print(Data.Instance.tagsData.tags.all.Length);
        if (Data.Instance.tagsData.tags.all.Length > 0)
            AddButtons();
        else
            Invoke("WaitForTags", 0.1f);
    }
    void AddButtons()
    {
        //print("Add buttons");
        //TagData t = new TagData();
        //t.id = 0;
        //t.name = "Todos";
        //AddButton(t);
        //foreach (TagData data in Data.Instance.tagsData.tags.all)
        //{
        //    AddButton(data);
        //}

    }
    //void AddButton(TagData data)
    //{
    //    ButtonStandard b = Instantiate(button);
    //    b.transform.SetParent(container);
    //    b.Init(data.id, data.name);
    //    b.transform.localScale = Vector3.one;
    //}
    public override void OnButtonClicked(ButtonStandard button)
    {
        Events.OnSoundFX("ui");

        if (button.id == 0)
            Data.Instance.mode = Data.modes.CLASSIC;
        else if (button.id == 1)
            Data.Instance.mode = Data.modes.TRIPLE;
        else
            Data.Instance.mode = Data.modes.CUADROPLE;

        //Data.Instance.triviaData.Load(button.id);
        LoadScreen(2, true);
    }
}
