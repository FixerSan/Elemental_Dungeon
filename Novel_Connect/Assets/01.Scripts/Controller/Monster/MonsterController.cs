using PlayerStates;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;

public class MonsterController : BaseController
{
    public MonsterData data;
    public MonsterMovement movement;
    public MonsterAttack attack;
    public MonsterSound sound;
    public MonsterState state;
    public Transform attackTrans;
    public Transform detecteTrans;
    public Transform targetTrans;
    public LayerMask attackLayer;
    public Animator effectAnim;
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
        effectAnim = Util.FindChild<Animator>(gameObject,"EffectSprite");
        rb = trans.GetOrAddComponent<Rigidbody2D>();
        attackTrans = Util.FindChild<Transform>(gameObject, "AttackTrans");
        detecteTrans = Util.FindChild<Transform>(gameObject, "DetecteTrans");
        targetTrans = null;
        attackLayer = LayerMask.GetMask("Hitable");
        Managers.Data.GetMonsterData(_monsterUID, (_data) =>
        {
            data = _data;
            status = new ControllerStatus(this);
            status.currentHP = _data.hp;
            status.maxHP = _data.hp;
            status.maxWalkSpeed = _data.speed;
            status.currentWalkSpeed = _data.speed;
            status.currentAttackForce = _data.force;
            status.isDead = false;
            elemental = Util.ParseEnum<Elemental>(_data.elemental);

            switch (_data.monsterUID)
            {
                case 0:
                    movement = new MonsterMovements.Ghost_Bat(this);
                    sound = new MonsterSounds.Ghost_Bat(this);
                    attack = new MonsterAttacks.Bat(this);
                    attack.attackDelay = _data.attackDelay;
                    attack.canAttackDistance = _data.canAttackDistance;
                    attack.canAttackDelay = _data.canAttackDelay;
                    states = new Dictionary<MonsterState, State<MonsterController>>();
                    states.Add(MonsterState.IDLE, new MonsterStates.Ghost_Bat.Idle());
                    states.Add(MonsterState.FOLLOW, new MonsterStates.Ghost_Bat.Follow());
                    states.Add(MonsterState.ATTACK, new MonsterStates.Ghost_Bat.Attack());
                    states.Add(MonsterState.DAMAGED, new MonsterStates.BaseMonster.Damaged());
                    states.Add(MonsterState.DIE , new MonsterStates.BaseMonster.Die());
                    break;

                default:
                    movement = new MonsterMovements.BaseMovement(this);
                    attack = new MonsterAttacks.BaseAttack(this);
                    attack.canAttackDelay = _data.attackDelay;
                    states = new Dictionary<MonsterState, State<MonsterController>>();
                    states.Add(MonsterState.IDLE, new MonsterStates.BaseMonster.Idle());
                    states.Add(MonsterState.MOVE, new MonsterStates.BaseMonster.Move());
                    states.Add(MonsterState.ATTACK, new MonsterStates.BaseMonster.Attack());
                    states.Add(MonsterState.DAMAGED, new MonsterStates.BaseMonster.Damaged());
                    states.Add(MonsterState.DIE , new MonsterStates.BaseMonster.Die());
                    break;
            }
            stateMachine = new StateMachine<MonsterController>(this, states[MonsterState.IDLE]);
            Managers.Resource.Load<RuntimeAnimatorController>(_data.monsterCodeName, (ac) => 
            {
                animator.runtimeAnimatorController = ac;
                ChangeDirection(Direction.Left);
                init = true; 
            });
        });
    }


    public override void Hit(Transform _attackerTrans,float _damage)
    {
        if (state == MonsterState.DIE ) return;
        sound.PlayHitSound();
        SetTarget(_attackerTrans);
        KnockBack();
        GetDamage(_damage);
        if (state == MonsterState.ATTACK) return;
        ChangeState(MonsterState.DAMAGED,true);
    }

    public override void GetDamage(float _damage)
    {
        status.currentHP -= _damage;
        CheckDie();
    }
    public void CheckDie()
    {
        if(status.currentHP <= 0)
            ChangeState(MonsterState.DIE );
    }

    public override void Die()
    {
        Managers.Event.OnIntEvent?.Invoke(IntEventType.OnDeadMonster, data.monsterUID);
        if (changeStateCoroutine != null) Managers.Routine.StopCoroutine(changeStateCoroutine);
        changeStateCoroutine = null;
        if(attack.attackCoroutine != null) Managers.Routine.StopCoroutine(attack.attackCoroutine);
        attack.attackCoroutine = null;
        status.StopAllEffect();
        movement.StopMoveCoroutine();
        init = false;
        Managers.Routine.StartCoroutine(DieRoutine());
    }

    protected override IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(3f);
        status.isDead = true;
        Managers.Pool.Push(gameObject);
    }

    public override void SetPosition(Vector2 _position)
    {
        trans.position = _position;
    }

    public void SetTarget(Transform _target)
    {
        targetTrans = _target;
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
        state = _state;
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
        if (targetTrans == null) return;
        if (targetTrans.position.x > trans.position.x) ChangeDirection(Direction.Right);
        if (targetTrans.position.x < trans.position.x) ChangeDirection(Direction.Left);
    }

    public override void KnockBack()
    {
        LookAtTarget();
        if (direction == Direction.Right) rb.AddForce(new Vector2(-data.knockBackForce, data.knockBackForce * 0.5f), ForceMode2D.Impulse);
        if (direction == Direction.Left) rb.AddForce(new Vector2(data.knockBackForce, data.knockBackForce * 0.5f), ForceMode2D.Impulse);
    }

    public override void KnockBack(float _force)
    {

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

    public override void Freeze()
    {
        throw new System.NotImplementedException();
    }

    public void Animation_Attack()
    {
        attack.Attack();
    }

    public void Animation_End()
    {
        if (state == MonsterState.ATTACK)
            Managers.Routine.StartCoroutine(attack.AttackRoutine());
    }
}

public enum MonsterState { IDLE, MOVE, FOLLOW, ATTACK, DAMAGED, DIE }

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

public class Enemy
{

}