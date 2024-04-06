using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InteractiveMonster : Interactive
{
    [SerializeField]
    private Flowchart flowchart;
    protected override void Interact()
    {
        StartCoroutine(Battle("Battle"));
    }
    private IEnumerator Battle(string blockName)
    {
        Block targetBlock = flowchart.FindBlock(blockName);

        if (targetBlock != null)
        {
            flowchart.ExecuteBlock(targetBlock);

            // 等待块执行完成
            while (targetBlock.IsExecuting())
            {
                yield return null;
            }
        }
        else
        {
            Debug.LogError("Block not found: " + blockName);
        }


        Destroy(gameObject);
    }
}
