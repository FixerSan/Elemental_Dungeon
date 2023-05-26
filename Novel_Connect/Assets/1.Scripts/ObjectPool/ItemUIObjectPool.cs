using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIObjectPool : MonoBehaviour
{
    #region Sington
    private static ItemUIObjectPool Instance;
    public static ItemUIObjectPool instance
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
    [SerializeField] private GameObject itemUIPrefab;
    public Queue<GameObject> itemUIs = new Queue<GameObject>();
    public void Init(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            GameObject itemUI = Instantiate(itemUIPrefab);
            itemUI.SetActive(false);
            itemUI.transform.SetParent(transform);
            itemUI.transform.position = transform.position;
            itemUIs.Enqueue(itemUI);
        }
    }

    public GameObject GetItemUI(Transform transform_)
    {
        if (itemUIs.Count > 0)
        {
            var itemUI = itemUIs.Dequeue();
            itemUI.transform.SetParent(transform_);
            itemUI.transform.position = transform_.position;
            itemUI.gameObject.SetActive(true);
            return itemUI;
        }

        Init(1);
        var itemUI_ = itemUIs.Dequeue();
        itemUI_.transform.SetParent(transform_);
        itemUI_.transform.position = transform_.position;
        itemUI_.gameObject.SetActive(true);
        return itemUI_;

    }
}
