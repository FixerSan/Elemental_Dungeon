using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public Vector2 checkGroundSize;
    public LayerMask groundLayer;
    private Dictionary<PlayerState, State<PlayerController>> states;
    private StateMachine<PlayerController> stateMachine;


    public void Init(int _level, string _elementalString)
    {
        Managers.Data.GetPlayerData(_level, (_data) => 
        {
            data = new PlayerData(_level);

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
        trans = gameObject.GetOrAddComponent<Transform>();
        trans.GetOrAddComponent<SpriteRenderer>();
        animator = trans.GetOrAddComponent<Animator>();
        inventory = new Inventory();
        rb = trans.GetOrAddComponent<Rigidbody2D>();
        Elemental _elemental = Util.ParseEnum<Elemental>(_elementalString);
        ChangeElemental(_elemental, () => 
        {
            states = new Dictionary<PlayerState, State<PlayerController>>();
            states.Add(PlayerState.Idle, new PlayerStates.Idle());
            states.Add(PlayerState.Walk, new PlayerStates.Walk());
            states.Add(PlayerState.RunStart, new PlayerStates.RunStart());
            states.Add(PlayerState.Run, new PlayerStates.Run());
            states.Add(PlayerState.RunEnd, new PlayerStates.RunEnd());
            states.Add(PlayerState.JumpStart, new PlayerStates.JumpStart());
            states.Add(PlayerState.Jump, new PlayerStates.Jump());
            states.Add(PlayerState.Fall, new PlayerStates.Fall());
            states.Add(PlayerState.FallEnd, new PlayerStates.FallEnd());
            stateMachine = new StateMachine<PlayerController>(this, states[PlayerState.Idle]);
            checkIsGroundTrans = Util.FindChild<Transform>(gameObject, "CheckIsGroundTrans");
            //checkGroundSize = 
            groundLayer = LayerMask.GetMask("Ground");

            init = true;
        });

    }

    public void Update()
    {
        if (!init)
            return; 
        stateMachine.UpdateState();
        movement.CheckIsGround();
        Debug.Log(movement.isGround);
    }

    public override void GetDamage(float _damage)
    {

    }

    public override void Hit(float _damage)
    {

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
                movement = new Playermovements.Normal(this);
                skills = new PlayerSkill[0];

                break;

            case Elemental.Fire:
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkIsGroundTrans.position,checkGroundSize);
    }
}
public enum PlayerState
{
    Idle,WalkStart, Walk, WalkEnd,RunStart, Run, RunEnd, JumpStart, Jump, Fall, FallEnd
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
