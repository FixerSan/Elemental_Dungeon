using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTotemController : InteractableObject
{
    private Animator animator;
    private bool isUsed;
    public int sceneEventIndex;

    protected override void Awake()
    {
        base.Awake();
        isUsed = false;
        animator = GetComponent<Animator>();
    }

    protected override void Use()
    {
        if (isUsed) return;

        if(!Managers.Object.Player.inventory.RemoveItem(1))
        {
            Managers.UI.ShowToast("토템열쇠가 부족합니다!").SetColor(Color.red);
            return;
        }
        Managers.Sound.PlaySoundEffect(Define.AudioClip_Effect.WarpTotem);
        isUsed = true;
        animator.SetBool("isUse", true);
        Managers.Routine.StartCoroutine(UseRoutine());
    }

    private IEnumerator UseRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        float animationTime = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationTime - 0.1f);
        Managers.Scene.GetScene<IceDungeonScene>().SceneEvent(sceneEventIndex);
    }
}
