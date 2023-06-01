using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : Actor
{
    public MonsterData monsterData;
    public StateMachine<BaseMonster> stateMachine;
    protected State<BaseMonster>[] states;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;

    public override void GetDamage(float damage)
    {
        
    }

    public override void Setup()
    {

    }
}
