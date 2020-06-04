using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastLight : TaskBehavior
{
    protected override void StartS()
    {
        SetTime(ConfigManager.obj.config.blastTime);
        back = () =>
        {
            GetComponent<Animator>()?.Play("BlastLight");
        };
    }

    protected override void UpdateS()
    {

    }


}
