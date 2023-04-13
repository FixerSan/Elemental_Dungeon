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

    //그라운드 체크 길이
    public float isGroundLength;
    //그라운드 레이어
    public LayerMask groundLayer;

    //부딪치는 콜라이더
    public GameObject objectCollider;

    //공격 범위 중심 위치
    public Transform attackPos;
    //공격 범위 크기
    public Vector2 attackSize;
    //공격 횟수 초기화 주기
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
        //아래로 체크 길이 만큼 체크, 레이어는 그라운드
        RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, Vector2.down, isGroundLength, groundLayer);

        //그라운드가 체크 되지 않았을 때
        if (hits.Length == 0)
        {
            //그라운드 상태 변경
            isGround = false;
            return;
        }

        //그라운드가 체크 됐을 때
        else
        {
            //그라운드 상태 변경
            isGround = true;
        }
    }
    public void CheckUpAndFall()
    {
        if (!isGround)
        {
            //올라가는 중일 때
            if (rb.velocity.y >= 0.01f)
            {
                if (playerState != PlayerState.Jump)
                    ChangeState(PlayerState.Jump);
            }

            //떨어지는 중일 때
            else if (rb.velocity.y <= -0.01f)
            {
                if (playerState != PlayerState.Fall)
                    ChangeState(PlayerState.Fall);
            }
        }

        //그라운드 상태 일 때
        else
        {
            //떨어지는 중이거나 점프 중이라면 아이들 상태로 변경
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
    //점프 중인지 체크
    public void CheckJump()
    {
        //아래 키를 누르면서 점프를 했을 때 
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //점프 불가능 상태 및 하단 점프 실행
                canJump = false;
                StartCoroutine(DownJump());
                return;
            }
        }
        //그라운드 상태이면서 점프가 가능할 때
        if (isGround && Input.GetKey(KeyCode.Space) && canJump)
        {
            //점프 불가능 상태 및 점프 실행
            canJump = false;
            StartCoroutine(Jump(playerData.jumpForce));
        }
    }
    //점프 코루틴
    public IEnumerator Jump(float jumpForce)
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        canJump = true;
    }
    //하단 점프 코루틴
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
    //방향 전환 함수
    public void ChangeDirection(Direction direction)
    {
        playerDirection = direction;
        if (playerDirection == Direction.Left)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (playerDirection == Direction.Right)
            transform.eulerAngles = new Vector3(0, 0, 0);
    }
    //땅 체크 레이 및 공격 범위 씬에 드로우
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(0, isGroundLength, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackPos.position, attackSize);
    }
    public void Hit(float damage)
    {
        Debug.Log("플레이어 공격 받음");
    }
    public void CheckAttack()
    {
        //공격 애니메이션
        if (Input.GetKey(KeyCode.A))
        {
            StopCoroutine("AttackCoroutine");
            ChangeState(PlayerState.Attack);
        }
    }
    IEnumerator AttackCoroutine()
    {
        //공격 단계에 따른 다른 공격 기능
        switch (animator.GetInteger("AttackCount"))
        {
            case 1: //1 타 공격 
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

            case 2://2 타 공격
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

            case 3: //3타 공격
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

        //설정한 공격 단계 초기화 시간 만큼 기다린 후 공격 단계 초기화
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


//플레이어 상태 이넘
public enum PlayerState { Idle, Attack, Walk, Jump, Fall, SkillCasting};


public enum Direction { Left, Right };
