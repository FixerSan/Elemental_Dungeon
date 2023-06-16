using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpear : MonoBehaviour
{
    public Vector2 startPos;
    public float addForcePower;
    public float fadeTime;
    public float returnDelayTime;
    public float lineRendererLength;
    public float lineRenderTime;

    IceBosSkill_1 parent;
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    BoxCollider2D boxCollder=> GetComponent<BoxCollider2D>();
    SpriteRenderer sr => GetComponent<SpriteRenderer>();
    LineRenderer lr => GetComponent<LineRenderer>();
    private void Awake()
    {
        lr.enabled = false;
    }

    public void SkillSetup(IceBosSkill_1 parent_)
    {
        parent = parent_;

        sr.color = Color.white;

        float angle;
        Vector3 pos2;
        Vector2 direction_;
        if (parent.direction == Direction.Right)
        {
            angle = -90;
            pos2 = new Vector3(transform.position.x + lineRendererLength, transform.position.y,transform.position.z);
            direction_ = Vector2.right;
        }
        else
        {
            angle = 90;
            pos2 = new Vector3(transform.position.x - lineRendererLength, transform.position.y, transform.position.z);
            direction_ = Vector2.left;
        }
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        lr.startWidth = 0.5f;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, pos2);
        lr.enabled = true;


        StartCoroutine(StartDelay(direction_));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DettectTarget(collision.GetComponent<Actor>());
        }
    }

    public void DettectTarget(Actor actor)
    {
        //애니메이션 실행
        BattleSystem.instance.HitCalculate(parent.actor.elemental, actor.elemental, actor, parent.actor.statuses.force * 1.5f);
    }

    public IEnumerator StartDelay(Vector2 direction)
    {
        yield return new WaitForSeconds(lineRenderTime);
        lr.enabled = false;
        SkillStart(direction);
    }

    public void SkillStart(Vector2 direction)
    {
        rb.AddForce(direction * addForcePower, ForceMode2D.Impulse);
        StartCoroutine(ReturnDelay(returnDelayTime));
    }


    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator ReturnPool()
    {
        boxCollder.enabled = false;
        yield return StartCoroutine(SpriteEffect.instance.FadeOutCoroutine(sr, fadeTime));
        Destroy();
    }

    public IEnumerator ReturnDelay(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        StartCoroutine(ReturnPool());
    }
}
