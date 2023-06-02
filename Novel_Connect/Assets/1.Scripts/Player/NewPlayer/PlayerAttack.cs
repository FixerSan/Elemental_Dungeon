using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerAttack
{
    public PlayerControllerV3 player;
    public Transform attackPos;
    public LayerMask attackLayer;
    public float canAttackDuration;

    public int attackCount;

    public Coroutine attackCoroutine;
    public IEnumerator AttackCoroutine()
    {
        //공격 단계에 따른 다른 공격 기능
        switch (attackCount)
        {
            case 1: //1 타 공격 
                yield return new WaitForSeconds(0.18f);
                Collider2D[] collider2Ds_1 = Physics2D.OverlapBoxAll(attackPos.position, attackPos.localScale, 0, attackLayer);
                foreach (Collider2D hitTarget in collider2Ds_1)
                {
                    Actor hitActor = hitTarget.GetComponent<Actor>();
                    if (hitActor != null)
                    {
                        BattleSystem.instance.Calculate(player.elemental, hitActor.elemental, hitActor, player.statuses.force);
                        hitActor.SetTarget(player.gameObject);
                        switch (player.elemental)
                        {
                            case Elemental.Fire:
                                    BattleSystem.instance.SetStatusEffect(hitActor, StatusEffect.Burns, 5);
                                break;
                        }
                    }
                }
                yield return new WaitForSeconds(0.3f);
                break;

            case 2://2 타 공격
                yield return new WaitForSeconds(0.21f);
                Collider2D[] collider2Ds_2 = Physics2D.OverlapBoxAll(attackPos.position, attackPos.localScale, 0, attackLayer);
                foreach (Collider2D hitTarget in collider2Ds_2)
                {
                    Actor hitActor = hitTarget.GetComponent<Actor>();
                    if (hitActor != null)
                    {
                        BattleSystem.instance.Calculate(player.elemental, hitActor.elemental, hitActor, player.statuses.force);
                        hitActor.SetTarget(player.gameObject);
                        switch (player.elemental)
                        {
                            case Elemental.Fire:
                                if (hitTarget.GetComponent<IStatusEffect>() != null)
                                    BattleSystem.instance.SetStatusEffect(hitActor, StatusEffect.Burns, 5);
                                break;
                        }
                    }
                }
                yield return new WaitForSeconds(0.35f);
                foreach (Collider2D hitTarget in collider2Ds_2)
                {
                    Actor hitActor = hitTarget.GetComponent<Actor>();
                    if (hitActor != null)
                    {
                        BattleSystem.instance.Calculate(player.elemental, hitActor.elemental, hitActor, player.statuses.force);
                        hitActor.SetTarget(player.gameObject);
                        switch (player.elemental)
                        {
                            case Elemental.Fire:
                                if (hitTarget.GetComponent<IStatusEffect>() != null)
                                    BattleSystem.instance.SetStatusEffect(hitActor, StatusEffect.Burns, 5);
                                break;
                        }
                    }
                }
                yield return new WaitForSeconds(0.35f);
                break;

            case 3: //3타 공격
                yield return new WaitForSeconds(0.84f);
                Collider2D[] collider2Ds_3 = Physics2D.OverlapBoxAll(attackPos.position, attackPos.localScale, 0, attackLayer);
                foreach (Collider2D hitTarget in collider2Ds_3)
                {
                    Actor hitActor = hitTarget.GetComponent<Actor>();
                    if (hitActor != null)
                    {
                        BattleSystem.instance.Calculate(player.elemental, hitActor.elemental, hitActor, player.statuses.force);
                        hitActor.SetTarget(player.gameObject);
                        switch (player.elemental)
                        {
                            case Elemental.Fire:
                                if (hitTarget.GetComponent<IStatusEffect>() != null)
                                    BattleSystem.instance.SetStatusEffect(hitActor, StatusEffect.Burns, 5);
                                break;
                        }
                    }
                }
                yield return new WaitForSeconds(0.35f);
                break;

            case 4: //3타 공격
                yield return new WaitForSeconds(0.84f);
                Collider2D[] collider2Ds_4 = Physics2D.OverlapBoxAll(attackPos.position, attackPos.localScale, 0, attackLayer);
                foreach (Collider2D hitTarget in collider2Ds_4)
                {
                    Actor hitActor = hitTarget.GetComponent<Actor>();
                    if (hitActor != null)
                    {
                        BattleSystem.instance.Calculate(player.elemental, hitActor.elemental, hitActor, player.statuses.force);
                        hitActor.SetTarget(player.gameObject);
                        switch (player.elemental)
                        {
                            case Elemental.Fire:
                                if (hitTarget.GetComponent<IStatusEffect>() != null)
                                    BattleSystem.instance.SetStatusEffect(hitActor, StatusEffect.Burns, 5);
                                break;
                        }
                    }
                }
                yield return new WaitForSeconds(0.66f);
                break;
        }
        player.ChangeState(PlayerState.Idle);

        //설정한 공격 단계 초기화 시간 만큼 기다린 후 공격 단계 초기화
        yield return new WaitForSeconds(canAttackDuration);
        attackCoroutine = null;
        attackCount = 0;
        player.anim.SetInteger("AttackCount", attackCount);
    }

    public void Setup(PlayerControllerV3 player_)
    {
        player = player_;
    }
}
