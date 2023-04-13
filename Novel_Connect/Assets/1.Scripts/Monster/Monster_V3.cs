using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_V3 : MonoBehaviour
{
        public enum State { idle, walk, detect, attack, hit, dead }

        protected Animator animator;
        protected SpriteRenderer spriteRenderer;
        protected IEnumerator hitCoroutine;
        protected IEnumerator moveStopCoroutine;
        protected IEnumerator attackCoroutine;
        protected Rigidbody2D rb;
        protected int direction = -1;  //-1 ���� , 1 ������ ���� �̵�
        protected Vector2 startPosition;
        protected bool isFirst = true;


        [Header("[ Monster Stats ]")]
        [Tooltip("ü��")]
        public float hp = 10;
        [Tooltip("���ݷ�")]
        public float force = 10;
        public float speed;
        public float maxSpeed;
        public State state = State.idle;


        [Space(10f)]
        [Header("[ Death Effect ]")]
        [Tooltip("���� ���� ������� ȿ���� ���� �� ������")]
        public float deathEffectDelay;
        [Tooltip("�� ���� ���� ����� ������")]
        public float deathEffectCount;
        [Tooltip("���������� �ɸ��� �ð�")]
        public float deathEffectTime;


        [Space(10f)]
        [Header("[ ETC ]")]
        [Tooltip("������ �� �ִ� �Ÿ�")]
        public float moveRange;
        [Tooltip("������ �ٲٱ� �� �����ִ� �ð�")]
        public float moveDirectionChangeDelay;
        public float canMoveDirectionChangeDistance;
        [Space(20f)]
        [Header("[ Attack ]")]
        public LayerMask attackLayer;
        public Transform attackPosition;
        public Vector2 attackSize;
        public float canAttackRange_X;
        public float canAttackRange_Y;


        public virtual void Start()
        {
            animator = transform.Find("Sprite").GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = transform.parent.GetComponent<Rigidbody2D>();
            startPosition = transform.parent.position;
            state = State.walk;
        }

        public virtual void Update()
        {
            switch (state)
            {
                case State.idle:
                    DeathCheck();


                    break;

                case State.walk:
                    Move_AreaCheck();
                    DeathCheck();

                    break;

                case State.detect:
                    DeathCheck();
                    AttackCheck();

                    break;

                case State.attack:
                    DeathCheck();
                    AttackCheck();

                    break;

                case State.hit:
                    DeathCheck();


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

                case State.detect:
                    Move();
                    Move_Deceleration();
                    FollowPlayer();

                    break;

                case State.attack:
                    Attack();

                    break;

                case State.hit:


                    break;

                case State.dead:
                    Death();

                    break;
            }
        }

        public virtual void Move()
        {
            if (!animator.GetBool("isWalk"))
                animator.SetBool("isWalk", true);
            rb.AddForce(speed * direction * Vector2.right, ForceMode2D.Impulse);
        }

        public virtual void Move_Deceleration()
        {
            //�̵��ӵ� ����
            if (rb.velocity.x > maxSpeed)
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);

            if (rb.velocity.x < maxSpeed * (-1))
                rb.velocity = new Vector2(maxSpeed * (-1), rb.velocity.y);
        }

        public virtual void Move_AreaCheck()         //�̵� �Ÿ� ����
        {
            if (transform.parent.position.x > startPosition.x + moveRange)      //�̵� ���� �Ÿ����� �̵� ���� ��
            {
                state = State.idle;
                animator.SetBool("isWalk", false);      //�̵� ���� ����
                rb.velocity = Vector2.zero;         //�̵� ��ν�Ƽ ����
                                                    //�ڵ尡 �ٽ� ������� �ʵ��� �ణ ������ �̵�
                transform.parent.position = new Vector3(startPosition.x + moveRange - 0.1f, transform.position.y, transform.position.z);

                //���� ��ȯ �ڷ�ƾ�� ���������� �ʴٸ� 
                if (moveStopCoroutine == null)
                {
                    //�ڷ�ƾ ����
                    moveStopCoroutine = Move_Stop();
                    StartCoroutine(moveStopCoroutine);
                }

                //���� ��ȯ �ڸ�ƾ�� �������̶��
                else
                {
                    //�ڷ�ƾ ���� �� �ڷ�ƾ ����
                    StopCoroutine(moveStopCoroutine);
                    moveStopCoroutine = Move_Stop();
                    StartCoroutine(moveStopCoroutine);
                }
            }

            if (transform.parent.position.x < startPosition.x - moveRange)     //���� �Ȱ���
            {
                state = State.idle;
                animator.SetBool("isWalk", false);
                rb.velocity = Vector2.zero;
                transform.parent.position = new Vector3(startPosition.x - moveRange + 0.1f, transform.position.y, transform.position.z);
                if (moveStopCoroutine == null)
                {
                    moveStopCoroutine = Move_Stop();
                    StartCoroutine(moveStopCoroutine);
                }

                else if (moveStopCoroutine != null)
                {
                    StopCoroutine(moveStopCoroutine);
                    moveStopCoroutine = Move_Stop();
                    StartCoroutine(moveStopCoroutine);
                }
            }
        }

        protected IEnumerator Move_Stop()         //�̵� �Ÿ� ���� �Լ����� �����Ű�� �̵� ���� �ڷ�ƾ
        {
            //������ ���� �ð���ŭ ��ٸ� ��
            yield return new WaitForSeconds(moveDirectionChangeDelay);
            //���� ��ȯ 
            if (direction >= 1)
            {
                direction = -1;
                transform.eulerAngles = new Vector3(0, 0, 0);

            }
            else if (direction <= 1)
            {
                direction = 1;
                transform.eulerAngles = new Vector3(0, 180, 0);

            }

            //�ȱ� ó�� ����
            state = State.walk;
            animator.SetBool("isWalk", true);
            moveStopCoroutine = null;
        }

        public virtual void FollowPlayer()
        {
            //�÷��̾ ���� �������ش�
            Transform player = GameObject.FindWithTag("Player").transform;
            //���� �� ���¿��� ���� ����Ī�� �߻��Ѵٸ� ���� ���� ����
            if (player == null)
                return;

            if (Mathf.Abs(player.transform.position.x - transform.position.x) <= canMoveDirectionChangeDistance) // ĳ���Ϳ� ������ �Ÿ��� ���� �Ÿ� ���� �־����� ���� ����
                return;

            if (transform.position.y > player.transform.position.y && Mathf.Abs(transform.position.y - player.position.y) > canAttackRange_Y)
            {
                if (isFirst == true)
                {
                    isFirst = false;
                    if (player.transform.position.x > transform.position.x)
                    {
                        direction = 1;
                        transform.eulerAngles = new Vector3(0, 180, 0);
                        if (moveStopCoroutine != null)
                            StopCoroutine(moveStopCoroutine);
                    }

                    else if (player.transform.position.x < transform.position.x)
                    {
                        direction = -1;
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        if (moveStopCoroutine != null)
                            StopCoroutine(moveStopCoroutine);
                    }
                }

                else
                    return;
            }

            else
            {
                if (player.transform.position.x > transform.position.x)
                {
                    direction = 1;
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    if (moveStopCoroutine != null)
                        StopCoroutine(moveStopCoroutine);
                }

                else if (player.transform.position.x < transform.position.x)
                {
                    direction = -1;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    if (moveStopCoroutine != null)
                        StopCoroutine(moveStopCoroutine);
                }
            }
        }

        public virtual void DeathCheck()     //���� ó��
        {
            if (hp <= 0)
                state = State.dead;
        }

        public virtual void Death()
        {
            if (moveStopCoroutine != null)
            {
                StopCoroutine(moveStopCoroutine);
                moveStopCoroutine = null;
            }

            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }

            if (hitCoroutine != null)
            {
                StopCoroutine(hitCoroutine);
                hitCoroutine = null;
            }

            if (!animator.GetBool("isDead"))
            {
                animator.SetBool("isDead", true); //���� ���·� ����
                rb.velocity = Vector2.zero;
                StartCoroutine(DeathEffect()); //���� ȿ�� ����
            }

        }

        IEnumerator DeathEffect()       //���� ȿ��
        {

            yield return new WaitForSeconds(deathEffectDelay);      //������ŭ ���� ȿ�� ���
            for (int i = 0; i < deathEffectCount; i++)      //���� ȿ�� ī��Ʈ��ŭ �ݺ�
            {
                //ī��Ʈ�� ����ؼ� ���� ����
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - 1 / deathEffectCount);

                //���� ȿ�� �ð��� ���߱� ���� ���
                yield return new WaitForSeconds(deathEffectTime / deathEffectCount);
            }

            //�ݺ����� ������ ������Ʈ �ı�
            Destroy(gameObject);

        }

        public virtual void AttackCheck()
        {
            //�÷��̾ ���� �������ش�
            Transform player = GameObject.FindWithTag("Player").transform;
            //���� �� ���¿��� ���� ����Ī�� �߻��Ѵٸ� ���� ���� ����
            if (player == null)
                return;

            //���� �÷��̾�� ������ �Ÿ��� ������ ��ġ ��ŭ �������� ��
            if (Mathf.Abs(transform.position.x - player.position.x) <= canAttackRange_X && Mathf.Abs(transform.position.y - player.position.y) <= canAttackRange_Y)
                state = State.attack;

            else
                state = State.detect;
        }

        public virtual void Attack()
        {
            //���������� �ʴٸ� ����
            if (attackCoroutine == null)
            {
                attackCoroutine = AttackCoroutine();
                StartCoroutine(attackCoroutine);
            }
        }

        IEnumerator AttackCoroutine()
        {
            //���� �ִϸ��̼� ���� �� �����̱� ����
            animator.SetBool("isAttack", true);
            animator.SetBool("isWalk", false);
            //���� �� �� �̵� ����
            rb.velocity = new Vector2(0, rb.velocity.y);
            //���� �ִϸ��̼� ���� ����
            yield return new WaitForSeconds(0.01f);
            float animLength = animator.GetCurrentAnimatorStateInfo(0).length;
            //���� �ִϸ��̼� �� ���� ������ �� ����
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / 8 * 5);
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackPosition.position, attackSize, 0, attackLayer);
            foreach (Collider2D col in collider2Ds)
            {
                if (col.CompareTag("PlayerHit"))
                    col.transform.parent.GetComponent<Player_V2>().Hit(force, transform.position);
            }

            //������ ���� �ִϸ��̼��� ������
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / 8 * 3 - 0.01f);
            //�ȱ�, �ִϸ��̼� ����
            animator.SetBool("isAttack", false);
            animator.SetBool("isWalk", true);

            state = State.detect;
            //���� �ڷ� ƾ����
            attackCoroutine = null;
        }

        public virtual void Hit(float damage)       //�¾��� ��
        {
            if (state == State.dead)     //�׾������� �ڵ� ó�� ����
                return;

            if (moveStopCoroutine != null)
            {
                StopCoroutine(moveStopCoroutine);
                moveStopCoroutine = null;
            }


            animator.SetTrigger("hitAnimation");
            state = State.hit;
            if (hitCoroutine == null)         //���� �������� ��Ʈ �ڷ�ƾ�� ���� ��
            {
                //�ڷ�ƾ ����
                hitCoroutine = Hit_(damage);
                StartCoroutine(hitCoroutine);
            }


            else // ��Ʈ �ڷ�ƾ�� ���� ��
            {
                //��Ʈ �ִϸ��̼��� ��ٸ��� �ʰ� �ڷ�ƾ�� �ִϸ��̼��� ����
                StopCoroutine(hitCoroutine);
                //�ڷ�ƾ ����
                hitCoroutine = Hit_(damage);
                StartCoroutine(hitCoroutine);
            }

            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        }

        protected IEnumerator Hit_(float damage)      //��Ʈ ȿ��
        {
            hp -= damage; //������ ��ŭ ü�� ���
            rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(0.01f);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(1).length - 0.01f); //��Ʈ �ִϸ��̼� ���̸�ŭ ��ٸ���
            hitCoroutine = null;        //��Ʈ �ڷ�ƾ �ʱ�ȭ
            state = State.detect;

        }




        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(attackPosition.position, attackSize);
        }
    
}
