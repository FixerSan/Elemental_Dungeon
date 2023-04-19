using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBuble : MonoBehaviour
{
    public SpeechBubbleData data;

    private void OnDisable()
    {

    }

    public IEnumerator Disable()
    {
        yield return new WaitForSeconds(data.duration);
        SpeechBubbleObjectPool.instance.ReturnSpeechBuble(this.gameObject);
    }
}
