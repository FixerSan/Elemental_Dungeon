using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterController : BaseController
{
    public MonsterData data;
    public override void GetDamage(float _damage)
    {

    }

    public override void Hit(float _damage)
    {

    }

    public override void SetPosition(Vector2 _position)
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