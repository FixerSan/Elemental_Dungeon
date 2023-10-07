using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;

public abstract class PlayerAttack
{
    protected PlayerController player;
    protected Coroutine attackCoroutine;
    protected Coroutine initAttackCountCoroutine;
    protected int currentAttackCount;
    protected int maxAttackCount;
    protected int InitAttackCountDelay;

    public virtual void CheckAttack()
    {
        Managers.Input.CheckInput(Managers.Input.attackKey, (_inputType) => 
        {
        if (_inputType == InputType.HOLD)
            {
                player.ChangeState(PlayerState.ATTACK);
            }
        });
    }

    public virtual void StartAttack()
    {
        if (maxAttackCount > currentAttackCount) currentAttackCount++;
        else currentAttackCount = 1;
        player.animator.SetInteger("AttackCount", currentAttackCount);
        attackCoroutine = Managers.Routine.StartCoroutine(AttackRoutine());
        player.sound.PlayAttackSound(currentAttackCount - 1);
    }

    public abstract void Attack();
    protected virtual IEnumerator AttackRoutine()
    {
        if(initAttackCountCoroutine != null)    Managers.Routine.StopCoroutine(initAttackCountCoroutine);
        yield return new WaitForSeconds(0.1f);
        float animationTime = player.animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationTime - 0.1f);
        player.ChangeState(PlayerState.IDLE);
        initAttackCountCoroutine = Managers.Routine.StartCoroutine(InitAttackCountRoutine());
    }

    protected IEnumerator InitAttackCountRoutine()
    {
        yield return new WaitForSeconds(InitAttackCountDelay);
        currentAttackCount = 0;
        player.animator.SetInteger("AttackCount", currentAttackCount);
    }
}

namespace PlayerAttacks
{
    public class Normal : PlayerAttack
    {
        public Normal(PlayerController _player)
        {
            player = _player;
            attackCoroutine = null;
            initAttackCountCoroutine = null;
            currentAttackCount = 0;
            maxAttackCount = 4;
            InitAttackCountDelay = 1;
        }

        public override void Attack()
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(player.attackTrans.position, player.attackTrans.localScale, 0, player.attackLayer);
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                if (collider2Ds[i].CompareTag("Player")) continue;

                if (collider2Ds[i].TryGetComponent(out BaseController hitController))
                    Managers.Battle.DamageCalculate(player, hitController, player.status.currentAttackForce);
            }
        }
    }

    public class Fire : PlayerAttack
    {
        public Fire(PlayerController _player)
        {
            player = _player;
            attackCoroutine = null;
            initAttackCountCoroutine = null;
            currentAttackCount = 0;
            maxAttackCount = 4;
            InitAttackCountDelay = 1;
        }

        public override void Attack()
        {
            Collider2D[] collider2Ds_1 = Physics2D.OverlapBoxAll(player.attackTrans.position, player.attackTrans.localScale, 0, player.attackLayer);
            foreach (Collider2D hitTarget in collider2Ds_1)
            {
                if (hitTarget.CompareTag("Player")) continue;
                BaseController hitController = hitTarget.GetComponent<BaseController>();
                if (hitController != null)
                {
                    Managers.Battle.DamageCalculate(player, hitController, player.status.currentAttackForce);
                    Managers.Battle.SetStatusEffect(player, hitController, StatusEffect.BURN);
                }
            }
        }
    }
}
