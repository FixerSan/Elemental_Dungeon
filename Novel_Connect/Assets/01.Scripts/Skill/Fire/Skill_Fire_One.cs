using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Skill_Fire_One : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform trans;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform attackTrans;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private Direction direction;
    [SerializeField] private Vector2 offset;


    public void Init(Direction _direction)
    {
        direction = _direction;
        if (_direction == Direction.Left) transform.eulerAngles = new Vector3(0, 0, 0);
        if (_direction == Direction.Right) transform.eulerAngles = new Vector3(0, 180, 0);
        Vector2 pos = new Vector2(Managers.Object.Player.trans.position.x + offset.x * (int)direction, Managers.Object.Player.trans.position.y + offset.y);
        trans.position = pos;
    }
    public void AnimationEvent_PlaySound()
    {
        Managers.Sound.PlaySoundEffect(AudioClip_Effect.Fire_Skill_1);
    }

    public void Hit()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackTrans.position, attackTrans.localScale, 0, Managers.Object.Player.attackLayer);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if (collider2Ds[i].CompareTag("Player")) continue;
                BaseController monster = collider2Ds[i].GetComponent<BaseController>();
            if (monster != null)
            {
                Managers.Battle.DamageCalculate(Managers.Object.Player, monster, Managers.Object.Player.status.currentAttackForce * 2f);
                Managers.Battle.SetStatusEffect(Managers.Object.Player, monster, StatusEffect.BURN);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackTrans.position, attackTrans.localScale);
    }
    public void EndSkill()
    {
        if (!Managers.Pool.CheckExist(gameObject))
            Managers.Pool.CreatePool(gameObject);
        Managers.Pool.Push(gameObject);
    }
}
