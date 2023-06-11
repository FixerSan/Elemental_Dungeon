using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : BaseMonster
{
    public Vector2 checkAroundSize;
    public LayerMask checkAroundLayer;
    public float canAttackDuration;
    public Vector2 attackSize;
    public Transform attackPos;
    public float attackDelay;
    private bool isCanAttack = true;


    public override void Setup()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer.color = Color.white;

        statuses.maxHp = monsterData.monsterHP;
        statuses.currentHp = statuses.maxHp;
        statuses.speed = monsterData.monsterSpeed;
        statuses.force = monsterData.monsterAttackForce;

        states = new Dictionary<int, State<BaseMonster>>();

        states.Add((int)MonsterState.Hold, new BaseMonsterState.Hold());
        states.Add((int)MonsterState.Follow, new BaseMonsterState.Follow());
        states.Add((int)MonsterState.Attack, new BaseMonsterState.Attack());
        states.Add((int)MonsterState.Hit, new BaseMonsterState.Hit());
        states.Add((int)MonsterState.Dead, new BaseMonsterState.Dead());

        stateMachine.Setup(this, states[(int)MonsterState.Hold]);
    }

    public override void CheckAround()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position,checkAroundSize,0, checkAroundLayer);
        foreach (var coll in collider2Ds)
        {
            if(coll.CompareTag("Player"))
            {
                SetTarget(coll.gameObject);
                if(detectCoroutine == null)
                    detectCoroutine = StartCoroutine(Detect());
            }
        }
    }
    Coroutine detectCoroutine;
    public IEnumerator Detect()
    {
        LookAtPlayer();
        animator.SetBool("isDetect", true);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length - 0.2f);

        ChangeState(MonsterState.Follow);
    }

    public override void CheckCanAttack()
    {
        if (!isCanAttack) return;
        if (Vector2.Distance(target.transform.position, transform.position) < canAttackDuration)
        {
            ChangeState(MonsterState.Attack);
        }
    }
    public Coroutine attackCoroutine;
    public override void Attack()
    {
        isCanAttack = false;
        Stop();
        LookAtPlayer();
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackPos.position, attackSize, 0, checkAroundLayer);
        foreach (var coll in collider2Ds)
        {
            if (coll.CompareTag("Player"))
            {
                Actor hiter = coll.GetComponent<Actor>();
                BattleSystem.instance.Calculate(elemental, hiter.elemental, hiter, statuses.force);
            }
        }
        attackCoroutine=StartCoroutine(AttackCoroutine());
    }
    public IEnumerator AttackCoroutine()
    {
        float animLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength);
        ChangeState(MonsterState.Follow);
        yield return new WaitForSeconds(attackDelay- animLength);
        isCanAttack = true;
    }

    public override void GetDamage(float damage)
    {
        if (statuses.currentHp <= 0) return;

        statuses.currentHp -= damage;
        ObjectPool.instance.GetDamageText(damage, this.transform);
        if (!isHasHpBar)
        {
            isHasHpBar = true;
            ObjectPool.instance.GetHpBar(this);
        }
        if (statuses.currentHp <= 0)
        {
            statuses.currentHp = 0;
            ChangeState(MonsterState.Dead);
            AudioSystem.Instance.PlayOneShotSoundProfile("Bat_Hit");
            return;
        }
        AudioSystem.Instance.PlayOneShotSoundProfile("Bat_Hit");
        if (state != MonsterState.Follow) return;
        ChangeState(MonsterState.Hit);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, checkAroundSize);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(attackPos.position, attackSize);
    }
}
