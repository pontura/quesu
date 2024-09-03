using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStandardCategories : ButtonStandard
{
    public int totalPairs;
    public Image[] activeImages;

    public void SetActives(int total)
    {
        int id = 0;
        foreach (Image i in activeImages)
        {
            if (id <= total)
                i.enabled = true;
            else
                i.enabled = false;
            id++;
        }
    }
    public void PairsNumSelected(int totalPairs)
    {
        this.totalPairs = totalPairs;
        Clicked();
    }
}
