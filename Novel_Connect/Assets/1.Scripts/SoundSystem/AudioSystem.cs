using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private AudioSystem() { }
    public static AudioSystem Instance { get; private set; }
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioSource buttonSource;

    public float bgmVolume = 1.0f;
    public float effectVolume = 1.0f;
    public float buttonVolume = 1.0f;

    private bool fadeInMusicflag = false;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        else
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //음악 호출
    public void PlayMusic(string audioName)
    {
        AudioClip clip = DataBase.instance.GetAudioClip(audioName);
        if (!clip) return;
        bgmSource.clip = clip;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }
    //효과음 호출
    public void PlayOneShot(string audioName)
    {
        AudioClip clip = DataBase.instance.GetAudioClip(audioName);
        if (!clip) return; 
        effectSource.clip = clip;
        effectSource.volume = effectVolume;
        effectSource.PlayOneShot(effectSource.clip);
    }

    public void PlayOneShot(AudioClip clip)
    {
        if (!clip) return;
        effectSource.clip = clip;
        effectSource.volume = effectVolume;
        effectSource.PlayOneShot(effectSource.clip);
    }

    public void PlayOneShotSoundProfile(string soundProfileName)
    {
        AudioClip clip = DataBase.instance.GetSoundProfileData(soundProfileName).GetRandomClip();
        if (!clip) return;
        effectSource.clip = clip;
        effectSource.volume = effectVolume;
        effectSource.PlayOneShot(effectSource.clip);
    }

    public void PlayOneShotSoundProfile(string soundProfileName,int index)
    {
        AudioClip clip = DataBase.instance.GetSoundProfileData(soundProfileName).GetClipIndex(index);
        if (!clip) return;
        effectSource.clip = clip;
        effectSource.volume = effectVolume;
        effectSource.PlayOneShot(effectSource.clip);
    }

    //버튼과 같은 인터렉션음 호출
    public void PlayOneShotButton(string audioName)
    {
        AudioClip clip = DataBase.instance.GetAudioClip(audioName);
        if (!clip) return; buttonSource.clip = clip;
        Debug.Log(clip.name);
        buttonSource.volume = buttonVolume;
        buttonSource.PlayOneShot(buttonSource.clip);
    }
    public IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        float startVolume = 0.0f;
        audioSource.volume = startVolume;
        audioSource.Play();

        while (audioSource.volume < bgmVolume)
        {
            audioSource.volume += bgmVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
    }
    public IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0.0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
    }
    //음악 페이드인, 아웃으로 호출
    public void FadeInMusic(string profileName, int index, float fadeTime)
    {
        AudioClip newMusic = DataBase.instance.GetSoundProfileData(profileName).GetClipIndex(index);
        if (!newMusic) return;
        if (fadeInMusicflag) return;

        StartCoroutine(FadeInMusicCoroutine(newMusic, fadeTime));
    }
    private IEnumerator FadeInMusicCoroutine(AudioClip newMusic, float fadeTime)
    {
        fadeInMusicflag = true;
        yield return StartCoroutine(FadeOut(bgmSource, fadeTime));
        bgmSource.clip = newMusic;
        yield return StartCoroutine(FadeIn(bgmSource, fadeTime));
        fadeInMusicflag = false;
    }
}
