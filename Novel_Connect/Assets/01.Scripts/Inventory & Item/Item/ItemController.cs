using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.UIElements;

public class ItemController : MonoBehaviour
{
    private bool isPushed;
    private bool isInit;
    public float moveSpeed;
    public float pickupSpeed;
    private float yPos;
    private float time;
    public BaseItem item;
    private SpriteRenderer spriteRenderer;

    public void Init(BaseItem _item)
    {
        isInit = false;
        spriteRenderer = Util.FindChild<SpriteRenderer>(gameObject);
        item = _item;
        Managers.Resource.Load<Sprite>(_item.itemData.itemImageKey, (_sprite) => 
        {
            spriteRenderer.sprite = _sprite;
        });
        isInit = true;
        isPushed = false;
    }

    private void Update()
    {
        if (!isInit) return;
        Movement();
    }

    private void Movement()
    {
        time += Time.deltaTime;
        yPos = Mathf.Cos(time * 1.5f) * moveSpeed;
        spriteRenderer.transform.position += new Vector3(0, yPos, 0);
    }

    public void PutInInventory(Transform _trans)
    {
        if (isPushed) return;
        isPushed = true;
        Managers.Object.Player.inventory.AddItem<BaseItem>(item.itemData.itemUID);
        transform.DOJump(_trans.position, 1, 1, pickupSpeed);
        spriteRenderer.material.DOFade(0, pickupSpeed).onComplete += () => { Managers.Resource.Destroy(gameObject); };
    }

    public void OnDisable()
    {
        
    }
}
