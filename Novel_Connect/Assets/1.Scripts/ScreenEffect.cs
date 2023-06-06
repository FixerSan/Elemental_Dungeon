using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffect : MonoBehaviour
{
    #region Singleton, DontDestoryOnLoad;
    private static ScreenEffect Instance;
    public static ScreenEffect instance
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
    public Image fadePanel;

    public IEnumerator FadeIn(float fadeTime)
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0, 0, 0, 0);
        while (fadePanel.color.a < 1)
        {
            fadePanel.color = new Color(0, 0, 0, fadePanel.color.a + Time.deltaTime / fadeTime);
            yield return null;
        }
    }

    public IEnumerator FadeOut(float fadeTime)
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0, 0, 0, 1);
        while (fadePanel.color.a > 0)
        {
            fadePanel.color = new Color(0, 0, 0, fadePanel.color.a - Time.deltaTime / fadeTime);
            yield return null;
        }
        fadePanel.gameObject.SetActive(false);
    }

    public IEnumerator FadeInOut(float fadeTime)
    {
        yield return StartCoroutine(FadeIn(fadeTime));
        yield return StartCoroutine(FadeOut(fadeTime));
    }

    public void Shake(float time)
    {
        CameraScript.instance.StartCoroutine(CameraScript.instance.Shake(time));
    }
}
