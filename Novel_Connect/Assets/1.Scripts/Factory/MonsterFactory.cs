using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterFactory: MonoBehaviour
{
    public GameObject Spawn(int index)
    {
        MonsterV2 monster = this.Create(index);
        return monster.gameObject;
    }

    // 타입이 다른 몬스터 상관없이 생산
    protected abstract MonsterV2 Create(int index);
}
