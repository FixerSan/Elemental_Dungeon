using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect
{
    public bool UseItem(int itemID)
    {
        switch(itemID)
        {
            case 1:
                Debug.Log("아이템 아이디 1번 아이템의 스킬이 사용됨");
                return true;
            default:

                return false;
        }
    }
}
