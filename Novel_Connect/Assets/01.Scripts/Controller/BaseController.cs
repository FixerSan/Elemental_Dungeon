using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public abstract class BaseController : MonoBehaviour
{
    public ControllerStatus status;
    protected Coroutine changeStateCoroutine;
    public Transform trans;
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator effectUIAnimator;
    public Elemental elemental;
    public Direction direction = Direction.Left;
    public abstract void GetDamage(float _damage);
    public abstract void Hit(Transform _attackTrans, float _damage);
    public abstract void SetPosition(Vector2 _position);
    public abstract void Die();
    protected abstract IEnumerator DieRoutine();
    public abstract void KnockBack();
    public abstract void KnockBack(float _force);
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

    public Transform GetTrans()
    {
        return trans;
    }

    public abstract void Freeze();

    public virtual void SetFreezeUI()
    {

    }
}

[System.Serializable]
public class ControllerStatus
{
    private BaseController controller;

    public float maxHP;
    public float currentHP;

    public float maxMP;
    public float currentMP;

    public float maxWalkSpeed;
    public float currentWalkSpeed;
    
    public float maxRunSpeed;
    public float currentRunSpeed;

    public float maxJumpForce;
    public float currentJumpForce;

    public float currentAttackForce;

    public bool isKnockback = false;

    public System.Action effectAction;
    public Coroutine effectActionCoroutine;

    public bool isDead = false;

    public bool isBurn = false;
    public float burnDamage;
    public Coroutine stopBurnCoroutine;

    public int freezeCount;
    public Coroutine initFreezeCountCoroutine;


    public void StartEffectCycle()
    {
        if(effectActionCoroutine != null)
            Managers.Routine.StopCoroutine(effectActionCoroutine);
        effectActionCoroutine = Managers.Routine.StartCoroutine(EffectCycle());
    }

    public IEnumerator EffectCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            effectAction?.Invoke();

            if (isDead) StopEffectCycle();
        }
    }

    private void StopEffectCycle()
    {
        if (effectActionCoroutine == null) return;
        Managers.Routine.StopCoroutine(effectActionCoroutine);
    }

    public void StopAllEffect()
    {
        StopBurn();
    }

    public void StartBurn(float _burnDamage)
    {
        burnDamage = _burnDamage;
        effectAction -= Burn;
        effectAction += Burn;
        StartEffectCycle();
        if (stopBurnCoroutine != null) Managers.Routine.StopCoroutine(stopBurnCoroutine);
        stopBurnCoroutine = Managers.Routine.StartCoroutine(StopBurnRoutine(5));
    }

    private IEnumerator StopBurnRoutine(float _duration)
    {
        yield return new WaitForSeconds(_duration);
        StopBurn();
    }

    private void Burn()
    {
        controller.GetDamage(burnDamage);
        Managers.Sound.PlaySoundEffect(SoundProfile_Effect.Effect, 0);
    }

    public void StopBurn()
    {
        effectAction -= Burn;
        burnDamage = 0;
        if (effectAction == null)   StopEffectCycle(); 
    }


    public void SetFreezeCount()
    {
        if (isDead) return;

        freezeCount++;
        controller.SetFreezeUI();
        if (initFreezeCountCoroutine != null) Managers.Routine.StopCoroutine(initFreezeCountCoroutine);
        initFreezeCountCoroutine = Managers.Routine.StartCoroutine(InitFreezeCount_Routine());
        if (freezeCount == 3)
        {
            StartFreeze();
        }
    }

    public void StartFreeze()
    {
        if (isDead) return;

        freezeCount = 0;
        controller.Freeze();
    }

    private IEnumerator InitFreezeCount_Routine()
    {
        if(isDead)
        {
            freezeCount = 0;
            controller.SetFreezeUI();
        }

        else
        {
            yield return new WaitForSeconds(5);
            freezeCount--;
            controller.SetFreezeUI();
            if(freezeCount != 0)
            {
                initFreezeCountCoroutine = Managers.Routine.StartCoroutine(InitFreezeCount_Routine());
            }
        }
    }

    public ControllerStatus(BaseController _controller)
    {
        controller = _controller;
    }
}


