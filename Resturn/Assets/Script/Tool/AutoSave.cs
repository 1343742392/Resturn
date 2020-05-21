using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteInEditMode]
public class AutoSave : MonoBehaviour
{
    public bool autoSaveScene = true;
    private long oldTime = 0;
    public int intervalScene = 1;
    public bool showMessage =  true;
    void Update()
    {
        if (autoSaveScene)
        {
            if (TimeManage.GetTimeStamp() - oldTime >= intervalScene * 600)
            {
                if (showMessage) Debug.Log("save");
                if (autoSaveScene) saveScene();
                oldTime = TimeManage.GetTimeStamp();
            }
        }
    }
    void saveScene()
    {
        try
        {
            EditorApplication.SaveScene(EditorApplication.currentScene);
        }
        catch
        {

        }
    }

}
