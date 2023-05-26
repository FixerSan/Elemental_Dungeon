using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopInventory : MonoBehaviour
{
    Inventory inventory;
    public TextMeshProUGUI moneyText;
    public Slot[] slots;

    public void RedrawSlotUI(int item)
    {
        inventory = Inventory.instance;

        slots = GetComponentsInChildren<Slot>();
        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].item.item = inventory.items[i];
            slots[i].UpdateSlotUI();
        }
    }

    private void Start()
    {
        inventory = Inventory.instance;
        inventory.onChangeItem += RedrawSlotUI;
    }

    private void FixedUpdate()
    {
        if (moneyText.text != "" + inventory.money)
            moneyText.text = "" + inventory.money;

    }



}
