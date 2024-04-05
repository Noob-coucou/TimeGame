using Cinemachine;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public Player player;
    public static float GameTime;
    TimeControlled[] timeObjects;
    [SerializeField]
    public float maxTimePoint;
    [SerializeField]
    public float minTimePoint;
    [SerializeField]
    public float shakeThreshold;
    public CinemachineImpulseSource impulseSource;

    private bool shakeWhere = true;
    // Start is called before the first frame update
    private void Awake()
    {
        timeObjects=GameObject.FindObjectsOfType<TimeControlled>();
    }
    void Start()
    {
        GameTime = 0f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        impulseSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineImpulseSource>();
    }  
    void Update()
    {
        if (player != null)
        {
            //Z轴距离决定时间
            float distance =player.transform.position.z- transform.position.z;
            // 更新GameTime
            GameTime = distance;
            //Debug.Log(GameTime);
            float maxDistance = Mathf.Abs(GameTime - maxTimePoint);
            float minDistance = Mathf.Abs(GameTime - minTimePoint);
            float threshold = Mathf.Min(maxDistance, minDistance);

            if (threshold < shakeThreshold)
            {
                float strength = (1f - (threshold / shakeThreshold)) * 0.25f;

                Vector3 shakeDirection = shakeWhere ? new Vector3(-strength, 0f, 0f) : new Vector3(strength, 0f, 0f);
                impulseSource.GenerateImpulse(shakeDirection);

                shakeWhere = !shakeWhere;
            }
            foreach (TimeControlled timeObject in timeObjects)
            {
                //Debug.Log("timeObjects" + timeObject); 
                timeObject.OnTimeUpdate();
            }


        }

    }


}
