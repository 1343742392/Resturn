using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("inv", 2, 0.5f);
        Invoke("inv", 2);
    }
    private void inv()
    {

    }
    
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.K))
        {
 
        }
    }
}
