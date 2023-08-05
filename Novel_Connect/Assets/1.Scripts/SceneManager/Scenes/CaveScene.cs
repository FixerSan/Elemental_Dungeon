using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveScene : BaseScene
{
    Centipede centipede => FindObjectOfType<Centipede>(true);
    BaseBoss iceBoss => FindObjectOfType<BaseBoss>(true);
    GameObject obstacle => FindObjectOfType<InteractableObstacle>().gameObject;
    GameObject brokeTrigger_1 = null;
    Dictionary<string, Transform> cameraPoses = new Dictionary<string, Transform>();

    PlayerControllerV3 player => GameManager.instance.player;

    float audioDuration = 5;
    float checkAudioDuration;
    float checkAudioTime;

    float cameraShakeDuration = 10;
    float checkCameraShakeTime;
    float checkCameraShakeDuration;

    bool isCanCameraShake = false;
    protected override void Setup()
    {
        CameraScript.instance.max = new Vector2(10000, 10000);
        CameraScript.instance.min = new Vector2(-10000, -10000);
        CameraScript.instance.ChangeSize(9);
        CameraScript.instance.playerPlusY = 5;
        CameraScript.instance.target = GameManager.instance.player.gameObject;
        GameManager.instance.player.rb.velocity = Vector2.zero;

        GameManager.instance.player.transform.position = new Vector3(12f,50,0);
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
        player.StopAndCantInput();
        StartCoroutine(SetupCoroutine());
    }

    IEnumerator SetupCoroutine()
    {
        yield return new WaitForSeconds(1);
        DialogSystem.Instance.UpdateDialog(2000);
    }

    private void FixedUpdate()
    {
        CheckEffectSound();
        CheckCameraShake();
    }

    void CheckEffectSound()
    {
        checkAudioTime += Time.deltaTime;
        if (checkAudioTime > checkAudioDuration)
        {
            checkAudioTime = 0;
            //checkAudioDuration = Random.Range(audioDuration + 1, audioDuration + 1);
            //AudioSystem.Instance.PlayOneShotSoundProfile("Water_CaveDrip");
        }
    }

    void CheckCameraShake()
    {
        if (!isCanCameraShake) return;
        checkCameraShakeTime += Time.deltaTime;
        if (checkCameraShakeTime > checkCameraShakeDuration)
        {
            checkCameraShakeTime = 0;
            checkCameraShakeDuration = Random.Range(cameraShakeDuration + 3f, cameraShakeDuration + 3f);
            ScreenEffect.instance.Shake(0.1f, 1);
        }
    }
    

    protected override void Clear()
    {
        player.ChangeElemental(Elemental.Default);
        player.GetComponent<InventoryV2>().ResetInven();
        MonsterSystem.instance.OnDeadBoss -= SceneEvent;
    }

    public override void SceneEvent(int i)
    {
        switch (i)
        {
            case 0:
                StartCoroutine(SceneEvent_0());
                break;
        }
    }

    public IEnumerator SceneEvent_0()
    {
        player.StopAndCantInput();
        yield return new WaitForSeconds(3);
        DialogSystem.Instance.UpdateDialog(2052);
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
            case 14:
                StartCoroutine(Trigger_14());
                break;
            case 15:
                StartCoroutine(Trigger_15());
                break;
            case 16:
                StartCoroutine(Trigger_16());
                break;
            case 17:
                StartCoroutine(Trigger_17());
                break;

            case 18:
                StartCoroutine(Trigger_18());
                break;
            case 19:
                StartCoroutine(Trigger_19());
                break;

            case 20:
                StartCoroutine(Trigger_20());
                break;

            case 21:
                StartCoroutine(Trigger_21());
                break;
            case 22:
                StartCoroutine(Trigger_22());
                break;
            case 23:
                StartCoroutine(Trigger_23());
                break;

            case 24:
                StartCoroutine(Trigger_24());
                break;

            case 25:
                StartCoroutine(Trigger_25());
                break;

            case 26:
                StartCoroutine(Trigger_26());
                break;
            
            case 27:
                StartCoroutine(Trigger_27());
                break;
            case 28:
                StartCoroutine(Trigger_28());
                break;

            case 29:
                StartCoroutine(Trigger_29());
                break;

        }
    }

    public IEnumerator Trigger_0()
    {
        CameraScript.instance.max = new Vector2(174.84f, 10000);
        CameraScript.instance.min = new Vector2(-1000000, -10000);
        ScreenEffect.instance.ShakeHorizontal(0.5f, 0.3f);
        yield return new WaitForSeconds(1f);
        DialogSystem.Instance.UpdateDialog(2001);
    }

    public IEnumerator Trigger_1()
    {
        yield return null;
        GameManager.instance.player.ChangeState(PlayerState.Idle);
        GameManager.instance.player.playerMovement.Stop();
        CameraScript.instance.max = new Vector2(321, CameraScript.instance.max.y);
    }

    public IEnumerator Trigger_2()
    {
        yield return null;
        CameraScript.instance.min = new Vector2(160, CameraScript.instance.min.y);
        CameraScript.instance.max = new Vector2(CameraScript.instance.max.x, -40f);
        ScreenEffect.instance.ShakeHorizontal(0.5f, 0.3f);
        player.StopAndCantInput();
        yield return new WaitForSeconds(1);
        DialogSystem.Instance.UpdateDialog(2065);
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
        DialogSystem.Instance.UpdateDialog(2032);
    }

    public IEnumerator Trigger_12()
    {
        CameraScript.instance.min = new Vector2(340, -200);
        CameraScript.instance.max = new Vector2(384, CameraScript.instance.max.y);
        GameManager.instance.player.playerInput.isCanControl = false;
        GameManager.instance.player.ChangeState(PlayerState.Idle);
        GameManager.instance.player.playerMovement.Stop();
        yield return new WaitForSeconds(1);
        iceBoss.gameObject.SetActive(true);
        CameraScript.instance.ChangeTarget(iceBoss.gameObject);
        yield return new WaitForSeconds(3f);
        CameraScript.instance.ChangeTarget(GameManager.instance.player.gameObject);
        yield return new WaitForSeconds(1);
        GameManager.instance.player.playerInput.isCanControl = true;
    }
    public IEnumerator Trigger_13()
    {
        yield return null;
        CameraScript.instance.min = new Vector2(255.9f, -145.9506f);
        CameraScript.instance.max = new Vector2(302.7f, CameraScript.instance.max.y);
    }
    public IEnumerator Trigger_14()
    {
        yield return new WaitForSeconds(2);
        player.ChangeDirection(Direction.Right);
        player.ChangeState(PlayerState.Walk);
        yield return new WaitForSeconds(1.5f);
        player.StopAndCantInput();
        yield return new WaitForSeconds(1f);
        DialogSystem.Instance.UpdateDialog(2003);
    }

    public IEnumerator Trigger_15()
    {
        player.StopAndCantInput();
        ScreenEffect.instance.Shake(0.1f, 1);
        
        yield return new WaitForSeconds(2);
        DialogSystem.Instance.UpdateDialog(2021);
    }

    public IEnumerator Trigger_16()
    {
        yield return new WaitForSeconds(1);
        player.playerInput.isCanControl = true;
    }

    public IEnumerator Trigger_17()
    {
        player.StopAndCantInput();
        yield return new WaitForSeconds(1);
        DialogSystem.Instance.UpdateDialog(2023);
    }

    public IEnumerator Trigger_18()
    {
        player.StopAndCantInput();
        yield return new WaitForSeconds(1);
        DialogSystem.Instance.UpdateDialog(2031);
    }

    public IEnumerator Trigger_19()
    {
        player.StopAndCantInput();
        yield return new WaitForSeconds(1);
        DialogSystem.Instance.UpdateDialog(2063);
    }

    public IEnumerator Trigger_20()
    {
        player.ChangeDirection(Direction.Left);
        player.ChangeState(PlayerState.Walk);
        yield return new WaitUntil(() => player.gameObject.transform.position.x < 169.5f);
        player.StopAndCantInput();
        yield return new WaitForSeconds(1);
        DialogSystem.Instance.UpdateDialog(2067);
    }

    public IEnumerator Trigger_21()
    {
        yield return new WaitForSeconds(1);
        player.StopAndCantInput();
        yield return new WaitForSeconds(1);
        DialogSystem.Instance.UpdateDialog(2068);
    }

    public IEnumerator Trigger_22()
    {
        player.StopAndCantInput();
        CameraScript.instance.max = new Vector2(10000f, 10000f);
        CameraScript.instance.min = new Vector2(-10000f, -10000f);

        yield return new WaitForSeconds(1);
        DialogSystem.Instance.UpdateDialog(2042);
    }

    public IEnumerator Trigger_23()
    {
        player.StopAndCantInput();
        yield return new WaitForSeconds(1);
        DialogSystem.Instance.UpdateDialog(2046);
    }

    public IEnumerator Trigger_24()
    {
        player.StopAndCantInput();
        yield return new WaitForSeconds(1);
        player.ChangeDirection(Direction.Left);
        player.ChangeState(PlayerState.Walk);
        yield return new WaitUntil(() => player.transform.position.x < 244.5f);
        player.StopAndCantInput();
        yield return new WaitForSeconds(1);
        DialogSystem.Instance.UpdateDialog(2070);
    }
    public IEnumerator Trigger_25()
    {
        UISystemManager.instance.ExitPopup("Cave_Obstacle_Interaction_Panel");
        yield return new WaitForSeconds(1);
        obstacle.SetActive(false);
        yield return new WaitForSeconds(1);
        DialogSystem.Instance.UpdateDialog(2047);
    }

    public IEnumerator Trigger_26()
    {
        yield return new WaitForSeconds(1);
        player.playerInput.isCanControl = true;
        obstacle.GetComponent<InteractableObject>().isRuning = true;
    }

    public IEnumerator Trigger_27()
    {
        yield return null;
        UISystemManager.instance.EnterPopup("Cave_Obstacle_Interaction_Panel");
        player.StopAndCantInput();
    }
    public IEnumerator Trigger_28()
    {
        yield return new WaitForSeconds(2);
        player.ChangeElemental(Elemental.Fire);

        yield return StartCoroutine(Trigger_16());
    }

    public IEnumerator Trigger_29()
    {
        SceneManager.instance.LoadScene("EndScene");
        yield return null;
        
    }
}
