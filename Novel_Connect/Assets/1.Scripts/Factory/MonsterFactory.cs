using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory: MonoBehaviour
{
    #region Sington
    private static MonsterFactory Instance;
    public static MonsterFactory instance
    {
        get
        {
            if (Instance != null)
                return Instance;
            return null;
        }
    }

    private void Awake()
    {
        if (Instance == null)
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

    [SerializeField]private GameObject baseMonsterPrefab;

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
                monster = Instantiate(baseMonsterPrefab).GetComponent<BaseMonster>();
                monster.monsterData = new MonsterData(index);
                break;
            case 10001:
                monster = Instantiate(baseMonsterPrefab).GetComponent<BaseMonster>();
                monster.monsterData = new MonsterData(index);
                break;
        }

        return monster;
    }
}
