using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static TestManager Instance;
    public Player player;
    private float deltaTime = 0.0f;
    [SerializeField]
    private Flowchart flowchart;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (Instance == null)
        {
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGame();
        }
        else if (Instance != this)
        {
        
            Destroy(gameObject);
        }
    }

    void Start()
    {
        flowchart.ExecuteBlock("Init");
    }
    private void InitializeGame()
    {

        ResetAllBoxes();

    }
    private void ResetAllBoxes()
    {
        // 假设你有一个方法来获取所有宝箱的ID
        foreach (var box in GetAllBoxIDs())
        {
            PlayerPrefs.SetInt("InteractiveBoxGotten_" + box, 0);
        }
        PlayerPrefs.Save();
    }
    private List<string> GetAllBoxIDs()
    {
        List<string> boxIDs = new List<string>();
        InteractiveBox[] boxes = FindObjectsOfType<InteractiveBox>();

        foreach (InteractiveBox box in boxes)
        {
            if (box.boxID != null && !boxIDs.Contains(box.boxID))
            {
                boxIDs.Add(box.boxID);
            }
        }

        return boxIDs;
    }
    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = h * 2 / 100;

        // 显示FPS
        float fps = 1.0f / deltaTime;
        string fpsText = string.Format("{0:0.} fps", fps);

        guiStyle.normal.textColor = Color.white;
        Rect fpsRect = new Rect(10, 10, w, h * 2 / 100);
        GUI.Label(fpsRect, fpsText, guiStyle);

        guiStyle.normal.textColor = TimeController.GameTime < 0 ? Color.white : Color.black;
        Rect timeRect = new Rect(10, h * 2 / 100 + 30, w, h * 2 / 100);
        GUI.Label(timeRect, "CurrentTime: " + TimeController.GameTime.ToString("0"), guiStyle);
    }

}
