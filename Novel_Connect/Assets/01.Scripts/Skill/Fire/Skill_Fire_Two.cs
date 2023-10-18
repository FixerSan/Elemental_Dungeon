using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;

public class Skill_Fire_Two : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform trans;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Direction direction;
    [SerializeField] private float speed;
    private bool init = false;
    public void Init(Direction _direction)
    {
        init = false;
        direction = _direction;
        if (direction == Direction.Right) spriteRenderer.flipX = true;
        if (direction == Direction.Left) spriteRenderer.flipX = false;
        trans.position = Managers.Object.Player.trans.position;
        init = true;
    }

    public void AnimationEvent_PlaySound()
    {
        Managers.Sound.PlaySoundEffect(AudioClip_Effect.Fire_Skill_2);
    }

    public void EndSkill()
    {
        init = false;
        if (!Managers.Pool.CheckExist(gameObject))
            Managers.Pool.CreatePool(gameObject);
        Managers.Pool.Push(gameObject);
    }

    public void FixedUpdate()
    {
        if (!init) return;
        MoveToDirection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;
        BaseController monster = collision.GetComponent<BaseController>();
        if (monster != null)
        {
            Managers.Battle.DamageCalculate(Managers.Object.Player, monster, Managers.Object.Player.status.currentAttackForce * 1.5f);
            Managers.Battle.SetStatusEffect(Managers.Object.Player, monster, StatusEffect.BURN);
        }
    }

    public void MoveToDirection()
    {
        Vector2 movePosition = new Vector2(speed * Time.deltaTime * (int)direction, 0);
        trans.Translate(movePosition);
    }
}
