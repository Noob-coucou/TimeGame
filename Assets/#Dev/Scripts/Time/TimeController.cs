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
            //Z��������ʱ��
            float distance =player.transform.position.z- transform.position.z;
            // ����GameTime
            GameTime = distance;
            //Debug.Log(GameTime);
           
        }
        foreach (TimeControlled timeObject in timeObjects)
        {
           //Debug.Log("timeObjects" + timeObject); 
           timeObject.OnTimeUpdate();
        }
    }
    public void ChangeCurrentTime(Vector3 playerPostion)
    { 
        playerPostion= playerPostion.normalized;

    }

    void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 24; // �����ı���СΪ24

        Rect rect = new Rect(10, 10, 200, 20);

        GUI.Label(rect, "CurrentTime: " + GameTime.ToString("0"),guiStyle);
    }

}
