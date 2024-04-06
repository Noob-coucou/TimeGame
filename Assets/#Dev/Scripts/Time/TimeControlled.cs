using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeControlled : MonoBehaviour
{
    [System.Serializable]
    public struct TimeState
    {
        public float time; // 时间点
        public bool state; // 在该时间点的状态
        public Vector3 position;
    }
    [SerializeField]
    private TimeState initialState;

    public List<TimeState> TimeStates;

    private int currentStateIndex = 0;
    
    protected virtual void Start()
    {
        TimeStates.Insert(0,initialState);
        TimeStates = TimeStates.OrderBy(ts => ts.time).ToList();
        UpdateState();
    }
    public virtual void OnTimeUpdate()
    {
        if (TimeStates.Count <= 1) 
            return;

        // 回到上一个状态
        if (TimeController.GameTime < TimeStates[currentStateIndex].time)
        {
            while (currentStateIndex > 0 && TimeController.GameTime < TimeStates[currentStateIndex].time)
            {
                currentStateIndex--;
            }
           
        }
        // 前进至下一个状态
        else if (currentStateIndex < TimeStates.Count - 1 && TimeController.GameTime >= TimeStates[currentStateIndex + 1].time)
        {
            while (currentStateIndex < TimeStates.Count - 1 && TimeController.GameTime >= TimeStates[currentStateIndex + 1].time)
            {
                currentStateIndex++;
            }
            
        }
        UpdateState();
        InterpolatePosition();
    }
    
    //* 应用当前状态
    private void UpdateState()
    {
           gameObject.SetActive(TimeStates[currentStateIndex].state);
    }
    //* Function：计算物体在两个状态之间的位置并更新
    private void InterpolatePosition()
    {
        TimeState previousState = currentStateIndex > 0 ? TimeStates[currentStateIndex - 1] : TimeStates[currentStateIndex];
        TimeState nextState = currentStateIndex < TimeStates.Count - 1 ? TimeStates[currentStateIndex + 1] : TimeStates[currentStateIndex];
    
        float lerpFactor = 0f;
        if (nextState.time != previousState.time)
        {
            lerpFactor = (TimeController.GameTime - previousState.time) / (nextState.time - previousState.time);
        }

        Vector3 interpolatedPosition = Vector3.Lerp(previousState.position, nextState.position, lerpFactor);
        transform.position = interpolatedPosition;
    }
}


