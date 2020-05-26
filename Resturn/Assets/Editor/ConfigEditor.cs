using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;




[CustomEditor(typeof(Config))]
[CanEditMultipleObjects]
public class ConfigEditor : Editor
{
    SerializedProperty damageProp;
    SerializedProperty armorProp;
    SerializedProperty gunProp;
    SerializedProperty fpsProp;
    SerializedProperty rePlayWaitTimeProp;
    SerializedProperty rePlayFrameNumProp;
    SerializedProperty blastTimeProp;
    void OnEnable()
    {
        // Setup the SerializedProperties.

        try
        {
            damageProp = serializedObject.FindProperty("damage");
            armorProp = serializedObject.FindProperty("armor");
            gunProp = serializedObject.FindProperty("gun");
            fpsProp = serializedObject.FindProperty("fps");
            rePlayWaitTimeProp = serializedObject.FindProperty("rePlayWaitTime");
            rePlayFrameNumProp = serializedObject.FindProperty("rePlayFrameNum");
            blastTimeProp = serializedObject.FindProperty("blastTime");
        }
        catch
        {

        }

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.IntSlider(damageProp, 0, 100, new GUIContent("Damage"));

        EditorGUILayout.IntSlider(armorProp, 0, 100, new GUIContent("Armor"));

        EditorGUILayout.PropertyField(gunProp, new GUIContent("Gun Object"));

        EditorGUILayout.IntSlider(fpsProp, 0, 400, new GUIContent("FPS"));

        EditorGUILayout.Slider(rePlayWaitTimeProp, 0, 20);

        EditorGUILayout.IntSlider(rePlayFrameNumProp, 0, 600);

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
