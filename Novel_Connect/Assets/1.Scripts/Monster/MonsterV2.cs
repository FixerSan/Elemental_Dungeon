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

    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        CheckDead();
    }
    public virtual void TurnDirection()
    {
        if (monsterData.direction == Direction.Left)
        {
            monsterData.direction = Direction.Right;
            transform.eulerAngles = new Vector3(0,0,0);
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
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null)
            return 0;
        if(player.gameObject.transform.position.x > transform.position.x)
        {
            TurnDirection(Direction.Right);
            return 0;
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
        if(checkTime > stopDeley)
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
            rb.velocity = new Vector2(monsterData.monsterSpeed * 1 , rb.velocity.y);
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
        rb.velocity = Vector2.zero;
    }

    public virtual IEnumerator Follow()
    {
        LookAtPlayer();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Follow());
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
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackPos.position,attackSize);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            BattleSystem.instance.Calculate(monsterData.elemental, PlayerController.instance.elemental, PlayerController.instance , monsterData.monsterAttackForce);   
    }

    public Elemental GetElemental()
    {
        return monsterData.elemental;
    }
}


    
