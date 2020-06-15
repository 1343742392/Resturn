using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public class TouchTarget : TaskBehavior
{
    public float m_StartDragLenght = 5;
    [HideInInspector]
    public int fingerId = -1;

    public Action<Vector2> OnStart = new Action<Vector2>(point=>{});
    public Action<Vector2> OnEnd = new Action<Vector2>(point => { });
    public Action<Vector2> OnMove = new Action<Vector2>(point => { });
    public Action<Vector2> OnDragStart = new Action<Vector2>(point => 
    {
        //LogDisplay.obj.AddLog("onDragStart");
    });
    /// <summary>
    /// 现在坐标 拖拽方向 拖拽距离
    /// </summary>
    public Action<Vector2, Vector2, float> OnDragEnd = new Action<Vector2, Vector2, float>(
            delegate(Vector2 point, Vector2 dic, float length) 
            {
                //LogDisplay.obj.AddLog("onDragEnd:" + dic + "   " + length);
            }
        );

    private Vector2 m_DragStartPoint = Vector2.zero;
    private Vector2 m_StartPoint = Vector2.zero;
    private bool m_DragRuning = false;
    private void Start()
    {
/*        tasks.Add(new Action(()=>
        {
            EventSystem.current.gameObject.GetComponent<TouchEvent>().targets.Add(this);
        }));*/
    }
    protected override void UpdateS()
    {
    }

    public void StartTouch(Vector2 point, int id)
    {
        fingerId = id;
        OnStart(point);
        m_StartPoint = point;
    }

    public void MoveTouch(Vector2 point)
    {
        //LogDisplay.obj.AddLog(point.ToString());
        OnMove(point);
        //if(fingerId != 0) LogDisplay.obj.AddLog("onmove" + fingerId + "  pos" + point);
        if (Vector3.Distance(m_StartPoint, point) > m_StartDragLenght && !m_DragRuning)
        {
            OnDragStart(point);
            m_DragStartPoint = point;
            m_DragRuning = true;
        }
    }

    public void EndTouch(Vector2 point, int id)
    {
        fingerId = id;
        OnEnd(point);
        if(m_DragRuning)
        {
            var dragPoint = point - m_DragStartPoint;
            dragPoint.Normalize();
            OnDragEnd(point, (dragPoint).normalized, Vector2.Distance(Vector2.zero, point - m_DragStartPoint));
            m_DragRuning = false;
            //LogDisplay.obj.AddLog(fingerId +  "onDragEnd:" + point + "startp" + m_DragStartPoint);
        }
    }

    protected override void StartS()
    {
        TouchEvent.obj.targets.Add(this);
    }

    protected override void FixedUpdateS()
    {
    }
}
