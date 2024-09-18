using System;
using UnityEngine;
using UnityEngine.UI;

public class CategoryIcon : MonoBehaviour
{
    [SerializeField] Image bg;
    [SerializeField] Image logo;

    void Start()
    {
        Events.OnChangeTag += OnChangeTag;
    } 
    private void OnDestroy()
    {
        Events.OnChangeTag -= OnChangeTag;
    }
    private void OnEnable()
    {
        if(Data.Instance.triviaData.IsLoaded())
             OnChangeTag(Data.Instance.triviaData.tag_id);
    }
    public void OnChangeTag(int tag_id)
    {
        Data.Instance.settings.GetCategoryData(tag_id, OnLogoDone);
    }
    public void OnLogoDone(Settings.CategorieData data)
    {
        bg.color = data.color;
        logo.sprite = data.logo;
        logo.color = data.color;
    }
}
