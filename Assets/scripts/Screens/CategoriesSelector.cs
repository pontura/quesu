using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoriesSelector : MainScreen
{
    public ButtonStandardCategories button;
    public Transform container;

    public override void OnInit()
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
        Utils.RemoveAllChildsIn(container);
        print("Add buttons " + Data.Instance.tagsData.tags.all.Length);
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
        ButtonStandardCategories b = Instantiate(button);
        b.transform.SetParent(container);
        b.Init(OnButtonClicked);
        b.SetText(data.name);
        b.SetData(data);
        b.transform.localScale = Vector3.one;
    }
    public void OnButtonClicked(ButtonUI button)
    {
        print("OnButtonClicked");
        Events.OnSoundFX("ui");
        int id = button.GetComponent<ButtonStandardCategories>().data.id;

        //switch (button.GetComponent<ButtonStandardCategories>().totalPairs)
        //{
        //    case 2:
                Data.Instance.mode = Data.modes.CLASSIC;
        //    case 3:
        //        Data.Instance.mode = Data.modes.TRIPLE; break;
        //    case 4:
        //        Data.Instance.mode = Data.modes.CUADROPLE; break;
        //}
        
        Data.Instance.triviaData.SetActualData(id); 
        LoadScreen(2, true);
    }
}
