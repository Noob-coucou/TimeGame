using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveStory :Interactive 
    {
    [SerializeField]
    private Flowchart flowchart;
    [SerializeField]
    private string Storyblock;
    private bool Seen;
    
    private void Start()
    {
        Seen= false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Seen)
        {
            flowchart.ExecuteBlock(Storyblock);
            Seen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
    protected override void Interact()
    {
        
    }
}
