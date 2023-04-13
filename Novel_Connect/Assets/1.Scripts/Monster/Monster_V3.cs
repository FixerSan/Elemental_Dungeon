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
        protected int direction = -1;  //-1 왼쪽 , 1 오른쪽 으로 이동
        protected Vector2 startPosition;
        protected bool isFirst = true;


        [Header("[ Monster Stats ]")]
        [Tooltip("체력")]
        public float hp = 10;
        [Tooltip("공격력")]
        public float force = 10;
        public float speed;
        public float maxSpeed;
        public State state = State.idle;


        [Space(10f)]
        [Header("[ Death Effect ]")]
        [Tooltip("죽은 다음 사라지는 효과로 가기 전 딜레이")]
        public float deathEffectDelay;
        [Tooltip("몇 번을 나눠 사라질 것인지")]
        public float deathEffectCount;
        [Tooltip("사라지기까지 걸리는 시간")]
        public float deathEffectTime;


        [Space(10f)]
        [Header("[ ETC ]")]
        [Tooltip("움직일 수 있는 거리")]
        public float moveRange;
        [Tooltip("방향을 바꾸기 전 멈춰있는 시간")]
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
            //이동속도 제한
            if (rb.velocity.x > maxSpeed)
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);

            if (rb.velocity.x < maxSpeed * (-1))
                rb.velocity = new Vector2(maxSpeed * (-1), rb.velocity.y);
        }

        public virtual void Move_AreaCheck()         //이동 거리 제한
        {
            if (transform.parent.position.x > startPosition.x + moveRange)      //이동 금지 거리까지 이동 했을 때
            {
                state = State.idle;
                animator.SetBool("isWalk", false);      //이동 상태 종료
                rb.velocity = Vector2.zero;         //이동 밸로시티 종료
                                                    //코드가 다시 실행되지 않도록 약간 전으로 이동
                transform.parent.position = new Vector3(startPosition.x + moveRange - 0.1f, transform.position.y, transform.position.z);

                //방향 전환 코루틴이 실행중이지 않다면 
                if (moveStopCoroutine == null)
                {
                    //코루틴 실행
                    moveStopCoroutine = Move_Stop();
                    StartCoroutine(moveStopCoroutine);
                }

                //방향 전환 코르틴이 실행중이라면
                else
                {
                    //코루틴 종료 후 코루틴 실행
                    StopCoroutine(moveStopCoroutine);
                    moveStopCoroutine = Move_Stop();
                    StartCoroutine(moveStopCoroutine);
                }
            }

            if (transform.parent.position.x < startPosition.x - moveRange)     //위랑 똑같음
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

        protected IEnumerator Move_Stop()         //이동 거리 제한 함수에서 실행시키는 이동 멈춤 코루틴
        {
            //지정한 멈출 시간만큼 기다린 후
            yield return new WaitForSeconds(moveDirectionChangeDelay);
            //방향 전환 
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

            //걷기 처리 시작
            state = State.walk;
            animator.SetBool("isWalk", true);
            moveStopCoroutine = null;
        }

        public virtual void FollowPlayer()
        {
            //플레이어를 적을 지정해준다
            Transform player = GameObject.FindWithTag("Player").transform;
            //지정 한 상태에서 만약 스위칭이 발생한다면 생길 문제 방지
            if (player == null)
                return;

            if (Mathf.Abs(player.transform.position.x - transform.position.x) <= canMoveDirectionChangeDistance) // 캐릭터와 몬스터의 거리가 일정 거리 보다 멀어졌을 때만 실행
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

        public virtual void DeathCheck()     //죽음 처리
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
                animator.SetBool("isDead", true); //죽음 상태로 변경
                rb.velocity = Vector2.zero;
                StartCoroutine(DeathEffect()); //죽음 효과 실행
            }

        }

        IEnumerator DeathEffect()       //죽음 효과
        {

            yield return new WaitForSeconds(deathEffectDelay);      //변수만큼 죽음 효과 대기
            for (int i = 0; i < deathEffectCount; i++)      //죽음 효과 카운트만큼 반복
            {
                //카운트에 비례해서 투명도 조절
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - 1 / deathEffectCount);

                //죽음 효과 시간에 맞추기 위해 대기
                yield return new WaitForSeconds(deathEffectTime / deathEffectCount);
            }

            //반복문이 끝나면 오브젝트 파괴
            Destroy(gameObject);

        }

        public virtual void AttackCheck()
        {
            //플레이어를 적을 지정해준다
            Transform player = GameObject.FindWithTag("Player").transform;
            //지정 한 상태에서 만약 스위칭이 발생한다면 생길 문제 방지
            if (player == null)
                return;

            //만약 플레이어와 몬스터의 거리가 지정한 수치 만큼 적어졌을 때
            if (Mathf.Abs(transform.position.x - player.position.x) <= canAttackRange_X && Mathf.Abs(transform.position.y - player.position.y) <= canAttackRange_Y)
                state = State.attack;

            else
                state = State.detect;
        }

        public virtual void Attack()
        {
            //공격중이지 않다면 공격
            if (attackCoroutine == null)
            {
                attackCoroutine = AttackCoroutine();
                StartCoroutine(attackCoroutine);
            }
        }

        IEnumerator AttackCoroutine()
        {
            //공격 애니메이션 실행 및 움직이기 종료
            animator.SetBool("isAttack", true);
            animator.SetBool("isWalk", false);
            //공격 할 때 이동 종료
            rb.velocity = new Vector2(0, rb.velocity.y);
            //공격 애니메이션 길이 측정
            yield return new WaitForSeconds(0.01f);
            float animLength = animator.GetCurrentAnimatorStateInfo(0).length;
            //공격 애니메이션 중 공격 프레임 때 공격
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / 8 * 5);
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackPosition.position, attackSize, 0, attackLayer);
            foreach (Collider2D col in collider2Ds)
            {
                if (col.CompareTag("PlayerHit"))
                    col.transform.parent.GetComponent<Player_V2>().Hit(force, transform.position);
            }

            //나머지 공격 애니메이션이 끝나면
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / 8 * 3 - 0.01f);
            //걷기, 애니메이션 실행
            animator.SetBool("isAttack", false);
            animator.SetBool("isWalk", true);

            state = State.detect;
            //공격 코루 틴종료
            attackCoroutine = null;
        }

        public virtual void Hit(float damage)       //맞았을 때
        {
            if (state == State.dead)     //죽어있으면 코드 처리 종료
                return;

            if (moveStopCoroutine != null)
            {
                StopCoroutine(moveStopCoroutine);
                moveStopCoroutine = null;
            }


            animator.SetTrigger("hitAnimation");
            state = State.hit;
            if (hitCoroutine == null)         //만약 실행중인 히트 코루틴이 없을 때
            {
                //코루틴 실행
                hitCoroutine = Hit_(damage);
                StartCoroutine(hitCoroutine);
            }


            else // 히트 코루틴이 있을 때
            {
                //히트 애니메이션을 기다리지 않고 코루틴과 애니메이션을 종료
                StopCoroutine(hitCoroutine);
                //코루틴 실행
                hitCoroutine = Hit_(damage);
                StartCoroutine(hitCoroutine);
            }

            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        }

        protected IEnumerator Hit_(float damage)      //히트 효과
        {
            hp -= damage; //데미지 만큼 체력 깎기
            rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(0.01f);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(1).length - 0.01f); //히트 애니메이션 길이만큼 기다리기
            hitCoroutine = null;        //히트 코루틴 초기화
            state = State.detect;

        }




        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(attackPosition.position, attackSize);
        }
    
}
