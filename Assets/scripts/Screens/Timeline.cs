using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Timeline : MainScreen
{
    public override void OnInit()
    {
        GetComponent<Results>().OnInit();
    }
    public void Next()
    {
        LoadScreen(6, true);
    }

}
