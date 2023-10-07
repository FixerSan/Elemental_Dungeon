using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;


public class Skill_Fire_Two_Floor : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform trans;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Direction direction;
    private bool isProcessing = false;

    public void Init(Direction _direction)
    {
        direction = _direction;
        if (direction == Direction.Right) spriteRenderer.flipX = true;
        if (direction == Direction.Left) spriteRenderer.flipX = false;
        trans.position = Managers.Object.Player.trans.position;
    }
    public void StartBurn()
    {
        isProcessing = true; 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isProcessing) return;
        if (collision.CompareTag("Hitable"))
        {
            BaseController monster = collision.GetComponent<BaseController>();
            Managers.Battle.SetStatusEffect(Managers.Object.Player, monster, StatusEffect.BURN);
        }
    }

    public void EndSkill()
    {
        isProcessing = false;
        if (!Managers.Pool.CheckExist(gameObject))
            Managers.Pool.CreatePool(gameObject);
        Managers.Pool.Push(gameObject);
    }
}
