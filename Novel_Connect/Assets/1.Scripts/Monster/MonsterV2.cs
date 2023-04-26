using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterV2 : MonoBehaviour, IHitable
{
    public MonsterData monsterData;
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public float moveDeley;
    public float stopDeley;
    public float checkTime;
    public Transform attackPos;
    public LayerMask attackLayer;
    public IEnumerator followCoroutin;
    public Vector2 attackSize;
    public bool isknukcBack = false;
    public bool isCutScene = true;

    //public List<Status<MonsterV2>> statuses = new List<Status<MonsterV2>>();
    //public StatusMachine<MonsterV2> statusMachine = new StatusMachine<MonsterV2>();

    //public Statuses statuses = new Statuses();
    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //statusMachine.Setup(this);
        //statuses.Add(new MonsterStatus.Burns());
        //statuses.Add(new MonsterStatus.D());
    }

    public virtual void Update()
    {
        CheckDead();
        //statusMachine.Update();
    }

    public virtual void TurnDirection()
    {
        if (monsterData.direction == Direction.Left)
        {
            monsterData.direction = Direction.Right;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            monsterData.direction = Direction.Left;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public virtual void TurnDirection(Direction direction)
    {
        monsterData.direction = direction;
        if (monsterData.direction == Direction.Left)
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
        PlayerController player = PlayerController.instance;
        if (player == null)
            return 0;
        if (player.gameObject.transform.position.x > transform.position.x)
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
    public virtual void Move()
    {
        if (monsterData.direction == Direction.Left)
            rb.velocity = new Vector2(monsterData.monsterSpeed * -1, rb.velocity.y);
        else if (monsterData.direction == Direction.Right)
            rb.velocity = new Vector2(monsterData.monsterSpeed * 1, rb.velocity.y);
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
    public virtual void Stop()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public virtual IEnumerator Follow()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Follow());
        if (!isCutScene && monsterData.monsterState != MonsterState.Dead)
        {
            LookAtPlayer();
        }

    }
    public virtual void Hit(float damage)
    {
        if (monsterData.monsterHP <= 0)
            return;

        monsterData.monsterHP -= damage;
    }
    public virtual IEnumerator HitEffect()
    {
        yield return new WaitForSeconds(0);
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

    public virtual void CheckCanAttack()
    {

    }

    public virtual IEnumerator Attack()
    {
        yield return new WaitForSeconds(0);
    }

    public virtual void CheckDead()
    {
        if (monsterData.monsterState == MonsterState.Dead)
            return;

        if (monsterData.monsterHP <= 0)
        {
            monsterData.monsterHP = 0;
            StartCoroutine(DeadEffect());
        }
    }
    public virtual IEnumerator DeadEffect()
    {
        Debug.Log("죽음 실행됨");
        yield return new WaitForSeconds(monsterData.deadEffectDelay);      //변수만큼 죽음 효과 대기
        for (int i = 0; i < monsterData.deadEffectCount; i++)      //죽음 효과 카운트만큼 반복
        {
            //카운트에 비례해서 투명도 조절
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - (1 / monsterData.deadEffectCount));

            //죽음 효과 시간에 맞추기 위해 대기
            yield return new WaitForSeconds(monsterData.deadEffectTimeLength / monsterData.deadEffectCount);
        }
        GameManager.instance.onEnenyDeath.Invoke(monsterData.monsterID);
        MonsterObjectPool.instance.ReturnMonster(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackPos.position, attackSize);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHit"))
            BattleSystem.instance.Calculate(monsterData.elemental, PlayerController.instance.elemental, PlayerController.instance, monsterData.monsterAttackForce);
    }

    public Elemental GetElemental()
    {
        return monsterData.elemental;
    }

    public void GetDamage(float damage)
    {
        if (monsterData.monsterHP <= 0)
            return;

        monsterData.monsterHP -= damage;
    }

    //IEnumerator burnCoroutine;
    //IEnumerator CheckburnCoroutine;
    //public virtual void SetStatusEffect(StatusEffect status, float duration, float damage)
    //{
    //    switch(status)
    //    {
    //        case StatusEffect.Burns:
    //            if(CheckburnCoroutine != null)
    //            {
    //                StopCoroutine(CheckburnCoroutine);
    //                CheckburnCoroutine = null;
    //                CheckburnCoroutine = CheckBurn(duration);
    //                StartCoroutine(CheckburnCoroutine);
    //                return;
    //            }
    //            else
    //            {
    //                statuses.isBurn = true;
    //                CheckburnCoroutine = CheckBurn(duration);
    //                StartCoroutine(CheckburnCoroutine);
    //                StartCoroutine(Burns(damage));
    //            }
    //            break;


    //    }

    //    //statusMachine.SetStatus(statuses[(int)status], duration, damage);
    //}

//    public IEnumerator CheckBurn(float duration)
//    {
//        if (monsterData.monsterState != MonsterState.Dead)
//        {
//            yield return new WaitForSeconds(duration);
//            statuses.isBurn = false;
//            CheckburnCoroutine = null;
//        }
//    }
//    public IEnumerator Burns(float damage)
//    {
//        if(statuses.isBurn)
//        {
//            GetDamage(damage);
//            spriteRenderer.color = Color.red;
//            yield return new WaitForSeconds(0.2f);
//            spriteRenderer.color = Color.white;

//            yield return new WaitForSeconds(0.8f);
//            StartCoroutine(Burns(damage));
//        }
//    }
//}
}


    
