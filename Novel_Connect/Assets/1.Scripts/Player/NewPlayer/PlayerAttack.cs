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

    public GameObject fireStartSkill;

    public List<Skill<PlayerControllerV3>> skills = new List<Skill<PlayerControllerV3>>();

    public int attackCount;

    public Coroutine attackCoroutine;

    public void AttackActor()
    {
        Collider2D[] collider2Ds_1 = Physics2D.OverlapBoxAll(attackPos.position, attackPos.localScale, 0, attackLayer);
        foreach (Collider2D hitTarget in collider2Ds_1)
        {
            if (hitTarget.CompareTag("Player")) continue;
            #region Actor
            Actor hitActor = hitTarget.GetComponent<Actor>();
            if (hitActor != null)
            {
                
                hitActor.SetTarget(player.gameObject);
                BattleSystem.instance.HitCalculate(player.elemental, hitActor.elemental, hitActor, player.statuses.force);
                switch (player.elemental)
                {
                    case Elemental.Fire:
                        BattleSystem.instance.SetStatusEffect(hitActor, StatusEffect.Burns, 5);
                        break;
                }
                continue;
            }
            #endregion
            #region HitableObejct
            HitableObejct hitableObejct = hitTarget.GetComponent<HitableObejct>();
            if (hitableObejct)
            {
                hitableObejct.GetDamage(player.statuses.force);

            }
            #endregion
        }
    }
    public IEnumerator AttackCoroutine()
    {
        //공격 단계에 따른 다른 공격 기능
        switch (attackCount)
        {
            case 1: //1 타 공격 
                yield return new WaitForSeconds(0.18f/1.5f);
                AttackActor();
                yield return new WaitForSeconds(0.3f/1.5f);
                break;

            case 2://1.5f 타 공격
                yield return new WaitForSeconds(0.21f/1.5f);
                AttackActor();
                yield return new WaitForSeconds(0.35f/1.5f);
                AttackActor();
                yield return new WaitForSeconds(0.35f/1.5f);
                break;

            case 3: //3타 공격
                yield return new WaitForSeconds(0.84f/1.5f);
                AttackActor();
                yield return new WaitForSeconds(0.35f/1.5f);
                break;

            case 4: //3타 공격
                yield return new WaitForSeconds(0.84f/1.5f);
                AttackActor();
                yield return new WaitForSeconds(0.66f/1.5f);
                break;
        }
        player.ChangeState(PlayerState.Idle);

        //설정한 공격 단계 초기화 시간 만큼 기다린 후 공격 단계 초기화
        yield return new WaitForSeconds(canAttackDuration);
        attackCoroutine = null;
        attackCount = 0;
        player.anim.SetInteger("AttackCount", attackCount);
    }

    public void UseSkill_1()
    {
        if (!skills[0].isCanUse) return;
        skills[0].Use();
        player.ChangeState(PlayerState.SkillCasting);
        player.playerMovement.Stop();
        player.anim.SetTrigger("UseSkill");
        player.StartCoroutine(UseSkill_1Coroutine());
    }

    public IEnumerator UseSkill_1Coroutine()
    {
        yield return new WaitForSeconds(0.1f);
        float delay = player.anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(delay - 0.1f);
        player.ChangeState(PlayerState.Idle);
    }

    public void UseSkill_2()
    {
        if (!skills[1].isCanUse) return;
        skills[1].Use();
        player.ChangeState(PlayerState.SkillCasting);
        player.playerMovement.Stop();
        player.anim.SetTrigger("UseSkill");
        player.StartCoroutine(UseSkill_2Coroutine());
    }

    public IEnumerator UseSkill_2Coroutine()
    {
        yield return new WaitForSeconds(0.1f);
        float delay = player.anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(delay - 0.1f);
        player.ChangeState(PlayerState.Idle);
    }

    public void Update()
    {
        if(skills.Count > 0)
        {
            foreach (var skill in skills)
            {
                skill.CheckCanUse();
            }
        }
    }
    public void Setup(PlayerControllerV3 player_)
    {
        player = player_;
    }
}
