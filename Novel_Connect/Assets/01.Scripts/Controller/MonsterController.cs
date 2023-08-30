using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterController : BaseController
{
    public MonsterData data;
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
}