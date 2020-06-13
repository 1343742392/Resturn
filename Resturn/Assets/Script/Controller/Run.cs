using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    [SerializeField] float m_speed = 1;
    private Vector3 m_startPos = Vector3.zero;
    public Vector3 m_endPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * m_speed * Time.deltaTime;
        transform.Rotate(0, m_speed* 0.35f * Time.deltaTime, 0);
        /* if (transform.position.z > m_endPos.z)
            transform.position = m_startPos;*/
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(m_endPos, 5f);
    }
}
