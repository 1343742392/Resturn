using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : TaskBehavior
{
    Animator m_anima = null;
    public Vector3 m_startPos = Vector3.zero;
    public Vector3 m_endPos = Vector3.zero;

    public GameObject m_rHand = null; 

    public float m_speed = 1;
    Vector3 m_dic = Vector3.zero;
    enum State{To,Back,Attack}
    State m_state = State.To;

    SkinnedMeshRenderer m_sm = null;

    public Material m_showM = null;

    private GameObject m_attackObj = null;

    private ParticleSystem m_blood  = null;
    protected override void StartS()
    {
    }

    void  OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == Tag.Ellen)
        {
            Attak(collision.gameObject);
        }
    }

    private void Walk(Vector3 obj)
    {
        transform.position =  Vector3.MoveTowards(transform.position, obj, m_speed * Time.deltaTime);

        m_anima?.SetBool("Walk", true);
        transform.forward = m_dic;
    }

    private void Attak(GameObject obj)
    {
        if(m_state == State.Attack)return;
        m_attackObj = obj;
        var dic  = (obj.transform.position - transform.position).normalized;
        //transform.forward = dic;
        m_anima?.SetBool("Attack", true);
        if(m_showM != null && m_sm != null)
        {
            m_sm.material = m_showM;
            m_sm.material.SetFloat("_StartTime", Time.time);
        }
        var cs = obj.GetComponentsInChildren<Collider>();
        foreach(var c in cs)
        {
            c.enabled = false;
        }
        var rs = obj.GetComponentsInChildren<Rigidbody>();
        foreach(var r in rs)
        {
            r.isKinematic = true;
        }
        
        m_state = State.Attack;
    }

    public void Touch()
    {
        if(m_rHand!=null) m_attackObj.transform.SetParent(m_rHand.transform);
        m_attackObj?.GetComponent<Character>().GrabHead();
    }

    private void RePlay()
    {

    }

    public  void Bite()
    {
        m_blood?.Play();
    }

    protected override void UpdateS()
    {
        if(transform.position == m_endPos)
        {
            m_state = State.Back;
        }
        if(transform.position == m_startPos)
        {
            m_state = State.To;
        }

        if(m_state == State.To)
        {
            m_dic = (m_endPos - transform.position).normalized;
            Walk(m_endPos);
        }
        else if(m_state == State.Back)
        {
            m_dic = (m_startPos - transform.position).normalized;
            Walk(m_startPos);
        }
        else if(m_state == State.Attack)
        {
            m_attackObj.transform.up = transform.up;
            var  lp = m_rHand.transform.position ;
            m_attackObj.transform.position = new Vector3(lp.x, lp.y - 1.6f, lp.z);
            m_attackObj.transform.localPosition += new Vector3(0.05f, 0.1f, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_sm = GetComponentInChildren<SkinnedMeshRenderer>();
        m_anima = GetComponent<Animator>();
        m_startPos = transform.position;
        m_blood = GetComponentInChildren<ParticleSystem>();
    }

        void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_endPos, 5f);
    }

}
