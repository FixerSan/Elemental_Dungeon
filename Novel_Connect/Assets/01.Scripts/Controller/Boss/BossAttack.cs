using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack 
{
    protected BossController boss;
}

namespace BossAttacks
{
    public class IceBoss : BossSound
    {
        public IceBoss(BossController _boss)
        {
            boss = _boss;
        }
    }
}
