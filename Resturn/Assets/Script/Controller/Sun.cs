using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    static public Sun sun = null;
    private Light light = null;
    // Start is called before the first frame update
    private void Awake()
    {
        sun = this;
    }
    void Start()
    {
        light = GetComponent<Light>();
        if(light!=null)light.enabled = false;
    }

    public void SW()
    {
        if(light != null)
        {
            light.enabled = !light.enabled;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
