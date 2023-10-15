using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public enum BossState { CREATED, IDLE, FOLLOW, ATTACK, DAMAGED, SKILL_1CAST, SKILL_2CAST, DIE }
public class BossController : BaseController
{
    public BossData data;
    public BossMovement movement;
    public BossAttack attack;
    public BossSound sound;
    public BossState state;
    public Animator effectAnim;
    public Transform attackTrans;
    public LayerMask attackLayer;
    public Transform targetTrans;
    private StateMachine<BossController> stateMachine;
    private Dictionary<BossState, State<BossController>> states;
    private bool init = false;

    public readonly int HASH_MOVE = Animator.StringToHash("isMove");
    public readonly int HASH_DIE = Animator.StringToHash("isDead");
    public readonly int HASH_ATTACK = Animator.StringToHash("isAttack");
    public readonly int HASH_ATTACK_COUNT = Animator.StringToHash("AttackCount");
    public readonly int HASH_SKILL_ONE = Animator.StringToHash("isSkill_One_Cast");
    public readonly int HASH_SKILL_TWO = Animator.StringToHash("isSkill_Two_Cast");

    public void Init(int _bossUID)
    {
        init = false;
        changeStateCoroutine = null;
        trans = gameObject.GetOrAddComponent<Transform>();
        spriteRenderer = trans.GetOrAddComponent<SpriteRenderer>();
        animator = trans.GetOrAddComponent<Animator>();
        effectAnim = Util.FindChild<Animator>(gameObject, _name: "EffectAnim");
        rb = trans.GetOrAddComponent<Rigidbody2D>();
        attackTrans = Util.FindChild<Transform>(gameObject, "AttackTrans");
        targetTrans = null;
        attackLayer = LayerMask.GetMask("Hitable");
            
        Managers.Data.GetBossData(_bossUID, (_data) => 
        {
            data = _data;
            status = new ControllerStatus(this);
            status.currentHP = _data.hp;
            status.maxHP = _data.hp;
            status.maxWalkSpeed = _data.speed;
            status.currentWalkSpeed = _data.speed;
            status.currentAttackForce = _data.force;
            elemental = Util.ParseEnum<Define.Elemental>(_data.elemental);

            switch (_data.bossUID)
            {
                case 0:
                    movement = new BossMovements.IceBoss(this);
                    sound = new BossSounds.IceBoss(this);
                    attack = new BossAttacks.IceBoss(this);
                    attack.attackDelay = _data.attackDelay;
                    attack.canAttackDistance = _data.canAttackDistance;
                    attack.canAttackDelay = _data.attackDelay;
                    states = new Dictionary<BossState, State<BossController>>();
                    states.Add(BossState.CREATED, new BossStates.BaseBoss.Created());
                    states.Add(BossState.FOLLOW, new BossStates.BaseBoss.Follow());
                    states.Add(BossState.ATTACK, new BossStates.BaseBoss.Attack());
                    states.Add(BossState.DAMAGED, new BossStates.BaseBoss.Damaged());
                    states.Add(BossState.DIE, new BossStates.BaseBoss.Die());
                    states.Add(BossState.IDLE, new BossStates.IceBoss.Idle());
                    states.Add(BossState.SKILL_1CAST, new BossStates.IceBoss.Skill_1Cast());
                    states.Add(BossState.SKILL_2CAST, new BossStates.IceBoss.Skill_2Cast());
                    break; 

                default:
                    break;
            }
            stateMachine = new StateMachine<BossController>(this, states[BossState.CREATED]);
            Managers.Resource.Load<RuntimeAnimatorController>(_data.bossCodeName, (ac) =>
            {
                animator.runtimeAnimatorController = ac;
                ChangeDirection(Direction.Left);
                init = true;
            });
        });
    }
    public void ChangeState(BossState _state, bool _isChangeSameState = false)
    {
        if (!init) return;
        if (state == _state)
        {
            if (_isChangeSameState)
                stateMachine.ChangeState(states[_state]);
            return;
        }
        state = _state;
        stateMachine.ChangeState(states[_state]);
    }

    public override void Die()
    {
        init = false;
        if (changeStateCoroutine != null) Managers.Routine.StopCoroutine(changeStateCoroutine);
        changeStateCoroutine = null;
        if (attack.attackCoroutine != null) Managers.Routine.StopCoroutine(attack.attackCoroutine);
        attack.attackCoroutine = null;
        status.StopAllEffect();
        Managers.Event.OnIntEvent(IntEventType.OnDeadBoss, data.bossUID);
    }

    public override void GetDamage(float _damage)
    {
        status.currentHP -= _damage;
        CheckDie();
    }
    public void CheckDie()
    {
        if (status.currentHP <= 0)
            ChangeState(BossState.DIE);
    }

    public override void Hit(Transform _attackTrans, float _damage)
    {
        if (state == BossState.DIE) return;
        sound.PlayHitSound();
        SetTarget(_attackTrans);
        KnockBack();
        GetDamage(_damage);
    }

    public override void KnockBack()
    {

    }

    public override void KnockBack(float _force)
    {

    }

    public override void SetPosition(Vector2 _position)
    {
        trans.position = _position;
    }

    public void SetTarget(Transform _target)
    {
        targetTrans = _target;
    }

    private void Update()
    {
        if (!init) return;
        stateMachine.UpdateState();
    }
    public void LookAtTarget()
    {
        if (targetTrans == null) return;
        if (targetTrans.position.x > trans.position.x) ChangeDirection(Direction.Right);
        if (targetTrans.position.x < trans.position.x) ChangeDirection(Direction.Left);
    }

    public void AnimationEvent_End()
    {
        if(state == BossState.CREATED || state == BossState.SKILL_1CAST || state == BossState.ATTACK || state == BossState.SKILL_2CAST)
            ChangeState(BossState.IDLE);
        if(state == BossState.DIE)
            spriteRenderer.FadeOut(1, () => { Managers.Resource.Destroy(gameObject); });
    }

    public void AnimationEvent_Attack()
    {
        attack.Attack();
    }

    public void AnimationEvent_Skill_Two()
    {
        attack.Skill_Two();
    }

    protected override IEnumerator DieRoutine()
    {
        throw new System.NotImplementedException();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackTrans.position, attackTrans.localScale);
    }

    public override void Freeze()
    {
        throw new System.NotImplementedException();
    }
}

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
            elemental = data.elemental;
            skill_OneCooltime = data.skill_OneCooltime;
            skill_TwoCooltime = data.skill_TwoCooltime;
            skill_UltCooltime = data.skill_UltCooltime  ;
        });
    }
}