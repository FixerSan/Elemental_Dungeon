using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableObject
{
    public int chestIndex;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interaction()
    {
        isUsed = true;
        animator.SetTrigger("Open");
        ChestSystem.Instance.ChestEvent(chestIndex, this.transform);
    }
}
