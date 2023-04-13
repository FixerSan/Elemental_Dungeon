using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonsterFactory : MonsterFactory
{
    #region Sington
    private static TestMonsterFactory Instance;
    public static TestMonsterFactory instance
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
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion
    [SerializeField]
    private GameObject testMonsterPrefab;
    protected override MonsterV2 Create(int index)
    {
        TestMonster monster = null;
        switch(index)
        {
            case 0:
                monster = Instantiate(testMonsterPrefab).GetComponent<TestMonster>();
                monster.monsterData = new MonsterData(index);
                break;
        }

        return monster;
    }
}
