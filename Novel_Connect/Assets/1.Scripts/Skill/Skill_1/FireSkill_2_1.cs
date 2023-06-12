using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill_2_1 : MonoBehaviour
{
    public PlayerControllerV3 player => FindObjectOfType<PlayerControllerV3>();
    public float damage;
    public float knuckBackForce;
    public float speed;
    public float burnsDuration;
    private float duration;
    public Direction direction;

    public void Setup()
    {
        //나중에 데미지 배율, 넉백 거리, 스피드 등등은 스킬 데이터에서 가져올 예정
        duration = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

        direction = player.direction;
        StartCoroutine(SkillExit());
    }

    private void FixedUpdate()
    {
        MoveFront();
    }

    public IEnumerator SkillExit()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    void MoveFront()
    {
        if(direction == Direction.Left)
            transform.position = new Vector2(transform.position.x - speed, transform.position.y);
        if(direction == Direction.Right)
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            BaseMonster monster = collision.GetComponent<BaseMonster>();
            Actor actor = monster.GetComponent<Actor>();
            actor.SetTarget(player.gameObject);
            BattleSystem.instance.HitCalculate(Elemental.Fire, actor.elemental, actor, player.statuses.force);
            BattleSystem.instance.SetStatusEffect(actor, StatusEffect.Burns, burnsDuration);
            monster.KnockBack(direction, knuckBackForce, 0);

        }
    }

    private void OnEnable()
    {
        Setup();
        AudioSystem.Instance.PlayOneShot(Resources.Load<AudioClip>("AudioClips/FireSkills/Fire2"));
    }
}

