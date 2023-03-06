//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Evan 
//{   근데 여기에 에반이 왜 있음? 
//    public BreakingpProperty bP = BreakingpProperty.sword;
//    public Vector2 skill_1Size;
//    public Transform skill_1Point;
//    public float skill_1Cost;
//    public float skill_1Cooldown;
//    public float skill_1Force;
//    public float skill_1BreakingCount;
//    public Vector2 skill_2Size;
//    public Transform skill_2Point;
//    public float skill_2Cost;
//    public float skill_2Cooldown;
//    public float skill_2Force;
//    public float skill_2BreakingCount;

//    public float skill_2MoveForce;

//    private IEnumerator attackCoroutine = null;
//    private bool isCanUseSkill_1 = true;
//    private bool isCanUseSkill_2 = true;



//    public override void Start()
//    {
//        base.Start();
       
//    }

//    // Update is called once per frame
//    public override void Update()
//    {
//        base.Update();
//        if (animator.GetBool("isDead"))
//            return;

//        Attack();
//        Skill_1();
//        Skill_2();
//    }

//    //공격 처리
//    private void Attack()
//    {
//        //공격 애니메이션
//        if (Input.GetKey(KeyCode.A))
//        {
//            //공격중이 아닐 때
//            if(!animator.GetBool("isAttack"))
//            {   
//                //공격 카운트가 설정한 카운트 보다 낮을 때
//                if (animator.GetInteger("isAttackCount") < attackCount)
//                {   
//                    //공격 카운트를 늘리고
//                    animator.SetInteger("isAttackCount", animator.GetInteger("isAttackCount") + 1);
//                }
//                //공격 카운트가 설정한 카운트 보다 높을 때
//                else
//                    //카운트를 1로 초기화
//                    animator.SetInteger("isAttackCount", 1);

//                //공격 코루틴이 작동중이지 않을 때 (공격이 끝나고 단계가 초기화 후)
//                if(attackCoroutine == null)
//                {
//                    //공격 시작
//                    attackCoroutine = AttackCoroutine();
//                    StartCoroutine(attackCoroutine);
//                    StartCoroutine(AttackMoveCoroutine());
//                }

//                //공격 코루틴이 작동중일 때 (공격이 끝나고 단계가 초기화가 되기 전)
//                else
//                {
//                    //지금 하는 공격 코루틴 종료 후 다음 공격 실행
//                    StopCoroutine(attackCoroutine);
//                    attackCoroutine = AttackCoroutine();
//                    StartCoroutine(attackCoroutine);
//                    StartCoroutine(AttackMoveCoroutine());
//                }
//            }
//        }
//    }

//    //공격 효과
//    IEnumerator AttackCoroutine()
//    {
//        //공격 상태로 변경
//        animator.SetBool("isAttack", true);

//        //애니메이션 시간 찾기
//        yield return new WaitForSeconds(0.01f);  //0.01
//        float curAnimationTime = animator.GetCurrentAnimatorStateInfo(0).length;
//        //콜라이더 찾기
//        //공격 단계에 따른 다른 공격 기능
//        switch (animator.GetInteger("isAttackCount"))
//        {
//            case 1: //1 타 공격 
//                yield return new WaitForSeconds(curAnimationTime / 4 * 1 - 0.01f);
//                Collider2D[] collider2Ds_1 = Physics2D.OverlapBoxAll(attackPoint.position, attackSize, 0, AttackLayer);
//                foreach (Collider2D hitTarget in collider2Ds_1)
//                {
//                    if (hitTarget.CompareTag("Monster"))
//                    {
//                        if(hitTarget.GetComponent<Monster>() != null)
//                            hitTarget.GetComponent<Monster>().Hit(attackForce);

//                        if (hitTarget.GetComponent<Monster_V2>() != null)
//                            hitTarget.GetComponent<Monster_V2>().Hit(attackForce);
//                    }

//                    if (hitTarget.CompareTag("Boss"))
//                        hitTarget.GetComponent<Boss>().Hit(attackForce);
//                }
//                yield return new WaitForSeconds(curAnimationTime - (curAnimationTime / 4 * 1 - 0.01f));   
//                break;

//            case 2://2 타 공격
//                yield return new WaitForSeconds(curAnimationTime / 5 * 1 - 0.01f);
//                Collider2D[] collider2Ds_2 = Physics2D.OverlapBoxAll(attackPoint.position, attackSize, 0, AttackLayer);
//                foreach (Collider2D hitTarget in collider2Ds_2)
//                {
//                    if (hitTarget.CompareTag("Monster"))
//                    {
//                        if (hitTarget.GetComponent<Monster>() != null)
//                            hitTarget.GetComponent<Monster>().Hit(attackForce);

//                        if (hitTarget.GetComponent<Monster_V2>() != null)
//                            hitTarget.GetComponent<Monster_V2>().Hit(attackForce);
//                    }

//                    if (hitTarget.CompareTag("Boss") && hitTarget.GetComponent<Boss>().hp > 0)
//                        hitTarget.GetComponent<Boss>().Hit(attackForce);
//                }
//                yield return new WaitForSeconds(curAnimationTime - (curAnimationTime / 5 * 1 - 0.01f));   
//                break;

