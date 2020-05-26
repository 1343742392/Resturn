using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftDead : TaskBehavior
{
    GameObject m_blastTimeLoad = null;

    float m_BlastTime = -1;
    // Start is called before the first frame update
    void Start()
    {
        tasks.Add(new Action(delegate ()
        {
            if(m_BlastTime == -1) SetBlastTime(ConfigManager.obj.config.blastTime);
        }));
    }

    public  void SetBlastTime(float blastTime)
    {
            if (m_blastTimeLoad == null)
            {
                m_blastTimeLoad = Tool.GetGameObjAllChild(gameObject, "Blast");
            }
var tls = m_blastTimeLoad.GetComponents<TimeLoad>();
            foreach (var ts in tls)
            {
                ts.SetTime(blastTime);
            }
             m_BlastTime = blastTime;
        }

    // Update is called once per frame


    protected override void UpdateS()
    {
    }
}
