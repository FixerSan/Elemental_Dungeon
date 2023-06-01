using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region ½Ì±ÛÅæ ¹× DontDestroy
    private static AudioManager Instance;

    public static AudioManager instance
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


    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioSource interactionSource;

    public float soundVolume = 1.0f;
    public float bgmVolume = 1.0f;


    private bool fadeInMusicflag = false;
    public void PlayBGM(AudioClip clip)
    {
        if (!clip) return;
        bgmSource.clip = clip;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }
    public void PlayOneShot(AudioClip clip)
    {
        if (!clip) return;
        effectSource.clip = clip;
        effectSource.volume = soundVolume;
        effectSource.PlayOneShot(effectSource.clip);
    }
    public void PlayOneShotInteraction(AudioClip clip)
    {
        if (!clip) return;
        interactionSource.clip = clip;
        interactionSource.volume = soundVolume;
        interactionSource.PlayOneShot(interactionSource.clip);
    }
    
    public IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        float startVolume = 0.0f;
        audioSource.volume = startVolume;
        audioSource.Play();

        while(audioSource.volume < bgmVolume)
        {
            audioSource.volume += bgmVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
    }
    public IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while(audioSource.volume > 0.0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
    }

    public void FadeInMusic(AudioClip newMusic, float fadeTime)
    {
        if (!newMusic) return;
        if (fadeInMusicflag) return;

        StartCoroutine(FadeInMusicCoroutine(newMusic, fadeTime));
    }

    public IEnumerator FadeInMusicCoroutine(AudioClip newMusic, float fadeTime)
    {
        fadeInMusicflag = true;

        yield return StartCoroutine(FadeOut(bgmSource, fadeTime));

        bgmSource.clip = newMusic;
        yield return StartCoroutine(FadeIn(bgmSource, fadeTime));
    }
}
