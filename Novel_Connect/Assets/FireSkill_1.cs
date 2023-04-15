using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill_1 : MonoBehaviour
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
        //knuckBackForce = 1;
        direction = PlayerController.instance.playerDirection;
        if(direction == Direction.Left)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            speed = -speed; 
        }
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
        transform.position = new Vector2(transform.position.x + speed, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            MonsterV2 monster = collision.GetComponent<MonsterV2>();
            BattleSystem.instance.Calculate(Elemental.Fire, monster.monsterData.elemental, collision.GetComponent<IHitable>(), damage * PlayerController.instance.playerData.force);
            BattleSystem.instance.SetStatusEffect(collision.GetComponent<IStatusEffect>(), StatusEffect.Burns, burnsDuration, PlayerController.instance.playerData.force / 5);
            monster.KnockBack(direction, knuckBackForce, 0);

        }
    }

    private void OnEnable()
    {
        Setup();
    }

    private void OnDisable()
    {
        transform.position = Vector3.zero;
    }
}
