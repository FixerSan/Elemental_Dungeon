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
                Debug.Log("������ ���̵� 1�� �������� ��ų�� ����");
                return true;
            default:

                return false;
        }
    }
}
