using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState { Create, CreateSword, Idle, Follow, Attack, Hit, Skill_1Cast, Skill_2Cast, Dead }
public class BaseBoss : Actor
{
    public MonsterData monsterData;
    public StateMachine<BaseBoss> stateMachine = new StateMachine<BaseBoss>();
    public Animator animator;
    public BossState state;
    public GameObject target;

    protected Dictionary<int, State<BaseBoss>> states = new Dictionary<int, State<BaseBoss>>();
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;

    private float checkTime;

    public float canAttackRange;

    public float attackPeriod;
    public int canAttackCount = 2;
    public Transform attackPos;
    public Vector2 attackSize;
    public LayerMask attackLayer;
    private bool isCanAttack = false;


    public float skill_1Cooltime;
    private float checkSkill_1Cooltime;
    private bool isCanUseSkill_1 = false;
    public GameObject skill_1;
    private bool isCanUseSkill_2 = false;
    public float skill_2Cooltime;
    public float skill_2Size;
    private float checkSkill_2Cooltime;
    public Transform skill_2Pos;

    private bool isCanGetDamage = true;

    public override void Setup()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        //monsterData = DataBase.instance.GetMonsterData(10001);
        states = new Dictionary<int, State<BaseBoss>>();
        statuses.maxHp = monsterData.monsterHP;
        statuses.currentHp = statuses.maxHp;
        statuses.speed = monsterData.monsterSpeed;
        statuses.force = monsterData.monsterAttackForce;
        statuses.Setup(this);

        elemental = monsterData.elemental;

        states.Add((int)BossState.Idle, new BaseBossState.Idle());
        states.Add((int)BossState.Follow, new BaseBossState.Follow());
        states.Add((int)BossState.Attack, new BaseBossState.Attack());
        states.Add((int)BossState.Dead, new BaseBossState.Dead());
        states.Add((int)BossState.Skill_1Cast, new BaseBossState.Skill_1Cast());
        states.Add((int)BossState.Skill_2Cast, new BaseBossState.Skill_2Cast());
        states.Add((int)BossState.Create, new BaseBossState.Create());
        states.Add((int)BossState.CreateSword, new BaseBossState.CreateSword());

        spriteRenderer.color = Color.white;

        checkTime = 0;

        target = null;

