using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//通过base.Tasks.add(任务)  子类要调用base.UpdateS
public abstract class TaskBehavior : MonoBehaviour
{
    protected ArrayList tasks = new ArrayList();

    private float m_start = 0;
    private float m_time = -1;

    public Action back = null;

    protected  void Update()
    {
        if (m_time != -1 || back != null)
        {
            if (Time.time - m_start >= m_time)
            {
                m_time = -1;
                back();
            }
        }


        UpdateS();
        Task();
    }

    protected void SetTime(float time)
    {
        m_start = Time.time;
        this.m_time = time;
    }

    private void Task()
    {
        if (tasks.Count == 0) return;
        //如果用forech 那么在其他线程tasks 被更改会报错
        for (int f = 0; f < tasks.Count; f++)
        {
            ((Action)tasks[f])();
        }
        tasks.RemoveRange(0, tasks.Count);
    }

    protected abstract void UpdateS();
}
