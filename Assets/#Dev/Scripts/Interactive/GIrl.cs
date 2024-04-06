using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIrl : Interactive
{
    [SerializeField]
    private Flowchart flowchart;
    [SerializeField]
    private string Storyblock;
    private bool Seen;

    private void Start()
    {
        Seen = false;
    }
    protected override void Interact()
    {
        if (!Seen)
        {
            flowchart.ExecuteBlock(Storyblock);
            Seen = true;
            TimeController.GameClear = true;
        }
        
        
    }
}
