using System.Collections;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;

#endif
using UnityEngine;
[ExecuteInEditMode]
public class AutoSave : MonoBehaviour
{
    public bool autoSaveScene = true;
    private long oldTime = 0;
    public int intervalScene = 1;
    public bool showMessage =  false;
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
#if UNITY_EDITOR
            EditorSceneManager.SaveOpenScenes();


#endif
        }
        catch
        {

        }
    }

}
