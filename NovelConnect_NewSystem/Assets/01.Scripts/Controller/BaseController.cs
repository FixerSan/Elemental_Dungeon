using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]protected ControllerStatus status = new ControllerStatus();
    public Elemental elemental;
    public abstract void GetDamage(float _damage);
    public abstract void Hit(float _damage);
    public abstract void SetPosition(Vector2 _position);
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
}
