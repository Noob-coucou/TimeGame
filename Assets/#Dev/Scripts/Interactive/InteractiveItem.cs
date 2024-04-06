using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItem : Interactive
{
    [SerializeField]
    private Flowchart flowchart;
    [SerializeField]
    private string Storyblock;
    protected override void Interact()
    {
        flowchart.ExecuteBlock(Storyblock);
        TimeController.GameClear = true;
        Destroy(gameObject);

    }
}
