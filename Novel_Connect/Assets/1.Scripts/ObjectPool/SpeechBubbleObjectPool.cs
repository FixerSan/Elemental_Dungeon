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

    public GameObject GetSpeechBuble(int index)
    {
        SpeechBubbleData data = DataBase.instance.GetSpeechBuble(index);
        GameObject speechBuble;
        
        if(speechBubleQueue.Count > 0)
            speechBuble = speechBubleQueue.Dequeue();

        else
            speechBuble = Instantiate(speechBublePrefab);

        speechBuble.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(data.path);
        speechBuble.transform.SetParent(null);
        speechBuble.SetActive(true);

        SpeechBuble sb = speechBuble.GetComponent<SpeechBuble>();

        sb.data = data;
        sb.StartCoroutine(sb.Disable());
        return speechBuble;
    }

    public void ReturnSpeechBuble(GameObject speechBuble)
    {
        speechBuble.transform.SetParent(transform);
        speechBuble.GetComponent<SpeechBuble>().data = null;
        speechBuble.transform.position = transform.position;
        speechBuble.GetComponent<SpriteRenderer>().sprite = null;
        speechBubleQueue.Enqueue(speechBuble);
    }

    private void Start()
    {
        Setup();
    }
}
