using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public class FpsUI : MonoBehaviour
{
    public int fointSize = 20;
    private string fps = "";
    private float time;
    private int frameCount;
    private void Awake()
    {
    }

    void Update()
    {
        time += Time.unscaledDeltaTime;
        frameCount++;
        if (time >= 1 && frameCount >= 1)
        {
            float fps = frameCount / time;
            time = 0;
            frameCount = 0;
            this.fps = fps.ToString("f2");//#0.00
        }
    }

    private void OnGUI()
    {
        var s = new GUIStyle();
        s.normal.background = null;    //这是设置背景填充的
        s.normal.textColor = new Color(1, 1, 1);   //设置字体颜色的
        s.fontSize = fointSize;       //当然，这是字体颜色

        GUILayout.Label("              " + fps, s);
    }
}
