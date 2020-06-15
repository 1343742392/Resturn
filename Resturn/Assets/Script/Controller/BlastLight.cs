using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastLight : TaskBehavior
{
    protected override void FixedUpdateS()
    {
    }

    protected override void StartS()
    {
        AddCallBack(ConfigManager.obj.config.blastTime, new Action(delegate()
        {
            GetComponent<Animator>()?.Play("BlastLight");
            
        }));
    }

    protected override void UpdateS()
    {

    }


}
