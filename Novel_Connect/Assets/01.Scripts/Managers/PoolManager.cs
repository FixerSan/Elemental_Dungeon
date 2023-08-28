using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pool
{
    private GameObject prefab;
    private Queue<GameObject> poolObjectQueue;
    private Transform transform_Pool;
    private string poolName;

    public Pool(GameObject _prefab, string _poolName)
    {
        prefab = _prefab;
        poolName = _poolName;
        poolObjectQueue = new Queue<GameObject>();
        Init();
    }

    private void Init()
    {
        GameObject go = GameObject.Find("@Pool");
        if(go == null)
        {
            go = new GameObject{ name = "@Pool" };
        }
        GameObject _transform_Pool = new GameObject { name = poolName };
        _transform_Pool.transform.SetParent(go.transform);
        transform_Pool = _transform_Pool.transform;
    }

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

    public void Push(GameObject _poolObject)
    {
        _poolObject.transform.SetParent(transform_Pool);
        _poolObject.SetActive(false);
        poolObjectQueue.Enqueue(_poolObject);
    }

    public void Clear()
    {
        poolObjectQueue.Clear();
        Managers.Resource.Destroy(transform_Pool.gameObject);
    }
}

public class PoolManager
{
    public Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();

    public void Clear()
    {
        poolDictionary.Clear();
    }

    public GameObject Get(GameObject _poolingObject)
    {
        if (!poolDictionary.ContainsKey(_poolingObject.name))
            CreatePool(_poolingObject);

        return poolDictionary[_poolingObject.name].Get();
    }

    public bool Push(GameObject _go)
    {
        if(poolDictionary.ContainsKey(_go.name))
        {
            poolDictionary[_go.name].Push(_go);
            return true;
        }
        return false;
    }

    public void CreatePool(GameObject _prefab, System.Action _callback = null)
    {
        string key = _prefab.name;
        if (poolDictionary.ContainsKey(key))
            return;
        Pool pool = new Pool(_prefab, $"{key} Pool");
        poolDictionary.Add(key, pool);
        _callback?.Invoke();
    }

    public void DeletePool(string _key)
    {
        if(poolDictionary.ContainsKey(_key))
        {
            poolDictionary[_key].Clear();
            poolDictionary.Remove(_key);
        }
    }
}
