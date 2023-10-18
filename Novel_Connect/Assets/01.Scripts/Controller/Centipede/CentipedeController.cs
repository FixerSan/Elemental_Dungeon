using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CentipedeController : BaseController
{
    private bool isMove;
    private bool isMoveUp;
    public float moveSpeed;
    public float rotationSpeed;
    public Vector2 dir;
    [SerializeField] private Transform target;
    [SerializeField] private Transform headPos;
    [SerializeField] private Transform[] otherPoses;
    private Coroutine moveCoroutine;
    
    private bool isUsingSkill;
    public bool isCanUseSkill;
    public float skillForce;
    public float skillDuratiom;
    public Transform skillStartPos;
    public Transform skillEndPos;
    public int skillMoveCount;
    public float skillMoveForce;
    public float skillTotalTime;
    public float skillCooltime;
    public float checkCanUseSkillTime;

    public float targetUpDownMoveForce;
    public float targetUpDownMoveSpeed;
    public float targetRightLeftMoveForce;

    public Transform[] targetUpPoses;
    public Transform[] targetDownPoses;

    private DG.Tweening.Sequence sequence;
    public float canHitDelay;

    public bool isDead;
    private bool isCanAttack;


    [SerializeField]private float moveSoundDelay;
    [SerializeField]private float skillSoundDelay;

    public void Init()
    {
        isMove = false;
        isCanHit = true;
        isDead = false;
        isCanUseSkill = false;
        checkCanUseSkillTime = skillCooltime;
        isMoveUp = false;
        isUsingSkill = false;
        isCanAttack = true;


        Managers.Data.GetBossData(1, (_data) => 
        {
            status.maxHP = _data.hp;
            status.currentHP = _data.hp;
            status.currentAttackForce = 10;
        });
    }

    public void CheckDie()
    {
        if (status.currentHP <= 0)
            Die();
    }

    public override void Die()
    {
        status.currentHP = 0;
        isDead = true;
        DOTween.Kill(sequence);
        status.currentHP = 0;
        Managers.Routine.StopCoroutine(moveCoroutine);
        Managers.Line.ReleaseLine("CentipedeMoveLine");
        Managers.Routine.StartCoroutine(DieRoutine());
        Managers.Event.OnIntEvent(Define.IntEventType.OnDeadBoss, 1);
    }

    protected override IEnumerator DieRoutine()
    {
        Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Centipede, 3);
        Collider2D[] colls = GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colls.Length; i++)
        {
            colls[i].isTrigger = false;
        }

        Rigidbody2D[] rbs = GetComponentsInChildren<Rigidbody2D>();
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].gravityScale = 1;
            rbs[i].AddForce(new Vector2(UnityEngine.Random.Range(-1, 2), 1), ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    public override void GetDamage(float _damage)
    {
        status.currentHP -= _damage;
        CheckDie();
        Debug.Log("데미지 입음");
    }

    public override void Hit(Transform _attackTrans, float _damage)
    {
        if (isDead) return;
        if (!isCanHit) return;
        isCanHit = false;
        GetDamage(_damage);
        Managers.Routine.StartCoroutine(HitRoutine());
    }

    public void Attack()
    {
        if (!isCanAttack) return;
        isCanAttack = false;
        Managers.Battle.DamageCalculate(this, Managers.Object.Player, status.currentAttackForce);
        Managers.Routine.StartCoroutine(AttackRoutine());
    }

    public IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        isCanAttack = true;
    }

    public IEnumerator HitRoutine()
    {
        yield return new WaitForSeconds(canHitDelay);
        isCanHit = true;
    }


    private void CheckCanUseSkill()
    {
        if (isUsingSkill) return;
        if (isCanUseSkill && !isMove)
        {
            isCanUseSkill = false;
            Managers.Routine.StartCoroutine(SkillRoutine());

            return;
        }

        if(!isCanUseSkill)
        {
            checkCanUseSkillTime -= Time.deltaTime;
            if (checkCanUseSkillTime <= 0)
            {
                isCanUseSkill = true;
                checkCanUseSkillTime = 0;
            }
        }
    }

    private IEnumerator SkillRoutine()
    {
        isUsingSkill = true;
        target.position = skillStartPos.position;
        yield return new WaitUntil(() => Vector2.Distance(headPos.position , target.position) < 0.1f);
        Managers.Routine.StartCoroutine(PlaySkillSound());
        sequence = target.DOJump(skillEndPos.position,skillMoveForce,skillMoveCount,skillTotalTime).SetEase(Ease.Linear);
        Managers.Screen.Shake(3, 5);
        sequence.onComplete = () => { isUsingSkill = false; checkCanUseSkillTime = skillCooltime; };
    }

    private IEnumerator PlaySkillSound()
    {
        yield return new WaitForSeconds(skillSoundDelay);
        Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Centipede, 2);
    }

    private void CheckMove()
    {
        if (isMove) return;
        if (checkCanUseSkillTime < 5f) return;
        if (isUsingSkill) { Managers.Line.ReleaseLine("CentipedeMoveLine"); return; }
        isMove = true;
        moveCoroutine = Managers.Routine.StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        Transform nextTargetPos = null;
        if (isMoveUp)
            nextTargetPos = targetDownPoses[UnityEngine.Random.Range(0, targetDownPoses.Length)];
        if (!isMoveUp) nextTargetPos = targetUpPoses[UnityEngine.Random.Range(0, targetUpPoses.Length)];
        yield return new WaitForSeconds(2);
        Managers.Line.SetLine("CentipedeMoveLine", headPos.position, nextTargetPos.position, 1);
        yield return new WaitForSeconds(2);
        Managers.Line.ReleaseLine("CentipedeMoveLine");
        Managers.Routine.StartCoroutine(PlayMoveSound());
        Managers.Screen.Shake(3, 2);
        target.position = nextTargetPos.position;
        yield return new WaitUntil(() => Vector2.Distance(headPos.position, nextTargetPos.position) > 0.1f);
        isMoveUp = !isMoveUp;
        isMove = false;
    }

    private IEnumerator PlayMoveSound()
    {
        yield return new WaitForSeconds(moveSoundDelay);
        Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Centipede_Move);
    }

    private void HeadMoveMove()
    {
        dir = target.position - headPos.position;
        float angle = MathF.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        headPos.rotation = Quaternion.Slerp(headPos.rotation, rotation, rotationSpeed * Time.deltaTime);

        headPos.position = Vector2.MoveTowards(headPos.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void OthersMove()
    {
        for (int i = 0; i < otherPoses.Length; i++)
        {
            Vector3 targetDir;
            if (i == 0) targetDir = headPos.position - otherPoses[i].position;
            else targetDir = otherPoses[i-1].position - otherPoses[i].position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
            otherPoses[i].rotation = Quaternion.Slerp(otherPoses[i].rotation, rotation, rotationSpeed * Time.deltaTime);
            

            Vector2 targetPos;
            if (i == 0) targetPos = headPos.position;
            else targetPos = otherPoses[i - 1].position;
            otherPoses[i].position = Vector2.Lerp(otherPoses[i].position, targetPos, moveSpeed * Time.deltaTime);
            if(Vector2.Distance(otherPoses[i].position , targetPos) > 0.45f)    otherPoses[i].position = Vector2.MoveTowards(otherPoses[i].position, targetPos, moveSpeed * Time.deltaTime);
        }
    }

    public override void KnockBack()
    {

    }

    public override void KnockBack(float _force)
    {

    }

    public override void SetPosition(Vector2 _position)
    {
        transform.position = _position;
    }

    private void Update()
    {
        if (isDead) return;
        HeadMoveMove();
        OthersMove();
        CheckCanUseSkill();
        CheckMove();
    }

    public override void Freeze()
    {
        throw new NotImplementedException();
    }
}
