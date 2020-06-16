using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightButton : MonoBehaviour
{
    private GameObject m_character = null;

    private GameObject m_OpenLightImage = null;
    private GameObject m_CloseLightImage = null;

    void Start()
    {
        m_character = GameObject.FindWithTag(Tag.Ellen);
        m_OpenLightImage = Tool.GetGameObjAllChild(gameObject,"Open");
        m_OpenLightImage.SetActive(false);
        m_CloseLightImage = Tool.GetGameObjAllChild(gameObject,"Close");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (m_character == null || m_CloseLightImage == null || m_OpenLightImage == null) return;
        m_CloseLightImage.SetActive(!m_CloseLightImage.activeSelf);
        m_OpenLightImage.SetActive(!m_OpenLightImage.activeSelf);
        if (m_OpenLightImage.activeSelf)
            m_character.GetComponent<Character>().OpenLight();
        else
            m_character.GetComponent<Character>().CloseLight();
    }
}
