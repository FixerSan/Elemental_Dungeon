using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : InteractableObject
{
    Animator animator => GetComponent<Animator>();
    public int totemIndex;

    public override void Interaction()
    {
        if (!TotemSystem.Instance.CheckCanUse(totemIndex)) return;
        animator.SetTrigger("On");
        isUsed = false;
        TotemSystem.Instance.TotemEvent(totemIndex, this.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCanUse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCanUse = false;
        }
    }
}
