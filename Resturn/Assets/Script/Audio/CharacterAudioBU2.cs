using RSG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioBU2 : MonoBehaviour
{
    [SerializeField] AudioClip NoseBreath = null;
    public float idePitch = 1;
    public float ideVolume = 0.1f;
    public float walkPitch = 1;
    public float walkVolume = 0.1f;
    [SerializeField] AudioClip MouthBreath = null;
    public float strength = 3;
    [SerializeField] AudioClip Heartbeat = null;



    AudioSource[] m_audioSources = null;
    Animator m_animator = null;
    float runTime = 0;
    void Start()
    {
        m_audioSources = GetComponents<AudioSource>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame


    void Update()
    {
        if (m_audioSources == null || m_animator == null) return;
        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Blend"))
        {
            m_audioSources[0].Stop();
            return;
        }

        var blend = m_animator.GetFloat("Blend");
        if(blend == 0)
        {
            Ide();
        }
        else if(blend > 0.8f)
        {
            Run();
        }
        else 
        {
            Walk();
        }
        if (m_audioSources.Length > 1)
        {
            var pos = runTime / strength;
            m_audioSources[1].pitch = Mathf.Lerp(1, 1.4f, pos);
            m_audioSources[1].volume = Mathf.Lerp(0.5f, 1, pos);
        }
    }

    void Ide()
    {
        //Debug.Log("ide");

        if(runTime < 0.5f)
        {
            m_audioSources[0].clip = NoseBreath;
            if (!m_audioSources[0].isPlaying)
                m_audioSources[0].Play();
        }
        m_audioSources[0].pitch = idePitch;
        m_audioSources[0].volume = ideVolume    ;

        runTime -= Time.deltaTime * 2;
    }

    void Run()
    {
        Debug.Log("run");
        runTime += Time.deltaTime;

        if (runTime > strength)
        {
            m_audioSources[0].clip = MouthBreath;
            if (!m_audioSources[0].isPlaying)
                m_audioSources[0].Play();
        }
    }

    void Walk()
    {
        //Debug.Log("walk");
        if(runTime < 2.5f)
        {
            m_audioSources[0].clip = NoseBreath;
            m_audioSources[0].pitch = walkPitch;
            m_audioSources[0].volume = walkVolume;
        }

        runTime -= Time.deltaTime;
    }
}
