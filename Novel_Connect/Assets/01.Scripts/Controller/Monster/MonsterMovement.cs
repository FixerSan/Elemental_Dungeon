using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class MonsterMovement
{
    protected Coroutine checkDetecteCoroutine;
    public abstract void CheckMove();
    public abstract void Follow();

}

namespace MonsterMovements
{
    public class BaseMovement : MonsterMovement
    {
        private MonsterController monster;
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
        public Ghost_Bat(MonsterController _monster)
        {
            monster= _monster;
            checkDetecteCoroutine = null;
        }

        public override void CheckMove()
        {
            if (checkDetecteCoroutine != null) return;
            checkDetecteCoroutine = Managers.Routine.StartCoroutine(CheckDetecteRoutine());
        }

        private IEnumerator CheckDetecteRoutine()
        {
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

            if (monster.direction == Define.Direction.Left)
                monster.rb.velocity = new Vector2(-monster.status.currentSpeed / 6, monster.rb.velocity.y);
            if (monster.direction == Define.Direction.Right)
                monster.rb.velocity = new Vector2(monster.status.currentSpeed / 6, monster.rb.velocity.y);

        }
    }
}

