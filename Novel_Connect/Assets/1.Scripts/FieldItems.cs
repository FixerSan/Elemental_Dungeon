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
        item.itemImagePath = _item.itemImagePath;
        item.itemtType = _item.itemtType;
        

        spriteRenderer.sprite = Resources.Load<Sprite>(item.itemImagePath);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Inventory.instance.AddItem(item.itemID))
                DestroyItem();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (Inventory.instance.AddItem(item.itemID))
                DestroyItem();
        }
    }

    public void DestroyItem()
    {
        //if(item.itemID == 1000001)
        //{
        //    CutSceneManager.instance.transform.GetComponent<Tutorial>().StartCoroutine(CutSceneManager.instance.transform.GetComponent<Tutorial>().Tutorial_9());
        //}
        Destroy(gameObject);
    }

}
