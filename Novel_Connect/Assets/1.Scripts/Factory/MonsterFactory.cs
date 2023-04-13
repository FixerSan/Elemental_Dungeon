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

    // Ÿ���� �ٸ� ���� ������� ����
    protected abstract MonsterV2 Create(int index);
}
