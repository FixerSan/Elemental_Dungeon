using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseController
{
    public BossData data;
    public BossMovement movement;
    public BossAttack attack;
    public BossSound sound;
    public BossState state;
    public LayerMask attackLayer;
    private StateMachine<BossController> stateMachine;
    private Dictionary<BossState, State<BossController>> states;
    private bool init = false;
    public void Init(int _bossUID)
    {
        init = true;
    }

    public override void Die()
    {

    }

    public override void GetDamage(float _damage)
    {

    }

    public override void Hit(Transform attackTrans, float _damage)
    {

    }

    public override void KnockBack()
    {

    }

    public override void KnockBack(float _force)
    {

    }

    public override void SetPosition(Vector2 _position)
    {

    }

    private void Update()
    {
        if (!init) return;
        stateMachine.UpdateState();
    }
}
public enum BossState { }

[System.Serializable]
public class BossData
{
    public int      bossUID;
    public string   bossCodeName;
    public string   bossName;
    public string   description;
    public int      exp;
    public float    hp;
    public float    force;
    public float    speed;
    public float    attackDelay;
    public float    canAttackDistance;
    public float    knockBackForce;
    public string   elemental;
    public float    skill_OneCooltime;
    public float    skill_TwoCooltime;
    public float    skill_UltCooltime;
    public BossData(int _bossUID)
    {
        Managers.Data.GetBossData(_bossUID, (data) =>
        {
            bossUID = data.bossUID;
            bossCodeName = data.bossCodeName;
            bossName = data.bossName;
            description = data.description;
            exp = data.exp;
            hp = data.hp;
            force = data.force;
            speed = data.speed;
            attackDelay = data.attackDelay;
            canAttackDistance = data.canAttackDistance;
            knockBackForce = data.knockBackForce;
            elemental = data.elemental;
            skill_OneCooltime = data.skill_OneCooltime;
            skill_TwoCooltime = data.skill_TwoCooltime;
            skill_UltCooltime = data.skill_UltCooltime  ;
        });
    }
}