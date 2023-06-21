using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BaseSlot : MonoBehaviour
{
    public ItemUI item;
    public abstract void UpdateSlotUI();
    public abstract void ResetSlot();
    public void CreateItemUI(int itemID)
    {
        item = Instantiate(Resources.Load<GameObject>("Prefabs/ItemUIPrefab")).GetComponent<ItemUI>();
        item.transform.SetParent(transform);
        item.rect.offsetMin = new Vector2(10, 10);
        item.rect.offsetMax = new Vector2(-10, -10);
        item.Setup(itemID, this);
    }

    public void RemoveItemUI()
    {
        Destroy(item.gameObject);
    }
    public void ResetItem()
    {
        item = null;
    }

}
