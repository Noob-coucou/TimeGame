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
    private TimeState initialState;

    public List<TimeState> TimeStates;

    private int currentStateIndex = 0;

    protected virtual void Start()
    {
        TimeStates.Insert(0, initialState);
        TimeStates = TimeStates.OrderBy(ts => ts.time).ToList();
        UpdateState();
    }
    public virtual void OnTimeUpdate()
    {
        if (TimeStates.Count <= 1)
            return;

        // �ص���һ��״̬
        if (TimeController.GameTime < TimeStates[currentStateIndex].time)
        {
            while (currentStateIndex > 0 && TimeController.GameTime < TimeStates[currentStateIndex].time)
            {
                currentStateIndex--;
            }

        }
        // ǰ������һ��״̬
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

    //* Ӧ�õ�ǰ״̬
    private void UpdateState()
    {
        gameObject.SetActive(TimeStates[currentStateIndex].state);
    }
    //* Function����������������״̬֮���λ�ò�����
    private void InterpolatePosition()
    {
        if (TimeStates.Count < 2)
        {
            return;
        }

        if (currentStateIndex >= TimeStates.Count - 1)
        {
            currentStateIndex = TimeStates.Count - 2;
        }

        TimeState previousState = TimeStates[currentStateIndex];
        TimeState nextState = TimeStates[currentStateIndex + 1];

        float lerpFactor = Mathf.Clamp01((TimeController.GameTime - previousState.time) / (nextState.time - previousState.time));

        Vector3 interpolatedPosition = Vector3.Lerp(previousState.position, nextState.position, lerpFactor);
        transform.position = interpolatedPosition;
    }
}

