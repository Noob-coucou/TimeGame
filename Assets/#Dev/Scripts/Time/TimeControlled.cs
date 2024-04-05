using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeControlled : MonoBehaviour
{
    [System.Serializable]
    public struct TimeState
    {
        public float time; // ʱ���
        public bool state; // �ڸ�ʱ����״̬
        public Vector3 position;
    }
    [SerializeField]
    private bool initialState;
    [SerializeField]
    private List<TimeState> TimeStates;
    private int currentStateIndex = 0;
    
    protected virtual void Start()
    {
        TimeState initialTimeState = new TimeState { time = 0f, state = initialState };
        TimeStates.Insert(0, initialTimeState);
        TimeStates = TimeStates.OrderBy(ts => ts.time).ToList();
    }
    public virtual void OnTimeUpdate()
    {
        if (TimeStates.Count <= 1) return;

        // �ص���һ��״̬
        if (TimeController.GameTime < TimeStates[currentStateIndex].time)
        {
            while (currentStateIndex > 0 && TimeController.GameTime < TimeStates[currentStateIndex].time)
            {
                currentStateIndex--;
            }
            UpdateState();
        }
        // ǰ������һ��״̬
        else if (currentStateIndex < TimeStates.Count - 1 && TimeController.GameTime >= TimeStates[currentStateIndex + 1].time)
        {
            while (currentStateIndex < TimeStates.Count - 1 && TimeController.GameTime >= TimeStates[currentStateIndex + 1].time)
            {
                currentStateIndex++;
            }
            UpdateState();
        }
    
    }

    private void UpdateState()
    {
        gameObject.SetActive(TimeStates[currentStateIndex].state);
        // �����Ҫ��Ҳ���Ը���λ��
        // transform.position = TimeStates[currentStateIndex].position;
    }

}


