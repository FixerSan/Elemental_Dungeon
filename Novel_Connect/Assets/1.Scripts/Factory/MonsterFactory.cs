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

    // Ÿ���� �ٸ� ���� ������� ����
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
