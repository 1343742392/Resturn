using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ButtonTarget : MonoBehaviour
{
    public Action OnTouch = new Action(delegate () { });

    private void Awake()
    {
        
    }

    public void Touch()
    {
        OnTouch();
    }



}
