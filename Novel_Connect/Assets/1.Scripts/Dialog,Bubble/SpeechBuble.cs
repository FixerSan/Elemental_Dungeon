using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBuble : MonoBehaviour
{
    public SpeechBubbleData data;

    private void OnEnable()
    {
        StartCoroutine(Disable());
    }

    private void OnDisable()
    {
        SpeechBubbleObjectPool.instance.ReturnSpeechBuble(this.gameObject);
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(data.duration);
        gameObject.SetActive(false);
    }
}
