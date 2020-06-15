using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CossDoor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_one = null;
    [SerializeField] GameObject m_two = null;
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("run");
        if(collider.name != Tag.Ellen)
            return ;
        var point = collider.gameObject.transform.position - transform.position;
        
        if(name == "One")
        {
            collider.gameObject.transform.position = m_two.transform.position + m_two.transform.forward * 2 + point;
        }
        else
        {
            collider.gameObject.transform.position = m_one.transform.position+ m_one.transform.forward * 2 + point;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
