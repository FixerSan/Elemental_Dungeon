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

    public List<ItemData> items = new List<ItemData>();
    // Start is called before the first frame update
    void Start()
    {
        SlotCount = 24;
    }


    public bool AddItem(int index)
    {
        foreach (var item in items)
        {
            if (item.itemID == index && item.count < item.maxCount)
            {
                item.count++;
                onChangeItem.Invoke(index);
                return true;
            }
        }

        if (SlotCount > items.Count)
        {
            ItemData item_ = new ItemData(index);
            items.Add(item_);
            onChangeItem.Invoke(index);
            return true;
        }

        else
            return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

}
