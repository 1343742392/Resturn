﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        var obj = collider.gameObject;
        var c = obj.GetComponent<CharacterAudio>();
        if (c != null)
        {
            c.Fall();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
