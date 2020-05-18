using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject m_light = null;
    void Start()
    {
        m_light = Tool.GetGameObjAllChild(gameObject, "Light");
        m_light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenLight()
    {
        m_light.SetActive(true);
    }

    public void CloseLight()
    {
        m_light.SetActive(false);
    }
}
