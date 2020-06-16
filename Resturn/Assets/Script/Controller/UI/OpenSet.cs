using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_setPage = null;
    void Start()
    {
        
    }

    public void OnClick()
    {
        m_setPage?.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
