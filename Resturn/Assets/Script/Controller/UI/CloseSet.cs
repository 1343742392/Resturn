using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSet : MonoBehaviour
{

    void Start()
    {
        
    }

    public void OnClick()
    {
        transform.parent.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
