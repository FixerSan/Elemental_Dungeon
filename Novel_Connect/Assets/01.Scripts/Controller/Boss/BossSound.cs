using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossSound
{
    protected BossController boss;
}

namespace BossSounds
{
    public class IceBoss : BossSound
    {
        public IceBoss(BossController _boss)
        {
            boss = _boss;
        }
    }
}
