using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandalonePlayerInputUI : MonoBehaviour
{
    public StandaloneInputManager.types type;
    public Animation anim;

    public void Clicked()
    {
        anim.Play();
    }

}
