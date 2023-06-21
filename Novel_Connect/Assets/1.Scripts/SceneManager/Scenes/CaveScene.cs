using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveScene : BaseScene
{
    Centipede centipede => FindObjectOfType<Centipede>(true);
    BaseBoss iceBoss => FindObjectOfType<BaseBoss>(true);
    GameObject brokeTrigger_1 = null;
    Dictionary<string, Transform> cameraPoses = new Dictionary<string, Transform>();
    protected override void Setup()
    {
        CameraScript.instance.max = new Vector2(10000, 10000);
        CameraScript.instance.min = new Vector2(-10000, -10000);
        CameraScript.instance.ChangeSize(9);
        CameraScript.instance.playerPlusY = 5;

        GameManager.instance.player.transform.position = new Vector3(11.7f,36,0);
        centipede.gameObject.SetActive(false);
        ObjectPool.instance.InitHpBar(5);

        checkAudioDuration = audioDuration;
        brokeTrigger_1 = GameObject.Find("BrokeTrigger_1");
        brokeTrigger_1.SetActive(false);

        Transform[] c_Poses = GameObject.Find("CameraPoses").GetComponentsInChildren<Transform>();

        foreach (var c_Pos in c_Poses)
        {
            cameraPoses.Add(c_Pos.name, c_Pos);
        }
        MonsterSystem.instance.OnDeadBoss += SceneEvent;
    }

    protected override void Clear()
    {
        MonsterSystem.instance.OnDeadBoss -= SceneEvent;
    }

    public override void SceneEvent(int i)
    {
        switch (i)
        {
            case 0:

                break;
        }
    }

    public override void TriggerEffect(int index)
    {
        switch (index)
        {
            case 0:
                StartCoroutine(Trigger_0());
                break;

            case 1:
                StartCoroutine(Trigger_1());
                break;

            case 2:
                StartCoroutine(Trigger_2());
                break;

            case 3:
                StartCoroutine(Trigger_3());
                break;

            case 4:
                StartCoroutine(Trigger_4());
                break;
            case 5:
                StartCoroutine(Trigger_5());
                break;
            case 6:
                StartCoroutine(Trigger_6());
                break;
            case 7:
                StartCoroutine(Trigger_7());
                break;
            case 8:
                StartCoroutine(Trigger_8());
                break;
            case 9:
                StartCoroutine(Trigger_9());
                break;
            case 10:
                StartCoroutine(Trigger_10());
                break;
            case 11:
                StartCoroutine(Trigger_11());
                break;
            case 12:
                StartCoroutine(Trigger_12());
                break;
            case 13:
                StartCoroutine(Trigger_13());
                break;
        }
    }

    public IEnumerator Trigger_0()
    {
        CameraScript.instance.max = new Vector2(174.84f, 10000);
        ScreenEffect.instance.ShakeHorizontal(0.5f, 0.3f);
        yield return new WaitForSeconds(1f);
        DialogSystem.Instance.UpdateDialog(2000);
    }

    public IEnumerator Trigger_1()
    {
        GameManager.instance.player.playerInput.isCanControl = false;
        GameManager.instance.player.ChangeState(PlayerState.Idle);
        GameManager.instance.player.playerMovement.Stop();
        CameraScript.instance.max = new Vector2(321, CameraScript.instance.max.y);
        yield return new WaitForSeconds(1f);
        CameraScript.instance.ChangeTarget(cameraPoses["CameraPos_0"].gameObject);
        yield return new WaitForSeconds(1f);
        while (Vector2.Distance(cameraPoses["CameraPos_0"].position, cameraPoses["CameraPos_1"].position) > 0.1f)
        {
            yield return null;
            cameraPoses["CameraPos_0"].position = Vector2.MoveTowards(cameraPoses["CameraPos_0"].position, cameraPoses["CameraPos_1"].position, Time.deltaTime * 10);
        }
        yield return new WaitForSeconds(1f);

        CameraScript.instance.ChangeTarget(GameManager.instance.player.gameObject);
        GameManager.instance.player.playerInput.isCanControl = true;

    }

    public IEnumerator Trigger_2()
    {
        yield return null;
        CameraScript.instance.min = new Vector2(160, CameraScript.instance.min.y);
        CameraScript.instance.max = new Vector2(CameraScript.instance.max.x, -40f);
        ScreenEffect.instance.ShakeHorizontal(0.5f, 0.3f);
    }

    public IEnumerator Trigger_3()
    {
        CameraScript.instance.max = new Vector2(349f, 10000);
        ScreenEffect.instance.ShakeHorizontal(0.5f, 0.3f);
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Trigger_4()
    {
        yield return null;
        CameraScript.instance.max = new Vector2(10000f, 10000f);
        CameraScript.instance.min = new Vector2(-10000f, -10000f);
    }

    public IEnumerator Trigger_5()
    {
        yield return null;
        CameraScript.instance.max = new Vector2(349f, 10000);
    }

    public IEnumerator Trigger_6()
    {
        yield return null;
        CameraScript.instance.max = new Vector2(381f, 10000);
    }

    public IEnumerator Trigger_7()
    {
        yield return null;
        CameraScript.instance.min = new Vector2(223, CameraScript.instance.min.y);
        CameraScript.instance.max = new Vector2(345f, 10000);
        ScreenEffect.instance.Shake(0.5f, 2);
        yield return new WaitForSeconds(2f);
        centipede.gameObject.SetActive(true);
        //마법진 생기고 지네 소환
    }

    public IEnumerator Trigger_8()
    {
        yield return null;
        CameraScript.instance.max = new Vector2(CameraScript.instance.max.x, -95f);
        CameraScript.instance.min = new Vector2(236f, CameraScript.instance.min.y);
    }
    public IEnumerator Trigger_9()
    {
        yield return null;
        CameraScript.instance.min = new Vector2(223, CameraScript.instance.min.y);
    }
    public IEnumerator Trigger_10()
    {
        yield return null;

    }

    public IEnumerator Trigger_11()
    {
        GameManager.instance.player.playerInput.isCanControl = false;
        GameManager.instance.player.ChangeState(PlayerState.Idle);
        yield return StartCoroutine(ScreenEffect.instance.FadeIn(1));
        CameraScript.instance.ChangeTarget(cameraPoses["CameraPos_2"].gameObject);
        CameraScript.instance.delayTime = 1f;
        CameraScript.instance.playerPlusY = 0;
        CameraScript.instance.ChangeSize(18.5f);
        yield return StartCoroutine(ScreenEffect.instance.FadeOut(1));
        ScreenEffect.instance.ShakeHorizontal(0.3f, 1f);
        yield return new WaitForSeconds(1);
        brokeTrigger_1.SetActive(true);

        yield return new WaitForSeconds(3);
        CameraScript.instance.ChangeTarget(GameManager.instance.player.gameObject);
        CameraScript.instance.ChangeSize(9);
        CameraScript.instance.delayTime = 0.2f;
        CameraScript.instance.playerPlusY = 5;
        GameManager.instance.player.playerInput.isCanControl = true;

    }

    public IEnumerator Trigger_12()
    {
        CameraScript.instance.min = new Vector2(340, -200);
        CameraScript.instance.max = new Vector2(384, CameraScript.instance.max.y);
        yield return new WaitForSeconds(1);
        iceBoss.gameObject.SetActive(true);
        CameraScript.instance.ChangeTarget(iceBoss.gameObject);
        yield return new WaitForSeconds(3f);
        CameraScript.instance.ChangeTarget(GameManager.instance.player.gameObject);
    }
    public IEnumerator Trigger_13()
    {
        yield return null;
        CameraScript.instance.min = new Vector2(255.9f, -145.9506f);
        CameraScript.instance.max = new Vector2(302.7f, CameraScript.instance.max.y);
    }

    //public IEnumerator SceneEvent_0()
    //{
        
    //}


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
