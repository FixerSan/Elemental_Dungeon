using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pool
{
    private GameObject prefab; // ����� ������
    private Queue<GameObject> poolObjectQueue; // ���� ������Ʈ�� ��Ȱ���� ť
    private Transform transform_Pool; // Ǯ�� �θ� Ʈ������
    private string poolName; // Ǯ�� �̸�

    // �ʱ�ȭ
    public Pool(GameObject _prefab, string _poolName)
    {
        prefab = _prefab;
        poolName = _poolName;
        poolObjectQueue = new Queue<GameObject>();
        Init(); 
    }

    // ���ӿ�����Ʈ ��ġ �� ��ġ ����
    private void Init()
    {
        GameObject go = GameObject.Find("@Pool");
        if (go == null)
        {
            go = new GameObject { name = "@Pool" };
        }
        GameObject _transform_Pool = new GameObject { name = poolName };
        _transform_Pool.transform.SetParent(go.transform);
        transform_Pool = _transform_Pool.transform;
    }

    // ���� ������Ʈ ��ȯ
    public GameObject Get()
    {
        GameObject poolObject;
        if (poolObjectQueue.TryDequeue(out GameObject _poolObject))
        {
            poolObject = _poolObject;
        }

        else
        {
            poolObject = GameObject.Instantiate(prefab);
            poolObject.name = prefab.name;
        }

        poolObject.SetActive(true); 
        return poolObject;
    }

    // ���� ������Ʈ Ǫ��
    public void Push(GameObject _poolObject)
    {
        _poolObject.transform.SetParent(transform_Pool);
        _poolObject.SetActive(false); 
        poolObjectQueue.Enqueue(_poolObject); 
    }

    // Ǯ �ʱ�ȭ
    public void Clear()
    {
        poolObjectQueue.Clear();
    }
}

public class PoolManager 
{
    public Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();    // Ǯ ��ųʸ�

    // ��� Ǯ Ŭ����
    public void Clear()
    {
        foreach (var item in poolDictionary)
        {
            poolDictionary[item.Key].Clear();
        }
        poolDictionary.Clear(); 
    }

    // ���� ������Ʈ�� Ǯ���� ������
    public GameObject Get(GameObject _poolingObject)
    {
        if (!poolDictionary.ContainsKey(_poolingObject.name))
            CreatePool(_poolingObject);

        return poolDictionary[_poolingObject.name].Get(); 
    }

    // ���� ������Ʈ Ǫ��
    public bool Push(GameObject _go)
    {
        if (poolDictionary.ContainsKey(_go.name))
        {
            poolDictionary[_go.name].Push(_go);
            return true;
        }
        return false;
    }

    // Ư�� �������� Ǯ ���� ���� Ȯ��
    public bool CheckExist(GameObject _prefab)
    {
        string key = _prefab.name;
        return poolDictionary.ContainsKey(key);
    }

    // Ǯ ���� �� �ʱ�ȭ
    public void CreatePool(GameObject _prefab, System.Action _callback = null)
    {
        string key = _prefab.name;
        if (poolDictionary.ContainsKey(key))
            return;
        Pool pool = new Pool(_prefab, $"{key} Pool"); 
        poolDictionary.Add(key, pool); 
        _callback?.Invoke();
    }

    // Ư�� Ǯ ����
    public void DeletePool(string _key)
    {
        if (poolDictionary.ContainsKey(_key))
        {
            poolDictionary[_key].Clear(); 
            poolDictionary.Remove(_key); 
        }
    }

    public void Init()
    {
        Clear();
    }
}
