using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UISlot_Item : MonoBehaviour
{
    public BaseItem item;
    public Image Image
    {
        get
        {
            if (image == null)
                image = transform.GetOrAddComponent<Image>();
            return image;
        }
    }
    private Image image;

    public void DrawSlot(BaseItem _item)
    {
        item = _item;
        if (item == null)
        {
            Image.sprite = null;
            Image.color = new Color(0,0,0,0);
            return;
        }

        Managers.Resource.Load<Sprite>(item.itemData.itemImageKey, (_sprite) => 
        {
            Image.sprite = _sprite;
            Image.color = new Color(255, 255, 255, 1);
        });
    }
}
