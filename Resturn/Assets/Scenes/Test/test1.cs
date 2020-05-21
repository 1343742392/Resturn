using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    public List<Collider> RagdollColliders = new List<Collider>();
    public List<Rigidbody> RagdollRigidbodys = new List<Rigidbody>();

    // Start is called before the first frame update
    void Start()
    {
        InitRagdoll();
        DisableRagdoll();
    }
    void InitRagdoll()
    {
        Rigidbody[] Rigidbodys = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < Rigidbodys.Length; i++)
        {
            if (Rigidbodys[i] == GetComponent<Rigidbody>())
            {
                //排除正常状态的Rigidbody
                continue;
            }
            //添加Rigidbody和Collider到List
            RagdollRigidbodys.Add(Rigidbodys[i]);
            Rigidbodys[i].isKinematic = true;
            Collider RagdollCollider = Rigidbodys[i].gameObject.GetComponent<Collider>();
            RagdollCollider.isTrigger = true;
            RagdollColliders.Add(RagdollCollider);
        }
    }
    IEnumerator SetAnimatorEnable(bool Enable)
    {
        yield return new WaitForEndOfFrame();
        //Anim.enabled = Enable;
    }
    void EnableRagdoll()
    {
        //开启布娃娃状态的所有Rigidbody和Collider
        for (int i = 0; i < RagdollRigidbodys.Count; i++)
        {
            RagdollRigidbodys[i].isKinematic = false;
            RagdollColliders[i].isTrigger = false;
        }
        //关闭正常状态的Collider
        GetComponent<Collider>().enabled = false;
        //下一帧关闭正常状态的动画系统
        GetComponent<Animator>().enabled = false;
        StartCoroutine(SetAnimatorEnable(false));
    }

    void DisableRagdoll()
    {
        //关闭布娃娃状态的所有Rigidbody和Collider
        for (int i = 0; i < RagdollRigidbodys.Count; i++)
        {
            RagdollRigidbodys[i].isKinematic = true;
            RagdollColliders[i].isTrigger = true;
        }
        //开启正常状态的Collider
        GetComponent<Collider>().enabled = true;
        //下一帧开启正常状态的动画系统
        StartCoroutine(SetAnimatorEnable(true));
    }



    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.K))
        {
            EnableRagdoll();
        }
    }
}
