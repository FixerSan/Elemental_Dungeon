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
        inventory = FindObjectOfType<InventoryV2>();
        RedrawGold();
        inventory.onGetItem += CheckItemUI;
        inventory.OnChangeGold += RedrawGold;
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    

        slots = slotHeader.GetComponentsInChildren<BaseSlot>();

    }
    private void OnDisable()
    {
        inventory.onGetItem -= CheckItemUI;
        inventory.OnChangeGold -= RedrawGold;
    }
    #endregion
    [SerializeField] private Transform slotHeader;
    private InventoryV2 inventory;
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

    public void RedrawGold()
    {
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
