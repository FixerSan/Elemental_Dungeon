using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill_1_2 : MonoBehaviour
{
    public float burnsDuration;



    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
            BattleSystem.instance.SetStatusEffect(collision.GetComponent<IStatusEffect>(), StatusEffect.Burns, burnsDuration, PlayerController.instance.playerData.force / 5);
    }


}
