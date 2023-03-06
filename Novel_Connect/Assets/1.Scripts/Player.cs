using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State{idle , attack , walk , run , jump , SkillCasting_1};
    public float walkSpeed, runSpeed;
    public float canAttackDuration;
    public float force;
    public Transform attackPos;
    public Vector2 attackSize;
    public LayerMask attackLayer;
    public AudioClip[] audioClips;


    private AudioSource audioSource;
    private IEnumerator attackCoroutine;
    public State m_State = State.idle;
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private int duration;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = transform.Find("Image").GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_State)
        {
            case State.idle:
                MoveCheck();
                AttackCheck();
                Skill_1Check();
                break;
            case State.attack:
                break;
            case State.walk:
                MoveCheck();
                AttackCheck();
                Skill_1Check();

                break;
            case State.run:
                break;
            case State.jump:
                break;
            case State.SkillCasting_1:
                break;

        }    
    }

    void FixedUpdate()
    {
        switch (m_State)
        {
            case State.idle:
                break;
            case State.attack:
                break;
            case State.walk:
                Move(walkSpeed);
                break;
            case State.run:
                Move(runSpeed);
                break;
            case State.jump:
                break;
            case State.SkillCasting_1:
                break;
        }
    }

    void Skill_1Check()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_State = State.SkillCasting_1;
            StartCoroutine(Skill_1Coroutine());
        }
    }

    IEnumerator Skill_1Coroutine()
    {
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x * 0.1f, m_Rigidbody2D.velocity.y);
        m_Animator.SetBool("isCastingSkill_1", true);
        yield return new WaitForSeconds(0.7f);
        m_State = State.idle;
        m_Animator.SetBool("isCastingSkill_1", false);
    }
    

    void MoveCheck()
    {
        if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
        {

        }

        else if(Input.GetKey(KeyCode.RightArrow))
        {
            m_State = State.walk;
            duration = 1;
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_State = State.walk;
            duration = -1;
        }

        else if(!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            m_State = State.idle;
            m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
            m_Animator.SetBool("isWalking", false); 
        }

        if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if(m_State == State.walk)
            {
                m_State = State.idle;
                m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
                m_Animator.SetBool("isWalking", false);
            }
        }


    }

    void Move(float moveSpeed)
    {
        m_Rigidbody2D.AddForce(Vector2.right * duration * moveSpeed);
        m_Rigidbody2D.velocity = new Vector2(moveSpeed * duration, m_Rigidbody2D.velocity.y);
        m_Animator.SetBool("isWalking", true);
        if(duration == -1)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void AttackCheck()
    {
        if(Input.GetKey(KeyCode.A) )
        {
            if(!m_Animator.GetBool("isAttacking"))
            {
                m_State = State.attack; 
                if (m_Animator.GetInteger("AttackCount") < 3)
                {
                    m_Animator.SetInteger("AttackCount", m_Animator.GetInteger("AttackCount") + 1);
                }

                else
                    m_Animator.SetInteger("AttackCount", 1);
            }

            m_Animator.SetBool("isAttacking", true);
            if(attackCoroutine == null)
            {
                attackCoroutine = Attack();
                StartCoroutine(attackCoroutine);
            }

            else
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
                attackCoroutine = Attack();
                StartCoroutine(attackCoroutine);
            }
        }
    }
    IEnumerator Attack()
    {
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x * 0.1f , m_Rigidbody2D.velocity.y);
        audioSource.PlayOneShot(audioClips[0]);
        switch(m_Animator.GetInteger("AttackCount"))
        {
            case 1:
                yield return new WaitForSeconds(0.1f);
                //공격 코드 공간
                Collider2D[] collider2Ds_1 = Physics2D.OverlapBoxAll(attackPos.position, attackSize, 0, attackLayer);
                foreach(Collider2D coll in collider2Ds_1)
                {
                    if(coll.CompareTag("Monster"))
                    {
                        coll.GetComponent<Monster>().Hit(force);
                    }
                }
                yield return new WaitForSeconds(0.3f);
                m_State = State.idle;
                break;

            case 2:
                yield return new WaitForSeconds(0.1f);
                //공격 코드 공간
                Collider2D[] collider2Ds_2 = Physics2D.OverlapBoxAll(attackPos.position, attackSize, 0, attackLayer);
                foreach (Collider2D coll in collider2Ds_2)
                {
                    if (coll.CompareTag("Monster"))
                    {
                        coll.GetComponent<Monster>().Hit(force);
                    }
                }
                yield return new WaitForSeconds(0.3f);
                m_State = State.idle;
                break;

            case 3:
                yield return new WaitForSeconds(0.1f);
                //공격 코드 공간
                Collider2D[] collider2Ds_3 = Physics2D.OverlapBoxAll(attackPos.position, attackSize, 0, attackLayer);
                foreach (Collider2D coll in collider2Ds_3)
                {
                    if (coll.CompareTag("Monster"))
                    {
                        coll.GetComponent<Monster>().Hit(force);
                    }
                }
                yield return new WaitForSeconds(0.3f);
                m_State = State.idle;
                break;

            default:
                break;
        }

        m_Animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(canAttackDuration);
        m_Animator.SetInteger("AttackCount", 0);
        attackCoroutine = null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackPos.position, attackSize);
    }
}
