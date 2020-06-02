using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
 * 1.挂在一个对象上 这个对象是场景里唯一的
 * 2.添加接受消息的脚步 touchtarget 
 * 3.控制器获取消息 getCompent<touchtarget>().OnMove = 
 */
public class TouchEvent : MonoBehaviour
{
    [HideInInspector]
    static public TouchEvent obj = null;
    [HideInInspector]
    public List<TouchTarget> targets = new List<TouchTarget>();
    private LogDisplay log = null;

    private void Awake()
    {
        obj = this;
    }
    void Start()
    {
        log = GameObject.Find("LogDisplay")?.GetComponent<LogDisplay>();
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
           /* if(Input.GetKeyDown(KeyCode.W))
            {
                var t = new Touch { fingerId = 99, phase = TouchPhase.Began, position = new Vector2(300, 300) };
                Debug.Log(Input.mousePosition);
                Switch(t);
            }

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.Q))
            {
                var t = new Touch { fingerId = 99, phase = TouchPhase.Moved, position = new Vector2(300, 1000) };
                Switch(t);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                var t = new Touch { fingerId = 99, phase = TouchPhase.Moved, position = new Vector2(300, 600) };
                Switch(t);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                var t = new Touch { fingerId = 99, phase = TouchPhase.Ended, position =  new Vector2(300, 600) };
                //Debug.Log(Input.mousePosition);
                Switch(t);
            }*/

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var t = new Touch { fingerId = 1, phase = TouchPhase.Began, position = Input.mousePosition };
                Switch(t);
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                var t = new Touch { fingerId = 1, phase = TouchPhase.Moved, position = Input.mousePosition };
                Switch(t);
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                var t = new Touch { fingerId = 1, phase = TouchPhase.Ended, position = Input.mousePosition };
                Switch(t);
            }
        } else if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                var touchs = Input.touches;
                for (int i = 0; i < touchs.Length; i++)
                {
                    var t = touchs[i];

                    //if (t.fingerId == 1) log?.AddLog("eu" + touchs.Length + " id " + t.fingerId + "pos" + t.position);
                    Switch(t);

                }
            }
        }

    }

    private void Switch(Touch t)
    {
        var touchTargets = TouchTargets(t.position);
        if (t.phase.Equals(TouchPhase.Began))
        {
            touchTargets.ForEach(tag =>
            {
                tag.StartTouch(t.position, t.fingerId);
            });
        }
        else if (t.phase.Equals(TouchPhase.Ended) || t.phase.Equals(TouchPhase.Canceled))
        {
            targets.ForEach(tag =>
            {
                if(tag.fingerId == t.fingerId)
                {
                    tag.EndTouch(t.position, -1);
                }
            });
        }else if(t.phase.Equals(TouchPhase.Moved))
        {
            targets.ForEach(tag =>
            {
                //if (t.fingerId == 1) LogDisplay.obj.AddLog("em" + t.fingerId + "  " + t.position);
                if (tag.fingerId == t.fingerId)
                    tag.MoveTouch(t.position);
            });
        }
    }

    private List<TouchTarget> TouchTargets(Vector2 point)
    {
        List<TouchTarget> res = new List<TouchTarget>();
        targets.ForEach(tag =>
        {
            var tf = tag.GetComponent<RectTransform>();
            if (InRect(tf, point))
            {
                res.Add(tag);
            }
        });
        //if(res.Count > 0) LogDisplay.obj.AddLog("targets" + res.Count + " point " + point +" id " + res[0].fingerId );
        return res;
    }

    private bool InRect(RectTransform tf, Vector2 point)
    {
        var rect = tf.rect;
        //统一锚点
        var pos = new Vector3(tf.position.x - 0.5f * rect.width,
                              tf.position.y - 0.5f * rect.height);
        //Debug.Log(pos.x + "    " + point.x);
        if (pos.x < point.x && point.x < pos.x + rect.width)
            if (pos.y < point.y && point.y < pos.y + rect.height)
                return true;
        return false;
    }
}
