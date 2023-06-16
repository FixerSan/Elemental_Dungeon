using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteEffect : MonoBehaviour
{
    #region Singleton, DontDestoryOnLoad;
    private static SpriteEffect Instance;
    public static SpriteEffect instance
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
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }
    #endregion

    public void FadeIn(SpriteRenderer sr, float fadeTime)
    {
        StartCoroutine(FadeInCoroutine(sr, fadeTime));
    }
    public IEnumerator FadeInCoroutine(SpriteRenderer sr,float fadeTime)
    {
        sr.color = new Color(0, 0, 0, 0);
        while (sr.color.a < 1)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + Time.deltaTime / fadeTime);
            yield return null;
        }
    }

    public void FadeOut(SpriteRenderer sr, float fadeTime)
    {
        StartCoroutine(FadeOutCoroutine(sr, fadeTime));
    }

    public IEnumerator FadeOutCoroutine(SpriteRenderer sr,float fadeTime)
    {
        while (sr.color.a > 0)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime / fadeTime);
            yield return null;
        }
    }
}
