using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : BaseMonster
{
    public override void Hit(float damage)
    {
        GetDamage(damage);
        if (statuses.currentHp <= 0)
        {
            statuses.currentHp = 0;
            ChangeState(MonsterState.Dead);
            return;
        }
        ChangeState(MonsterState.Hit);
    }

    public override void GetDamage(float damage)
    {
        if (statuses.currentHp <= 0) return;
        AudioSystem.Instance.PlayOneShotSoundProfile("Slime_Hit");
        statuses.currentHp -= damage;
        ObjectPool.instance.GetDamageText(damage, this.transform);
        if (!isHasHpBar)
        {
            isHasHpBar = true;
            ObjectPool.instance.GetHpBar(this);
        }

        if (statuses.currentHp <= 0)
        {
            statuses.currentHp = 0;
            ChangeState(MonsterState.Dead);
            return;
        }
    }
}
