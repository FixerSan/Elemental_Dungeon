using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class MonsterMovement
{
    public abstract void CheckMove();
    public abstract void Follow();

}

namespace MonsterMovements
{
    public class BaseMovement : MonsterMovement
    {
        private MonsterController monster;
        private Coroutine checkDetecteCoroutine;
        public BaseMovement(MonsterController _monster)
        {
            monster = _monster;
            checkDetecteCoroutine = null;
        }

        public override void CheckMove()
        {
            
        }

        public override void Follow()
        {
            monster.LookAtTarget();
        }
    }

    public class Ghost_Bat : MonsterMovement
    {
        private MonsterController monster;
        private Coroutine checkDetecteCoroutine;
        public Ghost_Bat(MonsterController _monster)
        {
            monster= _monster;
            checkDetecteCoroutine = null;
        }
        public override void CheckMove()
        {
            Debug.Log("체크중");
            if (checkDetecteCoroutine != null) return;
            checkDetecteCoroutine = Managers.Routine.StartCoroutine(CheckDetecteRoutine());
        }

        private IEnumerator CheckDetecteRoutine()
        {
            Debug.Log("체크 코루틴 들어옴");
            Collider2D[] colliders = Physics2D.OverlapBoxAll(monster.attackTrans.position,monster.attackTrans.localScale,0,monster.attackLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("Player"))
                {
                    monster.ChangeState(MonsterState.Follow);
                    monster.SetTarget(colliders[i].transform);
                }
            }
            yield return new WaitForSeconds(0.5f);
            checkDetecteCoroutine = null;
        }

        public override void Follow()
        {
            monster.LookAtTarget();
            if(Mathf.Abs(monster.trans.position.x - monster.targetTras.position.x) < monster.attack.canAttackDistance)
            {
                monster.ChangeState(MonsterState.Attack);
                return;
            }

            if (monster.direction == Define.Direction.Left)
                monster.rb.velocity = new Vector2(-monster.status.currentSpeed / 6, monster.rb.velocity.y);
            if (monster.direction == Define.Direction.Right)
                monster.rb.velocity = new Vector2(monster.status.currentSpeed / 6, monster.rb.velocity.y);

        }
    }
}

