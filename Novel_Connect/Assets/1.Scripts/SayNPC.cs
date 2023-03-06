using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayNPC : MonoBehaviour
{
    public bool isCanSay = false;
    public float volume;

    private int duration;
    AudioSource audioSource;
    private bool isSaid = false;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isCanSay && !isSaid)
        {
            audioSource.Play();
            isSaid = true;
            player = collision.gameObject;
        }
    }

    private void Update()
    {
        if (!player)
            return;
        if (transform.position.x > player.transform.position.x)
            duration = 1;
        else
            duration = -1;
        audioSource.panStereo = Mathf.Abs(transform.position.x - player.transform.position.x) * duration / 12.5f;
        audioSource.volume = Mathf.Clamp((1 - Mathf.Abs(audioSource.panStereo)) * volume, 0.25f, 0.75f);
    }
}