//            case 3: //3타 공격
//                yield return new WaitForSeconds(curAnimationTime / 5 * 1);
//                Collider2D[] collider2Ds_3 = Physics2D.OverlapBoxAll(attackPoint.position, attackSize, 0, AttackLayer);
//                foreach (Collider2D hitTarget in collider2Ds_3)
//                {
//                    if (hitTarget.CompareTag("Monster"))
//                    {
//                        if (hitTarget.GetComponent<Monster>() != null)
//                            hitTarget.GetComponent<Monster>().Hit(attackForce);

//                        if (hitTarget.GetComponent<Monster_V2>() != null)
//                            hitTarget.GetComponent<Monster_V2>().Hit(attackForce);
//                    }

//                    if (hitTarget.CompareTag("Boss") && hitTarget.GetComponent<Boss>().hp > 0)
//                        hitTarget.GetComponent<Boss>().Hit(attackForce);
//                }
//                yield return new WaitForSeconds(curAnimationTime - (curAnimationTime / 5 * 1 - 0.01f));
//                break;
//        }
//        //공격 종료
//        animator.SetBool("isAttack", false);

//        //설정한 공격 단계 초기화 시간 만큼 기다린 후 공격 단계 초기화
//        yield return new WaitForSeconds(canAttackDuration);
//        animator.SetInteger("isAttackCount", 0);
//        attackCoroutine = null;
//    }

//    IEnumerator AttackMoveCoroutine()
//    {
//        rb.velocity = new Vector2( rb.velocity.normalized.x , rb.velocity.y);
//        rb.AddForce(Vector2.right * rb.velocity.x * 50, ForceMode2D.Impulse);
//        yield return new WaitForSeconds(0.1f);
//        rb.velocity = new Vector2(0, rb.velocity.y);
//    }

//    private void Skill_1()
//    {
//        if (!isCanUseSkill_1)
//            return;
//        if (mp < skill_1Cost)
//            return;
//        if (Input.GetKeyDown(KeyCode.Q))
//        {
//            animator.SetBool("isCastingSkill_1", true);
//            isCanUseSkill_1 = false;
//            mp -= skill_1Cost;
//            StartCoroutine(Skill_1Coroutine());
//        }
//    }

//    IEnumerator Skill_1Coroutine()
//    {
//        rb.velocity = Vector2.zero;
//        yield return new WaitForSeconds(0.01f); //0.01
//        float curAnimationTime = animator.GetCurrentAnimatorStateInfo(0).length;
//        yield return new WaitForSeconds(curAnimationTime / 8 * 5 - 0.01f);  //0.406875
//        Collider2D[] collider2Ds_1 = Physics2D.OverlapBoxAll(skill_1Point.position, skill_1Size, 0, AttackLayer);
//        foreach (Collider2D hitTarget in collider2Ds_1)
//        {
//            if (hitTarget.CompareTag("Monster"))
//                hitTarget.GetComponent<Monster>().Hit(skill_1Force);
//            if (hitTarget.CompareTag("Boss"))
//                hitTarget.GetComponent<Boss>().Hit(skill_1Force, bP, skill_1BreakingCount);
//        }
//        yield return new WaitForSeconds(curAnimationTime - (curAnimationTime / 8 * 5 - 0.01f) - 0.02f); //0.260125
//        animator.SetBool("isCastingSkill_1", false);
//        yield return new WaitForSeconds(skill_1Cooldown - curAnimationTime - 0.01f); //5.739875
//        isCanUseSkill_1 = true;
//    }

//    private void Skill_2()
//    {
//        if (!isCanUseSkill_2)
//            return;
//        if (mp < skill_2Cost)
//            return;
//        if (Input.GetKeyDown(KeyCode.W))
//        {
//            animator.SetBool("isCastingSkill_2", true);
//            isCanUseSkill_2 = false;
//            mp -= skill_2Cost;
//            StartCoroutine(Skill_2Coroutine());
//        }
//    }

//    IEnumerator Skill_2Coroutine()
//    {
//        isCanHit = false;
//        yield return new WaitForSeconds(0.1f);
//        float curAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
//        maxSpeed = skill_2MoveForce;
//        Collider2D[] collider2Ds_1 = Physics2D.OverlapBoxAll(skill_2Point.position, skill_2Size, 0, AttackLayer);
//        foreach (Collider2D hitTarget in collider2Ds_1)
//        {
//            if (hitTarget.CompareTag("Monster"))
//                hitTarget.GetComponent<Monster>().Hit(skill_2Force);
//            if (hitTarget.CompareTag("Boss"))
//                hitTarget.GetComponent<Boss>().Hit(skill_2Force, bP, skill_2BreakingCount);
//        }
//        rb.AddForce(direction * skill_2MoveForce * Vector2.right, ForceMode2D.Impulse);
//        yield return new WaitForSeconds(0.15f);
//        maxSpeed = speed;
//        rb.velocity = Vector2.zero;
//        isCanHit = true;
//        yield return new WaitForSeconds(curAnimationLength - 0.1f -0.15f);
//        animator.SetBool("isCastingSkill_2", false);

//        yield return new WaitForSeconds(skill_2Cooldown - curAnimationLength - 0.1f);
//        isCanUseSkill_2 = true;

//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.black;
//        Gizmos.DrawWireCube(attackPoint.position, attackSize);
//        Gizmos.color = Color.blue;
//        Gizmos.DrawWireCube(skill_1Point.position, skill_1Size);
//        Gizmos.color = Color.cyan;
//        Gizmos.DrawWireCube(skill_2Point.position, skill_2Size);

//    }
//}
