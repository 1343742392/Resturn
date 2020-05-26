using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : TaskBehavior
{
    bool m_fall = false;
    protected override void UpdateS()
    {
        transform.localPosition += -transform.up * Time.deltaTime * 10;
        if (m_fall)
            transform.Rotate(Time.deltaTime , 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTime(4);
        back = delegate ()
        {
            m_fall = true;
            SetTime(4);
            back = delegate ()
            {
                ToDark.obj.Fade();
                SetTime(2);
                back = delegate
                {
                    Application.LoadLevel("One");

                };
            };
        };
    }

    // Update is called once per frame

}
