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

    public void SetSpeechBuble(int index)
    {
        SpeechBubbleData speechBubble = DataBase.instance.GetSpeechBuble(index);

        
    }
}