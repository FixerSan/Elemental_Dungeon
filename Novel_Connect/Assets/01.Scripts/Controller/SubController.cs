using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubController : BaseController
{
    public BaseController mainController;
    public override void Die()
    {

    }

    public override void Freeze()
    {

    }

    public override void GetDamage(float _damage)
    {
        mainController.GetDamage(_damage);
    }

    public override void Hit(Transform _attackTrans, float _damage)
    {
        mainController.Hit(_attackTrans, _damage);
    }

    public override void KnockBack()
    {

    }

    public override void KnockBack(float _force)
    {

    }

    public override void SetPosition(Vector2 _position)
    {

    }

    protected override IEnumerator DieRoutine()
    {
        yield return null;
    }
}
