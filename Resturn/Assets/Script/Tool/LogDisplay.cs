using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *1.挂在一个对象上 把这个对象拖到第一
 *2.LogDisplay.obj?.AddLog("touch");
 * 
 */
public class LogDisplay : MonoBehaviour
{
    static public LogDisplay obj = null;

    [SerializeField] int max = 10;
    [SerializeField] int fontSize = 20;
    [SerializeField] Color color = new Color(1,1,1);
    [HideInInspector]
    private List<string> logs = new List<string>();
    private List<string> times = new List<string>();

    private GUIStyle FontStyle = new GUIStyle();

    public void AddLog(string log)
    {
        logs.Add(log);
        times.Add(DateTime.Now.ToString());
    }

    private void Awake()
    {
        obj = this;
    }
    private void Start()
    {
        FontStyle.fontSize = fontSize;
        FontStyle.normal.textColor = color;  
    }

    private void OnGUI()
    {
        if (logs.Count > max)
        {
            logs.RemoveRange(0, logs.Count - max);
            times.RemoveRange(0, times.Count - max);
        }
        GUILayout.BeginVertical();
        GUILayout.Label("       ", FontStyle);//给fps显示留空
        var index = 0;
        logs.ForEach(log =>
        {
            GUILayout.Label("------ " + times[index++] + ":" + log, FontStyle);
        });
        GUILayout.EndVertical();
    }
}
