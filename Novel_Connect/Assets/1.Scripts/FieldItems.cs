using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public ItemData item;
    public SpriteRenderer spriteRenderer;

    public void SetItem(ItemData _item)
    {
        item.itemID = _item.itemID;
        item.itemName = _item.itemName;
        item.itemImagePath = _item.itemImagePath;
        item.itemtType = _item.itemtType;
        

        spriteRenderer.sprite = Resources.Load<Sprite>(item.itemImagePath);
    }

    public void Setup(int itemID)
    {
        item = new ItemData(itemID);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.GetComponent<InventoryV2>().AddItem(item.itemID))
                DestroyItem();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (collision.transform.GetComponent<InventoryV2>().AddItem(item.itemID))
                DestroyItem();
        }
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

}
