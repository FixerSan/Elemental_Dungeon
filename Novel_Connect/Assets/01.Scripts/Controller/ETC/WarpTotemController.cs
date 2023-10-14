using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTotemController : InteractableObject
{
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void Use()
    {
        animator.SetBool("isUse", true);
        Managers.Routine.StartCoroutine(UseRoutine());
    }

    private IEnumerator UseRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        float animationTime = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationTime - 0.1f);
        Managers.scene.GetScene<IceDungeonScene>().SceneEvent(0);
    }
}
