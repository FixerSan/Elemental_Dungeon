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
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);

        slots = slotHeader.GetComponentsInChildren<Slot>();
        inventory.onGetItem += CheckItemUI;
    }
    #endregion
    [SerializeField] private InventoryV2 inventory;
    [SerializeField] private Transform slotHeader;

    private Slot[] slots;

    private void OnDisable()
    {
        inventory.onGetItem -= CheckItemUI;
    }

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

    public void UseItem(int itemID)
    {
        inventory.UseItem(itemID);
    }

    public void UseItem(ItemData item)
    {
        inventory.UseItem(item);
    }
}
