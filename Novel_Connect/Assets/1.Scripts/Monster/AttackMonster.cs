using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMonster : MonsterV2
{
    public StateMachine<AttackMonster> stateMachine = new StateMachine<AttackMonster>();
    public State<AttackMonster>[] states;

    public float attackDelay;
    public bool isCanAttack = true;
    public bool isBoss = false;
    public IEnumerator hitCoroutine;

    public override void Awake()
    {
        base.Awake();
        Setup();
    }

    public virtual void Setup()
    {
        monsterData = new MonsterData(monsterData.monsterID);
        states = new State<AttackMonster>[6];

        states[(int)MonsterState.Idle] = new AttackMonsterState.Idle();
        states[(int)MonsterState.Hit] = new AttackMonsterState.Hit();
        states[(int)MonsterState.Follow] = new AttackMonsterState.Follow();
        states[(int)MonsterState.Attack] = new AttackMonsterState.Attack();
        states[(int)MonsterState.Dead] = new AttackMonsterState.Dead();

        stateMachine.Setup(this, states[(int)MonsterState.Idle]);

        spriteRenderer.color = Color.white;

    }

    public override void Update()
    {
        base.Update();
        stateMachine.UpdateState();
    }

    public override void Hit(float damage)
    {
        if (monsterData.monsterHP <= 0)
            return;
        base.Hit(damage);
        StartCoroutine(HitEffect_2());
        if (monsterData.monsterState != MonsterState.Attack && monsterData.monsterState != MonsterState.Dead)
        {
            stateMachine.ChangeState(states[(int)MonsterState.Hit]);
        }
    }
    public override void Move()
    {
        if (isCutScene)
            return;
        if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) <= monsterData.canAttackLength)
            return;
        if (monsterData.direction == Direction.Left)
            rb.velocity = new Vector2(monsterData.monsterSpeed * -1, rb.velocity.y);
        else if (monsterData.direction == Direction.Right)
            rb.velocity = new Vector2(monsterData.monsterSpeed * 1, rb.velocity.y);
    }

    public IEnumerator HitEffect_2()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    public override IEnumerator HitEffect()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.AddForce(-LookAtPlayer() * Vector2.right * 2f, ForceMode2D.Impulse);
        rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        if (monsterData.monsterState == MonsterState.Dead)
            StopCoroutine(hitCoroutine);
        yield return new WaitForSeconds(0.5f);

        if (!isCutScene)
        {
            if (monsterData.monsterAttackPattern == MonsterAttackPattern.NotAttack)
                stateMachine.ChangeState(states[(int)MonsterState.Idle]);

            else if (monsterData.monsterAttackPattern == MonsterAttackPattern.AfterHitAttack ||
                    monsterData.monsterAttackPattern == MonsterAttackPattern.BeforeHitAttack)
                stateMachine.ChangeState(states[(int)MonsterState.Follow]);

        }

        hitCoroutine = null;
    }

    public override void CheckCanAttack()
    {
        if (monsterData.monsterType == MonsterAttackType.Short)
            return;
        Debug.Log(gameObject.name + Vector2.Distance(transform.position, PlayerController.instance.transform.position));
        if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) <= monsterData.canAttackLength)
        {
            if (isCanAttack)
                stateMachine.ChangeState(states[(int)MonsterState.Attack]);
            else if (monsterData.monsterState != MonsterState.Idle)
                stateMachine.ChangeState(states[(int)MonsterState.Idle]);
        }

        else
        {
            if(monsterData.monsterState != MonsterState.Follow)
            stateMachine.ChangeState(states[(int)MonsterState.Follow]);
        }
    }

    public override IEnumerator Attack()
    {

        LookAtPlayer();
        Collider2D[] collider = Physics2D.OverlapBoxAll(attackPos.position, attackSize, 0, attackLayer);

        foreach (var item in collider)
        {
            Debug.Log(item.name);
            if (item.CompareTag("PlayerHit"))
            {
                BattleSystem.instance.Calculate(monsterData.elemental, PlayerController.instance.elemental, PlayerController.instance, monsterData.monsterAttackForce);
                isCanAttack = false;
                StartCoroutine(CheckAttackTime());
            }
        }

        yield return new WaitForSeconds(0.5f);
        if(isBoss)
            ScreenEffect.instance.Shake(0.5f);
        stateMachine.ChangeState(states[(int)MonsterState.Follow]);
    }

    public IEnumerator CheckAttackTime()
    {
        yield return new WaitForSeconds(attackDelay);
        isCanAttack = true;
    }

    public override void CheckDead()
    {
        if (monsterData.monsterState == MonsterState.Dead)
            return;

        if (monsterData.monsterHP <= 0)
        {
            monsterData.monsterHP = 0;
            StartCoroutine(DeadEffect());
            stateMachine.ChangeState(states[(int)MonsterState.Dead]);
            statuses.ExitAllEffect();
        }
    }

    private void OnEnable()
    {
        Setup();
    }

}
