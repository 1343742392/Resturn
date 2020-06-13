using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.ui;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : TaskBehavior, ExplosionTarget
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

    /*    private string m_leftFootName = "Ellen_Right_Foot";
        private string m_rightFootName = "Ellen_Left_Foot";*/
    private bool m_isJumpStart = false;

    public List<Collider> RagdollColliders = new List<Collider>();
    public List<Rigidbody> RagdollRigidbodys = new List<Rigidbody>();
    [SerializeField]
    private GameObject mainBone = null;
    [SerializeField]
    private GameObject RagdollBone = null;
    [SerializeField]
    private GameObject body = null;
    private Timer m_timer = null;

    private int m_rePlayIndex = -1;

    public string state = "";


    private float m_movAndRot = 0;
    float m_HP = 100;
    bool m_moveAble = true;
    class Operation
    {
        public Vector3 pos;
        public Vector3 rot;
        public float mov;
        public float rotY;
        public float ver;
        public float hor;
        public float animaValue;
        public float time;
        public Operation(float time, float mov, float rotY, float ver, float hor, float animaValue, Vector3 pos, Vector3 rot)
        {
            this.time = time;
            this.mov = mov;
            this.rotY = rotY;
            this.ver = ver;
            this.hor = hor;
            this.animaValue = animaValue;
            this.pos = pos;
            this.rot = rot;
        }
    }
    private List<Operation> m_operations = new List<Operation>();
    void Start()
    {
    }

    public GameObject GetHead()
    {
        return Tool.GetGameObjAllChild(mainBone, "Ellen_Head");
    }

    public void GrabHead()
    {
        m_animator?.SetTrigger("GrabHead");
    }

    
    // Update is called once per frame
    public void MoveAble(bool moveAble)
    {
        m_moveAble = moveAble;
    }

    private void RePlayer(float time)
    {
        var beforTime = Time.time - time;
        if (beforTime <= 0) return;
        var index = 0;
        for (int f = m_operations.Count - 1; f > -1; f--)
        {
            if (m_operations[f].time <= beforTime)
            {
                m_rePlayIndex = f;
                return;
            }
        }
    }
    protected override void UpdateS()
    {

        if (m_fLoatTime != -1 && Time.time - m_fLoatTime > 1f)
        {
            //Debug.Log("float");
            m_isGrounded = false;
        }
        //Debug.Log(m_isGrounded);
        m_animator?.SetBool("Ground", m_isGrounded);

        if (m_rePlayIndex == -1)
            InputEvent();
        else
        {
            InputEvent(m_operations[m_rePlayIndex - 1]);
            m_rePlayIndex = m_rePlayIndex + 1;
            if (m_rePlayIndex == m_operations.Count - 1)
            {
                var SceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(SceneName);
            }
        }
    }
    public void Hit()
    {
        m_HP -= 20;
        if (m_HP <= 0)
        {
            Dead();
        }
        else
        {
            m_animator?.Play("Hit");
            GetComponent<CharacterAudio>().Hit();
        }
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

    public void Dead(bool haveAudio = true, float replayerTime = 1, bool resurrection = false)
    {
        if (m_timer == null) return;
        if (state == "dead")
            return;
        state = "dead";
        EnableRagdoll();
        if (haveAudio)
        {
            GetComponent<CharacterAudio>().Dead();
        }
        else
        {
            var audios = GetComponentsInChildren<AudioSource>();
            foreach (var audio in audios)
            {
                audio.enabled = false;
            }
        }

        //开始回放处到死亡的时间
        var index = Math.Min(m_operations.Count - 1, m_operations.Count - ConfigManager.obj.config.rePlayFrameNum - 1);

        var time = 0f;
        if (m_operations.Count > 0) time = Time.time - m_operations[index].time;
        //死亡时所在帧
        var deadFrame = m_operations.Count;

        m_timer.SetTime(ConfigManager.obj.config.rePlayWaitTime);

        //变暗
        m_timer.back = new Action(delegate ()
        {
            ToDark.obj?.Fade();


            state = "dead";

            //第一关的飞机和爆炸
            var ad = GameObject.FindWithTag(Tag.AircraftDead);
            if (ad != null)
            {
                //第一关时
                var adpos = ad.transform.position;
                Destroy(ad);
                var newAd = Instantiate(Resources.Load<GameObject>("Profab/AircraftDead"));
                newAd.transform.position = adpos;
                newAd.GetComponent<AircraftDead>().SetBlastTime(time);

                //Debug.Log(time);
            }
            //布娃娃
            Camera.main?.gameObject.SetActive(false);
            //相机和光

            var dc = GameObject.FindWithTag(Tag.DeadCamera);
            dc.GetComponent<Camera>().enabled = true;
            if (SceneManager.GetActiveScene().name == "One")
            {
                var al = Tool.GetGameObjAllChild(dc, "AL");
                al.GetComponent<AudioListener>().enabled = true;
            }
            if (resurrection)
            {
                DisableRagdoll();
                state = "";
            }

            else
            {
                GetComponent<CharacterAudio>().Dead();
            }
            Sun.sun?.SW();
            //停止变暗
            ToDark.obj?.Stop();


            if (time != 0)
            {
                var offFrame = m_operations.Count - deadFrame;
                RePlayer(replayerTime);
                //_rePlayIndex = ConfigManager.obj.config.rePlayFrameNum + offFrame;
            }
        });
        GameObject.FindWithTag(Tag.Input)?.SetActive(false);
        GameObject.FindWithTag(Tag.Compass)?.SetActive(false);
    }

    public float GetMov()
    {
        return m_movAndRot;
    }

    private void InputEvent(Operation operation = null)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            RePlayer(3);
        }
        if (m_joystick != null)
        {
            float verValue = 0;
            float horValue = 0;
            float mov = 0;
            float rotY = 0;
            float animaValue = 0;

            if (operation == null)
            {
                //计算

                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    if (Input.GetKey(KeyCode.W))
                        verValue += 0.5f;
                    if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.Q))
                    {
                        m_joystick.m_length = 500;
                        verValue += 0.5f;

                    }
                    if (Input.GetKey(KeyCode.A))
                        horValue = -0.5f;
                    if (Input.GetKey(KeyCode.D))
                        horValue = 0.5f;
                    if (Input.GetKey(KeyCode.S))
                        verValue = -0.5f;
                    if (Input.GetKey(KeyCode.Space))
                        Jump();
                }
                else
                {
                    verValue = m_joystick.m_vertical;
                    horValue = m_joystick.m_horizontal;
                }

                if (verValue > 0.7f && m_joystick.m_length > 400)
                {
                    animaValue = 1;
                    mov = verValue * Time.deltaTime * m_runSpeed;
                }
                else
                {
                    animaValue = 0.7f;
                    mov = verValue * Time.deltaTime * m_walkSpeed;
                }
                //Debug.Log(mov);
                rotY = horValue * Time.deltaTime * m_rotateSpeed;

                //保存
                m_operations.Add(new Operation(Time.time, mov, rotY, verValue, horValue, animaValue, transform.position, transform.rotation.eulerAngles));

            }
            else
            {
                verValue = operation.ver;
                horValue = operation.hor;
                mov = operation.mov;
                rotY = operation.rotY;
                animaValue = operation.animaValue;
                transform.position = operation.pos;
                transform.rotation = Quaternion.Euler(operation.rot);
                //Debug.Log(verValue + " " + animaValue);
            }

            if (m_moveAble == true)
            {
                m_animator?.SetFloat("Blend", (float)Math.Min(verValue, animaValue));
                if (state != "dead")
                {
                    transform.localPosition += transform.forward * mov;
                    transform.Rotate(0, rotY, 0);
                    if (camera != null)
                    {
                        //设置相机位置
                        camera.transform.RotateAround(transform.position, transform.up, rotY);
                        camera.transform.position += mov * transform.forward;
                    }
                }
            }



            m_movAndRot = Math.Max(verValue, horValue);




        }
    }

    public void Fall()
    {

        GameObject.Destroy(m_leftFoot.GetComponent<CapsuleCollider>());
        GameObject.Destroy(m_rigthFoot.GetComponent<CapsuleCollider>());
        m_animator?.Play("Float");
        m_isGrounded = false;
        m_phone.transform.SetParent(null);
        m_phone.AddComponent<Rigidbody>();
        m_phone.GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().freezeRotation = false;
        GetComponent<Rigidbody>().AddForce(-transform.forward * 100);
        
    }


    private void Jump()
    {
        //Debug.Log("jump");
        if (m_isJumpStart) return;
        m_isJumpStart = true;
        m_isGrounded = false;

        if (m_animator.GetBool("Light"))
        {
            m_rigidbody.AddForce(Vector3.up * m_jumpPower * 0.6f);
        }
        else
        {
            m_rigidbody.AddForce(Vector3.up * m_jumpPower);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //用射线应该更好
        if (once) return;
        /*        Debug.Log("inter");*/
        var contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {

            if (!m_Collisions.Contains(collision.collider))
            {
                m_Collisions.Add(collision.collider);
            }
            var thisName = contactPoints[i].thisCollider.gameObject.name;
            //Debug.Log(contactPoints[i].otherCollider.name);
            if (thisName.Equals(m_leftFoot.name) || thisName.Equals(m_rigthFoot.name))
            {
                m_isJumpStart = false;
            }
            else if(thisName.Equals("BlastPhysics"))
            {
                Debug.Log("run");
            }
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (once) return ;
        //Debug.Log("stay" + collision.contactCount);
/*        m_isGrounded = false;
        m_fLoatTime = Time.time;*/
        for (int i = 0; i < collision.contactCount; i++)
        {
            var name = collision.contacts[i].thisCollider.name;
            if (name.Equals(m_leftFoot.name) || name.Equals(m_rigthFoot.name))
            {
                if (m_isJumpStart) continue;
                m_isGrounded = true;
                m_fLoatTime = -1;
            }
        }
      //  if (collision.contactCount > 0)
           // Debug.Log("stay" + collision.contacts[0].thisCollider.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (once) return;
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
        //Debug.Log("run");
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

    private void InitRagdoll()
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

    private void EnableRagdoll()
    {
        camera.GetComponent<Joint>().obj = Tool.GetGameObjAllChild(RagdollBone, "Ellen_Neck").transform;


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
        //GetComponent<Collider>().enabled = false;
        //下一帧关闭正常状态的动画系统
        GetComponent<Animator>().enabled = false;
    }

    private void DisableRagdoll()
    {
        camera.GetComponent<Joint>().obj = Tool.GetGameObjAllChild(mainBone, "Ellen_Neck").transform;


        var audios = GetComponentsInChildren<AudioSource>();
        foreach (var audio in audios)
        {
            audio.enabled = true;
        }
        transform.position = new Vector3(transform.position.x,  2.4f, transform.position.z);
        mainBone.SetActive(true);
        RagdollBone.SetActive(false);
        var rootBone = Tool.GetGameObjAllChild(mainBone, "Ellen_Hips");
        Tool.SetBone(body.GetComponent<SkinnedMeshRenderer>(), rootBone);
        //关闭布娃娃状态的所有Rigidbody和Collider
        for (int i = 0; i < RagdollRigidbodys.Count; i++)
        {
            RagdollRigidbodys[i].isKinematic = true;
            RagdollColliders[i].isTrigger = true;
        }
        //开启正常状态的Collider
        GetComponent<Collider>().enabled = true;
        //下一帧开启正常状态的动画系统
        GetComponent<Animator>().enabled = true;

        Destroy(m_leftFoot.GetComponent<Rigidbody>());

        Destroy(m_rigthFoot.GetComponent<Rigidbody>());

        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }

    public void Blast(Explosion exception = null)
    {
        Dead(false, 1, true);
    }

    protected override void StartS()
    {

        m_animator = GetComponent<Animator>();
        m_joystick = GameObject.FindWithTag(Tag.joystick)?.GetComponent<Joystick>();
        m_jumpButton = GameObject.FindWithTag(Tag.jump)?.GetComponent<JumpButton>();

        m_leftFoot = Tool.GetGameObjAllChild(mainBone, m_leftFootName);
        m_rigthFoot = Tool.GetGameObjAllChild(mainBone, m_rightFootName);
        if (m_jumpButton != null) m_jumpButton.OnJump = Jump;
        m_rigidbody = GetComponent<Rigidbody>();

        m_phone = Tool.GetGameObjAllChild(gameObject, "Phone");
        m_rightHand = Tool.GetGameObjAllChild(mainBone, "Ellen_Right_Hand");
        m_spine = Tool.GetGameObjAllChild(mainBone, "Ellen_Spine");

        InitRagdoll();
        m_timer = GetComponent<Timer>();
        ToDark.obj?.Show();


    }
}
