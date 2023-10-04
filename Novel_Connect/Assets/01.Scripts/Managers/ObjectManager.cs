using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class ObjectManager
{
    private PlayerController player;    // �÷��̾� ���� ����
    public PlayerController Player      // �÷��̾� ������Ƽ ����
    {
        get 
        {
            if (player == null)
            {
                GameObject go = GameObject.Find("Player");
                if(go == null)
                {
                    player = SpawnPlayer(Vector3.zero);
                    return player;
                }
                player = go.GetOrAddComponent<PlayerController>();
            }
            return player; 
        } 
    }
    public HashSet<MonsterController> Monsters { get; } = new HashSet<MonsterController>();     // ���� �ؽ� ��
    private Transform monsterTrasnform;
    public Transform MonsterTransform                                                           // ���� ���� ��ġ ����
    {
        get
        {
            if (monsterTrasnform == null)
            {
                GameObject root = GameObject.Find("@Monster");
                if (root == null)
                {
                    root = new GameObject { name = "@Monster" };
                    root.transform.position = Vector3.zero;
                }
                monsterTrasnform = root.transform;
            }
            return monsterTrasnform;
        }
    }
    private Transform itemTransform;
    public Transform ItemTransform                                                           // ������ ���� ��ġ ����
    {
        get
        {
            if(itemTransform == null)
            {
                GameObject root = GameObject.Find("@Item");
                if (root == null)
                {
                    root = new GameObject { name = "@Item" };
                    root.transform.position = Vector3.zero;
                }
                itemTransform = root.transform;
            }
            return itemTransform;
        }
    }

    // ���� �ʱ�ȭ
    public void ClearMonsters()
    {
        Monsters.Clear();
    }

    // �÷��̾� ����
    public PlayerController SpawnPlayer(Vector3 _position)
    {
        GameObject go = Managers.Resource.Instantiate("Player");
        PlayerController pc = go.GetOrAddComponent<PlayerController>();
        pc.transform.position = _position;
        player = pc;
        return pc;
    }

    // ���� ����
    public MonsterController SpawnMonster(Vector3 _position, Define.Monster _monster)
    {
        GameObject go = null;
        switch (_monster)
        {
            case Define.Monster.Ghost_Bat:
                go = Managers.Resource.Instantiate($"{_monster.ToString()}", _pooling: true);
                break;
        }

        MonsterController mc = go.GetOrAddComponent<MonsterController>();
        go.transform.position = _position;
        Monsters.Add(mc);
        mc.Init((int)_monster);
        return mc;
    }

    public BossController SpawnBoss(int _bossUID, Vector2 _position)
    {
        GameObject go = null;
        switch (_bossUID)
        {
            case 0:
                go = Managers.Resource.Instantiate($"Ice_Boss");
                break;
        }

        BossController mc = go.GetOrAddComponent<BossController>();
        go.transform.position = _position;
        mc.Init(_bossUID);
        return mc;
    }

    public ItemController SpawnItem(int _itemUID, Vector2 _position, int _count = 1)
    {
        ItemController ic = Managers.Resource.Instantiate("ItemController", _pooling: true).GetComponent<ItemController>();
        ic.transform.position = _position;
        ic.Init(CreateItem<BaseItem>(_itemUID, _count: _count));
        return ic;
    }

    // ���� ����
    public void Despawn<T>(T _object) where T : BaseController
    {
        System.Type type = typeof(T);

        if (type == typeof(MonsterController))
        {
            Monsters.Remove(_object as MonsterController);
            Managers.Resource.Destroy(_object.gameObject);
        }
    }
    
    // ������ ����
    public T CreateItem<T>(int _itemUID, int _count = 1) where T : BaseItem
    {
        BaseItem baseItem = new BaseItem(_itemUID, _count);
        return baseItem as T;
    }
}
