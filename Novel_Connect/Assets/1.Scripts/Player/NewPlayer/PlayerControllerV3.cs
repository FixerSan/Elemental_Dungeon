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
    public Direction direction;

    public StateMachine<PlayerControllerV3> stateMachine= new StateMachine<PlayerControllerV3>();
    public Dictionary<int, State<PlayerControllerV3>> states = new Dictionary<int, State<PlayerControllerV3>>();
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject objectCollider;

    public float canHitDuration;

    bool isCanHit = true;
    public override void GetDamage(float damage)
    {
        if (!isCanHit) return;
        isCanHit = false;
        StartCoroutine(CheckCanHit());
        anim.SetTrigger("HitEffect");
        if (state != PlayerState.Idle) return;
        ChangeState(PlayerState.Hit);
    }

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
        //¿¤¸®¸àÅ»À» °¡Áö°í ÀÖ´ÂÁö Ã¼Å©
        elemental = elemental_;
        playerAttack.skills = new List<Skill<PlayerControllerV3>>();

        switch (elemental)
        {
            case Elemental.Fire:
                playerAttack.skills.Add(new PlayerSkill_Fire.Skill_1());
                playerAttack.skills.Add(new PlayerSkill_Fire.Skill_2());
                anim.runtimeAnimatorController = DataBase.instance.GetAnimatorController(1);
                break;

            case Elemental.Default:
                playerAttack.skills.Clear();
                anim.runtimeAnimatorController = DataBase.instance.GetAnimatorController(0);
                break;
        }

        float curAnimTime = anim.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(SetCanMoveControl(true,curAnimTime));
    }

    public IEnumerator SetCanMoveControl(bool visible,float time)
    {
        yield return new WaitForSeconds(time);
        playerInput.isCanControl = visible;
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
        anim = GetComponent<Animator>();
        playerMovement.Setup(this);
        playerInput.Setup(this);
        playerAttack.Setup(this);

        states.Add((int)PlayerState.Idle, new PlayerControllerV3States.Idle());
        states.Add((int)PlayerState.Walk, new PlayerControllerV3States.Walk());
        states.Add((int)PlayerState.Attack, new PlayerControllerV3States.Attack());
        states.Add((int)PlayerState.Jump, new PlayerControllerV3States.Jump());
        states.Add((int)PlayerState.Fall, new PlayerControllerV3States.Fall());
        states.Add((int)PlayerState.SkillCasting, new PlayerControllerV3States.SkillCasting());
        states.Add((int)PlayerState.Hit, new PlayerControllerV3States.Hit());

        stateMachine.Setup(this,states[(int)PlayerState.Idle]);
        playerData = new PlayerData(level);
        statuses.maxHp = playerData.hp;
        statuses.currentHp = statuses.maxHp;
        statuses.speed = playerData.walkSpeed;
        statuses.force = playerData.force;
    }

    public void Update()
    {
        stateMachine.UpdateState();
        playerMovement.Update();
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
}
public enum PlayerState { Idle, Attack, Walk, Jump, Fall, SkillCasting, Sit, Hit };


public enum Direction { Left, Right };
