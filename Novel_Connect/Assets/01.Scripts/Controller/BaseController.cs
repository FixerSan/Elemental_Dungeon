using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]public ControllerStatus status = new ControllerStatus();
    public Transform trans;
    public Animator animator;
    public Rigidbody2D rb;
    public Elemental elemental;
    public Direction direction = Direction.Left;
    public abstract void GetDamage(float _damage);
    public abstract void Hit(float _damage);
    public abstract void SetPosition(Vector2 _position);
    public void ChangeDirection(Direction _direction) 
    {
        if (direction == _direction) return;
        direction = _direction;
        if (_direction == Direction.Left) transform.eulerAngles = Vector3.zero;
        if (_direction == Direction.Right) transform.eulerAngles = new Vector3(0,180,0);
    }
    public void Stop() 
    {
        if (status.isKnockback)
            return;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
}

[System.Serializable]
public class ControllerStatus
{
    public float maxHP;
    public float currentHP;

    public float maxMP;
    public float currentMP;

    public float maxSpeed;
    public float currentSpeed;

    public float maxJumpForce;
    public float currentJumpForce;

    public float attackForce;

    public bool isKnockback = false;
}
