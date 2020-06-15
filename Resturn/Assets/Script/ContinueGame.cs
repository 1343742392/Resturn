using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueGame : MonoBehaviour
{
    // Start is called before the first frame update
    int lv = 1;
    void Start()
    {
            if(PlayerPrefs.HasKey("LV"))
            {
                lv = PlayerPrefs.GetInt("LV");
            }
            else
            {
                GetComponent<Button>().interactable = false;
            }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        SceneManager.LoadScene(lv);
    }
}
