using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStartSkill : MonoBehaviour
{
    public float size;
    public LayerMask layerMask;
    public void Start()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, size, layerMask);

        foreach (var coll in collider2Ds)
        {
            Actor actor = null;
            if (coll.CompareTag("Monster"))
                actor = coll.GetComponent<Actor>();

            if (actor != null)
            {
                actor.SetTarget(GameManager.instance.player.gameObject);
                BattleSystem.instance.HitCalculate(Elemental.Fire, actor.elemental, actor, GameManager.instance.player.statuses.force);
                BattleSystem.instance.SetStatusEffect(actor, StatusEffect.Burns, 5);
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, size);
    }
}
