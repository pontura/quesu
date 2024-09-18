using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    [SerializeField] Animator anim;

    void Start()
    {
        Events.OnAnswer += OnAnswer;
        Events.OnTimeOver += OnTimeOver;
        Events.OnInitRonda += OnInitRonda;
        Events.OnChooseLeft += OnChooseLeft;
        Idle();
    }
    void OnDestroy()
    {
        Events.OnAnswer -= OnAnswer;
        Events.OnTimeOver -= OnTimeOver;
        Events.OnInitRonda -= OnInitRonda;
        Events.OnChooseLeft -= OnChooseLeft;
    }

    private void OnChooseLeft(bool left)
    {
        print("OnChooseLeft" + left);
        if(left)
            transform.localScale = new Vector2(1, 1);
        else
            transform.localScale = new Vector2(-1, 1);
    }   

    private void OnInitRonda()
    {
        print("OnInitRonda");
        anim.Play("loop");
    }
    private void OnTimeOver()
    {
        print("OnTimeOver");
        Idle();
    }
    void Idle()
    {
        print("idle");
        anim.Play("idle");
    }
    private void OnAnswer(bool win, Vector2 pos)
    {
        if(!win)
            anim.Play("lose");
        else
            anim.Play("done");
    }
}
