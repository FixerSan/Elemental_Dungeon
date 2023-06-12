using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill_2_2 : MonoBehaviour
{
    public float burnsDuration;
    public float duration;

    IEnumerator Exit()
    {
        yield return new WaitForSeconds(duration);
        GetComponent<Animator>().SetBool("isEnd", true);
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);

        transform.parent.GetComponent<FireSkill_2>().Disable();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Actor hiter = collision.GetComponent<Actor>();
            hiter.SetTarget(FindObjectOfType<PlayerControllerV3>().gameObject);
            BattleSystem.instance.SetStatusEffect(hiter, StatusEffect.Burns, 5);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Exit());
    }


}
