using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemtType
{
    Equipment,
    Consumables,
    Etc
}

[System.Serializable]
public class Item 
// Start is called before the first frame update
{
    public int itemID;
    public ItemtType itemtType;
    public string itemName;
    public Sprite itemImage;
    public int count = 1;
}
