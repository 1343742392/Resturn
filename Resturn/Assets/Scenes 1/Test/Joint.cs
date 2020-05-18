using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.UIWidgets.foundation;
using UnityEngine;

public class Joint : MonoBehaviour
{

    [SerializeField] Transform obj = null;
    [SerializeField] float rotSpeed = 0.1f;
    [SerializeField] float posSpeed = 50f;
    [SerializeField] bool freezeRotX = false;
    [SerializeField] bool freezeRotY = false;
    [SerializeField] bool freezeRotZ = false;
    private float time = 0;
    Vector3 transStartRot = Vector3.zero;
    Vector3 objStartFwd = Vector3.zero;

    void Start()
    {
        var myDic = (transform.position - obj.position).normalized;
        objStartFwd = obj.transform.forward;

        transStartRot = transform.rotation.eulerAngles;
    }
    private void Update()
    {
        //Debug.Log("fixed");

        if (obj == null) return;
        var objF = obj.transform.forward;
        var targetPos = obj.position + (new Vector3(objF.x, 1.2f, objF.z) * 0.09f);


        transform.position = Vector3.MoveTowards(transform.position, targetPos, posSpeed * 0.5f);

        
        if (transform.rotation.eulerAngles != obj.transform.rotation.eulerAngles)
        {
            time = Math.Min(8, time +Time.deltaTime);
/*            var tE = transform.rotation.eulerAngles;
            var oE = obj.transform.rotation.eulerAngles;
            tE.x = freezeRotX? transStartRot.x: tE.x;
            tE.y = freezeRotY ? transStartRot.y : tE.y;
            tE.z = freezeRotZ ? transStartRot.z : tE.z;
            oE.x = freezeRotX ? transStartRot.x : oE.x;
            oE.y = freezeRotY ? transStartRot.y : oE.y;
            oE.z = freezeRotZ ? transStartRot.z : oE.z;
            var newQ = Quaternion.Euler(tE);
            var newoQ = Quaternion.Euler(oE);*/
            //var q = 
            transform.rotation = Quaternion.RotateTowards(transform.rotation, obj.transform.rotation, rotSpeed * time);
        }
        else
        {
            time = 0;
        }
    }

}
