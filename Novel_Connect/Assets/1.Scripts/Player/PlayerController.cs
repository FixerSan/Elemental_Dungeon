using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHitable
{
    public PlayerData playerData;
    #region Singleton, DontDestoryOnLoad;
    private static PlayerController Instance;
    public static PlayerController instance
    {
        get
        {
            if (Instance != null)
                return Instance;
            else
                return null;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);

        AddState();
        stateMachine.Setup(this, states[(int)PlayerState.Idle]);

    }
    #endregion

    public List<State<PlayerController>> states = new List<State<PlayerController>>();
    public StateMachine<PlayerController> stateMachine = new StateMachine<PlayerController>();
    public PlayerState playerState;
    public Elemental elemental = Elemental.Default;
    public Direction playerDirection;
    public bool isGround = false;
    public bool isCanSliding = false;

    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask attackLayer;

    //�׶��� üũ ����
    public float isGroundLength;
    //�׶��� ���̾�
    public LayerMask groundLayer;

    //�ε�ġ�� �ݶ��̴�
    public GameObject objectCollider;

    //���� ���� �߽� ��ġ
    public Transform attackPos;
    //���� ���� ũ��
    public Vector2 attackSize;
    //���� Ƚ�� �ʱ�ȭ �ֱ�
    public float canAttackDuration;

    private bool canJump = true;
    public bool canControl = true;
    public float m_Time;

    public List<Skill<PlayerController>> skills = new List<Skill<PlayerController>>();

    private void Update()
    {
        CheckIsGround();
        CheckUpAndFall();
        CheckSkillCanUse();
        stateMachine.UpdateState();
    }
    public void CheckIsGround()
    {
        //�Ʒ��� üũ ���� ��ŭ üũ, ���̾�� �׶���
        RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, Vector2.down, isGroundLength, groundLayer);

        //�׶��尡 üũ ���� �ʾ��� ��
        if (hits.Length == 0)
        {
            //�׶��� ���� ����
            isGround = false;
            return;
        }

        //�׶��尡 üũ ���� ��
        else
        {
            //�׶��� ���� ����
            isGround = true;
        }
    }
    public void CheckUpAndFall()
    {
        if (!isGround)
        {
            //�ö󰡴� ���� ��
            if (rb.velocity.y >= 0.01f)
            {
                if (playerState != PlayerState.Jump)
                    ChangeState(PlayerState.Jump);
            }

            //�������� ���� ��
            else if (rb.velocity.y <= -0.01f)
            {
                if (playerState != PlayerState.Fall)
                    ChangeState(PlayerState.Fall);
            }
        }

        //�׶��� ���� �� ��
        else
        {
            //�������� ���̰ų� ���� ���̶�� ���̵� ���·� ����
            if (playerState == PlayerState.Fall || playerState == PlayerState.Jump)
            {
                if(isCanSliding)
                {
                    ChangeState(PlayerState.Idle);
                }

                else
                {
                    ChangeState(PlayerState.Idle);
                    Stop();
                }
            }
        }
    }
    public void CheckMove()
    {
        if (!canControl)
            return;

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            if (playerState != PlayerState.Idle)
                ChangeState(PlayerState.Idle);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            ChangeDirection(Direction.Right);
            if (playerState != PlayerState.Walk)
                ChangeState(PlayerState.Walk);
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            ChangeDirection(Direction.Left);
            if (playerState != PlayerState.Walk)
                ChangeState(PlayerState.Walk);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (playerState == PlayerState.Walk)
                Stop();
            ChangeState(PlayerState.Idle);
        }
    }
    public void CheckJumpMove()
    {
        if (!canControl)
            return;

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ChangeDirection(Direction.Left);
            Move();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            ChangeDirection(Direction.Right);
            Move();
        }
    }
    public void Move()
    {
        switch (playerState)
        {
            case PlayerState.Walk:
                if (playerDirection == Direction.Right)
                {
                    rb.velocity = new Vector2(playerData.walkSpeed, rb.velocity.y);
                }
                else if (playerDirection == Direction.Left)
                {
                    rb.velocity = new Vector2(-playerData.walkSpeed, rb.velocity.y);
                }
                break;

            case PlayerState.Jump:
                if (playerDirection == Direction.Right)
                {
                    rb.AddForce(Vector2.right * playerData.jumpMoveForce * Time.fixedDeltaTime);
                    if (rb.velocity.x > playerData.walkSpeed)
                        rb.velocity = new Vector2(playerData.walkSpeed, rb.velocity.y);
                }
                else if (playerDirection == Direction.Left)
                {
                    rb.AddForce(Vector2.left * playerData.jumpMoveForce * Time.fixedDeltaTime);
                    if (rb.velocity.x < -playerData.walkSpeed)
                        rb.velocity = new Vector2(-playerData.walkSpeed, rb.velocity.y);
                }
                break;

            case PlayerState.Fall:
                if (playerDirection == Direction.Right)
                {
                    rb.AddForce(Vector2.right * playerData.jumpMoveForce * Time.fixedDeltaTime);
                    if (rb.velocity.x > playerData.walkSpeed)
                        rb.velocity = new Vector2(playerData.walkSpeed, rb.velocity.y);
                }
                else if (playerDirection == Direction.Left)
                {
                    rb.AddForce(Vector2.left * playerData.jumpMoveForce * Time.fixedDeltaTime);
                    if (rb.velocity.x < -playerData.walkSpeed)
                        rb.velocity = new Vector2(-playerData.walkSpeed, rb.velocity.y);
                }
                break;

        }
    }
    public void Stop()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    //���� ������ üũ
    public void CheckJump()
    {
        //�Ʒ� Ű�� �����鼭 ������ ���� �� 
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //���� �Ұ��� ���� �� �ϴ� ���� ����
                canJump = false;
                StartCoroutine(DownJump());
                return;
            }
        }
        //�׶��� �����̸鼭 ������ ������ ��
        if (isGround && Input.GetKey(KeyCode.Space) && canJump)
        {
            //���� �Ұ��� ���� �� ���� ����
            canJump = false;
            StartCoroutine(Jump(playerData.jumpForce));
        }
    }
    //���� �ڷ�ƾ
    public IEnumerator Jump(float jumpForce)
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        canJump = true;
    }
    //�ϴ� ���� �ڷ�ƾ
    public IEnumerator DownJump()
    {
        objectCollider.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        canJump = true;
        objectCollider.SetActive(true);
    }
    public void CheckSkillCanUse()
    {
        if (skills == null)
            return;
        foreach (var item in skills)
        {
            item.CheckCanUse();
        }
    }
    public void AddState()
    {
        //states[(int)PlayerState.Idle] = new PlayerStates.Idle();
        //states[(int)PlayerState.Attack] = new PlayerStates.Attack();
        //states[(int)PlayerState.Walk] = new PlayerStates.Walk();
        //states[(int)PlayerState.Jump] = new PlayerStates.Jump();
        //states[(int)PlayerState.Fall] = new PlayerStates.Fall();
        //states[(int)PlayerState.SkillCasting] = new PlayerStates.SkillCasting();


        states.Add(new PlayerStates.Idle());
        states.Add(new PlayerStates.Attack());
        states.Add(new PlayerStates.Walk());
        states.Add(new PlayerStates.Jump());
        states.Add(new PlayerStates.Fall());
        states.Add(new PlayerStates.SkillCasting());

    }
    public void AddSkill(int index)
    {
        switch(index)
        {
            case 0:
                PlayerSkill_Fire.Skill_1 skill_1 = new PlayerSkill_Fire.Skill_1();
                skills.Add(skill_1);
                skill_1.Setup(this,index);
                break;

            case 1:
                PlayerSkill_Fire.Skill_2 skill_2 = new PlayerSkill_Fire.Skill_2();
                skills.Add(skill_2);
                skill_2.Setup(this, index);
                break;

        }
    }
    public void RemoveSkill(int index)
    {
        foreach (var item in skills)
        {
            if (item.skillData.index == index)
                skills.Remove(item);
        }
    }
    public void RemoveAllSkill()
    {
        for (int i = skills.Count; i > 0; i--)
        {
            skills.Remove(skills[i]);
        }
    }
    public void ChangeState(PlayerState playerState_)
    {
        stateMachine.ChangeState(states[(int)playerState_]);
    }
    //���� ��ȯ �Լ�
    public void ChangeDirection(Direction direction)
    {
        playerDirection = direction;
        if (playerDirection == Direction.Left)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (playerDirection == Direction.Right)
            transform.eulerAngles = new Vector3(0, 0, 0);
    }
    //�� üũ ���� �� ���� ���� ���� ��ο�
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(0, isGroundLength, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackPos.position, attackSize);
    }
    public void Hit(float damage)
    {
        Debug.Log("�÷��̾� ���� ����");
    }
    public void CheckAttack()
    {
        //���� �ִϸ��̼�
        if (Input.GetKey(KeyCode.A))
        {
            StopCoroutine("AttackCoroutine");
            ChangeState(PlayerState.Attack);
        }
    }
    IEnumerator AttackCoroutine()
    {
        //���� �ܰ迡 ���� �ٸ� ���� ���
        switch (animator.GetInteger("AttackCount"))
        {
            case 1: //1 Ÿ ���� 
                yield return new WaitForSeconds(0.2f);
                Collider2D[] collider2Ds_1 = Physics2D.OverlapBoxAll(attackPos.position, attackSize, 0, attackLayer);
                foreach (Collider2D hitTarget in collider2Ds_1)
                {
                    if (hitTarget.GetComponent<IHitable>() != null)
                    {
                        BattleSystem.instance.Calculate(elemental,hitTarget.GetComponent<IHitable>().GetElemental(), hitTarget.GetComponent<IHitable>(),playerData.force);
                    }
                }
                yield return new WaitForSeconds(0.2f);
                break;

            case 2://2 Ÿ ����
                yield return new WaitForSeconds(0.1f);
                Collider2D[] collider2Ds_2 = Physics2D.OverlapBoxAll(attackPos.position, attackSize, 0, attackLayer);
                foreach (Collider2D hitTarget in collider2Ds_2)
                {
                    if (hitTarget.GetComponent<IHitable>() != null)
                    {
                        BattleSystem.instance.Calculate(elemental, hitTarget.GetComponent<IHitable>().GetElemental(), hitTarget.GetComponent<IHitable>(), playerData.force);
                    }
                }
                yield return new WaitForSeconds(0.3f);
                break;

            case 3: //3Ÿ ����
                yield return new WaitForSeconds(0.3f);
                Collider2D[] collider2Ds_3 = Physics2D.OverlapBoxAll(attackPos.position, attackSize, 0, attackLayer);
                foreach (Collider2D hitTarget in collider2Ds_3)
                {
                    if (hitTarget.GetComponent<IHitable>() != null)
                    {
                        BattleSystem.instance.Calculate(elemental, hitTarget.GetComponent<IHitable>().GetElemental(), hitTarget.GetComponent<IHitable>(), playerData.force);
                    }
                }
                yield return new WaitForSeconds(0.2f);
                break;
        }
        ChangeState(PlayerState.Idle);

        //������ ���� �ܰ� �ʱ�ȭ �ð� ��ŭ ��ٸ� �� ���� �ܰ� �ʱ�ȭ
        yield return new WaitForSeconds(canAttackDuration);
        animator.SetInteger("AttackCount", 0);
    }
    IEnumerator AttackMoveCoroutine()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            switch(playerDirection)
            {
                case Direction.Left:
                    rb.AddForce(Vector2.left *playerData.attackMoveForce, ForceMode2D.Impulse);
                    break;

                case Direction.Right:
                    rb.AddForce(Vector2.right * playerData.attackMoveForce, ForceMode2D.Impulse);
                    break;
            }
            yield return new WaitForSeconds(0.1f);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    public Elemental GetElemental()
    {
        return elemental;
    }
}


//�÷��̾� ���� �̳�
public enum PlayerState { Idle, Attack, Walk, Jump, Fall, SkillCasting};


public enum Direction { Left, Right };
