using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public abstract class Interactive : MonoBehaviour
{

    private bool isPlayerNear = false;
    private Player player; // ���ڴ洢��Ҷ��������

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ȷ������������
        {
            isPlayerNear = true;
            player = other.GetComponent<Player>(); // ��ȡPlayer���������
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            player = null; 
        }
    }

    void Update()
    {
        // ��ҿ����Ұ����˶�������밴��ʱ���н���
        if (isPlayerNear && player != null && player.input.PlayerBasic.Interact.WasPressedThisFrame())
        {
            Interact();
        }
    }


    // �����߼�
    protected abstract void Interact();

}
