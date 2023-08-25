using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class SoundManager 
{
    private Transform sourceTransform;
    public Transform SourceTransform
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

    private AudioSource bgmSource;
    public AudioSource BgmSource 
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

    private AudioSourceController effectSourceController;
    public AudioSourceController EffectSourceController
    {
        get
        {
            if (effectSourceController == null)
                effectSourceController = new AudioSourceController();
            return effectSourceController;
        }
    }

    public List<AudioSourceController> effectSourceControllers = new List<AudioSourceController>();
    public float bgmVolume = 1;
    public float effectVolume = 1;
    private bool isFading;

    public void SetBGMVolume(float _volume)
    {
        bgmVolume = _volume;
        BgmSource.volume = bgmVolume;
    }

    public void SetEffectVolume(float _volume)
    {
        effectVolume = _volume;
        effectSourceController.SetVoulme(effectVolume);
        for (int i = 0; i < effectSourceControllers.Count; i++)
        {
            effectSourceControllers[i].SetVoulme(effectVolume);
        }
    }

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

    public void FadeInBGM(AudioClip_BGM _bgm, float _fadeTime)
    {
        Managers.Routine.StartCoroutine(FadeInBGMRoutine(_bgm, _fadeTime));
    }

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

    public void FadeOutBGM(float _fadeTime)
    {
        Managers.Routine.StartCoroutine(FadeOutBGMRoutine(_fadeTime));
    }

    private IEnumerator FadeOutBGMRoutine(float fadeTime)
    {
        while (BgmSource.volume > 0.0f)
        {
            BgmSource.volume -= bgmVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        BgmSource.Stop();
    }

    public void FadeChangeBGM(AudioClip_BGM _bgm, float _fadeTime)
    {
        Managers.Routine.StartCoroutine(FadeChangeBGMRoutine(_bgm,_fadeTime));
    }

    private IEnumerator FadeChangeBGMRoutine(AudioClip_BGM _bgm, float _fadeTime)
    {
        yield return Managers.Routine.StartCoroutine(FadeOutBGMRoutine(_fadeTime));
        yield return Managers.Routine.StartCoroutine(FadeInBGMRoutine(_bgm, _fadeTime));
    }
}
