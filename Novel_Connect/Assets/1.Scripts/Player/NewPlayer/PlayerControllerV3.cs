using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV3 : Actor
{
    public int level;
    public PlayerData playerData;
    public PlayerMovement playerMovement;
    public PlayerInput playerInput;
    public PlayerAttack playerAttack;
    public PlayerState state;

    public StateMachine<PlayerControllerV3> stateMachine= new StateMachine<PlayerControllerV3>();
    public Dictionary<int, State<PlayerControllerV3>> states = new Dictionary<int, State<PlayerControllerV3>>();
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject collisionCollider_Up;
    public GameObject collisionCollider_Down;

    public Transform collisionCollider_Up_pos_Idle;
    public Transform collisionCollider_Up_pos_Bend;

    public float canHitDuration;
    public float playerGravityScale;

    bool isCanHit = true;

    IEnumerator CheckCanHit()
    {
        yield return new WaitForSeconds(canHitDuration);
        isCanHit = true;
    }
    public IEnumerator BackIdle(float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeState(PlayerState.Idle);
    }

    public void ChangeState(PlayerState playerState_)
    {
        stateMachine.ChangeState(states[(int)playerState_]);
    }
    public void ChangeDirection(Direction direction_)
    {
        direction = direction_;
        if (direction == Direction.Left)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (direction == Direction.Right)
            transform.eulerAngles = new Vector3(0, 0, 0);
    }
    public void ChangeElemental(Elemental elemental_)
    {
        playerInput.isCanControl = false;
        ChangeState(PlayerState.Idle);
        playerMovement.Stop();
        //엘리멘탈을 가지고 있는지 체크 코드 적을 자리
        elemental = elemental_;
        playerAttack.skills = new List<Skill<PlayerControllerV3>>();

        switch (elemental)
        {
            case Elemental.Fire:
                StartCoroutine(ChangeElemental_Fire());
                break;

            case Elemental.Default:
                StartCoroutine(ChangeElemental_Default());
                break;
        }
    }

    public IEnumerator ChangeElemental_Default()
    {
        playerAttack.skills.Clear();
        anim.SetTrigger("ChangeExit");
        yield return new WaitForSeconds(0.1f);
        float delay = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(delay-0.1f);
        anim.runtimeAnimatorController = DataBase.instance.GetAnimatorController(0);
        playerInput.isCanControl = true;
    }

    public IEnumerator ChangeElemental_Fire()
    {
        playerAttack.skills.Add(new PlayerSkill_Fire.Skill_1());
        playerAttack.skills.Add(new PlayerSkill_Fire.Skill_2());
        anim.runtimeAnimatorController = DataBase.instance.GetAnimatorController(1);
        AudioSystem.Instance.PlayOneShot(Resources.Load<AudioClip>("AudioClips/Element Fire Transform"));
        yield return new WaitForSeconds(0.2f);
        GameObject startSkill = Instantiate(playerAttack.fireStartSkill);
        startSkill.transform.position = transform.position;
        playerInput.isCanControl = true;
    }
    private void Awake()
    {
        Setup();
        DontDestroyOnLoad(gameObject);
    }

    public override void Setup()
    {
        if(GameManager.instance.player == null)
        {
            GameManager.instance.player = this;
        }
        else
        {
            Destroy(gameObject);
        }
        statuses.Setup(this);
        rb = GetComponent<Rigidbody2D>();
        playerGravityScale = rb.gravityScale;
        anim = GetComponent<Animator>();
        playerMovement.Setup(this);
        playerInput.Setup(this);
        playerAttack.Setup(this);

        playerData = new PlayerData(level);
        statuses.maxHp = playerData.hp;
        statuses.currentHp = statuses.maxHp;
        statuses.speed = playerData.walkSpeed;
        statuses.force = playerData.force;

        collisionCollider_Down = transform.Find("CollisionCollider_Down").gameObject;
        collisionCollider_Up = transform.Find("CollisionCollider_Up").gameObject;

        collisionCollider_Up_pos_Bend = transform.Find("UpColliderPos_Bend");
        collisionCollider_Up_pos_Idle = transform.Find("UpColliderPos_Idle");

        states.Add((int)PlayerState.Idle, new PlayerControllerV3States.Idle());
        states.Add((int)PlayerState.Walk, new PlayerControllerV3States.Walk());
        states.Add((int)PlayerState.Attack, new PlayerControllerV3States.Attack());
        states.Add((int)PlayerState.Jump, new PlayerControllerV3States.Jump());
        states.Add((int)PlayerState.Fall, new PlayerControllerV3States.Fall());
        states.Add((int)PlayerState.SkillCasting, new PlayerControllerV3States.SkillCasting());
        states.Add((int)PlayerState.Hit, new PlayerControllerV3States.Hit());
        states.Add((int)PlayerState.StartLadder, new PlayerControllerV3States.StartLadder());
        states.Add((int)PlayerState.UseLadder, new PlayerControllerV3States.UseLadder());
        states.Add((int)PlayerState.EndLadder, new PlayerControllerV3States.EndLadder());
        states.Add((int)PlayerState.StartBend, new PlayerControllerV3States.StartBend());
        states.Add((int)PlayerState.Bend, new PlayerControllerV3States.Bend());
        states.Add((int)PlayerState.WalkBend, new PlayerControllerV3States.WalkBend());
        states.Add((int)PlayerState.EndBend, new PlayerControllerV3States.EndBend());
        stateMachine.Setup(this,states[(int)PlayerState.Idle]);
    }

    public void Update()
    {
        stateMachine.UpdateState();
        playerMovement.Update();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject Test = Instantiate(Resources.Load<GameObject>("IceBossSkill_1"));

            Test.GetComponent<IceBosSkill_1>().Setup(this);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(playerAttack.attackPos.position, playerAttack.attackPos.localScale);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(playerMovement.checkGroundPos.position, playerMovement.checkGroundSize);
    }

    public override void SetTarget(GameObject target)
    {

    }

    public override void Hit(float damage)
    {
        anim.SetTrigger("HitEffect");
        if (state != PlayerState.Idle) return;
        ChangeState(PlayerState.Hit);
    }
    public override void GetDamage(float damage)
    {
        if (!isCanHit) return;
        isCanHit = false;
        StartCoroutine(CheckCanHit());
        anim.SetTrigger("HitEffect");
        if (state != PlayerState.Idle) return;
        ChangeState(PlayerState.Hit);
    }
}
public enum PlayerState { Idle, Attack, Walk, Jump, Fall, SkillCasting, Sit, Hit , StartLadder, UseLadder, EndLadder, StartBend , Bend , WalkBend , EndBend };


public enum Direction { Left, Right };
