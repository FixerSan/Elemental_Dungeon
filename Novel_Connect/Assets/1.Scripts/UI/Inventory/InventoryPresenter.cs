using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPresenter : MonoBehaviour
{
    #region S
    private static InventoryPresenter Instance;

    public static InventoryPresenter instance
    {
        get
        {
            if (Instance != null)
                return Instance;
            else
                return null;
        }

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
        slots = slotHeader.GetComponentsInChildren<BaseSlot>();
    
        if (!inventory) return;
        RedrawGold();
        inventory.onGetItem += CheckItemUI;
        inventory.onRemoveItem += CheckRemoveItemUI;
        inventory.OnChangeGold += RedrawGold;

    }
    private void OnDisable()
    {
        if (!inventory) return;
        inventory.onGetItem -= CheckItemUI;
        inventory.onRemoveItem -= CheckRemoveItemUI;
        inventory.OnChangeGold -= RedrawGold;
    }
    #endregion
    [SerializeField] private Transform slotHeader;
    private InventoryV2 inventory => FindObjectOfType<InventoryV2>();
    [SerializeField] private TMPro.TextMeshProUGUI goldText;
    private BaseSlot[] slots;


    public void CheckItemUI(int itemID)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].CreateItemUI(itemID);
                break;
            }
        }
    }

    public void CheckRemoveItemUI(int itemID)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null) continue;
            if (slots[i].item.item.itemID == itemID)
            {
                slots[i].RemoveItemUI();
                break;
            }
        }
    }

    public void RedrawGold()
    {
        if (!inventory) return;
        goldText.text = inventory.gold.ToString();
    }

    public void UseItem(int itemID)
    {
        inventory.UseItem(itemID);
    }

    public void UseItem(ItemData item)
    {
        inventory.UseItem(item);
    }
}
