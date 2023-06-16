using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : Actor
{
    public MonsterData monsterData;
    public StateMachine<BaseMonster> stateMachine = new StateMachine<BaseMonster>();
    public Animator animator;
    public MonsterState state;

    protected Dictionary<int, State<BaseMonster>> states = new Dictionary<int, State<BaseMonster>>();
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;
    protected float checkTime;
    protected float rStopDelay;
    protected float rMoveDelay;
    protected GameObject target;

    public float moveDeley;
    public float stopDeley;

    protected bool isHasHpBar = false;

    public override void Setup()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        //monsterData = DataBase.instance.GetMonsterData(10001);
        states = new Dictionary<int, State<BaseMonster>>();
        statuses.Setup(this);
        statuses.maxHp = monsterData.monsterHP;
        statuses.currentHp = statuses.maxHp;
        statuses.speed = monsterData.monsterSpeed;
        statuses.force = monsterData.monsterAttackForce;

        elemental = monsterData.elemental;

        states.Add((int)MonsterState.Idle, new BaseMonsterState.Idle());
        states.Add((int)MonsterState.Patrol, new BaseMonsterState.Patrol());
        states.Add((int)MonsterState.Hit, new BaseMonsterState.Hit());
        states.Add((int)MonsterState.Follow, new BaseMonsterState.Follow());
        states.Add((int)MonsterState.Attack, new BaseMonsterState.Attack());
        states.Add((int)MonsterState.Dead, new BaseMonsterState.Dead());

        spriteRenderer.color = Color.white;

        rStopDelay = Random.Range(stopDeley - 0.2f, stopDeley);
        rMoveDelay = Random.Range(moveDeley - 0.2f, moveDeley);
        checkTime = 0;

        target = null;
        isHasHpBar = false;
        stateMachine.Setup(this, states[(int)MonsterState.Idle]);

        direction = (Direction)Random.Range(0, 2);
    }
    public void OnEnable()
    {
        Setup();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.UpdateState();
        statuses.Update();
    }

    public virtual bool CheckCanMove()
    {
        checkTime += Time.deltaTime;
        if (checkTime > rStopDelay)
        {
            checkTime = 0;
            return true;
        }
        return false;
    }

    public void CheckJump()
    {
        if(rb.velocity.x == 0)
        {
            jumpCoroutine = StartCoroutine(JumpCoroutine());
        }
    }

    public Coroutine jumpCoroutine;
    public virtual IEnumerator JumpCoroutine()
    {
        rb.AddForce(Vector2.up * 9 , ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        jumpCoroutine = null;
    }

    public virtual bool CheckCanStop()
    {
        checkTime += Time.deltaTime;
        if (checkTime > rMoveDelay)
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
    public override void Hit(float damage)
    {
        GetDamage(damage);
        if (statuses.currentHp <= 0)
        {
            statuses.currentHp = 0;
            ChangeState(MonsterState.Dead);
            return;
        }
        ChangeState(MonsterState.Hit);
    }

    public override void GetDamage(float damage)
    {
        if (statuses.currentHp <= 0)   return;

        statuses.currentHp -= damage;
        ObjectPool.instance.GetDamageText(damage, this.transform);
        if(!isHasHpBar)
        {
            isHasHpBar = true;
            ObjectPool.instance.GetHpBar(this);
        }

        if (statuses.currentHp <= 0)
        {
            statuses.currentHp = 0;
            ChangeState(MonsterState.Dead);
            return;
        }
    }

    public Coroutine hitCoroutine;
    public IEnumerator HitEffect()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        KnuckBack();
        rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.75f);

        if (monsterData.monsterAttackPattern == MonsterAttackPattern.NotAttack)
            ChangeState(MonsterState.Idle);

        else if (monsterData.monsterAttackPattern == MonsterAttackPattern.AfterHitAttack ||
                    monsterData.monsterAttackPattern == MonsterAttackPattern.BeforeHitAttack)
            ChangeState(MonsterState.Follow);

        hitCoroutine = null;
    }

    public void KnuckBack()
    {
        rb.AddForce(-LookAtPlayer() * Vector2.right * 2f, ForceMode2D.Impulse);
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

    public Coroutine deadCoroutine;
    public void Dead()
    {
        deadCoroutine = StartCoroutine(DeadCoroutine(3));
    }
    public IEnumerator DeadCoroutine(float fadeTime)
    {
        while(spriteRenderer.color.a > 0)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - Time.deltaTime / fadeTime);
            yield return null;
        }

        ObjectPool.instance.ReturnMonster(this.gameObject);
        deadCoroutine = null;
    }

    public void ChangeState(MonsterState state)
    {
        stateMachine.ChangeState(states[(int)state]);
    }

    public override void SetTarget(GameObject target_)
    {
        target = target_;
    }
    public virtual void CheckAround()
    {

    }

    public virtual void CheckCanAttack()
    {

    }

    public virtual void Attack()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (state == MonsterState.Dead) return;
        if(collision.CompareTag("Player"))
        {
            Actor player = collision.GetComponent<Actor>();

            BattleSystem.instance.Calculate(elemental,player.elemental,player,statuses.force);
        }
    }

}


