using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField]protected SpriteRenderer spriteRenderer;
    protected bool isUsed = false;
    protected bool isCanUse = false;
    public bool isRuning = true;

    public abstract void Interaction();

    public virtual void Awake()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }
    void Update()
    {
        if (!isRuning) return;
        if (Input.GetKeyDown(KeyCode.F) && !isUsed && isCanUse)
        {
            Interaction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isRuning) return;
        if (collision.CompareTag("Player"))
        {
            if (spriteRenderer != null && !isUsed && isCanUse)
            {
                spriteRenderer.enabled = true;
            }
            isCanUse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isRuning) return;
        if (collision.CompareTag("Player"))
        {
            if (spriteRenderer != null && !isUsed)
            {
                spriteRenderer.enabled = false;
            }
            isCanUse = false;
        }
    }
}
