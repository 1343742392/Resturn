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

    protected override void StartS()
    {
        if (m_BlastTime == -1) SetBlastTime(Mathf.Max(0, ConfigManager.obj.config.blastTime));
    }

    protected override void FixedUpdateS()
    {
    }
}
