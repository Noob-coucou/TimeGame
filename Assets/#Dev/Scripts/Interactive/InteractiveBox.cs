using Fungus;
using System.Collections;
using System.Collections.Generic;
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
        flowchart = GameObject.FindObjectOfType<Flowchart>();

        if (PlayerPrefs.GetInt(GetPrefKey(), 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    protected override void Interact()
    {
        if (PlayerPrefs.GetInt(GetPrefKey(), 0) == 1)
            return;

        flowchart.ExecuteBlock("GetTreasure");

        PlayerPrefs.SetInt(GetPrefKey(), 1);
        PlayerPrefs.Save();

        Destroy(gameObject);
    }
}

