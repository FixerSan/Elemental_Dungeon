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
    public Elemental elemental;

    public StateMachine<PlayerControllerV3> stateMachine;
    public List<State<PlayerControllerV3>> states = new List<State<PlayerControllerV3>>();
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject objectCollider;
    public override void Hit(float damage)
    {

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
        //�� �ٽ� �����~
        ////������Ż�� ������ �ִ��� üũ
        //elemental = elemental_;
        //currentSkills = new List<Skill<PlayerController>>();

        //switch (elemental)
        //{
        //    case Elemental.Fire:
        //        foreach (var item in skills)
        //        {
        //            if (item.skillData.elemental == elemental)
        //                currentSkills.Add(item);
        //            //������Ż ���� �� ������Ż�� �´� �ִϸ��̼� ��Ʈ�ѷ��� ����
        //            animator.runtimeAnimatorController = DataBase.instance.GetAnimatorController(1);
        //        }
        //        break;
        //    case Elemental.Default:
        //        animator.runtimeAnimatorController = DataBase.instance.GetAnimatorController(0);
        //        break;
        //}
    }
    private void Awake()
    {
        Setup();
    }

    public override void Setup()
    {
        base.Setup();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerData = new PlayerData(level);
        playerMovement.Setup(this);
        playerInput.Setup(this);
        playerAttack.Setup(this);
    }
}
