using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class SoundManager 
{
    private Transform sourceTransform;  // ����� �ҽ� ��ġ ����
    public Transform SourceTransform    // ����� �ҽ� ��ġ ������Ƽ ����
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

    private AudioSource bgmSource;      // ������� ����� �ҽ� ����
    public AudioSource BgmSource        // ������� ����� �ҽ� ������Ƽ
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


    private AudioSourceController effectSourceController;   // �⺻ ȿ���� ����� �ҽ� ����
    public AudioSourceController EffectSourceController     // �⺻ ȿ���� ����� �ҽ� ������Ƽ ����
    {
        get
        {
            if (effectSourceController == null)
                effectSourceController = new AudioSourceController();
            return effectSourceController;
        }
    }

    public List<AudioSourceController> effectSourceControllers = new List<AudioSourceController>(); // ���� ȿ���� ������ҽ� ����Ʈ ���� 
    public float bgmVolume = 1;                                                                     // ������� ����� �ҽ� ����
    public float effectVolume = 1;                                                                  // ȿ���� ����� �ҽ� ����
    private bool isFading;                                                                          // Fading �������� üũ

    // ������� ���� ����
    public void SetBGMVolume(float _volume)
    {
        bgmVolume = _volume;
        BgmSource.volume = bgmVolume;
    }

    // ȿ���� ���� ����
    public void SetEffectVolume(float _volume)
    {
        effectVolume = _volume;
        EffectSourceController.SetVoulme(effectVolume);
        for (int i = 0; i < effectSourceControllers.Count; i++)
        {
            effectSourceControllers[i].SetVoulme(effectVolume);
        }
    }

    // ȿ���� ����
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

    // ȿ���� ���� 2
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

    // ȿ���� ����
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

    // ������� ���� 
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

    // ������� FadeIn ����
    public void FadeInBGM(AudioClip_BGM _bgm, float _fadeTime, Action _callback = null)
    {
        Managers.Routine.StartCoroutine(FadeInBGMRoutine(_bgm, _fadeTime, _callback));
    }

    // ������� FadeIn ��ƾ
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

    // ������� FadeOut ����
    public void FadeOutBGM(float _fadeTime, Action _callback = null)
    {
        Managers.Routine.StartCoroutine(FadeOutBGMRoutine(_fadeTime, _callback));
    }

    // ������� FadeOut ��ƾ
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

    // ������� FadeIn,FadeOut���� ����
    public void FadeChangeBGM(AudioClip_BGM _bgm, float _fadeTotalTime, Action _callback = null)
    {
        Managers.Routine.StartCoroutine(FadeChangeBGMRoutine(_bgm, _fadeTotalTime, _callback));
    }

    // ������� FadeIn, FadeOut ��ƾ
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

    // ������� FadeIn, FadeOut ��ƾ
    private IEnumerator FadeChangeBGMRoutine(SoundProfile_BGM _bgm, float _fadeTotalTime, int _index = 0, Action _callback = null)
    {
        yield return Managers.Routine.StartCoroutine(FadeOutBGMRoutine(_fadeTotalTime * 0.5f)) ;
        yield return Managers.Routine.StartCoroutine(FadeInBGMRoutine(_bgm, _fadeTotalTime * 0.5f, _index));
        _callback?.Invoke();
    }
}
