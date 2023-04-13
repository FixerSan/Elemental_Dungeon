using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemIcon;
    public TextMeshProUGUI itemCountText;

    public void UpdateSlotUI()
    {
        itemIcon.color = Color.white;
        itemIcon.sprite = Resources.Load<Sprite>(item.itemImagePath);
        itemIcon.gameObject.SetActive(true);
        if(item.count > 1)
        {
            itemCountText.text = "" + item.count;
            itemCountText.transform.gameObject.SetActive(true);
        }
    }
}
