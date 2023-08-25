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
    private new Transform transform;

    public PlayerData data;
    public Playermovement movement;
    public PlayerAttack attack;
    public PlayerSkill[] skills;
    public Inventory inventory;
    public PlayerState state;
    public Rigidbody2D rb;
    public Animator animator;
    private Dictionary<PlayerState, State<PlayerController>> states;
    private StateMachine<PlayerController> stateMachine;
    
    public void Init(int _level, string _elementalString)
    {
        init = true;
        transform = GetComponent<Transform>();
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

        Elemental _elemental = Util.ParseEnum<Elemental>(_elementalString);
        ChangeElemental(_elemental);
        inventory = new Inventory();
        state = PlayerState.Idle;
        rb = gameObject.GetOrAddComponent<Rigidbody2D>();
        animator = gameObject.GetOrAddComponent<Animator>();
        states = new Dictionary<PlayerState, State<PlayerController>>();
        states.Add(PlayerState.Idle, new PlayerStates.Idle());
        states.Add(PlayerState.Walk, new PlayerStates.Walk());
        states.Add(PlayerState.Run, new PlayerStates.Run());
        states.Add(PlayerState.Jume, new PlayerStates.Jump());
        states.Add(PlayerState.Fall, new PlayerStates.Fall());
        stateMachine = new StateMachine<PlayerController>(this, states[PlayerState.Idle]);
    }

    public void Update()
    {
        if (!init)
            return; 
        stateMachine.UpdateState();
    }

    public override void GetDamage(float _damage)
    {

    }

    public override void Hit(float _damage)
    {

    }

    public override void SetPosition(Vector2 _position)
    {
        transform.position = _position;
    }

    public void ChangeState(PlayerState _state)
    {
        stateMachine.ChangeState(states[_state]);
    }

    public void ChangeElemental(Elemental _elemental)
    {
        switch(_elemental)
        {
            case Elemental.Normal:
                elemental = _elemental;
                movement = new Playermovements.Normal(this);
                skills = new PlayerSkill[0];
                break;

            case Elemental.Fire:
                elemental = _elemental;
                movement = new Playermovements.Fire(this);
                skills = new PlayerSkill[2];
                skills[0] = new PlayerSkills.Fire.One();
                skills[1] = new PlayerSkills.Fire.Two();
                break;
        }
    }
}
public enum PlayerState
{
    Idle, Walk, Run, Jume, Fall
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
