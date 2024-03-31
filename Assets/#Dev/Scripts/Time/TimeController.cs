using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public Player player;
    public float GameTime;
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
      
    }
    public void ChangeCurrentTime(Vector3 playerPostion)
    { 
        playerPostion= playerPostion.normalized;

    }

}
