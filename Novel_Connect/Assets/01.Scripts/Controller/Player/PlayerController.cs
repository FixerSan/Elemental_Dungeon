using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static Define;

public class PlayerController : BaseController
{
    private bool init = false;
    public PlayerData data;
    public Playermovement movement;
    public PlayerAttack attack;
    public PlayerSkill[] skills;
    public PlayerSound sound;
    public PlayerElemental elementals;
    public Inventory inventory;
    public PlayerState state;
    public PlayerState beforestate;
    public Transform checkIsGroundTrans;
    public Transform attackTrans;
    public LayerMask groundLayer;
    public LayerMask attackLayer;
    private Dictionary<PlayerState, State<PlayerController>> states;
    private StateMachine<PlayerController> stateMachine;


    public void Init(int _level, string _elementalString)
    {
        elementals = new PlayerElemental(this);
        trans = gameObject.GetOrAddComponent<Transform>();
        trans.GetOrAddComponent<SpriteRenderer>();
        animator = trans.GetOrAddComponent<Animator>();
        rb = trans.GetOrAddComponent<Rigidbody2D>();
        inventory = new Inventory();
        checkIsGroundTrans = Util.FindChild<Transform>(gameObject, "CheckIsGroundTrans");
        attackTrans = Util.FindChild<Transform>(gameObject, "AttackTrans");
        groundLayer = LayerMask.GetMask("Ground");
        attackLayer = LayerMask.GetMask("Hitable");
        Managers.Data.GetPlayerData(_level, (_data) =>
        {
            data = _data;
            status = new ControllerStatus(this);
            status.currentHP = _data.hp;
            status.maxHP = _data.hp;
            status.currentMP = _data.mp;
            status.maxMP = _data.mp;
            status.maxJumpForce = _data.jumpForce;
            status.currentJumpForce = _data.jumpForce;
            status.maxWalkSpeed = _data.walkSpeed;
            status.currentWalkSpeed = _data.walkSpeed;
            status.maxRunSpeed = _data.runSpeed;
            status.currentRunSpeed = _data.runSpeed;
            status.currentAttackForce = _data.force;
        });
        Elemental _elemental = Util.ParseEnum<Elemental>(_elementalString);
        elementals.ChangeElemental(_elemental, () =>
        {
            states = new Dictionary<PlayerState, State<PlayerController>>();
            states.Add(PlayerState.IDLE, new PlayerStates.Idle());
            states.Add(PlayerState.WALK, new PlayerStates.Walk());
            states.Add(PlayerState.RUN, new PlayerStates.Run());
            states.Add(PlayerState.JUMP, new PlayerStates.JumpStart());
            states.Add(PlayerState.JUMPING, new PlayerStates.Jumping());
            states.Add(PlayerState.FALL, new PlayerStates.Falling());
            states.Add(PlayerState.ATTACK, new PlayerStates.Attack());
            states.Add(PlayerState.CASTSKILL_ONE, new PlayerStates.CastSkill_One());
            states.Add(PlayerState.CASTSKILL_TWO, new PlayerStates.CastSkill_Two());
            states.Add(PlayerState.DASH, new PlayerStates.Dash());
            stateMachine = new StateMachine<PlayerController>(this, states[PlayerState.IDLE]);
            init = true;
        });

    }

    public void Update()
    {
        if (!init) return;
        if (!Managers.Input.isCanControl) return;
        CheckSkillCooltime();
        stateMachine.UpdateState();
        movement.CheckIsGround();
        inventory.CheckOpenUIInventory();
    }

    public override void GetDamage(float _damage)
    {
        status.currentHP -= _damage;
        Managers.Event.OnVoidEvent?.Invoke(VoidEventType.OnChangeHP);
    }

    public override void Hit(Transform _attackerTrans, float _damage)
    {
        GetDamage(_damage);
    }

    public override void SetPosition(Vector2 _position)
    {
        trans.position = _position;
    }

    public void ChangeState(PlayerState _state, bool _isChangeSameState = false)
    {
        if (state == _state)
        {
            if (_isChangeSameState)
                stateMachine.ChangeState(states[_state]);
            return;
        }
        beforestate = state;
        state = _state;
        stateMachine.ChangeState(states[_state]);
    }

    public void ChangeStateWithDelay(PlayerState _nextState,float _delayTime)
    {
        Managers.Routine.StartCoroutine(ChangeStateWithDelayRoutine(_nextState, _delayTime));
    }

    private IEnumerator ChangeStateWithDelayRoutine(PlayerState _nextState, float _delayTime)
    {
        yield return new WaitForSeconds(_delayTime);
        ChangeState(_nextState);
    }

    public void ChangeStateWithAnimtionTime(PlayerState _nextState)
    {
        Managers.Routine.StartCoroutine(ChangeStateWithAnimtionTimeRoutine(_nextState));
    }

    private IEnumerator ChangeStateWithAnimtionTimeRoutine(PlayerState _nextState)
    {
        yield return new WaitForSeconds(0.05f);
        float animationPlayTime = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationPlayTime - 0.05f);
        ChangeState(_nextState);
    }
    public override void KnockBack()
    {

    }

    public override void KnockBack(float _force)
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkIsGroundTrans.position,checkIsGroundTrans.localScale);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackTrans.position, attackTrans.localScale);
    }

    public override void Die()
    {

    }

    protected override IEnumerator DieRoutine()
    {
        throw new NotImplementedException();
    }

    public void AnimationEvent()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_1")) attack.Attack();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_2")) attack.Attack();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_3")) attack.Attack();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_4")) attack.Attack();
    }

    public void CheckSkillCooltime()
    {
        if(skills.Length > 0)
        {
            skills[0]?.CheckCoolTime();
            skills[1]?.CheckCoolTime();
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            Managers.Input.CheckInput(Managers.Input.pickupItemKey, (_inputType) => 
            {
                if (_inputType != InputType.HOLD) return;

                ItemController item = collision.GetOrAddComponent<ItemController>();
                item.PutInInventory(trans);
            });
        }
    }
}
public enum PlayerState
{
    IDLE, WALK, RUN, JUMP, JUMPING, FALL, ATTACK , CASTSKILL_ONE, CASTSKILL_TWO, DASH
}

[System.Serializable]
public class PlayerData
{
    public int level;
    public int levelUpExp;
    public float hp;
    public float mp;
    public float force;
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;



    public PlayerData(int _level)
    {
        Managers.Data.GetPlayerData(_level, (data) =>
        {
            level = data.level;
            levelUpExp = data.levelUpExp;
            hp = data.hp;
            mp = data.mp;
            force = data.force;
            walkSpeed = data.walkSpeed;
            runSpeed = data.runSpeed;
            jumpForce = data.jumpForce;
        });
    }
}

[System.Serializable]
public class PlayerSaveData
{
    public int level;
    public string elemental;
}
