using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : Stage
{
    List<GameObject> monsters = new List<GameObject>();
    [SerializeField]
    private Transform monsterSpawnPos;
    public override void Setup()
    {
        monsterSpawnPos = GameObject.Find("MonsterSpawnPos").transform;
        MonsterObjectPool.instance.Init(0, 6);

        for (int i = 0; i < monsterSpawnPos.childCount; i++)
        {
            monsters.Add(MonsterObjectPool.instance.GetMonster(0, monsterSpawnPos.GetChild(i)));
        }
    }

    public override void UpdateStage()
    {
        foreach (var item in monsters)
        {
            if (!item.activeSelf)
            {
                StartCoroutine(Respawn(item));
            }
        }
    }

    IEnumerator Respawn(GameObject monster)
    {
        yield return new WaitForSeconds(5); //아 이거 아님 수정해야함
    }
}
