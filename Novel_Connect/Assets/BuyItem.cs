using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    Inventory inventory;
    public Item item;
    public float itemPrice;

    private void Start()
    {
        inventory = Inventory.instance;
    }
    public void Buy()
    {
        if(inventory.money >= itemPrice)
        {
            inventory.AddItem(item);
            inventory.money -= itemPrice;
        }
    }
}
