using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : Stage
{
    //List<GameObject> monsters = new List<GameObject>();
    //[SerializeField]
    //private Transform monsterSpawnPos;
    bool isFirstPlay = true;
    public override void Setup()
    {
        if(isFirstPlay)
        {
            CutSceneManager.instance.StartCoroutine(CutSceneManager.instance.Tutorial_1());
        }


        //monsterSpawnPos = GameObject.Find("MonsterSpawnPos").transform;
        //MonsterObjectPool.instance.Init(0, monsterSpawnPos.childCount);

        //for (int i = 0; i < monsterSpawnPos.childCount; i++)
        //{
        //    monsters.Add(MonsterObjectPool.instance.GetMonster(0, monsterSpawnPos.GetChild(i)));
        //}
    }

    public override void UpdateStage()
    {

    }
}
