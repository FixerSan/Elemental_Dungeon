using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveScene : BaseScene
{
    Centipede centipede => FindObjectOfType<Centipede>();
    GameObject brokeTrigger_1 = null;
    Dictionary<string, Transform> cameraPoses = new Dictionary<string, Transform>();
    protected override void Setup()
    {
        //centipede.gameObject.SetActive(false);
        ObjectPool.instance.InitHpBar(5);

        checkAudioDuration = audioDuration;
        brokeTrigger_1 = GameObject.Find("BrokeTrigger_1");
        brokeTrigger_1.SetActive(false);

        Transform[] c_Poses = GameObject.Find("CameraPoses").GetComponentsInChildren<Transform>();

        foreach (var c_Pos in c_Poses)
        {
            cameraPoses.Add(c_Pos.name, c_Pos);
        }
    }

    public override void TriggerEffect(int index)
    {
        switch (index)
        {
            case 0:
                ScreenEffect.instance.ShakeHorizontal(0.3f, 0.2f);
                break;

            case 1:

                break;

            case 2:
                StartCoroutine(Trigger2Coroutine());
                break;
        }
    }

    public IEnumerator Trigger2Coroutine()
    {
        GameManager.instance.player.playerInput.isCanControl = false;
        GameManager.instance.player.ChangeState(PlayerState.Idle);
        yield return StartCoroutine(ScreenEffect.instance.FadeIn(1));
        CameraScript.instance.ChangeTarget(cameraPoses["Trigger_2"].gameObject);
        CameraScript.instance.delayTime = 1f;
        CameraScript.instance.playerPlusY = 0;
        CameraScript.instance.ChangeSize(15);
        yield return StartCoroutine(ScreenEffect.instance.FadeOut(1));
        //while (CameraScript.instance.GetSize() < 12f)
        //{
        //    yield return null;
        //    CameraScript.instance.ChangeSize(CameraScript.instance.GetSize() + Time.deltaTime* 2);
        //}
        ScreenEffect.instance.ShakeHorizontal(0.3f, 1f);
        yield return new WaitForSeconds(1);
        brokeTrigger_1.SetActive(true);

        yield return new WaitForSeconds(3);
        CameraScript.instance.ChangeTarget(GameManager.instance.player.gameObject);
        CameraScript.instance.ChangeSize(9);
        CameraScript.instance.delayTime = 0.2f;
        CameraScript.instance.playerPlusY = 5;
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
            //checkAudioDuration = Random.Range(audioDuration + 1, audioDuration + 1);
            //AudioSystem.Instance.PlayOneShotSoundProfile("Water_CaveDrip");
        }
    }


}
