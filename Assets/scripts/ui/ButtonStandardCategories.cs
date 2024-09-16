using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStandardCategories : ButtonUI
{
    public int totalPairs;
    public TagData data;

    public void SetData(TagData data)
    {
        this.data = data;
    }
}
