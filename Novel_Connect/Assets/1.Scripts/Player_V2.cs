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

    [Tooltip("�������� �ӵ� ����")]
    public float speed;
    [Tooltip("�ִ� �ӵ� ����")]
    public float maxSpeed;
    [Tooltip("���� �Ŀ�")]
    public float jumpForce;
    [Tooltip("ü��")]
    public float maxHp = 10;
    public float currentHp;
    [Tooltip("���ݷ�")]
    public float attackForce;
    public float maxMp;
    public float currentMp;
    public float breakingCount = 4;
    public GameObject quickSlot;
    public AudioClip potionAudioClip;


    [Space(20f)]
    [Header("[ Attack ]")]
    [Tooltip("��� ���� ����")]
    public int attackCount;
    [Tooltip("���� ���ݱ��� �� �� �ִ� �ð�")]
    public float canAttackDuration;
    [Tooltip("������ ���� �Ǻ�")]
    public Transform attackPoint;
    [Tooltip("������ ���� ũ��")]
    public Vector2 attackSize;
    [Tooltip("������ ���̾�")]
    public LayerMask AttackLayer;

    [Space(20f)]
    [Header("[ Etc ]")]
    [Tooltip("�ٴ� üũ �Ÿ�")]
    public Vector2 isGroundLength;
    public Transform groundCheckPos;
    [Tooltip("�°� ���� ���� �ð�")]
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
        //������
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

        //������
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

    public virtual void Move_Deceleration()        //�ִ�ӵ� ����
    {
        //�̵��ӵ� ����
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
        //���� �پ� ���� ��
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
                //��ٸ� ��� ���� ����
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
        //��ٸ� �ִϸ��̼� ����
        if (!animator.GetBool("isLadder"))
            animator.SetBool("isLadder", true);

        if (animator.GetBool("isWalk"))
            animator.SetBool("isWalk", false);

        //���� ���� Ű�� ������ ����
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (animator.GetBool("isJump"))
                animator.SetBool("isJump", false);
            //��ٸ� ���� �ִϸ��̼� ����
            if (!animator.GetBool("isMoveLadder"))
                animator.SetBool("isMoveLadder", true);
            //���� �ö�� ��ɰ� �ö󰡴� �ӵ� ����
            rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
            if (rb.velocity.y >= maxSpeed * 0.3f)
                rb.velocity = new Vector2(0, maxSpeed * 0.3f);
        }


        //���� �ö󰡴� Ű�� ���� ��
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            animator.SetBool("isMoveLadder", false); //��ٸ� ���� �ִϸ��̼� ����
            rb.velocity = Vector2.zero; //�߷��� �����ϱ� �ѹ� ���ν�Ƽ �ʱ�ȭ
        }


        //�Ʒ��� ���� Ű�� ������ ����
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (animator.GetBool("isJump"))
                animator.SetBool("isJump", false);
            //��ٸ� ���� �ִϸ��̼� ����
            if (!animator.GetBool("isMoveLadder"))
                animator.SetBool("isMoveLadder", true);
            //�Ʒ��� �������� ��ɰ� �������� �ӵ� ����
            rb.AddForce(Vector2.down * speed, ForceMode2D.Impulse);
            if (rb.velocity.y <= -maxSpeed * 0.3f)
                rb.velocity = new Vector2(0, -maxSpeed * 0.3f);
        }

        //�Ʒ��� ���� Ű�� ���� ��
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool("isMoveLadder", false); //��ٸ� ���� �ִϸ��̼� ����
            rb.velocity = Vector2.zero; //�߷��� �����ϱ� �ѹ� ���ν�Ƽ �ʱ�ȭ
        }

        //���� Ű�� ������ ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //���� �������� ����
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
            Debug.Log("�� ������ ���");
            quickSlot.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isQuickSloatOn = false;
            Debug.Log("���� ������ ���");
            quickSlot.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isQuickSloatOn = false;
            Debug.Log("���� ������ ���");
            quickSlot.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            isQuickSloatOn = false;
            Debug.Log("�����̻� ġ�� ������ ���");
            quickSlot.SetActive(false);
            return; 
        }
    }

    //��Ʈ ó��
    public virtual void Hit(float damage, Vector2 monsterPosition)
    {
        if (state == State.dead)
            return;
        if (isCanHit)
            StartCoroutine(Hit_(damage, monsterPosition));
    }
    //��Ʈ ��� 
    IEnumerator Hit_(float damage, Vector2 monsterPosition)
    {
        //�ѹ��� ���� �ǰԲ� ���� �ʴ� ���·� ����
        isCanHit = false;
        //������ ó��
        currentHp -= damage;
        //��Ʈ �ִϸ��̼� 
        animator.SetBool("isHit", true);
        //��Ʈ�� �ڷ� ���󰡱�
        rb.velocity = Vector2.zero;
        float direction = 0;

        if (monsterPosition.x > transform.position.x)
            direction = -1f;

        else if (monsterPosition.x < transform.position.x)
            direction = 1f;

        else
            direction = 0f;

        rb.AddForce(new Vector2(direction, 0) * jumpForce, ForceMode2D.Impulse);


        //��Ʈ �ִϸ��̼� ���̸�ŭ ��ٸ���
        yield return new WaitForSeconds(0.1f);
        //��Ʈ �ִϸ��̼� ���� ��ŭ ��ٸ� �� ��Ʈ �ִϸ��̼� ����
        if (animator.GetBool("isHit"))
            animator.SetBool("isHit", false);
        float hitAnimationLength = animator.GetCurrentAnimatorStateInfo(1).length;
        yield return new WaitForSeconds(hitAnimationLength - 0.1f);
        //��Ʈ����Ʈ ��ŭ ��ٷ��� ������ ���� �ִϸ��̼� ���̸�ŭ ��ٷȱ� ������ �� ���̸�ŭ ���ֱ�
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(hitRate - hitAnimationLength);

        //�´� ���·� ����
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
