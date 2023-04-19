using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleObjectPool : MonoBehaviour
{
    #region Singleton, DontDestoryOnLoad;
    private static SpeechBubbleObjectPool Instance;
    public static SpeechBubbleObjectPool instance
    {
        get
        {
            if (Instance != null)
                return Instance;
            else
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
            Destroy(gameObject);
    }
    #endregion
    [SerializeField]
    private GameObject speechBublePrefab;
    Queue<GameObject> speechBubleQueue = new Queue<GameObject>();
    
    void Setup()
    {
        Init(1);
    }

    void Init(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject speechBuble = Instantiate(speechBublePrefab);

            speechBuble.SetActive(false);
            speechBuble.transform.position = transform.position;
            speechBuble.transform.SetParent(transform);
            speechBuble.GetComponent<SpriteRenderer>().sprite = null;
            speechBubleQueue.Enqueue(speechBuble);
        }
    }

    public GameObject GetSpeechBuble(int index, Transform pos)
    {
        SpeechBubbleData data = DataBase.instance.GetSpeechBuble(index);
        GameObject speechBuble;
        
        if(speechBubleQueue.Count > 0)
        {
            speechBuble = speechBubleQueue.Dequeue();
            speechBuble.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(data.path);
            speechBuble.transform.SetParent(null);
            speechBuble.transform.position = pos.position;
            speechBuble.SetActive(true);
        }

        else
        {
            speechBuble = Instantiate(speechBublePrefab);
            speechBuble.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(data.path);
            speechBuble.transform.SetParent(null);
            speechBuble.transform.position = pos.position;
            speechBuble.SetActive(true);
        }
        return speechBuble;
    }

    public void ReturnSpeechBuble(GameObject speechBuble)
    {
        speechBuble.SetActive(false);
        speechBuble.transform.position = transform.position;
        speechBuble.transform.SetParent(transform);
        speechBuble.GetComponent<SpriteRenderer>().sprite = null;
        speechBubleQueue.Enqueue(speechBuble);
    }

    private void Start()
    {
        Setup();
    }
}
