using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : BaseMonster
{
    float soundCheckTime;
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
        AudioSystem.Instance.PlayOneShotSoundProfile("Slime_Move");
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

    public void Update()
    {
        CheckMoveSoundDuration();
    }

    public void CheckMoveSoundDuration()
    {
        if (Vector2.Distance(GameManager.instance.player.transform.position, transform.position) > 10f) return;
        if (state == MonsterState.Patrol)
        {
            if (soundCheckTime == 0)
            {
                AudioSystem.Instance.PlayOneShotSoundProfile("Slime_Move");
            }
            soundCheckTime += Time.deltaTime;
            if (soundCheckTime > 0.57f)
            {
                soundCheckTime = 0;
            }
        }

        else if (soundCheckTime != 0)
        {
            soundCheckTime = 0;
        }
    }
}
