using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObjectPool : MonoBehaviour
{
    #region Sington
    private static MonsterObjectPool Instance;
    public static MonsterObjectPool instance
    {
        get
        {
            if(Instance != null)
                return Instance;
            return null;
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public Dictionary<int, Queue<GameObject>> monsterQueues = new Dictionary<int, Queue<GameObject>>();
    //public GameObject CreateTestMonster()
    //{
    //    GameObject monster = Instantiate(testMonsterPrefab);
    //    monster.SetActive(false);
    //    monster.transform.SetParent(transform);
    //    return monster;
    //}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            GetMonster(0, PlayerController.instance.transform.position);
        }
    }
    public void Init(int initMonsterIndex,int initCount)
    {
        if(!monsterQueues.ContainsKey(initMonsterIndex))
            monsterQueues.Add(initMonsterIndex, new Queue<GameObject>());

        for (int i = 0; i < initCount; i++)
        {
            switch (initMonsterIndex)
            {
                case 0:
                    monsterQueues[initMonsterIndex].Enqueue(TestMonsterFactory.instance.Spawn(0));
                    break;
                //case 1:
                //    monsterQueues[initMonsterIndex].Enqueue(CreateTestMonster_2());
                //    break;
            }
        }
    }
    //풀에게 몬스터를 호출 하는 함수 , 맞는 몬스터 배열에 몬스터가 있으면 몬스터를 재 설정 하고 내보내기, 아니라면 생성과 재 설정 후 내보내기
    public GameObject GetMonster(int monsterIndex,Vector3 spawnPos)
    {
        if(!monsterQueues.ContainsKey(monsterIndex))
            monsterQueues.Add(monsterIndex, new Queue<GameObject>());

        if(monsterQueues[monsterIndex].Count > 0)
        {
            var monster = monsterQueues[monsterIndex].Dequeue();
            monster.transform.SetParent(null);
            monster.transform.position = spawnPos;
            monster.gameObject.SetActive(true);
            return monster;
        }

        else
        {
            switch(monsterIndex)
            {
                case 0:
                    var monster = TestMonsterFactory.instance.Spawn(0);
                    monster.transform.SetParent(null);
                    monster.transform.position = spawnPos;
                    monster.SetActive(true);
                    return monster;
                case 1:
                    var monster_2 = TestMonsterFactory.instance.Spawn(1);
                    monster_2.transform.SetParent(null);
                    monster_2.transform.position = spawnPos;
                    monster_2.SetActive(true);
                    return monster_2;

                case 10001:
                    var monster_3 = TutorialMonsterFactory.instance.Spawn(10001);
                    monster_3.transform.SetParent(null);
                    monster_3.transform.position = spawnPos;
                    monster_3.SetActive(true);
                    monster_3.GetComponent<AttackMonster>().Setup();
                    return monster_3;

                default:
                    return null;
            }
        }
    }

    public void ReturnMonster(GameObject monster)
    {
        int monsterIndex = monster.GetComponent<MonsterV2>().monsterData.monsterID;
        monster.gameObject.SetActive(false);
        monster.transform.SetParent(transform);
        if(!monsterQueues.ContainsKey(monsterIndex))
        {
            monsterQueues.Add(monsterIndex, new Queue<GameObject>());
        }
        monsterQueues[monsterIndex].Enqueue(monster);
        Debug.Log(monsterQueues[monsterIndex].Count);
    }
}