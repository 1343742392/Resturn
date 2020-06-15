using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCamera : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 m_point = Vector3.zero;
    [SerializeField] Transform m_obj = null;
    void Start()
    {
        m_point = transform.position - m_obj.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(m_obj == null)return;
        transform.position = m_obj.position + m_point;
    }
}
