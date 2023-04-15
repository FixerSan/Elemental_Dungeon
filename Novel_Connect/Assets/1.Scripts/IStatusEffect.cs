using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffect
{
    public void SetStatusEffect(StatusEffect status, float duration, float damage);
}
