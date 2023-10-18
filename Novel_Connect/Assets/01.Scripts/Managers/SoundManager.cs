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
        EffectSourceController.SetVoulme(effectVolume);
        for (int i = 0; i < effectSourceControllers.Count; i++)
        {
            effectSourceControllers[i].SetVoulme(effectVolume);
        }
    }

    // 효과음 설정
    public void PlaySoundEffect(SoundProfile_Effect _profileName, int _index = -1, Action _callback = null)
    {
        string loadKey = _profileName.ToString();
        Managers.Resource.Load<SoundProfile>(loadKey, (soundProfile) => 
        {
            AudioClip audioClip;
            if (_index == -1)    audioClip = soundProfile.PlaySoundToRandom();
            else                audioClip = soundProfile.PlaySoundToIndex(_index);

            AudioSourceController sourceController = EffectSourceController;
            if (sourceController.AudioSource.isPlaying)
            {
                sourceController = new AudioSourceController();
                effectSourceControllers.Add(sourceController);
            }
            sourceController.Play(audioClip);
            if (_callback != null)
                Managers.Routine.StartCoroutine(PlaySoundCallbackRoutine(audioClip.length, _callback));
        });
    }

    private IEnumerator PlaySoundCallbackRoutine(float _soundLength, Action _callback)
    {
        yield return new WaitForSeconds(_soundLength);
        _callback?.Invoke();
    }

    // 효과음 설정 2
    public void PlaySoundEffect(AudioClip_Effect _clip, Action _callback = null)
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
            if (_callback != null)
                Managers.Routine.StartCoroutine(PlaySoundCallbackRoutine(effectAudioClip.length, _callback));

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

    public void PlayBGM(SoundProfile_BGM _bgm, int index = 0)
    {
        string loadKey = _bgm.ToString();
        Managers.Resource.Load<SoundProfile>(loadKey, (bgmClip) => 
        {
            BgmSource.clip = bgmClip.PlaySoundToIndex(index);
            BgmSource.Play();
        });
    }

    // 배경음악 FadeIn 설정
    public void FadeInBGM(AudioClip_BGM _bgm, float _fadeTime, Action _callback = null)
    {
        Managers.Routine.StartCoroutine(FadeInBGMRoutine(_bgm, _fadeTime, _callback));
    }

    // 배경음악 FadeIn 루틴
    private IEnumerator FadeInBGMRoutine(AudioClip_BGM _bgm, float _fadeTime, Action _callback = null)
    {
        BgmSource.volume = 0;
        PlayBGM(_bgm);

        while (BgmSource.volume < bgmVolume)
        {
            BgmSource.volume += bgmVolume * Time.deltaTime / _fadeTime;
            yield return null;
        }

        _callback?.Invoke();
    }

    public void FadeInBGM(SoundProfile_BGM _bgm, float _fadeTime, int _index = 0, Action _callback = null)
    {
        Managers.Routine.StartCoroutine(FadeInBGMRoutine(_bgm, _fadeTime, _index, _callback));
    }

    private IEnumerator FadeInBGMRoutine(SoundProfile_BGM _bgm,  float _fadeTime, int _index = 0,  Action _callback = null)
    {
        BgmSource.volume = 0;
        PlayBGM(_bgm, _index);

        while (BgmSource.volume < bgmVolume)
        {
            BgmSource.volume += bgmVolume * Time.deltaTime / _fadeTime;
            yield return null;
        }

        _callback?.Invoke();
    }

    // 배경음악 FadeOut 설정
    public void FadeOutBGM(float _fadeTime, Action _callback = null)
    {
        Managers.Routine.StartCoroutine(FadeOutBGMRoutine(_fadeTime, _callback));
    }

    // 배걍음악 FadeOut 루틴
    private IEnumerator FadeOutBGMRoutine(float fadeTime, Action _callback = null)
    {
        while (BgmSource.volume > 0.0f)
        {
            BgmSource.volume -= bgmVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        BgmSource.Stop();
        _callback?.Invoke();
    }

    // 배경음악 FadeIn,FadeOut으로 변경
    public void FadeChangeBGM(AudioClip_BGM _bgm, float _fadeTotalTime, Action _callback = null)
    {
        Managers.Routine.StartCoroutine(FadeChangeBGMRoutine(_bgm, _fadeTotalTime, _callback));
    }

    // 배경음악 FadeIn, FadeOut 루틴
    private IEnumerator FadeChangeBGMRoutine(AudioClip_BGM _bgm, float _fadeTotalTime, Action _callback = null)
    {
        yield return Managers.Routine.StartCoroutine(FadeOutBGMRoutine(_fadeTotalTime * 0.5f));
        yield return Managers.Routine.StartCoroutine(FadeInBGMRoutine(_bgm, _fadeTotalTime * 0.5f));
        _callback?.Invoke();
    }

    public void FadeChangeBGM(SoundProfile_BGM _bgm, float _fadeTotalTime, int _index = 0,Action _callback = null)
    {
        Managers.Routine.StartCoroutine(FadeChangeBGMRoutine(_bgm, _fadeTotalTime, _index, _callback));
    }

    // 배경음악 FadeIn, FadeOut 루틴
    private IEnumerator FadeChangeBGMRoutine(SoundProfile_BGM _bgm, float _fadeTotalTime, int _index = 0, Action _callback = null)
    {
        yield return Managers.Routine.StartCoroutine(FadeOutBGMRoutine(_fadeTotalTime * 0.5f)) ;
        yield return Managers.Routine.StartCoroutine(FadeInBGMRoutine(_bgm, _fadeTotalTime * 0.5f, _index));
        _callback?.Invoke();
    }
}
