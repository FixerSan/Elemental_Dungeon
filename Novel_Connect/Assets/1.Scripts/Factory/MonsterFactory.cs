using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory: MonoBehaviour
{
    [SerializeField]private GameObject testMonsterPrefab;

    public GameObject Spawn(int index)
    {
        BaseMonster monster = this.Create(index);
        return monster.gameObject;
    }

    // 타입이 다른 몬스터 상관없이 생산
    protected virtual BaseMonster Create(int index)
    {
        BaseMonster monster = null;
        switch (index)
        {
            case 0:
                monster = Instantiate(testMonsterPrefab).GetComponent<BaseMonster>();
                monster.monsterData = new MonsterData(index);
                break;
        }

        return monster;
    }
}
