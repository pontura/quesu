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
        foreach (TagData data in Data.Instance.tagsData.tags.all)
        {
            AddButton(data);
        }
        TagData t = new TagData();
        t.id = 0;
        t.name = "Todos";
        AddButton(t);
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
        if (button.id == 0)
        {
            Data.Instance.serverManager.LoadTriviaByCategory("all", 40);
        }
        else 
        {
            Data.Instance.serverManager.LoadTrivia(button.id, 40);
        }
				
        Data.Instance.triviaData.triviaName = button.field.text;
        LoadScreen(2, true);
    }
}
