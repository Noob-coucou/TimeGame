using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;
using Fungus;

public abstract class Interactive : MonoBehaviour
{
    private bool isPlayerNear = false;
    private Player player; // 用于存储玩家对象的引用
    [SerializeField]
    private GameObject interactPrompt;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 确保进入的是玩家
        {
            isPlayerNear = true;
            player = other.GetComponent<Player>(); // 获取Player组件的引用
            interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            player = null;
            interactPrompt.SetActive(false);
        }
    }

    void Update()
    {
        // 玩家靠近且按下了定义的输入按键时进行交互
        if (isPlayerNear && player != null && player.input.PlayerBasic.Interact.WasPressedThisFrame())
        {
            Interact();
            interactPrompt.SetActive(false);
        }
    }


    // 交互逻辑
    protected abstract void Interact();

}
