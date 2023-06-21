using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : Actor
{
    public SpriteRenderer[] spriteRenderers;
    public Color color;
    public RotateToTarget movement => transform.GetChild(0).GetComponent<RotateToTarget>();
    public Transform movePos;
    public List<Transform> upPoses;
    public List<Transform> downPoses;
    public MonsterData monsterData;
    public bool isCanHit = true;
    public float canHitDuration;

    private void Awake()
    {
        movePos.SetParent(null);
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }
    public override void GetDamage(float damage)
    {
        if (statuses.currentHp <= 0 || !isCanHit)    return;
        StartCoroutine(HitEffect());
        isCanHit = false;
        statuses.currentHp -= damage;
        AudioSystem.Instance.PlayOneShot(Resources.Load<AudioClip>("AudioClips/FootAudioClip/Foot_Conc2_SneakerLand01"));
        StartCoroutine(CheckCanHit());
        if (statuses.currentHp <= 0)
        {
            statuses.currentHp = 0;
            Dead();
            return;
        }
    }

    public IEnumerator CheckCanHit()
    {
        yield return new WaitForSeconds(canHitDuration);
        isCanHit = true;
    }


    public override void SetTarget(GameObject target)
    {
        ScreenEffect.instance.Shake(0.1f,1);
        if (movement.isUp)
            movement.ChangeTarget(downPoses.RandomItem());
        else
            movement.ChangeTarget(upPoses.RandomItem());
    }

    public void Retargettind()
    {
        movement.isCanMove = true;
    }

    public override void Setup()
    {
        statuses.Setup(this);
        statuses.maxHp = monsterData.monsterHP;
        statuses.currentHp = statuses.maxHp;
        statuses.speed = monsterData.monsterSpeed;
        statuses.force = monsterData.monsterAttackForce;

        elemental = monsterData.elemental;
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    public void Attack(Collider2D collision)
    {
        Actor player = collision.GetComponent<Actor>();

        BattleSystem.instance.Calculate(elemental, player.elemental, player, statuses.force);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Attack(collision);
    }

    public override void Hit(float damage)
    {
        StartCoroutine(HitEffect());
    }

    public IEnumerator HitEffect()
    {
        foreach (var sr in spriteRenderers)
        {
            UpdateHitEffect(sr, true);
        }

        yield return new WaitForSeconds(0.1f);

        foreach (var sr in spriteRenderers)
        {
            DisableHitEffect(sr, false);
        }
    }

    void UpdateHitEffect(SpriteRenderer spriteRenderer  , bool outline)
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", color);
        mpb.SetFloat("_OutlineSize", 16);
        spriteRenderer.SetPropertyBlock(mpb);
    }

    void DisableHitEffect(SpriteRenderer spriteRenderer, bool outline)
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", color);
        mpb.SetFloat("_OutlineSize", 16);
        spriteRenderer.SetPropertyBlock(mpb);
    }


}
