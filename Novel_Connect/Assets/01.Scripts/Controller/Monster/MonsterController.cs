using PlayerStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterController : BaseController
{
    public MonsterData data;
    public MonsterMovement movement;
    public MonsterAttack attack;
    public MonsterState state;
    public Transform attackTrans;
    public Transform detecteTrans;
    public Transform targetTras;
    public LayerMask attackLayer;
    private StateMachine<MonsterController> stateMachine;
    private Dictionary<MonsterState, State<MonsterController>> states;
    private bool init = false;
    public void Init(int _monsterUID)
    {
        trans = gameObject.GetOrAddComponent<Transform>();
        trans.GetOrAddComponent<SpriteRenderer>();
        animator = trans.GetOrAddComponent<Animator>();
        rb = trans.GetOrAddComponent<Rigidbody2D>();
        attackTrans = Util.FindChild<Transform>(gameObject, "AttackTrans");
        detecteTrans = Util.FindChild<Transform>(gameObject, "DetecteTrans");
        attackLayer = LayerMask.GetMask("Hitable");
        Managers.Data.GetMonsterData(_monsterUID, (_data) =>
        {
            data = _data;
            status.currentHP = _data.hp;
            status.maxHP = _data.hp;
            status.maxSpeed = _data.speed;
            status.currentSpeed = _data.speed;
            status.attackForce = _data.force;
            elemental = Util.ParseEnum<Define.Elemental>(_data.elemental);

            switch (_data.monsterUID)
            {
                case 0:
                    movement = new MonsterMovements.Ghost_Bat(this);
                    attack = new MonsterAttacks.BaseAttack(this);
                    attack.attackDelay = _data.attackDelay;
                    attack.canAttackDistance = _data.canAttackDistance;
                    attack.canAttackDelay = _data.canAttackDelay;
                    states = new Dictionary<MonsterState, State<MonsterController>>();
                    states.Add(MonsterState.Idle, new MonsterStates.Ghost_BatIdle());
                    states.Add(MonsterState.Follow, new MonsterStates.Ghost_BatFollow());
                    states.Add(MonsterState.Attack, new MonsterStates.BaseAttack());
                    states.Add(MonsterState.Damaged, new MonsterStates.BaseDamaged());
                    states.Add(MonsterState.Die, new MonsterStates.BaseDie());
                    break;

                default:
                    movement = new MonsterMovements.BaseMovement(this);
                    attack = new MonsterAttacks.BaseAttack(this);
                    attack.canAttackDelay = _data.attackDelay;
                    states = new Dictionary<MonsterState, State<MonsterController>>();
                    states.Add(MonsterState.Idle, new MonsterStates.BaseIdle());
                    states.Add(MonsterState.Move, new MonsterStates.BaseMove());
                    states.Add(MonsterState.Attack, new MonsterStates.BaseAttack());
                    states.Add(MonsterState.Damaged, new MonsterStates.BaseDamaged());
                    states.Add(MonsterState.Die, new MonsterStates.BaseDie());
                    break;
            }
            stateMachine = new StateMachine<MonsterController>(this, states[MonsterState.Idle]);
            Managers.Resource.Load<RuntimeAnimatorController>(_data.monsterCodeName, (ac) => 
            {
                animator.runtimeAnimatorController = ac;
                init = true; 
            });
        });
    }

    public override void GetDamage(float _damage)
    {
        status.currentHP -= _damage;
    }

    public override void Hit(Transform _attackerTrans,float _damage)
    {
        SetTarget(_attackerTrans);
        GetDamage(_damage);
    }

    public override void SetPosition(Vector2 _position)
    {

    }

    public void SetTarget(Transform _target)
    {
        targetTras = _target;
    }

    public void ChangeState(MonsterState _state)
    {
        if (state == _state) return;
        stateMachine.ChangeState(states[_state]);
    }

    public void LookAtTarget()
    {
        if (targetTras == null) return;
        if (targetTras.position.x > trans.position.x) ChangeDirection(Define.Direction.Right);
        if (targetTras.position.x < trans.position.x) ChangeDirection(Define.Direction.Left);
    }

    private void FixedUpdate()
    {
        if (!init) return;
        stateMachine.UpdateState();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(detecteTrans.position, detecteTrans.localScale);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackTrans.position, attackTrans.localScale);
    }

}

public enum MonsterState { Idle, Move, Follow, Attack, Damaged, Die}

[System.Serializable]
public class MonsterData
{
    public int monsterUID;
    public string monsterCodeName;
    public string monsterName;
    public string description;
    public int exp;
    public float hp;
    public float force;
    public float speed;
    public float attackDelay;
    public float canAttackDistance;
    public float canAttackDelay;
    public string elemental;

    public MonsterData(int _monsterUID)
    {
        Managers.Data.GetMonsterData(_monsterUID, (data) =>
        {
            monsterUID = data.monsterUID;
            monsterCodeName = data.monsterCodeName;
            monsterName = data.monsterName;
            description = data.description;
            exp = data.exp;
            hp = data.hp;
            force = data.force;
            speed = data.speed;
            attackDelay = data.attackDelay;
            canAttackDistance = data.canAttackDistance;
            canAttackDelay = data.canAttackDelay;
            elemental = data.elemental;
        });
    }
}