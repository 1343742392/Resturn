using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : TaskBehavior
{
    public float m_radius = 200;
    public AnimationCurve FadeIn = null;
    public float m_animationSpeed = 9;

    public float m_vertical = 0;
    public float m_horizontal = 0;
    public float m_length = 0;

    [HideInInspector]
    public Vector3 m_dicNorma = Vector3.zero;

    private Animator m_anima = null;
    private Vector3 m_startPos = Vector3.zero;
    private RectTransform m_rectTransform = null;
    private bool m_isRes = false;
    private Vector3 m_resDic = Vector3.zero;
    private float m_resDis = 0;
    private float m_resTime = 0;
    private float m_disOver = 0;
    void Start()
    {
        m_anima = GetComponent<Animator>();
        m_rectTransform = GetComponent<RectTransform>();
        m_startPos = m_rectTransform.position;
        tasks.Add(new Action(()=>
        {
            var tt = GetComponent<TouchTarget>();
            tt.OnStart = OnTouch;
            tt.OnMove = OnMove;
            tt.OnEnd = OnEnd;
        }));
    }

    protected override void UpdateS()
    { 
        if(m_isRes)
        {
            var value = FadeIn.Evaluate(Mathf.InverseLerp(0, 1 / m_animationSpeed, m_resTime));
            var step = m_animationSpeed * value;
            //Debug.Log(value);
            if(step > m_resDis - m_disOver)
            {
                m_rectTransform.position = m_startPos;
                StopResPos();
            }
            else
            {
                m_rectTransform.position -= step * m_resDic;
                m_disOver += step;
            }
            m_resTime += Time.deltaTime;
        }
    }

    private void ResPos()
    {
        m_resTime = 0;
        m_resDic = (m_rectTransform.position - m_startPos).normalized;
        m_resDis = Vector3.Distance(m_startPos, m_rectTransform.position);
        m_isRes = true;
    }

    private void StopResPos()
    {
        m_resTime = 0;
        m_isRes = false;
        m_resDis = 0;
        m_disOver = 0;
    }
    private void OnMove(Vector2 point)
    {
        var v3p = new Vector3(point.x, point.y, 0);
        m_dicNorma = Vector3.Normalize(v3p - m_startPos);
        var dis = Vector2.Distance(point, m_startPos);
        if (dis > m_radius)
        {
            m_rectTransform.position = m_startPos + m_dicNorma * m_radius;
        }
        else
        {
            m_rectTransform.position = point;
        }

        var dic = (m_rectTransform.position - m_startPos) / m_radius;
        //m_vertical = Vector2.Distance(dic, Vector2.zero);
        m_vertical = dic.y;
       // if (dic.y < 0) m_vertical *= -1;
        m_horizontal = dic.x;
        m_length = dis;
        
/*        Debug.Log("move" + point);
        LogDisplay.obj?.AddLog("move " + point);*/
    }

    private void OnEnd(Vector2 point)
    {
        
/*        Debug.Log("end" + point);
        LogDisplay.obj?.AddLog("end " + point);*/
        m_dicNorma = Vector3.zero;
        ResPos();
        //m_anima?.SetTrigger("Up");
        m_anima?.Play("JoystickUp");
        m_vertical = 0;
        m_horizontal = 0;
    }

    private void OnTouch(Vector2 point)
    {
        //Debug.Log("touch" + point);
        //LogDisplay.obj?.AddLog("touch " + point);
        StopResPos();
        m_anima?.Play("JoystickDown");
    }


}
