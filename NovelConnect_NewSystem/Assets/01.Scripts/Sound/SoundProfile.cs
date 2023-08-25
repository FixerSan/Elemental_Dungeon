using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Profile", menuName = "Scriptable Object/Sound Profile", order = 0)]
public class SoundProfile : ScriptableObject
{
    public List<AudioClip> audios;
    public AudioClip PlaySoundToRandom()
    {
        return audios.Random();
    }

    public AudioClip PlaySoundToIndex(int _index)
    {
        return audios.TryGetValue(_index);
    }
}
