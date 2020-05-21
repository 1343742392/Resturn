using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLoad : MonoBehaviour
{
    [SerializeField] GameObject obj = null;
    float start = 0;
    public float time = -1;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetTime(float time)
    {
        start = Time.time;
        this.time = time;
    }

    void Update()
    {
        if (time == -1|| obj == null) return;
        if(Time.time - start >=  time)
        {
            time = -1;
            var objClone = Instantiate(obj);
            objClone.transform.position = transform.position;
            objClone.transform.SetParent(this.transform);
        }
    }
}
