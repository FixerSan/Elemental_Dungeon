using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObstacle : Obstacle
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void GetDamage(float damage)
    {
        hp -= damage;
        animator.SetTrigger("HitEffect");
        if (hp <= 0)
        {
            StartCoroutine(DestroyCoroutine());
        }
    }

    IEnumerator DestroyCoroutine()
    {
        animator.SetBool("isUsed", true);
        yield return new WaitForSeconds(0.1f);
        float delay = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(delay - 0.1f);
        if (isUseTriggerEvent) SceneManager.instance.GetCurrentScene().TriggerEffect(triggerIndex);
        Destroy(gameObject);
    }
}
