using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public Player player;
    public List<Interactive> interactives;

    public bool hasInteractive=false;
    public Interactive currentInteractive;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    
}
