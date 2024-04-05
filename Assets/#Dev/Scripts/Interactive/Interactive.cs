using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;
using Fungus;

public abstract class Interactive : MonoBehaviour
{
    private bool isPlayerNear = false;
    private Player player; // ���ڴ洢��Ҷ��������
    [SerializeField]
    private GameObject interactPrompt;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ȷ������������
        {
            isPlayerNear = true;
            player = other.GetComponent<Player>(); // ��ȡPlayer���������
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
        // ��ҿ����Ұ����˶�������밴��ʱ���н���
        if (isPlayerNear && player != null && player.input.PlayerBasic.Interact.WasPressedThisFrame())
        {
            Interact();
            interactPrompt.SetActive(false);
        }
    }


    // �����߼�
    protected abstract void Interact();

}
