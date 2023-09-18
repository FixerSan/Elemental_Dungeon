using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeController : BaseController
{
    public float moveSpeed;
    public float rotationSpeed;
    public Vector2 dir;

    public float skillForce;
    public float skillDuratiom;
    [SerializeField] private Transform target;
    [SerializeField] private Transform headPos;
    [SerializeField] private Transform[] otherPoses;

    private float mathTime;
    public float targetUpDownMoveForce;
    public float targetUpDownMoveSpeed;
    public float targetRightLeftMoveForce;

    public Transform[] targetPoses;

    public void Init()
    {
         
    }

    public override void Die()
    {

    }

    public override void GetDamage(float _damage)
    {

    }

    public override void Hit(Transform _attackTrans, float _damage)
    {

    }

    public override void KnockBack()
    {

    }

    public override void KnockBack(float _force)
    {

    }

    public override void SetPosition(Vector2 _position)
    {

    }
    private void OnEnable()
    {
        Init();
        Managers.Routine.StartCoroutine(TargetPositionRoutine());
    }

    private void Update()
    {
        //TargetMove();
        HeadMoveMove();
        OthersMove();
    }
    private void TargetMove()
    {
        target.position = new Vector3(target.position.x + Time.deltaTime * targetRightLeftMoveForce, 0 + Mathf.Cos(Time.time * targetUpDownMoveSpeed) * targetUpDownMoveForce);
    }

    private IEnumerator Skill()
    {
        yield return new WaitForSeconds(skillDuratiom);
        target.position = target.position + ((Managers.Object.Player.trans.position - target.position) * skillForce);
        Managers.Routine.StartCoroutine(Skill());
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
            if(Vector2.Distance(otherPoses[i].position , targetPos) > 0.45f)    otherPoses[i].position = Vector2.MoveTowards(otherPoses[i].position, targetPos, moveSpeed * Time.deltaTime);
        }
    }

    protected override IEnumerator DieRoutine()
    {
        yield return null;
    }

    private IEnumerator TargetPositionRoutine()
    {
        for (int i = 0; i < targetPoses.Length; i++)
        {
            target.position = targetPoses[i].position;
            yield return new WaitUntil(() => Vector2.Distance(headPos.position , target.position) < 0.2f);
        }
    }
}
