using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill_Two : MonoBehaviour
{
    public List<BaseController> hitControllers = new List<BaseController>();
    private BaseController user;
    public void Init(BaseController _user)
    {
        user = _user;
        transform.position = user.trans.position;
        hitControllers.Clear();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        BaseController hiter = collision.GetComponent<BaseController>();
        for (int i = 0; i < hitControllers.Count; i++)
            if (hiter == hitControllers[i]) return;
        hitControllers.Add(hiter);
        Managers.Battle.DamageCalculate(user, hiter, user.status.currentAttackForce * 2);
    }

    private void OnDisable()
    {
        user = null;
        hitControllers.Clear();   
    }
}
