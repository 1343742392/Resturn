using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

using UnityEngine;
using UnityEditor;
using System;
using System.Threading;

[ExecuteInEditMode]


public class AS : EditorWindow
{
    private bool autoSaveScene = true;
    private bool showMessage = true;
    private bool isStarted = false;
    private int intervalScene;
    private DateTime lastSaveTimeScene = DateTime.Now;
   // private string projectPath = Application.dataPath;
    private string scenePath;
    int time = 60000;
    long oldTime = 0;
    [MenuItem("Window/AutoSave")]
    static void Init()
    {
        AS saveWindow = (AS)EditorWindow.GetWindow(typeof(AS));
        saveWindow.Show();
    }

    private void OnEnable()
    {
    }
    void OnGUI()
    {
        GUILayout.Label("Info:", EditorStyles.boldLabel);
       // EditorGUILayout.LabelField("Saving to:", "" + projectPath);
        EditorGUILayout.LabelField("Saving scene:", "" + scenePath);
        GUILayout.Label("Options:", EditorStyles.boldLabel);
        autoSaveScene = EditorGUILayout.BeginToggleGroup("Auto save", autoSaveScene);
        intervalScene = EditorGUILayout.IntSlider("Interval (minutes)", intervalScene, 1, 10);
        if (isStarted)
        {
            EditorGUILayout.LabelField("Last save:", "" + lastSaveTimeScene);
        }
        EditorGUILayout.EndToggleGroup();
        showMessage = EditorGUILayout.BeginToggleGroup("Show Message", showMessage);
        EditorGUILayout.EndToggleGroup();
    }
    void Update()
    {
        Debug.Log("run");
        scenePath = EditorApplication.currentScene;
        if (autoSaveScene)
        {
            if (TimeManage.GetTimeStamp() - oldTime >= intervalScene * 60000)
            {
                if(showMessage)Debug.Log("save");
                oldTime = TimeManage.GetTimeStamp();
                saveScene();
            }
        }
        else
        {
            isStarted = false;
        }
    }
    void saveScene()
    {
        try
        {
            EditorApplication.SaveScene(scenePath);
            lastSaveTimeScene = DateTime.Now;
            isStarted = true;
            if (showMessage)
            {
                //Debug.Log("AutoSave saved: " + scenePath + " on " + lastSaveTimeScene);
            }
            AS repaintSaveWindow = (AS)EditorWindow.GetWindow(typeof(AS));
            repaintSaveWindow.Repaint();
        }catch
        {

        }
       
    }
}
