using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : TaskBehavior
{
    // Start is called before the first frame update
    RectTransform m_arrow = null;
    Transform m_playerTF = null;
    Transform m_doorTF = null;

    float angle = 0;

    protected override void FixedUpdateS()
    {
    }

    protected override void StartS()
    {
    }

    protected override void UpdateS()
    {
        if(m_arrow != null && m_playerTF != null && m_doorTF != null)
        {
            var fw = m_playerTF.forward;
            var dic = (m_doorTF.position - m_playerTF.position).normalized;
            angle = Vector2.Angle(new Vector2(fw.x, fw.z), new Vector2(dic.x, dic.z));
            var axis = Vector3.Cross(fw, dic).y < 0 ? Vector3.forward : -Vector3.forward;
            m_arrow.up = Tool.Rotate(Vector3.up, axis, angle); 
        }
    }

    void Start()
    {
        m_arrow = Tool.GetGameObjAllChild(gameObject, "Arrow").GetComponent<RectTransform>();
        tasks.Add(new Action(delegate ()
        {
            m_playerTF = GameObject.FindWithTag(Tag.Ellen)?.transform;
            m_doorTF = GameObject.FindWithTag(Tag.SuccessDoor)?.transform;
        }));
    }
}
