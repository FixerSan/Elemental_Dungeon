using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerAttack
{
    public PlayerControllerV3 player;
    public Transform attackPos;
    public Vector2 attackSize;
    public LayerMask attackLayer;
    public float canAttackDuration;
    public int attackCount;

    public IEnumerator attackCoroutine;
    IEnumerator AttackCoroutine()
    {
        //공격 단계에 따른 다른 공격 기능
        switch (attackCount)
        {
            case 1: //1 타 공격 
                yield return new WaitForSeconds(0.2f);
                Collider2D[] collider2Ds_1 = Physics2D.OverlapBoxAll(attackPos.position, attackSize, 0, attackLayer);
                foreach (Collider2D hitTarget in collider2Ds_1)
                {
                    if (hitTarget.GetComponent<IHitable>() != null)
                    {
                        BattleSystem.instance.Calculate(player.elemental, hitTarget.GetComponent<IHitable>().GetElemental(), hitTarget.GetComponent<Actor>(), player.playerData.force);
                        switch (player.elemental)
                        {
                            case Elemental.Fire:
                                if (hitTarget.GetComponent<IStatusEffect>() != null)
                                    BattleSystem.instance.SetStatusEffect(hitTarget.GetComponent<IStatusEffect>(), StatusEffect.Burns, 5, player.playerData.force * 0.1f);
                                break;
                        }
                    }
                }
                yield return new WaitForSeconds(0.2f);
                break;

            case 2://2 타 공격
                yield return new WaitForSeconds(0.1f);
                Collider2D[] collider2Ds_2 = Physics2D.OverlapBoxAll(attackPos.position, attackSize, 0, attackLayer);
                foreach (Collider2D hitTarget in collider2Ds_2)
                {
                    if (hitTarget.GetComponent<IHitable>() != null)
                    {
                        BattleSystem.instance.Calculate(player.elemental, hitTarget.GetComponent<IHitable>().GetElemental(), hitTarget.GetComponent<Actor>(), player.playerData.force);
                        switch (player.elemental)
                        {
                            case Elemental.Fire:
                                if (hitTarget.GetComponent<IStatusEffect>() != null)
                                    BattleSystem.instance.SetStatusEffect(hitTarget.GetComponent<IStatusEffect>(), StatusEffect.Burns, 5, player.playerData.force * 0.1f);
                                break;
                        }
                    }
                }
                yield return new WaitForSeconds(0.3f);
                break;

            case 3: //3타 공격
                yield return new WaitForSeconds(0.3f);
                Collider2D[] collider2Ds_3 = Physics2D.OverlapBoxAll(attackPos.position, attackSize, 0, attackLayer);
                foreach (Collider2D hitTarget in collider2Ds_3)
                {
                    if (hitTarget.GetComponent<IHitable>() != null)
                    {
                        BattleSystem.instance.Calculate(player.elemental, hitTarget.GetComponent<IHitable>().GetElemental(), hitTarget.GetComponent<Actor>(), player.playerData.force);
                        switch (player.elemental)
                        {
                            case Elemental.Fire:
                                if (hitTarget.GetComponent<IStatusEffect>() != null)
                                    BattleSystem.instance.SetStatusEffect(hitTarget.GetComponent<IStatusEffect>(), StatusEffect.Burns, 5, player.playerData.force * 0.1f);
                                break;
                        }
                    }
                }
                yield return new WaitForSeconds(0.2f);
                break;
        }
        player.ChangeState(PlayerState.Idle);

        //설정한 공격 단계 초기화 시간 만큼 기다린 후 공격 단계 초기화
        yield return new WaitForSeconds(canAttackDuration);
        attackCount = 0;
    }

    public void Setup(PlayerControllerV3 player_)
    {
        player = player_;
    }
}
