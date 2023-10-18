using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceSkill_Two : MonoBehaviour
{
    public List<BaseController> hitControllers = new List<BaseController>();
    private BaseController user;
    public void Init(BaseController _user)
    {
        user = _user;
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

    public void FixedUpdate()
    {
        if (user != null)
        {
            transform.position = new Vector2(user.trans.position.x,user.trans.position.y + 0.57f);
        }
    }

    private void OnDisable()
    {
        user = null;
        hitControllers.Clear();   
    }
}
