using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Statuses
{
    public float currentHp;
    public float maxHp;
    public float speed;
    public float force;

    public Actor entity;

    
    public void Setup(Actor entity_)
    {
        entity = entity_;
    }

    public void ExitAllEffect()
    {
        burnTime = 0;
    }

    public void Update()
    {
        CheckBurn();
    }
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
            AudioSystem.Instance.PlayOneShot(Resources.Load<AudioClip>("Fire Damage"));
        }
        yield return new WaitForSeconds(1);
        if (burnCoroutine != null)
        {
            entity.StopCoroutine(burnCoroutine);
            burnCoroutine = entity.StartCoroutine(Burn());
        }
    }
}
