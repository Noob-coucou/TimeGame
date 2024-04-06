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
    protected override void Interact()
    {
        flowchart.ExecuteBlock(Storyblock);
        TimeController.GameClear = true;
        
    }
}
