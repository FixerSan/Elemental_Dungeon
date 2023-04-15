using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill_1 : MonoBehaviour
{
    public float damage;
    public float knuckBackForce;
    public float speed;
    private float duration;
    public int directionInt;

    public void Setup()
    {
        //나중에 데미지 배율, 넉백 거리, 스피드 등등은 스킬 데이터에서 가져올 예정
        duration = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        //knuckBackForce = 1;
        if (PlayerController.instance.playerDirection == Direction.Left)
            directionInt = -1;
        else
            directionInt = 1;
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
            monster.KnockBack(PlayerController.instance.playerDirection, knuckBackForce, knuckBackForce) ;
            BattleSystem.instance.Calculate(Elemental.Fire, monster.monsterData.elemental, collision.GetComponent<IHitable>(), damage * PlayerController.instance.playerData.force);
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.right * directionInt * knuckBackForce, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * knuckBackForce, ForceMode2D.Impulse);
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
