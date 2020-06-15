using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fly : TaskBehavior
{
    bool m_fall = false;

    public static AsyncOperation async;


    protected override void StartS()
    {
    }

    protected override void UpdateS()
    {

        transform.localPosition += -transform.up * Time.deltaTime * 10;
        if (m_fall)
            transform.Rotate(Time.deltaTime * 2 , 0, 0);
    }



    // Start is called before the first frame update
    void Start()
    {

        async = SceneManager.LoadSceneAsync("One");
        //禁止加载完成后自动切换场景
        async.allowSceneActivation = false;


        AddCallBack(4, new System.Action(delegate()
        {
            m_fall = true;
            AddCallBack(6, new System.Action(delegate()
            {
                ToDark.obj.Fade();
                    AddCallBack(2,new System.Action(delegate()
                    {
                        async.allowSceneActivation = true;

                    }));
            }));

        }));

    }

    protected override void FixedUpdateS()
    {
    }

    // Update is called once per frame

}
