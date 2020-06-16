using System;
using System.Collections;
using System.Collections.Generic;
using Unity.UIWidgets.ui;
using UnityEngine;

public class ToDark : MonoBehaviour
{
    // Start is called before the first frame update\
    public static ToDark obj = null;
    Animator m_anim = null;
    void Start()
    {
        m_anim = GetComponent<Animator>();
        obj = this;
        //Show();
    }

    public void Fade()
    {
        m_anim.Play("ToDark");
    }

    public void Stop()
    {
        m_anim?.Play("Null");
    }

    public void Show()
    {
/*        try
        {*/
            m_anim?.Play("Show");

/*        }
        catch
        {

        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
    }


}
