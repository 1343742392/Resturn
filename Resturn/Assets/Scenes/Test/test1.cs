using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    public List<Collider> RagdollColliders = new List<Collider>();
    public List<Rigidbody> RagdollRigidbodys = new List<Rigidbody>();
    [SerializeField]
    private GameObject mainBone = null;
    [SerializeField]
    private GameObject RagdollBone = null;
    [SerializeField]
    private GameObject body = null;

    // Start is called before the first frame update
    void Start()
    { 
        Transform[] bs = body.GetComponent<SkinnedMeshRenderer>().bones;
        InitRagdoll();
    }
    void InitRagdoll()
    {
        RagdollBone.SetActive(false);
        var g = Tool.GetGameObjAllChild(mainBone, "Ellen_Hips");
        var sr = body.GetComponent<SkinnedMeshRenderer>();
        Transform[] bfB = sr.bones;
        Tool.SetBone(sr, g);
        Transform[] afB = sr.bones;


        Rigidbody[] Rigidbodys = RagdollBone.GetComponentsInChildren<Rigidbody>();
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

    void EnableRagdoll()
    {
        //开启布娃娃状态的所有Rigidbody和Collider
        mainBone.SetActive(false);
        RagdollBone.SetActive(true);
        var rootBone = Tool.GetGameObjAllChild(RagdollBone, "Ellen_Hips");
        Tool.SetBone(body.GetComponent<SkinnedMeshRenderer>(), rootBone);
        for (int i = 0; i < RagdollRigidbodys.Count; i++)
        {
            RagdollRigidbodys[i].isKinematic = false;
            RagdollColliders[i].isTrigger = false;
        }
        //关闭正常状态的Collider
       // GetComponent<Collider>().enabled = false;
        //下一帧关闭正常状态的动画系统
        GetComponent<Animator>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = new Vector3(transform.position.x, 2, transform.position.z);
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
