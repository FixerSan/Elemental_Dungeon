using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveScene : BaseScene
{
    Centipede centipede => FindObjectOfType<Centipede>();

    protected override void Setup()
    {
        //centipede.gameObject.SetActive(false);
        ObjectPool.instance.InitHpBar(5);

        checkAudioDuration = audioDuration;
    }

    public override void TriggerEffect(int index)
    {
        switch (index)
        {
            case 0:
                ScreenEffect.instance.ShakeHorizontal(0.3f, 0.2f);
                
                break;
        }
    }


    float audioDuration = 5;
    float checkAudioDuration;
    float checkTime;
    private void FixedUpdate()
    {
        CheckEffectSound();
    }

    void CheckEffectSound()
    {
        checkTime += Time.deltaTime;
        if (checkTime > checkAudioDuration)
        {
            checkTime = 0;
            checkAudioDuration = Random.Range(audioDuration + 1, audioDuration + 1);
            AudioSystem.Instance.PlayOneShotSoundProfile("Water_CaveDrip");
        }
    }


}
