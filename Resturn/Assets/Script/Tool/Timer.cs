using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float m_start = 0;
    private float m_time = -1;

    public Action back = null;
    // Start is called before the first frame update
    void Start()
    {
        m_start = Time.time;
    }

    public void SetTime(float time)
    {
        m_start = Time.time;
        this.m_time = time;
    }

    void Update()
    {
        if (m_time == -1 ||back == null) return;
        if (Time.time - m_start >= m_time)
        {
            m_time = -1;
            back();
        }
    }
}
