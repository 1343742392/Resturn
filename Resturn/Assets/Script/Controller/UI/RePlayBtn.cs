using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RePlayBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick()
    {
        var SceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(SceneName);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
