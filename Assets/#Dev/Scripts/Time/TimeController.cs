using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public Player player;
    public static float GameTime;
    TimeControlled[] timeObjects;

    // Start is called before the first frame update
    private void Awake()
    {
        timeObjects=GameObject.FindObjectsOfType<TimeControlled>();
    }
    void Start()
    {
        GameTime = 0f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

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

            foreach (TimeControlled timeObject in timeObjects)
            {
                //Debug.Log("timeObjects" + timeObject); 
                timeObject.OnTimeUpdate();
            }
        }

    }

    void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 24;
        if (GameTime < 0)
        {
            guiStyle.normal.textColor = Color.white;
        }
        else
        {
            guiStyle.normal.textColor = Color.black;
        }

        Rect rect = new Rect(10, 10, 200, 20);

        GUI.Label(rect, "CurrentTime: " + GameTime.ToString("0"), guiStyle);
    }

}
