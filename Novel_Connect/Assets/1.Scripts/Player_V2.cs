using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_V2 : MonoBehaviour
{
    public enum State { idle, walk, jump , fall ,ladder, attack, skill_1Casting, skill_2Casting, breaking, dead }
    public State state;
    protected Animator animator;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D ladder;
    public bool isGrounded = false;
    protected bool isCanDownJump = false;
    protected bool isCanHit = true;
    protected bool isCanUseLadder = false;
    protected bool isUseLadder = false;
    protected int direction = 1;
    protected bool isQuickSloatOn = false;

    [Header("[ State ]")]

    [Tooltip("순간적인 속도 조절")]
    public float speed;
    [Tooltip("최대 속도 조절")]
    public float maxSpeed;
    [Tooltip("점프 파워")]
    public float jumpForce;
    [Tooltip("체력")]
    public float maxHp = 10;
    public float currentHp;
    [Tooltip("공격력")]
    public float attackForce;
    public float maxMp;
    public float currentMp;
    public float breakingCount = 4;
    public GameObject quickSlot;
    public AudioClip potionAudioClip;


    [Space(20f)]
    [Header("[ Attack ]")]
    [Tooltip("몇단 공격 인지")]
    public int attackCount;
    [Tooltip("다음 공격까지 할 수 있는 시간")]
    public float canAttackDuration;
    [Tooltip("공격할 범위 피봇")]
    public Transform attackPoint;
    [Tooltip("공격할 범위 크기")]
    public Vector2 attackSize;
    [Tooltip("공격할 레이어")]
    public LayerMask AttackLayer;

    [Space(20f)]
    [Header("[ Etc ]")]
    [Tooltip("바닥 체크 거리")]
    public Vector2 isGroundLength;
    public Transform groundCheckPos;
    [Tooltip("맞고 나서 무적 시간")]
    public float hitRate = 1;
    public LayerMask groundLayer;



    // Start is called before the first frame update
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHp = maxHp;
        currentMp = maxMp;
    }

    // Update is called once per frame
    [System.Obsolete]
    public virtual void Update()
    {
        IsGroundCheck();
        DeadCheck();
        switch (state)
        {
            case State.idle:
                MoveCheck();
                MoveDirection();
                Jump();
                DownJump();
                JumpCheck();
                LadderCheck();
                QuickSlotCheck();
                break;

            case State.walk:
                MoveCheck();
                MoveDirection();
                MoveStop();
                Jump();
                DownJump();
                JumpCheck();
                LadderCheck();
                QuickSlotCheck();
                break;

            case State.jump:
                MoveCheck();
                MoveDirection();
                MoveStop();
                JumpCheck();
                LadderCheck();
                QuickSlotCheck();
                break;

            case State.fall:
                MoveCheck();
                MoveDirection();
                MoveStop();
                JumpCheck();
                LadderCheck();
                QuickSlotCheck();
                break;

            case State.ladder:
                Ladder();
                Jump();
                QuickSlotCheck();
                break;

            case State.attack:
                
                
                break;

            case State.skill_1Casting:

                break;

            case State.skill_2Casting:

                break;

            case State.breaking:
                QuickSlotCheck();
                
                break;
            case State.dead:

                break;

        }
    }

    public virtual void FixedUpdate()
    {
        switch (state)
        {
            case State.idle:

                break;

            case State.walk:
                Move();
                Move_Deceleration();

                break;

            case State.jump:
                Move();
                Move_Deceleration();

                break;

            case State.fall:
                Move();
                Move_Deceleration();

                break;

            case State.ladder:

                break;

            case State.attack:

                break;

            case State.skill_1Casting:

                break;

            case State.skill_2Casting:

                break;

            case State.breaking:

                break;
            case State.dead:
                Dead();
                break;

        }
    }

    public virtual void IsGroundCheck()
    {
         Collider2D[] collider = Physics2D.OverlapBoxAll(groundCheckPos.position, isGroundLength, 0, groundLayer);
        if(collider == null || collider.Length == 0)
        {
            isGrounded = false;
            return;
        }

        foreach (Collider2D col in collider)
        {
            if (col.transform.CompareTag("Ground"))
            {
                isGrounded = true;
                isCanDownJump = true;
                break;
            }

            else if (col.transform.CompareTag("LastGround"))
            {
                isGrounded = true;
                isCanDownJump = false;
                break;
            }

            else
            {
                isGrounded = false;
                break;
            }
        }
        animator.SetBool("isGround", isGrounded);
    }


    public virtual void MoveCheck()
    {
        if (Input.GetButton("Horizontal"))
            state = State.walk;
        else
            state = State.idle;
    }
    public virtual void MoveDirection()
    {
        //움직임
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            if (animator.GetBool("isWalk"))
                animator.SetBool("isWalk", false);
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!animator.GetBool("isWalk"))
                animator.SetBool("isWalk", true);
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.GetChild(7).transform.eulerAngles = new Vector3(0, 180, 0);
            direction = -1;
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!animator.GetBool("isWalk"))
                animator.SetBool("isWalk", true);
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.GetChild(7).eulerAngles = new Vector3(0, 0, 0);
            direction = 1;
        }
    }

    public virtual void Move()
    {
        if (state == State.attack)
            return;

        //움직임
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-Vector2.right * speed, ForceMode2D.Impulse);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        }
    }

    public virtual void Move_Deceleration()        //최대속도 제한
    {
        //이동속도 제한
        if (rb.velocity.x > maxSpeed)
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);

        if (rb.velocity.x < maxSpeed * (-1))
            rb.velocity = new Vector2(maxSpeed * (-1), rb.velocity.y);
    }

    public virtual void MoveStop()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (animator.GetBool("isWalk"))
                animator.SetBool("isWalk", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public virtual void JumpCheck()
    {
        if (rb.velocity.y == 0)
        {
            if (animator.GetBool("isJump"))
                animator.SetBool("isJump", false);
            if (animator.GetBool("isFall"))
                animator.SetBool("isFall", false);
            return;
        }

        else if (rb.velocity.y >= 0.1f)
        {
            state = State.jump;
            if (!animator.GetBool("isJump"))
                animator.SetBool("isJump", true);
            if (animator.GetBool("isFall"))
                animator.SetBool("isFall", false);
        }

        else if (rb.velocity.y <= 0.1f)
        {
            state = State.fall;
            if (animator.GetBool("isJump"))
                animator.SetBool("isJump", false);
            if (!animator.GetBool("isFall"))
                animator.SetBool("isFall", true);
        }
    }

    public virtual void Jump()
    {
        if(isGrounded && Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    public virtual void DownJump()
    {
        //땅에 붙어 있을 때
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (Input.GetKeyDown(KeyCode.Space) && isCanDownJump)
                {
                    StartCoroutine(DownJumpCoroutine());
                }
            }
        }
    }

    public IEnumerator DownJumpCoroutine()
    {
        transform.Find("GroundCheck").gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        transform.Find("GroundCheck").gameObject.SetActive(true);

    }

    public virtual void LadderCheck()
    {
        if (isCanUseLadder)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //사다리 사용 모드로 변경
                state = State.ladder;
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                transform.position = new Vector2(ladder.transform.position.x, transform.position.y);
                isCanUseLadder = false;
            }
        }
    }
    public virtual void Ladder()
    {
        //사다리 애니메이션 시작
        if (!animator.GetBool("isLadder"))
            animator.SetBool("isLadder", true);

        if (animator.GetBool("isWalk"))
            animator.SetBool("isWalk", false);

        //위로 가는 키를 누르는 동안
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (animator.GetBool("isJump"))
                animator.SetBool("isJump", false);
            //사다리 무브 애니메이션 시작
            if (!animator.GetBool("isMoveLadder"))
                animator.SetBool("isMoveLadder", true);
            //위로 올라는 기능과 올라가는 속도 제어
            rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
            if (rb.velocity.y >= maxSpeed * 0.3f)
                rb.velocity = new Vector2(0, maxSpeed * 0.3f);
        }


        //위로 올라가는 키를 땠을 때
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            animator.SetBool("isMoveLadder", false); //사다리 무브 애니메이션 종료
            rb.velocity = Vector2.zero; //중력이 없으니까 한번 벨로시티 초기화
        }


        //아래로 가는 키를 누르는 동안
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (animator.GetBool("isJump"))
                animator.SetBool("isJump", false);
            //사다리 무브 애니메이션 시작
            if (!animator.GetBool("isMoveLadder"))
                animator.SetBool("isMoveLadder", true);
            //아래로 내려가는 기능과 내려가는 속도 제어
            rb.AddForce(Vector2.down * speed, ForceMode2D.Impulse);
            if (rb.velocity.y <= -maxSpeed * 0.3f)
                rb.velocity = new Vector2(0, -maxSpeed * 0.3f);
        }

        //아래로 가는 키를 땠을 때
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool("isMoveLadder", false); //사다리 무브 애니메이션 종료
            rb.velocity = Vector2.zero; //중력이 없으니까 한번 벨로시티 초기화
        }

        //점프 키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //본래 설정으로 변경
            isUseLadder = false;
            rb.gravityScale = 15f;
            animator.SetBool("isLadder", false);
            animator.SetBool("isMoveLadder", false);
            state = State.idle;
        }

    }

    [System.Obsolete]
    public virtual void QuickSlotCheck()
    {
        if (state == State.attack || state == State.skill_1Casting || state == State.skill_2Casting || state == State.dead)
        {
            isQuickSloatOn = false;
            quickSlot.SetActive(false);
            return;
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isQuickSloatOn = !isQuickSloatOn;
            quickSlot.SetActive(isQuickSloatOn);
        }
    }

    public virtual void QuickSlot()
    {
        if (!isQuickSloatOn)
            return;
        quickSlot.transform.position = new Vector2(transform.position.x, transform.position.y + 6);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isQuickSloatOn = false;
            Debug.Log("힐 아이템 사용");
            quickSlot.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isQuickSloatOn = false;
            Debug.Log("마나 아이템 사용");
            quickSlot.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isQuickSloatOn = false;
            Debug.Log("공격 아이템 사용");
            quickSlot.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            isQuickSloatOn = false;
            Debug.Log("상태이상 치유 아이템 사용");
            quickSlot.SetActive(false);
            return; 
        }
    }

    //히트 처리
    public virtual void Hit(float damage, Vector2 monsterPosition)
    {
        if (state == State.dead)
            return;
        if (isCanHit)
            StartCoroutine(Hit_(damage, monsterPosition));
    }
    //히트 기능 
    IEnumerator Hit_(float damage, Vector2 monsterPosition)
    {
        //한번만 실행 되게끔 맞지 않는 상태로 변경
        isCanHit = false;
        //데미지 처리
        currentHp -= damage;
        //히트 애니메이션 
        animator.SetBool("isHit", true);
        //히트시 뒤로 날라가기
        rb.velocity = Vector2.zero;
        float direction = 0;

        if (monsterPosition.x > transform.position.x)
            direction = -1f;

        else if (monsterPosition.x < transform.position.x)
            direction = 1f;

        else
            direction = 0f;

        rb.AddForce(new Vector2(direction, 0) * jumpForce, ForceMode2D.Impulse);


        //히트 애니메이션 길이만큼 기다리기
        yield return new WaitForSeconds(0.1f);
        //히트 애니메이션 길이 만큼 기다린 후 히트 애니메이션 종료
        if (animator.GetBool("isHit"))
            animator.SetBool("isHit", false);
        float hitAnimationLength = animator.GetCurrentAnimatorStateInfo(1).length;
        yield return new WaitForSeconds(hitAnimationLength - 0.1f);
        //히트레이트 만큼 기다려야 하지만 위에 애니메이션 길이만큼 기다렸기 때문에 그 길이만큼 빼주기
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(hitRate - hitAnimationLength);

        //맞는 상태로 변경
        isCanHit = true;
    }

    public virtual void DeadCheck()
    {
        if (currentHp <= 0)
            state = State.dead;
    }

    public virtual void Dead()
    {
        if(!animator.GetBool("isDead"))
        animator.SetBool("isDead", true);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            ladder = collision;
            isCanUseLadder = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isUseLadder = false;
            isCanUseLadder = false;
            rb.gravityScale = 15f;
            animator.SetBool("isLadder", false);
            animator.SetBool("isMoveLadder", false);
            state = State.idle;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(groundCheckPos.position , isGroundLength);
    }
}
