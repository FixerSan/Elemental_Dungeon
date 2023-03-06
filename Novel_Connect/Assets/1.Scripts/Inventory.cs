using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    public TextMeshProUGUI moneyText;

    public delegate void OnChangeItem(int itemIndex);
    public OnChangeItem onChangeItem;

    public int SlotCount;
    public float money = 100000;

    public List<Item> items = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        SlotCount = 24;
    }


    private void FixedUpdate()
    {
        if(moneyText.text != "" + money)
            moneyText.text = "" + money;

    }

    public bool AddItem(Item _itme)
    {
        if(items.Count < SlotCount)
        {
            foreach(Item item in items)
            {
                if(item.itemName == _itme.itemName && item.count < 64)
                {
                    item.count++;
                    if (onChangeItem != null)
                        onChangeItem.Invoke(item.itemID);
                    return true;
                }
            }
            items.Add(_itme);
            if(onChangeItem != null)
                onChangeItem.Invoke(_itme.itemID);
            return true;
        }
        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            FieldItems fieldItems = collision.GetComponent<FieldItems>();
            if (AddItem(fieldItems.GetItem()))
            {
                fieldItems.DestroyItem();
            }
        }
    }

}
