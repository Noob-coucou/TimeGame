using Fungus;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
public class InteractiveBox : Interactive
{
    [SerializeField]
    private Flowchart flowchart;

    public string boxID;

    private string GetPrefKey()
    {
        return "InteractiveBoxGotten_" + boxID;
    }

    void Start()
    {

        if (PlayerPrefs.GetInt(GetPrefKey(), 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    protected override void Interact()
    {
        if (PlayerPrefs.GetInt(GetPrefKey(), 0) == 1)
            return;
        StartCoroutine(ExecuteAndDestroyBlock("GetTreasure"));
    }
    private IEnumerator ExecuteAndDestroyBlock(string blockName)
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

        PlayerPrefs.SetInt(GetPrefKey(), 1);
        PlayerPrefs.Save();

        Destroy(gameObject);
    }
}

