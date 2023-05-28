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

    public IEnumerator FadeIn()
    {
        bool isStart = false;
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0, 0, 0, 0);
        while (!isStart)
        {
            fadePanel.color = new Color(0, 0, 0, fadePanel.color.a + 0.02f);
            if (fadePanel.color.a >= 1)
                isStart = true;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator FadeOut()
    {
        bool isEnd = false;
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0, 0, 0, 1);
        while (!isEnd)
        {
            fadePanel.color = new Color(0, 0, 0, fadePanel.color.a - 0.02f);
            if (fadePanel.color.a <= 0)
                isEnd = true;
            yield return new WaitForSeconds(0.02f);
        }
        fadePanel.gameObject.SetActive(false);
    }

    public IEnumerator FadeInOut()
    {
        bool isStart = false;
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0, 0, 0, 0);
        while (!isStart)
        {
            fadePanel.color = new Color(0, 0, 0, fadePanel.color.a + 0.02f);
            if (fadePanel.color.a >= 1)
                isStart = true;
            yield return new WaitForSeconds(0.01f);
        }

        bool isEnd = false;
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0, 0, 0, 1);
        while (!isEnd)
        {
            fadePanel.color = new Color(0, 0, 0, fadePanel.color.a - 0.02f);
            if (fadePanel.color.a <= 0)
                isEnd = true;
            yield return new WaitForSeconds(0.01f);
        }
        fadePanel.gameObject.SetActive(false);
    }

    public void Shake(float time)
    {
        CameraScript.instance.StartCoroutine(CameraScript.instance.Shake(time));
    }
}