        stateMachine.Setup(this, states[(int)BossState.Create]);
        SetTarget(FindObjectOfType<PlayerControllerV3>().gameObject);
    }

    private void FixedUpdate()
    {
        stateMachine.UpdateState();
        statuses.Update();
        CheckDead();
    }

    public override void GetDamage(float damage)
    {
        animator.SetTrigger("HitEffect");
        statuses.currentHp -= damage;
    }

    public override void Hit(float damage)
    {
        if(isCanGetDamage)  GetDamage(damage);
    }

    public override void SetTarget(GameObject target_)
    {
        target = target_;
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
    public void ChangeState(BossState state)
    {
        stateMachine.ChangeState(states[(int)state]);
    }

    public void Create()
    {
        StartCoroutine(ChangeStateWithAnimationDelay(BossState.CreateSword));
    }

    public void CreateSword()
    {
        StartCoroutine(ChangeStateWithAnimationDelay(BossState.Idle));
    }

    public IEnumerator ChangeStateWithAnimationDelay(BossState state_)
    {
        yield return new WaitForSeconds(0.1f);
        float delay = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(delay - 0.1f);
        ChangeState(state_);
    }

    public void CheckAttackTime()
    {
        if(!isCanAttack)
        {
            checkTime += Time.deltaTime;
            if(checkTime >= attackPeriod)
            {
                checkTime = 0;
                isCanAttack = true;
            }
        }
    }

    public void CheckCanAttack()
    {
        if(isCanAttack && state == BossState.Idle)
        {
            ChangeState(BossState.Attack);
        }
    }

    public void StartAttack()
    {
        isCanAttack = false;
        int attackCount = Random.Range(1, canAttackCount+1);
        StartCoroutine(AttackCoroutine(attackCount));
    }

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPos.position, attackSize, 0, attackLayer);
        foreach (var coll in colliders)
        {
            if(coll.CompareTag("Player"))
            {
                Actor player = coll.GetComponent<Actor>();
                BattleSystem.instance.HitCalculate(elemental, player.elemental, player ,player.statuses.force);
            }
        }
    }

    public IEnumerator AttackCoroutine(int attackCount)
    {
        animator.SetInteger("AttackCount", attackCount);
        yield return new WaitForSeconds(0.1f);
        float currentAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        animator.SetInteger("AttackCount", 0);
        switch (attackCount)
        {
            case 1:
                yield return new WaitForSeconds(((currentAnimationLength / 101) * 41) - 0.1f);
                Attack();
                yield return new WaitForSeconds(currentAnimationLength - ((currentAnimationLength / 101) * 41));
                break;

            case 2:
                yield return new WaitForSeconds((currentAnimationLength / 81 * 40) - 0.1f);
                Attack();
                yield return new WaitForSeconds(currentAnimationLength - (currentAnimationLength / 81 * 40));
                break;
        }
        ChangeState(BossState.Idle);
    }



    public void CheckSkill_1CoolTime()
    {
        if (!isCanUseSkill_1)
        {
            checkSkill_1Cooltime -= Time.deltaTime;
            if (checkSkill_1Cooltime <= 0)
            {
                checkSkill_1Cooltime = 0;
                isCanUseSkill_1 = true;
            }
        }
    }
    public void CheckCanUseSkill_1()
    {
        if(isCanUseSkill_1 && state == BossState.Idle)
        {
            ChangeState(BossState.Skill_1Cast);
        }
    }

    public void Skill_1()
    {
        isCanUseSkill_1 = false;
        checkSkill_1Cooltime = skill_1Cooltime;
        GameObject skill = (GameObject)Instantiate(skill_1);
        skill.GetComponent<IceBosSkill_1>().Setup(this);
        animator.SetTrigger("UseSkill_1");
        StartCoroutine(ChangeStateWithAnimationDelay(BossState.Idle));
    }

    public void CheckSkill_2CoolTime()
    {
        if (!isCanUseSkill_2)
        {
            checkSkill_2Cooltime -= Time.deltaTime;
            if (checkSkill_2Cooltime <= 0)
            {
                checkSkill_2Cooltime = 0;
                isCanUseSkill_2 = true;
            }
        }
    }

    public void CheckCanUseSkill_2()
    {
        if (isCanUseSkill_2 && state == BossState.Idle)
        {
            ChangeState(BossState.Skill_2Cast);
        }
    }

    public void Skill_2()
    {
        isCanUseSkill_2 = false;
        checkSkill_2Cooltime = skill_2Cooltime;
        StartCoroutine(Skill_2Coroutine());
    }

    public IEnumerator Skill_2Coroutine()
    {
        isCanGetDamage = false;
        animator.SetTrigger("UseSkill_2");
        yield return new WaitForSeconds(0.1f);
        float currentAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(((currentAnimationLength / 230) * 173) - 0.1f);
        isCanGetDamage = true;
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(skill_2Pos.position, skill_2Size,attackLayer);
        foreach (var coll in collider2Ds)
        {
            Actor player;
            if(coll.CompareTag("Player"))
            {
                player = coll.GetComponent<Actor>();
                BattleSystem.instance.HitCalculate(elemental, player.elemental, player, statuses.force);
            }
        }
        yield return new WaitForSeconds(currentAnimationLength - ((currentAnimationLength / 230) * 173));
        ChangeState(BossState.Idle);
    }

    public void Stop()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void CheckTargetDistance()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > canAttackRange)
        {
            if (state == BossState.Idle)
            {
                ChangeState(BossState.Follow);
            }
        }
        else if (state == BossState.Follow)
        {
            ChangeState(BossState.Idle);
        }
    }
    public void Move()
    {
        if(direction == Direction.Right)
        {
            rb.velocity = new Vector2(statuses.nowSpeed * 1 , rb.velocity.y);
        }
        if (direction == Direction.Left)
        {
            rb.velocity = new Vector2(statuses.nowSpeed * -1, rb.velocity.y);
        }
    }

    public void Move(int direction)
    {
        if(state == BossState.Follow)
            rb.velocity = new Vector2(statuses.nowSpeed * direction, rb.velocity.y);
    }

    public void CheckDead()
    {
        if(statuses.currentHp <= 0 && state != BossState.Dead)
        {
            ChangeState(BossState.Dead);
        }
    }

    public void Dead()
    {
        StopAllCoroutines();
        rb.gravityScale = 0f;
        Stop();
        GetComponent<BoxCollider2D>().enabled = false;
        animator.SetTrigger("Dead");
        StartCoroutine(DeadCoroutine());
    }

    public IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        float currentAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(currentAnimationLength-0.1f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackPos.position, attackSize);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(skill_2Pos.position,skill_2Size);
    }

    private void OnDestroy()
    {
        MonsterSystem.instance.OnDeadBoss(0);
    }
}
