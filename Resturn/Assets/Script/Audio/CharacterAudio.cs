using RSG;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.UIWidgets.widgets;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField] AudioClip NoseBreath = null;
    public float idePitch = 1;
    public float walkPitch = 1;
    public float ideVolume = 0.1f;
    public float walkVolume = 0.1f;
    [SerializeField] AudioClip MouthBreath = null;
    public float MouthVolumeMin = 1;
    public float MouthVolumeMax = 1;
    public float MouthPitchMin = 1;
    public float MouthPitchMax = 1;
    public float strength = 10;
    public float HeartbeatVolumeMin = 0.5f;
    public float HeartbeatVolumeMax = 1;
    public float HeartbeatPitchMin = 1;
    public float HeartbeatPitchMax = 1.4f;
    [SerializeField] AudioClip FootStone = null;
    [SerializeField] AudioClip FootGrass = null;
    [SerializeField] AudioClip FootPuddle = null;
    [SerializeField] AudioClip FootEarth = null;
    [SerializeField] AudioClip DeadClip = null;
    public int FootRandomVolume = 2;
    public float StratFootVolume = 1;
    public int FootRandomPitch = 2;
    public float StratFootPitch = 1;

    [SerializeField] public AudioClip Jump = null;
    AudioSource[] m_audioSources = null;
    AudioSource m_rFootAudio = null;
    AudioSource m_lFootAudio = null;
    Animator m_animator = null;
    float m_strength = 0;

    [SerializeField] private GameObject rfoot = null;
    [SerializeField] private GameObject lfoot = null;

    string beforeFoot = "";
    private bool m_oldGround = false;
    private bool m_animaAudio = true;
    void Start()
    {
        m_strength = strength;
        m_audioSources = GetComponents<AudioSource>();
        m_animator = GetComponent<Animator>();

        m_rFootAudio = rfoot.GetComponent<AudioSource>();
        m_lFootAudio = lfoot.GetComponent<AudioSource>();
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {


        var contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            var name = contactPoints[0].thisCollider.gameObject.name;
            if (beforeFoot.Equals(name))
                break;
            if (name.Equals("Ellen_Right_LowerLeg"))
            {
                RightFoot(contactPoints[0].otherCollider.gameObject.tag);
                beforeFoot = name;
            }
            if (name.Equals("Ellen_Left_LowerLeg"))
            {
                beforeFoot = name;
                LeftFoot(contactPoints[0].otherCollider.gameObject.tag);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
    }
    private void OnCollisionExit(Collision collision)
    {

    }


    public static float[] GetTextureMix(Vector3 worldPos)
    {
        // returns an array containing the relative mix of textures
        // on the main terrain at this world position.
        // The number of values in the array will equal the number
        // of textures added to the terrain.
        Terrain terrain = Terrain.activeTerrain;
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPos = terrain.transform.position;
        // calculate which splat map cell the worldPos falls within (ignoring y)
        int mapX = (int)(((worldPos.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth);
        int mapZ = (int)(((worldPos.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight);
        // get the splat data for this cell as a 1x1xN 3d array (where N = number of textures)
        float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);
        // extract the 3D array data to a 1D array:
        float[] cellMix = new float[splatmapData.GetUpperBound(2) + 1];
        for (int n = 0; n < cellMix.Length; ++n)
        {
            cellMix[n] = splatmapData[0, 0, n];
        }
        return cellMix;
    }
    public static int GetMainTexture(Vector3 worldPos)
    {
        // returns the zero-based index of the most dominant texture
        // on the main terrain at this world position.
        float[] mix = GetTextureMix(worldPos);
        float maxMix = 0;
        int maxIndex = 0;
        // loop through each mix value and find the maximum
        for (int n = 0; n < mix.Length; ++n)
        {
            if (mix[n] > maxMix)
            {
                maxIndex = n;
                maxMix = mix[n];
            }
        }
        return maxIndex;
    }

    public void Dead()
    {
        if(DeadClip != null)
        {
            m_audioSources[0].clip = DeadClip;
            m_audioSources[0].loop = false;
            m_audioSources[0].volume = 1;
            m_audioSources[0].Play();
            m_animaAudio = false;


        }
    }
    private void LeftFoot(string obj)
    {
        int texture = -1;
        if (Terrain.activeTerrain != null)
            texture = GetMainTexture(transform.position);

        if (m_lFootAudio == null) return;
        if (obj.Equals("Stone"))
        {
            if(FootStone == null) return;
            m_lFootAudio.clip = FootStone;
        }else if(obj.Equals("Grassland") || texture == 1)
        {
            if (FootGrass == null) return;
            m_lFootAudio.clip = FootGrass;
        }
        else if (obj.Equals("Puddle"))
        {
            if (FootPuddle == null) return;
            m_lFootAudio.clip = FootPuddle;
        }
        else if (obj.Equals("Earth") || texture == 0)
        {
            if (FootEarth == null) return;
            m_lFootAudio.clip = FootEarth;
        }
        m_lFootAudio.volume = StratFootVolume - (FootRandomVolume * 0.01f) + new System.Random().Next(-FootRandomVolume, FootRandomVolume) * 0.1f;
        m_lFootAudio.pitch = StratFootPitch + new System.Random().Next(-FootRandomPitch, FootRandomPitch) * 0.01f;
        m_lFootAudio.Play();
    }

    private void RightFoot(string obj)
    {
        int texture = -1;
        if (Terrain.activeTerrain != null)
            texture = GetMainTexture(transform.position);

        if (m_rFootAudio == null) return;
        if (obj.Equals("Stone"))
        {
            if (FootStone == null) return;
            m_rFootAudio.clip = FootStone;
        }
        else if (obj.Equals("Grassland") || texture == 1)
        {
            if (FootGrass == null) return;
            m_rFootAudio.clip = FootGrass;
        }
        else if (obj.Equals("Puddle"))
        {
            if (FootPuddle == null) return; ;
            m_rFootAudio.clip = FootPuddle;
        }
        else if (obj.Equals("Earth")||texture == 0)
        {
            if (m_rFootAudio == null || FootEarth == null) return;
            m_rFootAudio.clip = FootEarth;
        }
        m_rFootAudio.volume = StratFootVolume - (FootRandomVolume * 0.1f) + new System.Random().Next(-FootRandomVolume, FootRandomVolume) * 0.1f;
        m_lFootAudio.pitch = StratFootPitch + new System.Random().Next(-FootRandomPitch, FootRandomPitch) * 0.01f;
        m_rFootAudio.Play();
    }

    private bool IsJumpUp()
    {
        var nowGround = m_animator.GetBool("Ground");
        //Debug.Log(m_oldGround + "  " + nowGround);
        if (m_oldGround == true && nowGround == false)
        {
            m_oldGround = nowGround;
            return true;
        }
        m_oldGround = nowGround;
        return false;
    }
    void Update()
    {
        if (!m_animaAudio) return;
        if (m_audioSources == null || m_animator == null) return;


        if (IsJumpUp())
        {
            m_audioSources[0].clip = Jump;
            m_audioSources[0].volume = 1;
            m_audioSources[0].pitch = 1;
            m_audioSources[0].loop = false;

            m_audioSources[0].Play();
           // Debug.Log("jump");
        }


        if(m_animator.GetBool("Ground"))
        {

            var blend = m_animator.GetFloat("Blend");
            if (blend == 0)
            {
                Ide();
            }
            else if (blend > 0.8f)
            {
                Run();
            }
            else
            {
                Walk();
            }
            //Debug.Log("p" + m_strength);

            if (m_strength >= strength * 0.59f)
            {
                var p = Mathf.InverseLerp(strength, strength * 0.59f, m_strength);

                m_audioSources[0].volume = Mathf.Lerp(ideVolume, walkVolume, p);
                m_audioSources[0].pitch = Mathf.Lerp(idePitch, walkPitch, p);

                if (m_audioSources[0].clip == null || !m_audioSources[0].clip.name.Equals(NoseBreath.name))
                {
                    //LogDisplay.obj.AddLog("run");
                    m_audioSources[0].clip = NoseBreath;
                    m_audioSources[0].Play();
                }
                if (!m_audioSources[0].isPlaying)
                {
                    //LogDisplay.obj.AddLog("play");
                    m_audioSources[0].Play();
                }
            }
            else
            {
                var p = Mathf.InverseLerp(strength * 0.59f, 0, m_strength);
                m_audioSources[0].volume = Mathf.Lerp(MouthVolumeMin, MouthVolumeMax, p);
                m_audioSources[0].pitch = Mathf.Lerp(MouthPitchMin, MouthPitchMax, p);
                if (m_audioSources[0].clip.name != MouthBreath.name)
                {
                    m_audioSources[0].clip = MouthBreath;
                    m_audioSources[0].Play();
                }
                if (!m_audioSources[0].isPlaying)
                {
                    //LogDisplay.obj.AddLog("play");
                    m_audioSources[0].Play();
                }

            }
        }

        if (m_audioSources.Length > 1)
        {
            var pos = (strength - m_strength) / strength;
            m_audioSources[1].pitch = Mathf.Lerp(HeartbeatPitchMin, HeartbeatPitchMax, pos);
            m_audioSources[1].volume = Mathf.Lerp(HeartbeatVolumeMin, HeartbeatVolumeMax, pos);
            if (!m_audioSources[1].isPlaying) m_audioSources[1].Play();
        }
    }

    private void Walk()
    {
        if (strength * 0.6f - m_strength > 0.2f)
        {
            //跑动停下
            m_strength = Mathf.Clamp(m_strength + (Time.deltaTime / 2), 0, strength * 0.6f); ;
            return;
        }
        m_strength = Mathf.Clamp(m_strength - (Time.deltaTime /8), strength * 0.6f, strength); ;
    }

    private void Run()
    {
       if (m_strength > (strength * 0.6f))
        {
/*            Debug.Log("run");*/
            m_strength = Mathf.Clamp(m_strength - (Time.deltaTime * 4) , 0, strength);
        }
        else
        {
            m_strength = Mathf.Clamp(m_strength - (Time.deltaTime * 1), 0, strength);
        }
    }

    private void Ide()
    {
        m_strength = Mathf.Clamp(m_strength + Time.deltaTime , 0, strength);
    }

  
}
