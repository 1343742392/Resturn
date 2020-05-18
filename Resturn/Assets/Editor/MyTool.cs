using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


public class MyTool : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //只需要在 Tag 类写好 tag 然后把指定 gameobj 名字改成tag 就能一键给 gameobjs 设置 tag 
    //如果 Tag 写错了或者 gameobj 名字改错了就会报错为以后使用 Tag 排雷

    [MenuItem("MyTool/SetTag")]
    static public void test()
    {
        Type t = new Tag().GetType();
        foreach (PropertyInfo mi in t.GetProperties())
        {
            string tag =  (string)mi.GetValue(new Tag());
            Debug.Log(tag);

            GameObject p = GameObject.Find(tag);
            if (p == null)
            {
                Debug.LogError(tag + " no find ");
                continue;
            }
            SerializedObject tagManager = new SerializedObject(p);
            SerializedProperty it = tagManager.GetIterator();//序列化属性
            while (it.NextVisible(true))//下一属性的可见性
            {
                if (it.name == "m_TagString")
                {
                    for (int i = 0; i < it.arraySize; i++)
                    {
                        it.stringValue = tag;
                        tagManager.ApplyModifiedProperties();
                    }
                }
            }
        }
        return;
    }
}
