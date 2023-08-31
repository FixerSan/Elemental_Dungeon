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
    public Inventory inventory;
    public PlayerState state;
    public Transform checkIsGroundTrans;
    public Transform attackTrans;
    public LayerMask groundLayer;
    public LayerMask attackLayer;
    private Dictionary<PlayerState, State<PlayerController>> states;
    private StateMachine<PlayerController> stateMachine;


    public void Init(int _level, string _elementalString)
    {
        trans = gameObject.GetOrAddComponent<Transform>();
        trans.GetOrAddComponent<SpriteRenderer>();
        animator = trans.GetOrAddComponent<Animator>();
        rb = trans.GetOrAddComponent<Rigidbody2D>();
        inventory = new Inventory();
        Managers.Data.GetPlayerData(_level, (_data) => 
        {
            data = _data;
            status.currentHP = _data.hp;
            status.maxHP = _data.hp;
            status.currentMP = _data.mp;
            status.maxMP = _data.mp;
            status.maxJumpForce = _data.jumpForce;
            status.currentJumpForce = _data.jumpForce;
            status.maxSpeed = _data.speed;
            status.currentSpeed = _data.speed;
            status.attackForce = _data.force;
        });
        Elemental _elemental = Util.ParseEnum<Elemental>(_elementalString);
        ChangeElemental(_elemental, () => 
        {
            states = new Dictionary<PlayerState, State<PlayerController>>();
            states.Add(PlayerState.Idle, new PlayerStates.Idle());
            states.Add(PlayerState.Walk, new PlayerStates.Walk());
            states.Add(PlayerState.Run, new PlayerStates.Run());
            states.Add(PlayerState.JumpStart, new PlayerStates.JumpStart());
            states.Add(PlayerState.Jump, new PlayerStates.Jump());
            states.Add(PlayerState.Fall, new PlayerStates.Fall());
            states.Add(PlayerState.FallEnd, new PlayerStates.FallEnd());
            states.Add(PlayerState.Attack, new PlayerStates.Attack());
            stateMachine = new StateMachine<PlayerController>(this, states[PlayerState.Idle]);
            checkIsGroundTrans = Util.FindChild<Transform>(gameObject, "CheckIsGroundTrans");
            attackTrans = Util.FindChild<Transform>(gameObject, "AttackTrans");
            groundLayer = LayerMask.GetMask("Ground");
            attackLayer = LayerMask.GetMask("Hitable");
            init = true;
        });

    }

    public void Update()
    {
        if (!init)
            return;
        stateMachine.UpdateState();
        movement.CheckIsGround();
    }

    public override void GetDamage(float _damage)
    {
        status.currentHP -= _damage;
    }

    public override void Hit(Transform _attackerTrans, float _damage)
    {
        GetDamage(_damage);

    }

    public override void SetPosition(Vector2 _position)
    {
        trans.position = _position;
    }

    public void ChangeState(PlayerState _state)
    {
        if (state == _state) return;
        stateMachine.ChangeState(states[_state]);
    }

    public void ChangeElemental(Elemental _elemental, Action _callback)
    {
        switch(_elemental)
        {
            case Elemental.Normal:
                attack = new PlayerAttacks.Normal(this);
                movement = new Playermovements.Normal(this);
                skills = new PlayerSkill[0];

                break;

            case Elemental.Fire:
                attack = new PlayerAttacks.Fire(this);
                movement = new Playermovements.Fire(this);
                skills = new PlayerSkill[2];
                skills[0] = new PlayerSkills.Fire.One();
                skills[1] = new PlayerSkills.Fire.Two();
                break;
        }

        elemental = _elemental;
        Managers.Resource.Load<RuntimeAnimatorController>($"Player_{_elemental}", (ac) => 
        {
            animator.runtimeAnimatorController = ac;
            _callback?.Invoke();
        });

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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkIsGroundTrans.position,checkIsGroundTrans.localScale);
    }
}
public enum PlayerState
{
    Idle, Walk, Run, JumpStart, Jump, Fall, FallEnd , Attack
}

[System.Serializable]
public class PlayerData
{
    public int level;
    public int levelUpExp;
    public float hp;
    public float mp;
    public float force;
    public float speed;
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
            speed = data.speed;
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
