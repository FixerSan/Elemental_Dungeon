using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    protected bool isCanUse;
    protected bool isUsed;
    protected SpriteRenderer guideSprite;
    protected virtual void Awake()
    {
        isCanUse = false;
        guideSprite = Util.FindChild<SpriteRenderer>(gameObject, "GuideSprite");
        if(guideSprite != null)
        {
            guideSprite.transform.eulerAngles = Vector3.zero;
            guideSprite.gameObject.SetActive(false);
        }
    }

    protected virtual void CheckUse()
    {
        if (!isCanUse) return;
        Managers.Input.CheckInput(Managers.Input.interactionKey, (_inputType) =>
        {
            if (_inputType != InputType.PRESS) return;
            Use();
        });
    }

    protected abstract void Use();

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUsed) return;
        if (collision.CompareTag("Player"))
        {
            isCanUse = true;
            guideSprite.gameObject.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (isUsed) return;
        if (collision.CompareTag("Player"))
        {
            isCanUse = false;
            guideSprite.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        CheckUse();
    }
}
