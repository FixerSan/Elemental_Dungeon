using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMonsterFactory : MonsterFactory
{
    #region Sington
    private static TutorialMonsterFactory Instance;
    public static TutorialMonsterFactory instance
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
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    [SerializeField]
    private GameObject tutorialMonsterPrefab;
    protected override MonsterV2 Create(int index)
    {
        AttackMonster monster = null;
        switch (index)
        {
            case 10001:
                monster = Instantiate(tutorialMonsterPrefab).GetComponent<AttackMonster>();
                monster.monsterData = new MonsterData(index);
                break;
        }

        return monster;
    }
}
