using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControlled : MonoBehaviour
{
    #region 外部参数
    [System.Serializable]
    public struct TimeState
    {
        public float time; // 时间点
        public bool state; // 在该时间点的状态（true 或 false）
    }
    [SerializeField]
    private bool initialState;
    [SerializeField]
    private List<TimeState> TimeStates;
    #endregion


    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameObject.SetActive(initialState);

    }

    // Update is called once per frame
    void Update()
    {
  
    }
    public virtual void OnTimeUpdate()
    {
        foreach (TimeState timeState in TimeStates)
        {
           
        }
    }

}

