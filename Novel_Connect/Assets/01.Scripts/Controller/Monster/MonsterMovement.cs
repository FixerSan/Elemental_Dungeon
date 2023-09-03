using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class MonsterMovement
{
    protected MonsterController monster;
    protected Coroutine checkDetecteCoroutine;
    public abstract void CheckMove();
    public void StopCheckMove()
    {
        if(checkDetecteCoroutine != null)
        {
            Managers.Routine.StopCoroutine(checkDetecteCoroutine);
            checkDetecteCoroutine = null;
        }
    }
    public abstract void Move();
    public void LookAtTarget()
    {
        if(Vector2.Distance(monster.trans.position, monster.targetTras.position) > 0.5f)
            monster.LookAtTarget();
    }

    public void KnockBack()
    {

    }

}

namespace MonsterMovements
{
    public class BaseMovement : MonsterMovement
    {
        public BaseMovement(MonsterController _monster)
        {
            monster = _monster;
            checkDetecteCoroutine = null;
        }

        public override void CheckMove()
        {
            
        }


        public override void Move()
        {

        }
    }

    public class Ghost_Bat : MonsterMovement
    {
        public Ghost_Bat(MonsterController _monster)
        {
            monster= _monster;
            checkDetecteCoroutine = null;
        }

        public override void CheckMove()
        {
            checkDetecteCoroutine = Managers.Routine.StartCoroutine(CheckMoveRoutine());
        }

        private IEnumerator CheckMoveRoutine()
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(monster.detecteTrans.position,monster.detecteTrans.localScale,0,monster.attackLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("Player"))
                {
                    monster.ChangeState(MonsterState.Follow);
                    monster.SetTarget(colliders[i].transform);
                    break;
                }
            }
            yield return new WaitForSeconds(0.5f);
            checkDetecteCoroutine = Managers.Routine.StartCoroutine(CheckMoveRoutine());
        }

        public override void Move()
        {
            monster.rb.velocity = new Vector2(monster.status.currentWalkSpeed * Time.fixedDeltaTime * 10 * (int)monster.direction, monster.rb.velocity.y);
        }
    }
}

