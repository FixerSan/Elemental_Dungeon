using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Fire_Two : MonoBehaviour
{
    [SerializeField] private Transform trans;
    [SerializeField] private Transform attackTrans;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private Define.Direction direction;
    [SerializeField] private float speed;
    [SerializeField] private float knockBackForce;
    [SerializeField] private float durationTime;
    private bool init = false;
    public void Init(Define.Direction _direction)
    {
        direction = _direction;
        trans.position = Managers.Object.Player.trans.position;
        Managers.Routine.StartCoroutine(CheckDuration());
        init = true;
    }

    public IEnumerator CheckDuration()
    {
        yield return new WaitForSeconds(durationTime);
        EndSkill();
    }

    public void EndSkill()
    {
        if (Managers.Pool.CheckExist(gameObject))
        {
            Managers.Pool.Push(gameObject);
            return;
        }
        Managers.Pool.CreatePool(gameObject);
        init = false;
        Managers.Pool.Push(gameObject);
    }

    public void FixedUpdate()
    {
        if (!init) return;
        CheckAttack();
        MoveToDirection();
    }

    public void CheckAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackTrans.position,attackTrans.localScale,0,attackLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player")) continue;

            BaseController monster = colliders[i].GetComponent<BaseController>();
            Managers.Battle.DamageCalculate(Managers.Object.Player, monster);
            monster.KnockBack(knockBackForce);
        }
    }

    public void MoveToDirection()
    {
        Vector2 movePosition = new Vector2(speed * Time.deltaTime * (int)direction, 0);
        trans.Translate(movePosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackTrans.position,attackTrans.localScale);
    }
}
