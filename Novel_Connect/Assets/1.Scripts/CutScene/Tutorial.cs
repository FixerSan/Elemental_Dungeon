using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : CutScene
{
    private CameraScript m_Camera;
    private PlayerController player;
    private DialogSystem dialogSystem;
    private Princess princess;

    public override void Play()
    {
        StartCoroutine(Tutorial_1());
    }

    private void Start()
    {
        Play();
    }

    public IEnumerator Tutorial_1()
    {
        Setup();

        player.transform.position = new Vector3(36.5f, -1f);
        StartCoroutine(ScreenEffect.instance.FadeOut());
        player.ChangeState(PlayerState.Idle);
        player.ChangeDirection(Direction.Left);
        yield return new WaitForSeconds(1f);

        player.ChangeState(PlayerState.Walk);
        //여기까지는 되는데 이 다음 언틸이 체크가 안 되는 건지 튜토리얼 2가 안 돼요...
        yield return new WaitUntil(() => player.transform.localPosition.x <= 17);
        StartCoroutine(Tutorial_2());   
    }

    public IEnumerator Tutorial_2()
    {
        player.ChangeState(PlayerState.Idle);
        player.Stop();
        yield return new WaitForSeconds(1f);
        dialogSystem.UpdateDialog(1001);
    }
    
    public IEnumerator Tutorial_3()
    {
        SpeechBubbleSystem.instance.SetSpeechBuble(101, princess.transform, new Vector3(2.67f, 2.09f, 0));
        princess.direction = Direction.Right;
        princess.isWalking = true;
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Tutorial_4()
    {
        ScreenEffect.instance.Shake(0.5f);
        yield return new WaitForSeconds(2f);
        SpeechBubbleSystem.instance.SetSpeechBuble(102, player.transform, new Vector3(-1.5f, 3.5f, 0));

        CameraScript.instance.ChangeState(CameraState.cutscene);
        CameraScript.instance.StartCoroutine(CameraScript.instance.MoveToPos(new Vector3(-25, 3.916215f, -10f),Vector2.left));
    }

    public override void Setup()
    {
        m_Camera = CameraScript.instance;
        player = PlayerController.instance;
        dialogSystem = DialogSystem.instance;
        princess = FindObjectOfType<Princess>();
        index = 0;
    }
}
