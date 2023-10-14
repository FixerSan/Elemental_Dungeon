using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    protected bool isCanUse;

    protected virtual void Awake()
    {
        isCanUse = false;
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
        if (collision.CompareTag("Player"))
        {
            isCanUse = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCanUse = false;
        }
    }

    private void Update()
    {
        CheckUse();
    }
}
