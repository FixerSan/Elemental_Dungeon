using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Singleton
    private static ObjectPool Instance;
    public static ObjectPool instance
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
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion
    [SerializeField]
    private GameObject poolingObjectPrefab;

    Queue<MonsterV2> poolingObjectQueue = new Queue<MonsterV2>();
    private void Init(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private MonsterV2 CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<MonsterV2>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static MonsterV2 GetObject()
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject(MonsterV2 obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}
