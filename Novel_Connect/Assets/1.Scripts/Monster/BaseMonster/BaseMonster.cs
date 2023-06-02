using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : Actor
{
    public MonsterData monsterData;
    public StateMachine<BaseMonster> stateMachine = new StateMachine<BaseMonster>();
    public Animator animator;
    public Direction direction;
    public MonsterState state;

    protected State<BaseMonster>[] states;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;
    protected float checkTime;
    protected GameObject target;

    public float moveDeley;
    public float stopDeley;

    public override void Setup()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        //monsterData = new MonsterData(0);
        states = new State<BaseMonster>[6];
        statuses.Setup(this);
        statuses.maxHp = monsterData.monsterHP;
        statuses.currentHp = statuses.maxHp;
        statuses.speed = monsterData.monsterSpeed;
        statuses.force = monsterData.monsterAttackForce;

        elemental = monsterData.elemental;

        states[(int)MonsterState.Idle] = new BaseMonsterState.Idle();
        states[(int)MonsterState.Patrol] = new BaseMonsterState.Patrol();
        states[(int)MonsterState.Hit] = new BaseMonsterState.Hit();
        states[(int)MonsterState.Follow] = new BaseMonsterState.Follow();
        states[(int)MonsterState.Attack] = new BaseMonsterState.Attack();
        states[(int)MonsterState.Dead] = new BaseMonsterState.Dead();

        stateMachine.Setup(this, states[(int)MonsterState.Idle]);
    }

    public void FixedUpdate()
    {
        stateMachine.UpdateState();
    }

    public virtual bool CheckCanMove()
    {
        checkTime += Time.deltaTime;
        if (checkTime > stopDeley)
        {
            checkTime = 0;
            return true;
        }
        return false;
    }
    public virtual bool CheckCanStop()
    {
        checkTime += Time.deltaTime;
        if (checkTime > moveDeley)
        {
            checkTime = 0;
            return true;
        }
        return false;
    }

    public virtual void Move()
    {
        if (direction == Direction.Left)
            rb.velocity = new Vector2(statuses.speed * -1, rb.velocity.y);
        else if (direction == Direction.Right)
            rb.velocity = new Vector2(statuses.speed * 1, rb.velocity.y);
    }

    public void Stop()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public virtual void TurnDirection()
    {
        if (direction == Direction.Left)
        {
            direction = Direction.Right;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        else
        {
            direction = Direction.Left;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public virtual void TurnDirection(Direction direction_)
    {
        direction = direction_;
        if (direction == Direction.Left)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public virtual int LookAtPlayer()
    {
        if (target == null)
            return 0;
        if (target.transform.position.x > transform.position.x)
        {
            TurnDirection(Direction.Right);
            return 1;
        }

        else
        {
            TurnDirection(Direction.Left);
            return -1;
        }
    }
    public Coroutine followCoroutine;
    public virtual IEnumerator Follow()
    {
        yield return new WaitForSeconds(0.5f);
        LookAtPlayer();
        StartCoroutine(Follow());
    }

    public override void GetDamage(float damage)
    {
        if (statuses.currentHp <= 0)   return;

        statuses.currentHp -= damage;
        if(statuses.currentHp <= 0)
        {
            statuses.currentHp = 0;
            ChangeState(MonsterState.Dead);
            return;
        }
        ChangeState(MonsterState.Hit);
    }

    public Coroutine hitCoroutine;
    public IEnumerator HitEffect()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.AddForce(-LookAtPlayer() * Vector2.right * 2f, ForceMode2D.Impulse);
        rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.75f);

        if (monsterData.monsterAttackPattern == MonsterAttackPattern.NotAttack)
            ChangeState(MonsterState.Idle);

        else if (monsterData.monsterAttackPattern == MonsterAttackPattern.AfterHitAttack ||
                    monsterData.monsterAttackPattern == MonsterAttackPattern.BeforeHitAttack)
            ChangeState(MonsterState.Follow);

        hitCoroutine = null;
    }

    public virtual void KnockBack(Direction direction, float xKnockBackforce, float yKnockBackforce)
    {
        int intDirection;
        if (direction == Direction.Left)
            intDirection = -1;
        else
            intDirection = 1;

        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.AddForce(intDirection * Vector2.right * xKnockBackforce, ForceMode2D.Impulse);
        rb.AddForce(Vector2.up * yKnockBackforce, ForceMode2D.Impulse);

    }

    public void ChangeState(MonsterState state)
    {
        stateMachine.ChangeState(states[(int)state]);
    }

    public override void SetTarget(GameObject target_)
    {
        target = target_;
    }
}
