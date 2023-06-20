using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Statuses
{
    public float currentHp;
    public float maxHp;
    public float speed;
    public float nowSpeed;
    public float force;

    public Actor entity;

    
    public void Setup(Actor entity_)
    {
        entity = entity_;
        currentHp = maxHp;
        nowSpeed = speed;
    }

    public void ExitAllEffect()
    {
        burnTime = 0;
    }

    public void Update()
    {
        CheckBurn();
    }

    #region Burn
    public bool isburn;
    public float burnTime;
    public Coroutine burnCoroutine;

    public void SetBurn(float time)
    {
        if(burnCoroutine == null)
        {
            burnCoroutine = entity.StartCoroutine(Burn());
        }

        if(burnTime >= time)
        {
            return;
        }

        else
        {
            burnTime = time;
        }
    }

    public void CheckBurn()
    {
        if (burnTime > 0)
        {
            if(!isburn)
            {
                isburn = true;
            }

            burnTime -= Time.deltaTime;

            if (burnTime <= 0)
            {
                burnTime = 0;
                isburn = false;
            }
        }
    }

    public IEnumerator Burn()
    {
        if(isburn)
        {
            entity.GetDamage(maxHp * 0.02f);
            AudioSystem.Instance.PlayOneShot(Resources.Load<AudioClip>("AudioClips/Monster/General/Fire_Damage_1"));
        }
        yield return new WaitForSeconds(1);
        if (burnCoroutine != null)
        {
            entity.StopCoroutine(burnCoroutine);
        }
        burnCoroutine = entity.StartCoroutine(Burn());
    }

    #endregion
    #region Freezing
    public bool isFreeze;
    public float freezeTime;
    public Coroutine freezeCoroutine;

    public void SetFreeze(float time)
    {
        if (freezeCoroutine == null)
        {
            freezeCoroutine = entity.StartCoroutine(Freeze());
        }

        if (freezeTime >= time)
        {
            return;
        }

        else
        {
            freezeTime = time;
        }
    }

    public void CheckFreeze()
    {
        if (freezeTime > 0)
        {
            if (!isFreeze)
            {
                isFreeze = true;
            }

            freezeTime -= Time.deltaTime;

            if (freezeTime <= 0)
            {
                freezeTime = 0;
                isFreeze = false;
            }
        }
    }

    public IEnumerator Freeze()
    {
        if (isFreeze)
        {
            //여기다 프리즈 내용
            nowSpeed = speed / 100 * 30;
        }
        yield return new WaitForSeconds(1);
        if (freezeCoroutine != null)
        {
            entity.StopCoroutine(freezeCoroutine);
        }
        freezeCoroutine = entity.StartCoroutine(Freeze());
    }


    #endregion
}
