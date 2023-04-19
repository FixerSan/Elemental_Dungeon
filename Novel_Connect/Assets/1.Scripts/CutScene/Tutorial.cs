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
        //��������� �Ǵµ� �� ���� ��ƿ�� üũ�� �� �Ǵ� ���� Ʃ�丮�� 2�� �� �ſ�...
        yield return new WaitUntil(() => player.transform.localPosition.x <= 18);
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
        yield return new WaitForSeconds(1f);
        princess.direction = Direction.Right;
        princess.isWalking = true;
        yield return new WaitForSeconds(1f);
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
