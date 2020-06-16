using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
        public static AsyncOperation async;
    // Start is called before the first frame update
    private void Start()
    {

    }
    public void Click()
    {
        var back =  GameObject.Find("LoadingBack")?.GetComponent<Image>();
        back.enabled = true;
        var lad = GameObject.Find("Loading");
        lad.GetComponent<Image>().enabled = true;
        lad.GetComponent<Animator>().enabled = true;
        Invoke("Run", 2);
                async = SceneManager.LoadSceneAsync("One");
        //禁止加载完成后自动切换场景
        async.allowSceneActivation = false;
    }

    void Run()
    {
        Debug.Log(async.progress);
        if(async.progress >= 0.9f)
            async.allowSceneActivation = true;
        else 
            Invoke("Run", 2);

    }
}
