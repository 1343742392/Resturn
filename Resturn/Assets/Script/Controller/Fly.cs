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

    private void FixedUpdate()
    {
        Debug.Log(async.progress);
    }

    // Start is called before the first frame update
    void Start()
    {

        async = SceneManager.LoadSceneAsync("One");
        //禁止加载完成后自动切换场景
        async.allowSceneActivation = false;


        SetTime(4);
        back = delegate ()
        {
            m_fall = true;
            SetTime(6);
            back = delegate ()
            {


                ToDark.obj.Fade();
                SetTime(2);
                back = delegate
                {
                    async.allowSceneActivation = true;
                    //Application.LoadLevel("");

                };
            };
        };
    }

    // Update is called once per frame

}
