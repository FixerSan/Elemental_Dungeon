using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public abstract class BossMovement
{
    protected BossController boss;
    public abstract bool CheckFollow();
    public abstract void FollowTarget();
    public void LookAtTarget()
    {
        if (Vector2.Distance(boss.trans.position, boss.targetTrans.position) > 0.5f)
            boss.LookAtTarget();
    }
}

namespace BossMovements
{
    public class IceBoss : BossMovement
    {
        public IceBoss(BossController _boss)
        {
            boss = _boss;
        }

        public override bool CheckFollow()
        {
            if (boss.targetTrans != null && Mathf.Abs(boss.targetTrans.position.x - boss.trans.position.x) > boss.attack.canAttackDistance)
            {
                boss.ChangeState(BossState.FOLLOW);
                return true;
            }
            boss.ChangeState(BossState.IDLE);
            return false;
        }

        public override void FollowTarget()
        {
            LookAtTarget();
            boss.rb.velocity = new Vector2(boss.status.currentWalkSpeed * Time.fixedDeltaTime * 10 * (int)boss.direction, boss.rb.velocity.y);
        }
    }
}
