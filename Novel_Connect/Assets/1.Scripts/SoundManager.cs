using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] bgm;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += ChangeBGM;
    }
    
    void ChangeBGM(Scene scene , LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            audioSource.clip = bgm[0];
            audioSource.Play();
            audioSource.loop = true;

        }
    }


}
