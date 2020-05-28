using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class JumpButton : TaskBehavior
{
    public float m_ActivationAnlge = 0.6f;

    public Action OnJump = new Action(()=> { });


    private TouchTarget m_touchTarget = null;
    void Start()
    {

    }

    protected override void UpdateS()
    {
        
    }

    public void BeginDrag(Vector2 point)
    {
        //LogDisplay.obj?.AddLog("begin;" );
        //Debug.Log("begin");
    }

    public void EndDrag(Vector2 point, Vector2 dic, float length)
    {
        //LogDisplay.obj?.AddLog("test;" + (Vector2.Dot(Vector2.up, dic) > m_ActivationAnlge));

        if (Vector2.Dot(Vector2.up, dic) > m_ActivationAnlge)
        {
            //LogDisplay.obj?.AddLog("test;");

            OnJump();
            //LogDisplay.obj?.AddLog("jump");
            //Debug.Log("jump");
        }


    }

    protected override void StartS()
    {
        m_touchTarget = GetComponent<TouchTarget>();
        m_touchTarget.OnDragStart = BeginDrag;
        m_touchTarget.OnDragEnd = EndDrag;
    }
}
