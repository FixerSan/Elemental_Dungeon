using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleSystem : MonoBehaviour
{
    #region Singleton, DontDestoryOnLoad;
    private static SpeechBubbleSystem Instance;
    public static SpeechBubbleSystem instance
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

    public void SetSpeechBuble(int index , Transform transform , Vector3 pos)
    {
        GameObject speechBuble = SpeechBubbleObjectPool.instance.GetSpeechBuble(index);

        speechBuble.transform.SetParent(transform);
        speechBuble.transform.localPosition = pos;
        speechBuble.transform.rotation = Quaternion.identity;
    }

    public void SetSpeechBuble(int index, Transform transform, Vector3 pos, float scale)
    {
        GameObject speechBuble = SpeechBubbleObjectPool.instance.GetSpeechBuble(index);

        speechBuble.transform.localScale = new Vector3(scale,scale,scale);
        speechBuble.transform.SetParent(transform);
        speechBuble.transform.localPosition = pos;
        speechBuble.transform.rotation = Quaternion.identity;
    }
}
