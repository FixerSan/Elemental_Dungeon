using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemV2 
{
    public ItemData itemData;
    public abstract void UseItem();
    public ItemV2(int itemID)
    {
        itemData = new ItemData(itemID);
    }
}
