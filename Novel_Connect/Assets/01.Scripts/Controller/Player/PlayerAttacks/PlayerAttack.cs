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
        if(Input.GetKey(Managers.Input.attackKey))
        {
            player.ChangeState(PlayerState.Attack);
        }
    }

    public virtual void StartAttack()
    {
        if (maxAttackCount > currentAttackCount) currentAttackCount++;
        else currentAttackCount = 1;
        player.animator.SetInteger("AttackCount", currentAttackCount);
        attackCoroutine = Managers.Routine.StartCoroutine(AttackRoutine());
    }

    protected abstract void Attack();
    protected virtual IEnumerator AttackRoutine()
    {
        if(initAttackCountCoroutine != null)    Managers.Routine.StopCoroutine(initAttackCountCoroutine);
        yield return new WaitForSeconds(0.05f);
        float animationTime = player.animator.GetCurrentAnimatorStateInfo(0).length;
        switch (currentAttackCount)
        {
            case 1:
                yield return new WaitForSeconds(0.18f - 0.05f);
                Attack();
                yield return new WaitForSeconds(animationTime - 0.18f);
                break;

            case 2:
                yield return new WaitForSeconds(0.14f - 0.05f);
                Attack();
                yield return new WaitForSeconds(0.49f - 0.14f);
                Attack();
                yield return new WaitForSeconds(animationTime - 0.49f);
                break;

            case 3:
                yield return new WaitForSeconds(0.57f - 0.05f);
                Attack();
                yield return new WaitForSeconds(animationTime - 0.57f);
                break;

            case 4:
                yield return new WaitForSeconds(0.57f - 0.05f);
                Attack();
                yield return new WaitForSeconds(animationTime - 0.57f);
                break;
        }

        player.ChangeState(PlayerState.Idle);
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

        ~Normal()
        {
            if(attackCoroutine != null)
                Managers.Routine.StopCoroutine(attackCoroutine);

            if (initAttackCountCoroutine != null)
                Managers.Routine.StopCoroutine(initAttackCountCoroutine);
        }

        protected override void Attack()
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(player.attackTrans.position, player.attackTrans.localScale, 0, player.attackLayer);
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                if (collider2Ds[i].CompareTag("Player")) continue;

                if (collider2Ds[i].TryGetComponent(out BaseController hitController))
                    Managers.Battle.DamageCalculate(player, hitController);
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

        ~Fire()
        {
            if (attackCoroutine != null)
                Managers.Routine.StopCoroutine(attackCoroutine);

            if (initAttackCountCoroutine != null)
                Managers.Routine.StopCoroutine(initAttackCountCoroutine);
        }

        protected override void Attack()
        {
            Collider2D[] collider2Ds_1 = Physics2D.OverlapBoxAll(player.attackTrans.position, player.attackTrans.localScale, 0, player.attackLayer);
            foreach (Collider2D hitTarget in collider2Ds_1)
            {
                if (hitTarget.CompareTag("Player")) continue;
                BaseController hitController = hitTarget.GetComponent<BaseController>();
                if (hitController != null)
                {
                    Managers.Battle.DamageCalculate(player, hitController);
                    Managers.Battle.SetStatusEffect(hitController, StatusEffect.Burn, 5);
                }
            }
        }
    }
}
