using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;




[CustomEditor(typeof(Config))]
[CanEditMultipleObjects]
public class ConfigEditor : Editor
{
    SerializedProperty fpsProp;
    SerializedProperty rePlayWaitTimeProp;
    SerializedProperty rePlayTimeProp;
    SerializedProperty blastTimeProp;
    void OnEnable()
    {
        // Setup the SerializedProperties.

        try
        {
            fpsProp = serializedObject.FindProperty("fps");
            rePlayWaitTimeProp = serializedObject.FindProperty("rePlayWaitTime");
            rePlayTimeProp = serializedObject.FindProperty("rePlayTime");
            blastTimeProp = serializedObject.FindProperty("blastTime");
        }
        catch
        {

        }

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.IntSlider(fpsProp, 0, 400, new GUIContent("FPS"));

        EditorGUILayout.Slider(rePlayWaitTimeProp, 0, 20);

        EditorGUILayout.Slider(rePlayTimeProp, 0, 20);

        EditorGUILayout.IntSlider(blastTimeProp, 0, 60);

        serializedObject.ApplyModifiedProperties();
    }
    void ProgressBar(float value, string label)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }
    [MenuItem("Assets/Create/Resturn/Config", priority = -999)]
    static public void CreateWeapon()
    {
        var newPlayer = CreateInstance<Config>();
        ProjectWindowUtil.CreateAsset(newPlayer, "weapon.asset");
    }
}
