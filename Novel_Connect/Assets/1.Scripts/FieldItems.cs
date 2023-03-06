using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer spriteRenderer;

    public void SetItem(Item _item)
    {
        item.itemID = _item.itemID;
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemtType = _item.itemtType;
        

        spriteRenderer.sprite = item.itemImage;
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

}
