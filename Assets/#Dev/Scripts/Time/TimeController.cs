using Cinemachine;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public Player player;
    public static float GameTime;
    TimeControlled[] timeObjects;
    [SerializeField]
    private float maxTimePoint;
    [SerializeField]
    private float minTimePoint;
    [SerializeField]
    private float shakeThreshold;
    [SerializeField]
    private float strengthnumber;
    
    public CinemachineImpulseSource impulseSource;

    public bool GamePlaying;
    public Flowchart flowchart;
    public static bool GameClear;

    private bool shakeWhere = true;
    // Start is called before the first frame update
    private void Awake()
    {
        GamePlaying = true;
        timeObjects=GameObject.FindObjectsOfType<TimeControlled>();
    }
    void Start()
    {
        GameClear= false; 
        GameTime = 0f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        impulseSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineImpulseSource>();
    }  
    void Update()
    {
        if (player != null && GamePlaying)
        {


            if ((GameTime >= maxTimePoint || GameTime <= minTimePoint) && !GameClear)
            {
                // 重新加载当前场景，重置游戏状态
                flowchart.ExecuteBlock("Reload");
                GamePlaying = false; 
            }
            //玩家与远点距离决定时间
            float xDistance = player.transform.position.x - transform.position.x;
            float zDistance = player.transform.position.z - transform.position.z;
            float radialDistance = Mathf.Sqrt(xDistance * xDistance + zDistance * zDistance);
            GameTime = radialDistance * Mathf.Sign(zDistance);
            //Debug.Log(GameTime);
            float maxDistance = Mathf.Abs(GameTime - maxTimePoint);
            float minDistance = Mathf.Abs(GameTime - minTimePoint);
            float threshold = Mathf.Min(maxDistance, minDistance);

            if (threshold < shakeThreshold && !GameClear)
            {
                float strength = (1f - (threshold / shakeThreshold)) * strengthnumber;

                Vector3 shakeDirection = shakeWhere ? new Vector3(-strength, 0f, 0f) : new Vector3(strength, 0f, 0f);
                impulseSource.GenerateImpulse(shakeDirection);

                shakeWhere = !shakeWhere;
            }
            foreach (TimeControlled timeObject in timeObjects)
            {
                if (timeObject == null || timeObject.Equals(null))
                {
                    continue;
                }
                //Debug.Log("timeObjects" + timeObject); 
                timeObject.OnTimeUpdate();
            }


        }
        else
        {
            return;
        }

    }


}
