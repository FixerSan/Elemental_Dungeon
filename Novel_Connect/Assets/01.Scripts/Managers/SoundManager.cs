using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class SoundManager 
{
    private Transform sourceTransform;  // 오디오 소스 위치 선언
    public Transform SourceTransform    // 오디오 소스 위치 프로퍼티 선언
    {
        get
        {
            if(sourceTransform == null)
            {
                GameObject go = GameObject.Find("@AudioSources");
                if(go == null)
                    go = new GameObject(name : "@AudioSources");
                sourceTransform = go.transform;
                UnityEngine.Object.DontDestroyOnLoad(go);
            }
            return sourceTransform;
        }
    }

    private AudioSource bgmSource;      // 배경음악 오디오 소스 선언
    public AudioSource BgmSource        // 배경음악 오디오 소스 프로퍼티
    { 
        get
        {
            if(bgmSource == null)
            {
                GameObject go = new GameObject(name: "AudioSource_BGM");
                go.transform.SetParent(SourceTransform);
                bgmSource = go.GetOrAddComponent<AudioSource>();
                bgmSource.playOnAwake = false;
                bgmSource.loop = true;
            }    
            return bgmSource;
        } 
    }


    private AudioSourceController effectSourceController;   // 기본 효과음 오디오 소스 선언
    public AudioSourceController EffectSourceController     // 기본 효과음 오디오 소스 프로퍼티 선언
    {
        get
        {
            if (effectSourceController == null)
                effectSourceController = new AudioSourceController();
            return effectSourceController;
        }
    }

    public List<AudioSourceController> effectSourceControllers = new List<AudioSourceController>(); // 서브 효과음 오디오소스 리스트 선언 
    public float bgmVolume = 1;                                                                     // 배경음악 오디오 소스 볼륨
    public float effectVolume = 1;                                                                  // 효과음 오디오 소스 볼륨
    private bool isFading;                                                                          // Fading 상태인지 체크

    // 배경음악 볼륨 설정
    public void SetBGMVolume(float _volume)
    {
        bgmVolume = _volume;
        BgmSource.volume = bgmVolume;
    }

    // 효과음 볼륨 설정
    public void SetEffectVolume(float _volume)
    {
        effectVolume = _volume;
        effectSourceController.SetVoulme(effectVolume);
        for (int i = 0; i < effectSourceControllers.Count; i++)
        {
            effectSourceControllers[i].SetVoulme(effectVolume);
        }
    }

    // 효과음 설정
    public void PlaySoundEffect(SoundProfile_Effect _profileName ,int index = -1)
    {
        string loadKey = _profileName.ToString();
        Managers.Resource.Load<SoundProfile>(loadKey, (soundProfile) => 
        {
            AudioClip audioClip;
            if (index == -1)    audioClip = soundProfile.PlaySoundToRandom();
            else                audioClip = soundProfile.PlaySoundToIndex(index);

            AudioSourceController sourceController = EffectSourceController;
            if (sourceController.AudioSource.isPlaying)
            {
                sourceController = new AudioSourceController();
                effectSourceControllers.Add(sourceController);
            }
            sourceController.Play(audioClip);
        });
    }

    // 효과음 설정 2
    public void PlaySoundEffect(AudioClip_Effect _clip)
    {
        string loadKey = _clip.ToString();
        Managers.Resource.Load<AudioClip>(loadKey, (effectAudioClip) =>
        {
            AudioSourceController sourceController = EffectSourceController;
            if (sourceController.AudioSource.isPlaying)
            {
                sourceController = new AudioSourceController();
                effectSourceControllers.Add(sourceController);
            }
            sourceController.Play(effectAudioClip);
        });
    }

    // 효과음 종료
    public void StopSoundEffect(AudioSourceController _audioSourceController)
    {
        if (EffectSourceController == _audioSourceController)
        {
            _audioSourceController.Stop();
            return;
        }

        _audioSourceController.RemoveAudioSource();
        effectSourceControllers.Remove(_audioSourceController);
    }

    // 배경음악 설정 
    public void PlayBGM(AudioClip_BGM _bgm)
    {
        string loadKey = _bgm.ToString();
        BgmSource.Stop();
        Managers.Resource.Load<AudioClip>(loadKey, (bgmClip) =>
        {
            BgmSource.clip = bgmClip;
            BgmSource.Play();
        });
    }

    // 배경음악 FadeIn 설정
    public void FadeInBGM(AudioClip_BGM _bgm, float _fadeTime)
    {
        Managers.Routine.StartCoroutine(FadeInBGMRoutine(_bgm, _fadeTime));
    }

    // 배경음악 FadeIn 루틴
    private IEnumerator FadeInBGMRoutine(AudioClip_BGM _bgm, float _fadeTime)
    {
        BgmSource.volume = 0;
        PlayBGM(_bgm);

        while (BgmSource.volume < bgmVolume)
        {
            BgmSource.volume += bgmVolume * Time.deltaTime / _fadeTime;
            yield return null;
        }
    }

    // 배경음악 FadeOut 설정
    public void FadeOutBGM(float _fadeTime)
    {
        Managers.Routine.StartCoroutine(FadeOutBGMRoutine(_fadeTime));
    }

    // 배걍음악 FadeOut 루틴
    private IEnumerator FadeOutBGMRoutine(float fadeTime)
    {
        while (BgmSource.volume > 0.0f)
        {
            BgmSource.volume -= bgmVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        BgmSource.Stop();
    }

    // 배경음악 FadeIn,FadeOut으로 변경
    public void FadeChangeBGM(AudioClip_BGM _bgm, float _fadeTime)
    {
        Managers.Routine.StartCoroutine(FadeChangeBGMRoutine(_bgm,_fadeTime));
    }

    // 배경음악 FadeIn, FadeOut 루틴
    private IEnumerator FadeChangeBGMRoutine(AudioClip_BGM _bgm, float _fadeTime)
    {
        yield return Managers.Routine.StartCoroutine(FadeOutBGMRoutine(_fadeTime));
        yield return Managers.Routine.StartCoroutine(FadeInBGMRoutine(_bgm, _fadeTime));
    }
}
