using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    [SerializeField]public Config config = null;
    public static ConfigManager obj;
    void Start()
    {
        if (config == null) return;
        obj = this;
        Application.targetFrameRate = config.fps;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
    }
}
