using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterAttack
{
    public float canAttackDistance;
    public float attackDelay;
    public float canAttackDelay;
    public bool isCanAttack;
    public Coroutine attackCoroutine;
    protected MonsterController monster;
    public abstract void CheckAttack();
    public abstract void StartAttack();
    public abstract void Attack();
    public abstract IEnumerator AttackRoutine();
}

namespace MonsterAttacks
{

    public class BaseAttack : MonsterAttack
    {
        public BaseAttack(MonsterController _monster)
        {
            monster = _monster;
            isCanAttack = true;
        }
        public override void CheckAttack()
        {
            if (!isCanAttack) return;
            if (Mathf.Abs(monster.trans.position.x - monster.targetTras.position.x) < monster.attack.canAttackDistance)
            {
                monster.ChangeState(MonsterState.Attack);
                return;
            }
        }

        public override void StartAttack()
        {
            isCanAttack = false;
            monster.LookAtTarget();
            attackCoroutine = Managers.Routine.StartCoroutine(AttackRoutine());
        }

        public override void Attack()
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(monster.attackTrans.position, monster.attackTrans.localScale, 0, monster.attackLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("Player"))
                {
                    Managers.Battle.DamageCalculate(monster, Managers.Object.Player);
                    break;
                }
            }
        }

        public override IEnumerator AttackRoutine()
        {
            yield return new WaitForSeconds(0.05f);
            float animationTime = monster.animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(attackDelay - 0.05f);
            Attack();
            yield return new WaitForSeconds(animationTime - attackDelay);
            monster.ChangeState(MonsterState.Follow);
            yield return new WaitForSeconds(canAttackDelay);
            isCanAttack = true;
            attackCoroutine = null;
        }

    }
}
