using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    Inventory inventory;

    public GameObject inventoryPanel;
    bool activeInventory = false;

    public Slot[] slots;
    public Transform slotHolder;
    private void Start()
    {
        inventory = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        // GetComponentsInChildren�� ��ǥ�ȿ� �� �ִ� �ڽĵ��� <T> ������
        // ������ ���� ��� ������ �ִ� �ڽĵ��� <T>���� ���������� �ִ� �Լ�

        inventoryPanel.SetActive(activeInventory);
        inventory.onChangeItem += RedrawSlotUI;
    }

    public void RedrawSlotUI(int itemIndex)
    {
        for(int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].item = inventory.items[i];
            slots[i].UpdateSlotUI();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }
}
