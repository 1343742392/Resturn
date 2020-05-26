using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    // Start is called before the first frame update
     void Start()
    {
        GetComponent<Light>().enabled = false;

    }
    private void Awake()
    {
    }
}
