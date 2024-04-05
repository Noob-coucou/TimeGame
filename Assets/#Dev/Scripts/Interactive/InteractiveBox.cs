using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBox : Interactive
{
    
    void Start()
    {
        
    }
    protected override void Interact()
    {
        Flowchart flowchart = GameObject.FindFirstObjectByType<Flowchart>();
        flowchart.ExecuteBlock("GetTreasure");
        Destroy(gameObject);
    }
    
}
