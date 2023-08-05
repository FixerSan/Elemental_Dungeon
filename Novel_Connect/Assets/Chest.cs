using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableObject
{
    public int chestIndex;
    public ChestType type;
    Animator animator;

    public override void Awake()
    {
        base.Awake();
        isRuning = true;
        animator = GetComponent<Animator>();
    }

    public override void Interaction()
    {
        isUsed = true;
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        animator.SetTrigger("Open");
        if(type == ChestType.Positive) AudioSystem.Instance.PlayOneShotSoundProfile("Chest", 0);
        if(type == ChestType.Negative) AudioSystem.Instance.PlayOneShotSoundProfile("Chest", 1);

        ChestSystem.Instance.ChestEvent(chestIndex, this.transform);
    }
}

public enum ChestType { Positive , Negative }
