using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterAttack
{
    public float canAttackDistance;
    public abstract void Attack();
}

namespace MonsterAttacks
{

    public class BaseAttack : MonsterAttack
    {
        private MonsterController monster;
        public BaseAttack(MonsterController _monster)
        {
            monster = _monster;
        }

        public override void Attack()
        {

        }
    }
    public class Ghost_Bat : MonsterAttack
    {
        private MonsterController monster;
        public Ghost_Bat(MonsterController _monster)
        {
            monster = _monster;
            canAttackDistance = 1;
        }

        public override void Attack()
        {

        }

    }
}
