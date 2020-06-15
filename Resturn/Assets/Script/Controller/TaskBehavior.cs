using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /*
     * 通过base.Tasks.add(任务)  子类要调用base.UpdateS
     * 子类不要继承update
     * starts会在碰撞之后运行 使用once来判断
     * 
     * 
     * */

public abstract class TaskBehavior : MonoBehaviour
{

    class CallBack
    {
        public CallBack(float startTime, float time, Action back)
        {
            this.startTime = startTime; 
            this.time = time;
            this.back = back;
        }
        public float startTime;
        public float time;
        public  Action back;
    }

    private ArrayList m_callBacks = new ArrayList();
    protected ArrayList tasks = new ArrayList();

    protected bool once = true;

    private void FixedUpdate()
    {
        for(var i = 0; i < m_callBacks.Count;)
        {
            CallBack cb = (CallBack) m_callBacks[i];
            if (Time.time - cb.startTime >= cb.time)
            {
                cb.back();
                m_callBacks.Remove(cb);
            }
            else
            {
                i ++;
            }
        }
        FixedUpdateS();
    }

    protected  void Update()
    {
        if(once)
        {
            StartS();
            once = false;
        }


        UpdateS();
        Task();
    }

    protected void AddCallBack(float time, Action back = null)
    {
        m_callBacks.Add(new CallBack(Time.time, time, back));
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
    protected abstract void FixedUpdateS();

    protected abstract void StartS();
}
