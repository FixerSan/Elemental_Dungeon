using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill_One_After : MonoBehaviour
{
    public Transform size;
    public void Boom()
    {
        Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Skill_Ice_One_After);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(size.position, size.localScale.x);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
                Managers.Object.Player.status.StartFreeze();
        }
    }

    public void AnimationEvent_End()
    {
        Managers.Resource.Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(size.position, size.localScale.x);
    }
}
