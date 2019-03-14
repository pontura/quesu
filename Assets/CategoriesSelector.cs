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
        if (Data.Instance.tagsData.tags.all.Length > 0)
            AddButtons();
        else
            Invoke("WaitForTags", 0.1f);
    }
    void AddButtons()
    {
         TagData t = new TagData();
        t.id = 0;
        t.name = "Todos";
        AddButton(t);
        foreach (TagData data in Data.Instance.tagsData.tags.all)
        {
            AddButton(data);
        }
       
    }
    void AddButton(TagData data)
    {
        ButtonStandard b = Instantiate(button);
        b.transform.SetParent(container);
        b.Init(data.id, data.name);
        b.transform.localScale = Vector3.one;
    }
    public override void OnButtonClicked(ButtonStandard button)
    {
        Data.Instance.triviaData.Load(button.id);       
        LoadScreen(2, true);
    }
}
