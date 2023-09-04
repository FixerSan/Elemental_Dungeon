using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossMovement
{
    protected BossController boss;
}

namespace BossMovements
{
    public class IceBoss : BossSound
    {
        public IceBoss(BossController _boss)
        {
            boss = _boss;
        }
    }
}