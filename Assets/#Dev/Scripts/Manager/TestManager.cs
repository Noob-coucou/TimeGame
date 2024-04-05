using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public Player player;
    private float deltaTime = 0.0f;

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
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = h * 2 / 100;


        guiStyle.normal.textColor = TimeController.GameTime < 0 ? Color.white : Color.black;
        Rect timeRect = new Rect(10, 10, w, h * 2 / 100);
        GUI.Label(timeRect, "CurrentTime: " + TimeController.GameTime.ToString("0"), guiStyle);

        // 显示FPS
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string fpsText = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);

        guiStyle.normal.textColor = Color.white;
        Rect fpsRect = new Rect(10, h * 2 / 100 + 20, w, h * 2 / 100); // 放在当前时间下方
        GUI.Label(fpsRect, fpsText, guiStyle);
    }
}
