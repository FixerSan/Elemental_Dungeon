using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryUI : MonoBehaviour
{

    Inventory inventory;

    public GameObject inventoryPanel;
    public TextMeshProUGUI moneyText;
    bool activeInventory = false;

    public BaseSlot[] slots;
    public Transform slotHolder;

    private void FixedUpdate()
    {
        if (moneyText.text != "" + inventory.money)
            moneyText.text = "" + inventory.money;
    }
    private void Start()
    {
        inventory = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<BaseSlot>();
        // GetComponentsInChildren�� ��ǥ�ȿ� �� �ִ� �ڽĵ��� <T> ������
        // ������ ���� ��� ������ �ִ� �ڽĵ��� <T>���� ���������� �ִ� �Լ�

        inventoryPanel.SetActive(activeInventory);
        inventory.onChangeItem += RedrawSlotUI;
    }

    public void RedrawSlotUI(int itemIndex)
    {
        for(int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].item.item = inventory.items[i];
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
