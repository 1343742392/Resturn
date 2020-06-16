using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioBtn : MonoBehaviour
{
    bool m_isOpen = true;
    [SerializeField] AudioListener m_fakeAL = null;
    void Start()
    {
        if(PlayerPrefs.HasKey("Audio"))
        {
            m_isOpen = PlayerPrefs.GetInt("Audio") == 1;
        }
        SetImg();
        SetAudio();
    }

    void SetImg()
    {
        var img = GetComponentInChildren<Image>();
        if(m_isOpen == false)
        {
            var sp = Instantiate(Resources.Load<Sprite>("Sprite/AudioClose"));
            img.sprite = sp;
        }
        else
        {
            var sp = Instantiate(Resources.Load<Sprite>("Sprite/AudioOpen"));
            img.sprite = sp;
        }
    }

    public void OnClick()
    {
        m_isOpen = !m_isOpen;
        PlayerPrefs.SetInt("Audio",m_isOpen?1:0);
        PlayerPrefs.Save();
        SetImg();
        SetAudio();
    }

    private void SetAudio()
    {
        var MainCam = Camera.main;
        if(MainCam!= null)
            MainCam.GetComponent<AudioListener >().enabled = m_isOpen;
        // var DeadCam = GameObject.FindWithTag(Tag.DeadCamera);
        // if(DeadCam != null)
        //     DeadCam.GetComponentInChildren<AudioListener>().enabled = m_isOpen;
        if(m_fakeAL!= null)
            m_fakeAL.enabled = !m_isOpen;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
