using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : InteractableObject
{
    public int sceneEventIndex;
    private bool isUsed;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
    protected override void Use()
    {
        animator.SetBool("IsOpened", true);
        Managers.scene.GetScene<IceDungeonScene>().SceneEvent(sceneEventIndex);
    }
}
