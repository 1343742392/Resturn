using System;
using UnityEngine;

public class TimeManage : MonoBehaviour
{
    static int time = 0;
    static long startTime = 0;
    static Action back = null;
    public static bool mShowText = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public static long GetTimeStamp()
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
        long timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds;
        return timeStamp;
    }

}
