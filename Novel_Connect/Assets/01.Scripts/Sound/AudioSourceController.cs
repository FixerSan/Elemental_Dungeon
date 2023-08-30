using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController
{
    private AudioSource audioSource;
    public AudioSource AudioSource 
    {
        get
        {
            if (audioSource == null)
                Init();
            return audioSource;
        }
    }

    public void Init()
    {
        if (audioSource != null)
            return;
        GameObject go = Managers.Resource.Instantiate("AudioSource", _parent: Managers.Sound.SourceTransform, _pooling: true);
        audioSource = go.GetOrAddComponent<AudioSource>();
        Stop();
        SetVoulme(Managers.Sound.effectVolume);
    }

    public void Play(AudioClip _audioClip)
    {
        AudioSource.clip = _audioClip;
        Managers.Routine.StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        AudioSource.Play();
        float playtime = audioSource.clip.length;
        yield return new WaitForSeconds(playtime);
        audioSource.Stop();
        Managers.Sound.StopSoundEffect(this);
    }

    public void Stop()
    {
        audioSource.Stop();
        audioSource.playOnAwake = false;
        audioSource.clip = null;
        audioSource.loop = false;
    }

    public void RemoveAudioSource()
    {
        Stop();
        Managers.Resource.Destroy(audioSource.gameObject);
        audioSource = null;
    }

    public void SetVoulme(float _volume)
    {
        AudioSource.volume = _volume;
    }
}
