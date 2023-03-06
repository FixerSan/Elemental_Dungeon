using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDatabase : MonoBehaviour
{
    public List<Monster> monsters;

    public Monster GetMonster(int index)
    {
        return monsters[index];
    }
}
