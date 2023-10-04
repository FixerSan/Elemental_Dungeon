using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossMovement
{
    protected BossController boss;
    public abstract bool CheckFollow();
    public abstract void FollowTarget();
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
            if(boss.targetTrans != null)
            {
                boss.ChangeState(BossState.FOLLOW);
                return true;
            }
            return false;
        }


        public override void FollowTarget()
        {
            Debug.Log("따라가는 중");
        }

    }
}
