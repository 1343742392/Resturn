using System;
using System.Collections;
using System.Collections.Generic;
using Unity.UIWidgets.ui;
using UnityEngine;

public class Character : TaskBehavior
{
    // Start is called before the first frame update
    public float m_rotateSpeed = 10;
    public float m_walkSpeed = 1;
    public float m_runSpeed = 3;
    public float m_jumpPower = 100;
    [SerializeField] GameObject camera = null;

    private Animator m_animator = null;
    private Joystick m_joystick = null;
    private JumpButton m_jumpButton = null;
    private Rigidbody m_rigidbody = null;

    private GameObject m_leftFoot = null;
    private GameObject m_rigthFoot = null;
    //private List<Collider> m_lCollisions = new List<Collider>();
    private List<Collider> m_Collisions = new List<Collider>();
    private bool m_isGrounded = false;
    private float m_fLoatTime = -1;

    private GameObject m_phone = null;
    private bool m_isTakePhone = false;
    //骨骼
    private GameObject m_rightHand = null;
    private GameObject m_spine = null;

    private string m_leftFootName = "Ellen_Left_LowerLeg";
    private string m_rightFootName = "Ellen_Right_LowerLeg";

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_joystick = GameObject.FindWithTag(Tag.joystick)?.GetComponent<Joystick>();
        m_jumpButton = GameObject.FindWithTag(Tag.jump)?.GetComponent<JumpButton>();

        m_leftFoot = Tool.GetGameObjAllChild(gameObject, m_leftFootName);
        m_rigthFoot = Tool.GetGameObjAllChild(gameObject, m_rightFootName);
        if(m_jumpButton!=null) m_jumpButton.OnJump = Jump;
        m_rigidbody = GetComponent<Rigidbody>();

        m_phone = Tool.GetGameObjAllChild(gameObject, "Phone");
        m_rightHand = Tool.GetGameObjAllChild(gameObject, "Ellen_Right_Hand");
        m_spine = Tool.GetGameObjAllChild(gameObject, "Ellen_Spine");
    }



    // Update is called once per frame
    protected override void UpdateS()
    {
        if (m_fLoatTime != -1 && Time.time - m_fLoatTime > 1f)
        {
            Debug.Log("float");
            m_isGrounded = false;
        }
        m_animator?.SetBool("Ground", m_isGrounded);


        InputEvent();
    }

    public void OpenLight()
    {
        if (m_animator == null) return;
        m_animator.SetBool("Light", !m_animator.GetBool("Light"));
    }

    public void CloseLight()
    {
        if (m_animator == null) return;
        m_animator.SetBool("Light", !m_animator.GetBool("Light"));
    }




    private void InputEvent()
    {
        if (m_joystick != null)
        {
            var verValue = m_joystick.m_vertical;
            float mov = 0;
            if (verValue > 0.7f && m_joystick.m_length > 600)
            {
                m_animator?.SetFloat("Blend", 1);
                mov = m_joystick.m_vertical * Time.deltaTime * m_runSpeed;
            }
            else
            {
                m_animator?.SetFloat("Blend", (float)Math.Min(verValue, 0.7));
                mov =  m_joystick.m_vertical * Time.deltaTime * m_walkSpeed;
            }
            var rotY = m_joystick.m_horizontal * Time.deltaTime * m_rotateSpeed;
            transform.localPosition += transform.forward * mov;
            transform.Rotate(0, rotY, 0);


            if (camera != null)
            {
                camera.transform.RotateAround(transform.position, transform.up, rotY);
                camera.transform.position += mov * transform.forward;
            }
        }
    }

    private void Jump()
    {
        Debug.Log("jump");
        m_isGrounded = false;
        m_animator?.SetTrigger("Jump");
        m_rigidbody.AddForce(Vector3.up * m_jumpPower);
    }

    private void OnCollisionEnter(Collision collision)
    {
/*        Debug.Log("inter");*/
        var contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (!m_Collisions.Contains(collision.collider))
            {
                m_Collisions.Add(collision.collider);
            }
        }
        /*            if (contactPoints[i].thisCollider.gameObject.Equals(m_leftFoot))
                    {
                        if (!m_lCollisions.Contains(collision.collider))
                        {
                            m_lCollisions.Add(collision.collider);
                        }
                        m_fLoatTime = -1;
                    }
                }*/

        //Debug.Log("inter lc " + m_lCollisions.Count + " rc  " + m_rCollisions.Count);
    }


    private void OnCollisionStay(Collision collision)
    {
/*        Debug.Log("stay" + collision.contactCount);
        m_isGrounded = false;
        m_fLoatTime = Time.time;*/
        for (int i = 0; i < collision.contactCount; i++)
        {
            var name = collision.contacts[i].thisCollider.name;
            //Debug.Log(name);
            if (name.Equals(m_leftFoot.name) || name.Equals(m_rigthFoot.name))
            {
                m_isGrounded = true;
                m_fLoatTime = -1;
            }
        }
      //  if (collision.contactCount > 0)
           // Debug.Log("stay" + collision.contacts[0].thisCollider.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_Collisions.Contains(collision.collider))
        {
            m_Collisions.Remove(collision.collider);
        }
        if (m_Collisions.Count == 0)
        {
/*            Debug.Log("run " + m_isGrounded);
            Debug.Log("run");*/
            m_fLoatTime = Time.time;
        }
/*        else if (m_lCollisions.Contains(collision.collider))
        {
            m_lCollisions.Remove(collision.collider);
        }
        if (m_lCollisions.Count == 0 && m_rCollisions.Count == 0)
        {
            if (m_fLoatTime == -1)
            {
                m_fLoatTime = Time.time;
               // Debug.Log("time");
            }
        }*/
         //Debug.Log("out lc " + m_lCollisions.Count + " rc  " + m_rCollisions.Count);
    }

    //Anima Event
    private void TakePhone()
    {
        Debug.Log("run");
        if (m_rightHand != null && m_spine != null && m_phone != null)
        {
            if (m_isTakePhone)
            {
                m_phone.transform.SetParent(m_spine.transform);
                m_phone.transform.localRotation = Quaternion.Euler(new Vector3(11.161f, -81.30601f, 170.849f));
                m_phone.transform.localPosition = new Vector3(0.1461076f, 0.1777581f, -0.01665149f);
                m_phone.GetComponent<Phone>().CloseLight();
            }
            else
            {
                m_phone.transform.SetParent(m_rightHand.transform);
                m_phone.transform.localRotation = Quaternion.Euler(new Vector3(2.838f, -28.498f, 28.982f));
                m_phone.transform.localPosition = new Vector3(0.06401002f, 0.03699063f, -0.04600625f);
                m_phone.GetComponent<Phone>().OpenLight();
            }
        }

        m_isTakePhone = !m_isTakePhone;

    }


}
