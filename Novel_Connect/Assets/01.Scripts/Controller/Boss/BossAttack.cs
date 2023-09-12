using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack 
{
    protected BossController boss;
    public float            canAttackDelay;
    public float            attackDelay;
    public float            canAttackDistance;
    public Coroutine        attackCoroutine;
}

namespace BossAttacks
{
    public class IceBoss : BossAttack
    {
        public IceBoss(BossController _boss)
        {
            boss = _boss;
            attackCoroutine = null;
        }
    }
}
