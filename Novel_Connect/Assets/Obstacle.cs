using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : HitableObejct
{
    public bool isUseTriggerEvent;
    public int triggerIndex;
    public float hp;
    public override void GetDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            DestroyObejct();
        }
    }

    public override void Hit(float damage)
    {

    }

    private void DestroyObejct()
    {
        if(isUseTriggerEvent) SceneManager.instance.GetCurrentScene().TriggerEffect(triggerIndex);
        Destroy(gameObject);
    }
}
