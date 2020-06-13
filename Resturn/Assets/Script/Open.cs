using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Open : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        if(PlayerPrefs.HasKey("Open"))
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            PlayerPrefs.SetInt("Open", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Zero");
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
