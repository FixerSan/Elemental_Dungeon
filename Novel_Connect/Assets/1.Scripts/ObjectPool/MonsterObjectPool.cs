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
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    #region Monster
    public Dictionary<int, Queue<GameObject>> monsterQueues = new Dictionary<int, Queue<GameObject>>();
    public void Init(int initMonsterIndex,int initCount)
    {
        if(!monsterQueues.ContainsKey(initMonsterIndex))
            monsterQueues.Add(initMonsterIndex, new Queue<GameObject>());


        for (int i = 0; i < initCount; i++)
        {
            GameObject monster = MonsterFactory.instance.Spawn(initMonsterIndex);
            monster.SetActive(false);
            monster.transform.SetParent(transform);
            monster.transform.position = transform.position;
            monsterQueues[initMonsterIndex].Enqueue(monster);
        }
    }
    //Ǯ���� ���͸� ȣ�� �ϴ� �Լ� , �´� ���� �迭�� ���Ͱ� ������ ���͸� �� ���� �ϰ� ��������, �ƴ϶�� ������ �� ���� �� ��������
    public GameObject GetMonster(int monsterIndex,Vector3 spawnPos)
    {
        if(!monsterQueues.ContainsKey(monsterIndex))
            monsterQueues.Add(monsterIndex, new Queue<GameObject>());

        if(monsterQueues[monsterIndex].Count > 0)
        {
            GameObject monster = monsterQueues[monsterIndex].Dequeue();
            monster.transform.SetParent(null);
            monster.transform.position = spawnPos;
            monster.gameObject.SetActive(true);
            return monster;
        }

        else
        {
            GameObject monster = MonsterFactory.instance.Spawn(monsterIndex);
            monster.transform.SetParent(null);
            monster.transform.position = spawnPos;
            monster.gameObject.SetActive(true);
            return monster;
        }
    }

    public void ReturnMonster(GameObject monster)
    {
        int monsterIndex = monster.GetComponent<BaseMonster>().monsterData.monsterID;
        monster.gameObject.SetActive(false);
        monster.transform.SetParent(transform);
        if(!monsterQueues.ContainsKey(monsterIndex))
        {
            monsterQueues.Add(monsterIndex, new Queue<GameObject>());
        }
        monsterQueues[monsterIndex].Enqueue(monster);
    }

    #endregion
}