using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//通过base.Tasks.add(任务)  子类要调用base.UpdateS
public abstract class TaskBehavior : MonoBehaviour
{
    protected ArrayList tasks = new ArrayList();


    protected  void Update()
    {
        UpdateS();
        Task();
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
