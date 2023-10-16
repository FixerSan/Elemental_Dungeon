using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SubController : BaseController
{
    public CentipedeController mainController;
    public BoxCollider2D coll;
    private UnityEngine.Color color = new UnityEngine.Color(255, 255, 255, 255);
    public float hitTime;
    private LayerMask layer;

    public override void Die()
    {

    }

    public override void Freeze()
    {

    }

    public override void GetDamage(float _damage)
    {
        mainController.GetDamage(_damage);
    }

    public override void Hit(Transform _attackTrans, float _damage)
    {
        if (mainController.isDead) return;
        mainController.Hit(_attackTrans, _damage);
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", 1);
        mpb.SetColor("_OutlineColor", UnityEngine.Color.red);
        mpb.SetFloat("_OutlineSize", 2);
        spriteRenderer.SetPropertyBlock(mpb);
        Managers.Routine.StartCoroutine(HitCoroutine());
    }

    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(hitTime);
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", 0);
        mpb.SetColor("_OutlineColor", UnityEngine.Color.white);
        mpb.SetFloat("_OutlineSize", 0);
        spriteRenderer.SetPropertyBlock(mpb);
    }

    public override void KnockBack()
    {

    }

    public override void KnockBack(float _force)
    {

    }

    public override void SetPosition(Vector2 _position)
    {

    }

    protected override IEnumerator DieRoutine()
    {
        yield return null;
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        layer = LayerMask.GetMask("Hitable");
    }

    private void Update()
    {
        CheckAttack();
    }

    private void CheckAttack()
    {
        if (mainController.isDead) return;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position, coll.size,0, layer);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if (collider2Ds[i].CompareTag("Player"))
                mainController.Attack();
        }
    }
}
