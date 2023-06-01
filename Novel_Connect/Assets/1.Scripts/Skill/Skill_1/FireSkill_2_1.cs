using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill_2_1 : MonoBehaviour
{
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

        direction = PlayerController.instance.playerDirection;
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
            BattleSystem.instance.Calculate(Elemental.Fire, monster.monsterData.elemental, collision.GetComponent<Actor>(), damage * PlayerController.instance.playerData.force);
            BattleSystem.instance.SetStatusEffect(collision.GetComponent<IStatusEffect>(), StatusEffect.Burns, burnsDuration, PlayerController.instance.playerData.force / 5);
            monster.KnockBack(direction, knuckBackForce, 0);

        }
    }

    private void OnEnable()
    {
        Setup();
    }
}

