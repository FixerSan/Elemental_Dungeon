using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterStatus
{
    public class Burns : Status<MonsterV2>
    {
        public override void Enter(MonsterV2 entity)
        {
            if (isUsing)
                entity.StartCoroutine(entity.Burns(damage));
        }

        public override void Exit(MonsterV2 entity)
        {
            entity.StopCoroutine(entity.Burns(damage));

        }

        public override void Update(MonsterV2 entity)
        {
            if (isUsing)
            {
                CheckDuration();
                Debug.Log(checkTime);
                if(!isUsing)
                {
                    Exit(entity);
                    //entity.statusMachine.ExitStatus(this);
                }
            }
        }
    }

    public class D : Status<MonsterV2>
    {
        public override void Enter(MonsterV2 entity)
        {

        }

        public override void Exit(MonsterV2 entity)
        {

        }

        public override void Update(MonsterV2 entity)
        {
            
        }
    }
}