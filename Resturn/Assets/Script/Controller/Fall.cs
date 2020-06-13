using System.Collections;
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
        var ca = obj.GetComponent<CharacterAudio>();
        var c = obj.GetComponent<Character>();
        if (c != null && ca != null)
        {
            c.Fall();
            ca.Fall();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
