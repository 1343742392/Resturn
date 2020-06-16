using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueGame : MonoBehaviour
{
    // Start is called before the first frame update
    int lv = 1;
    public static AsyncOperation async2;

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
        var back =  GameObject.Find("LoadingBack")?.GetComponent<Image>();
        back.enabled = true;
        var lad = GameObject.Find("Loading");
        lad.GetComponent<Image>().enabled = true;
        lad.GetComponent<Animator>().enabled = true;
        Invoke("Run", 2);
        async2 = SceneManager.LoadSceneAsync(lv);
        //禁止加载完成后自动切换场景
        async2.allowSceneActivation = false;

    }

    void Run()
    {
        if(async2.progress>= 0.9f)
            async2.allowSceneActivation = true;
        else 
            Invoke("Run", 2);

    }
}
