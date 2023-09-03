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
        init = false;
        changeStateCoroutine = null;
        trans = gameObject.GetOrAddComponent<Transform>();
        trans.GetOrAddComponent<SpriteRenderer>();
        animator = trans.GetOrAddComponent<Animator>();
        rb = trans.GetOrAddComponent<Rigidbody2D>();
        attackTrans = Util.FindChild<Transform>(gameObject, "AttackTrans");
        detecteTrans = Util.FindChild<Transform>(gameObject, "DetecteTrans");
        targetTras = null;
        attackLayer = LayerMask.GetMask("Hitable");
        Managers.Data.GetMonsterData(_monsterUID, (_data) =>
        {
            data = _data;
            status.currentHP = _data.hp;
            status.maxHP = _data.hp;
            status.maxWalkSpeed = _data.speed;
            status.currentWalkSpeed = _data.speed;
            status.currentAttackForce = _data.force;
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
                ChangeDirection(Define.Direction.Left);
                init = true; 
            });
        });
    }


    public override void Hit(Transform _attackerTrans,float _damage)
    {
        if (state == MonsterState.Die) return;
        SetTarget(_attackerTrans);
        LookAtTarget();
        KnuckBack();
        GetDamage(_damage);
        if (state == MonsterState.Attack) return;
        ChangeState(MonsterState.Damaged,true);
    }

    public override void GetDamage(float _damage)
    {
        status.currentHP -= _damage;
    }
    public void CheckDie()
    {
        if(status.currentHP <= 0)
            ChangeState(MonsterState.Die);
    }

    public override void Die()
    {
        if(changeStateCoroutine != null) Managers.Routine.StopCoroutine(changeStateCoroutine);
        changeStateCoroutine = null;
        if(attack.attackCoroutine != null) Managers.Routine.StopCoroutine(attack.attackCoroutine);
        attack.attackCoroutine = null;
        init = false;
        Managers.Routine.StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(3f);
        Managers.Pool.Push(gameObject);
    }

    public override void SetPosition(Vector2 _position)
    {

    }

    public void SetTarget(Transform _target)
    {
        targetTras = _target;
    }

    public void ChangeState(MonsterState _state, bool _isChangeSameState = false)
    {
        if (!init) return;
        if (state == _state)
        {
            if (_isChangeSameState)
                stateMachine.ChangeState(states[_state]);
            return;
        }
        stateMachine.ChangeState(states[_state]);
    }

    public void ChangeStateWithAnimtionTime(MonsterState _nextState)
    {
        changeStateCoroutine = Managers.Routine.StartCoroutine(ChangeStateWithAnimtionTimeRoutine(_nextState));
    }

    private IEnumerator ChangeStateWithAnimtionTimeRoutine(MonsterState _nextState)
    {
        yield return new WaitForSeconds(0.05f);
        float animationPlayTime = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationPlayTime - 0.05f);
        ChangeState(_nextState);
    }

    public void LookAtTarget()
    {
        if (targetTras == null) return;
        if (targetTras.position.x > trans.position.x) ChangeDirection(Define.Direction.Right);
        if (targetTras.position.x < trans.position.x) ChangeDirection(Define.Direction.Left);
    }

    public override void KnuckBack()
    {
        if (direction == Define.Direction.Right) rb.AddForce(new Vector2(-data.knockBackForce, data.knockBackForce * 0.5f), ForceMode2D.Impulse);
        if (direction == Define.Direction.Left) rb.AddForce(new Vector2(data.knockBackForce, data.knockBackForce * 0.5f), ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (!init) return;
        stateMachine.UpdateState();
        CheckDie();
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
    public float knockBackForce;
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