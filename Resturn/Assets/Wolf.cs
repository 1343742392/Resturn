using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : TaskBehavior
{
    [SerializeField]
    private Transform m_mouth = null;
    private GameObject m_player = null;
    private Animator m_anim = null;

    public float speed = 10;

    public float Radius = 0;

    protected override void StartS()
    {
        m_anim = GetComponent<Animator>();
        m_player = GameObject.FindWithTag(Tag.Ellen);
    }

    protected override void UpdateS()
    {
        var playerPos = m_player.transform.position;
        var dis = Vector3.Distance(playerPos, transform.position);
        if(dis <  Radius && dis > 1f)
        {
            m_anim.SetBool("Wake", true);
        }else if (dis < 1f /*&& !m_anim.GetCurrentAnimatorStateInfo(0).IsName("Wake")*/)
        {
            var dic = (playerPos - transform.position).normalized;
            transform.transform.forward = dic;

            m_anim?.SetBool("AttackAble", true);
            var cc = m_player.GetComponent<CapsuleCollider>();
            var rig = m_player.GetComponent<Rigidbody>();
            if (rig != null) rig.isKinematic = true;
            if (cc != null) cc.enabled = false;

            if(back == null)
            {
                SetTime(0.3f, new System.Action(delegate ()
                {
                    m_player.GetComponent<Character>()?.Dead(true) ;


                    var b = Tool.GetGameObjAllChild(m_player, "Ellen_Hips");
                    b.GetComponent<Rigidbody>().isKinematic = true;
                    b.transform.position = m_mouth.position;
                    b.transform.SetParent(m_mouth);

                    

                }));
            }

        }
        if(Input.GetKeyDown(KeyCode.K))
        {

        }
        if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            var dic = (playerPos - transform.position).normalized;
            transform.transform.forward = dic;
            transform.position += dic * speed * Time.deltaTime;
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    // Update is called once per frame

}
