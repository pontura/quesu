using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoriesSelector : MainScreen
{
    public ButtonStandard button;
    public Transform container;

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
        print("Add buttons");
         TagData t = new TagData();
        t.id = 0;
        t.name = "Todos";

        //AddButton(t);
        foreach (TagData data in Data.Instance.tagsData.tags.all)
        {
            if(!data.lockedByClient)
            AddButton(data);
        }
       
    }
    void AddButton(TagData data)
    {
        print(data.name);
        ButtonStandard b = Instantiate(button);
        b.transform.SetParent(container);
        b.Init(data.id, data.name);
        b.transform.localScale = Vector3.one;
    }
    public override void OnButtonClicked(ButtonStandard button)
    {
        Events.OnSoundFX("ui");

        switch (button.GetComponent<ButtonStandardCategories>().totalPairs)
        {
            case 2:
                Data.Instance.mode = Data.modes.CLASSIC; break;
            case 3:
                Data.Instance.mode = Data.modes.TRIPLE; break;
            case 4:
                Data.Instance.mode = Data.modes.CUADROPLE; break;
        }
        
        Data.Instance.triviaData.Load(button.id);       
        LoadScreen(2, true);
    }
}
