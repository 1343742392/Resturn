using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Success : TaskBehavior
{
    // Start is called before the first frame update
    bool once = true;
    public static AsyncOperation async;
    private void OnCollisionEnter(Collision collision)
    {
        foreach(var c in collision.contacts)
        {
            var obj = c.otherCollider.gameObject;
            if (obj.name.Equals(Tag.Ellen))
            {
                if (once == false) return;
                once = false;

                GameObject.FindWithTag(Tag.Input)?.SetActive(false);
                GameObject.FindWithTag(Tag.Compass)?.SetActive(false);
                ToDark.obj.Fade();

                var canvas = GameObject.FindWithTag(Tag.canvas);
                var loading = Instantiate(Resources.Load<GameObject>("Profab/Loading"));
                loading.transform.SetParent(canvas.transform);
                loading.GetComponent<RectTransform>().localPosition = Vector3.zero;
                obj.GetComponent<Character>().MoveAble(false);
                obj.GetComponent<Animator>().SetFloat("Blend", 0);

                AddCallBack(2, new System.Action(delegate(){
                    async.allowSceneActivation = true;

                }));
                PlayerPrefs.SetInt("LV", SceneManager.GetActiveScene().buildIndex + 1);
                PlayerPrefs.Save();
            }
        }
    }

    void Start()
    {
        async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        //禁止加载完成后自动切换场景
        async.allowSceneActivation = false;
    }

    protected override void UpdateS()
    {
    }

    protected override void StartS()
    {
    }

    protected override void FixedUpdateS()
    {
    }
}
