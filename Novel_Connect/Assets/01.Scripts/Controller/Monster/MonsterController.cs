using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterController : BaseController
{
    public MonsterData data;
    public MonsterMovement movement;
    private bool init = false;
    public void Init(int _monsterUID)
    {
        trans = gameObject.GetOrAddComponent<Transform>();
        trans.GetOrAddComponent<SpriteRenderer>();
        animator = trans.GetOrAddComponent<Animator>();
        rb = trans.GetOrAddComponent<Rigidbody2D>();
        Managers.Data.GetMonsterData(_monsterUID, (_data) =>
        {
            data = _data;
            status.currentHP = _data.hp;
            status.maxHP = _data.hp;
            status.maxSpeed = _data.speed;
            status.currentSpeed = _data.speed;
            status.attackForce = _data.force;
            elemental = Util.ParseEnum<Define.Elemental>(_data.elemental);

            Managers.Resource.Load<RuntimeAnimatorController>(_data.monsterName, (ac) => 
            {
                animator.runtimeAnimatorController = ac;
                switch(_data.monsterName)
                {
                    case "TestMonster":
                        movement = new MonsterMovements.BaseMovement();
                        break;
                }
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

    }
}

[System.Serializable]
public class MonsterData
{
    public int monsterUID;
    public string monsterName;
    public string description;
    public int exp;
    public float hp;
    public float force;
    public float speed;
    public string elemental;

    public MonsterData(int _monsterUID)
    {
        Managers.Data.GetMonsterData(_monsterUID, (data) =>
        {
            monsterUID = data.monsterUID;
            monsterName = data.monsterName;
            description = data.description;
            exp = data.exp;
            hp = data.hp;
            force = data.force;
            speed = data.speed;
        });
    }
}