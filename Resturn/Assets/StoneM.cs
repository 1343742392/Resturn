
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class StoneM : TaskBehavior
{
    [SerializeField]
    private Transform m_mouth = null;
    private GameObject m_player = null;
    private Animator m_anim = null;
    [SerializeField]
    private AudioClip m_runClip = null;
    [SerializeField]
    private AudioClip m_biteClip = null;

    private AudioSource m_audioSource = null;
    public float speed = 10;

    public float Radius = 0;
    private ParticleSystem[] bloods = null;

    private bool m_isFind = false;

    private Vector3 m_objDic = Vector3.zero;
    private float m_objDis = 0;
    protected override void StartS()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_anim = GetComponent<Animator>();
        m_player = GameObject.FindWithTag(Tag.Ellen);
        bloods = GetComponentsInChildren<ParticleSystem>();

    }

    private void RunAudio()
    {
        if (m_audioSource == null || m_runClip == null)
            return;
        if (m_audioSource.clip != m_runClip)
            m_audioSource.clip = m_runClip;
        m_audioSource.loop = true;
        if (!m_audioSource.isPlaying)
            m_audioSource.Play();
    }

    private void BiteAudio()
    {
        if (m_audioSource == null || m_biteClip == null)
            return;
        if (m_audioSource.clip != m_biteClip)
            m_audioSource.clip = m_biteClip;
        m_audioSource.loop = false;
        if (!m_audioSource.isPlaying)
            m_audioSource.Play();
    }

    private void Blood()
    {
        if (bloods == null) return;
        foreach (var blood in bloods)
        {
            blood.Play();
        }
    }

    private void Rote()
    {
        var q = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(m_objDic), 10f);
        transform.rotation = q;
    }


    private void Run()
    {
        transform.position += m_objDic * speed * Time.deltaTime;
        m_anim.SetBool("Run", true);
    }

    private void Find()
    {
        if(m_objDis < Radius /*&& m_player.GetComponent<Character>().GetMov() > 0.5f*/)
        {
            m_isFind = true;
        }
    }

    private void Attack()
    {
        m_anim.SetBool("Attack", true);
    }
    private bool IsLook()
    {
        if (Vector3.Dot(m_objDic, transform.forward) > 0.8f)
        {
            return true;
        }
        else
            return false;
    }

    protected override void UpdateS()
    {
        m_objDis = (m_player.transform.position - transform.position).magnitude; 
        m_objDic = (m_player.transform.position - transform.position).normalized;

        if (m_isFind)
        {
            Rote();
            if(IsLook())
            {
                if(m_objDis > 0.7f)
                    Run();
                else
                {
                    Attack();
                }
            }
        }
        else
        {
            Find();
        }    


/*        else if (dis < 1f *//*&& !m_anim.GetCurrentAnimatorStateInfo(0).IsName("Wake")*//*)
        {
            m_anim?.SetBool("AttackAble", true);
            BiteAudio();
            Blood();

            var dic = (playerPos - transform.position).normalized;
            transform.transform.forward = dic;

            var cc = m_player.GetComponent<CapsuleCollider>();
            var rig = m_player.GetComponent<Rigidbody>();
            if (rig != null) rig.isKinematic = true;
            if (cc != null) cc.enabled = false;

            if (back == null)
            {
                SetTime(0.3f, new System.Action(delegate ()
                {
                    m_player.GetComponent<Character>()?.Dead(true);


                    var b = Tool.GetGameObjAllChild(m_player, "Ellen_Hips");
                    b.GetComponent<Rigidbody>().isKinematic = true;
                    b.transform.position = m_mouth.position;
                    b.transform.SetParent(m_mouth);



                }));
            }

        }*/



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
