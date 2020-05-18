using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool{


    public const int ADD = 1;
    public const int LOSS = 2;


    static public float ClampDistance(float now, float push, float min, float max)
    {
        float reslut = 0;
        if (now + push > max)
            reslut = max - now;
        else if (now + push < min)
            reslut = min - now;
        else
            reslut = push;
        return reslut;
    }

    public static void LookAt(GameObject source, Vector3 obj)
    {
        source.transform.forward = (obj - source.transform.position).normalized;
    }

    //计算一个向量围绕世界轴旋转之后的位置
    static public Vector3 Rotate(Vector3 source, Vector3 axis, float angle)
    {

        Quaternion q = Quaternion.AngleAxis(angle, axis);// 旋转系数

        return q * source;
    }

    //计算transform 围绕自身轴旋转之后的方向
    static public Vector3 RotateWithRotation(Vector3 source, Vector3 axis, Vector3 Rotation, float angle)
    {
        Quaternion q = Quaternion.identity;
        q.eulerAngles = Rotation;
        Vector3 ax = q * axis;
        return Quaternion.AngleAxis(angle, ax) * source;
    }
    
    static public GameObject GetGameObjAllChild(GameObject gameobj, string childName)
    {
        Transform[] cs = gameobj.transform.GetComponentsInChildren<Transform>();
        foreach (Transform c in cs)
        {
            if (c.name == childName) return c.gameObject;
        }
        return null;
    }

    static public Transform GetTransformTop(Transform tf)
    {
        return tf.parent == null ? tf : GetTransformTop(tf.parent);
    }

    //n^2 不适合有很多collider
    static public void FindCollider(
        Collision collision, 
        string name, 
        out Collider colliderRes, 
        out ContactPoint contactRes)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            var c = collision.contacts[i].thisCollider;
            var gameobj = c.gameObject;
            if(gameobj.name == name)
            {
                colliderRes = c;
                contactRes = collision.contacts[i];
                return;
            }
        }
        colliderRes = null ;
        contactRes = new ContactPoint();
    }

}
