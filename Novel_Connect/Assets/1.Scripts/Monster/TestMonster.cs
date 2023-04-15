using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : MonsterV2
{
    public StateMachine<TestMonster> stateMachine = new StateMachine<TestMonster>();
    public State<TestMonster>[] states;

    public IEnumerator hitCoroutine;

    public override void Awake()
    {
        base.Awake();
        Setup();
    }

    public void Setup()
    {
        monsterData = new MonsterData(0);
        states = new State<TestMonster>[6];

        states[(int)MonsterState.Idle] = new TestMonsterState.Idle();
        states[(int)MonsterState.Patrol] = new TestMonsterState.Patrol();
        states[(int)MonsterState.Hit] = new TestMonsterState.Hit();
        states[(int)MonsterState.Follow] = new TestMonsterState.Follow();
        states[(int)MonsterState.Attack] = new TestMonsterState.Attack();
        states[(int)MonsterState.Dead] = new TestMonsterState.Dead();

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
        if (monsterData.monsterState != MonsterState.Attack)
        {
            stateMachine.ChangeState(states[(int)MonsterState.Hit]);

        }
    }

    public override IEnumerator HitEffect()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.AddForce(-LookAtPlayer() * Vector2.right * 2f , ForceMode2D.Impulse);
        rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        if (monsterData.monsterState == MonsterState.Dead)
            StopCoroutine(hitCoroutine);
        yield return new WaitForSeconds(0.5f);


        if(monsterData.monsterAttackPattern == MonsterAttackPattern.NotAttack)
            stateMachine.ChangeState(states[(int)MonsterState.Idle]);

        else if(monsterData.monsterAttackPattern == MonsterAttackPattern.AfterHitAttack ||
                monsterData.monsterAttackPattern == MonsterAttackPattern.BeforeHitAttack)
            stateMachine.ChangeState(states[(int)MonsterState.Follow]);

        hitCoroutine = null;
    }

    public override void CheckCanAttack()
    {
        if (monsterData.monsterType == MonsterAttackType.Short)
            return;

        PlayerController player = FindObjectOfType<PlayerController>();

        if (Vector2.Distance(transform.position, player.transform.position) <= monsterData.canAttackLength)
        {
            stateMachine.ChangeState(states[(int)MonsterState.Attack]);
        }
    }

    public override IEnumerator Attack()
    {
        LookAtPlayer();
        Collider2D[] collider = Physics2D.OverlapBoxAll(transform.position, attackSize, 0, attackLayer);

        foreach (var item in collider)
        {
            if (item.CompareTag("Player"))
            {
                BattleSystem.instance.Calculate(monsterData.elemental, PlayerController.instance.elemental,PlayerController.instance, monsterData.monsterAttackForce);
            }
        }

        yield return new WaitForSeconds(0.5f);
        stateMachine.ChangeState(states[(int)MonsterState.Follow]);
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
        }
    }

    private void OnEnable()
    {
        Setup();
    }
}
