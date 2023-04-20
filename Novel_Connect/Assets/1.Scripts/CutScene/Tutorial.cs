using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : CutScene
{
    private CameraScript m_Camera;
    private PlayerController player;
    private DialogSystem dialogSystem;
    private Princess princess;

    private List<GameObject> townContentSprites = new List<GameObject>();
    private SpriteRenderer mapText;

    public GameObject fieldItem;
    public GameObject portal;

    public override void Setup()
    {
        m_Camera = CameraScript.instance;
        player = PlayerController.instance;
        dialogSystem = DialogSystem.instance;
        princess = FindObjectOfType<Princess>();
        princess.gameObject.SetActive(false);
        index = 0;


        GameObject townContentHeader = GameObject.Find("TownContentSprite");

        for (int i = 0; i < townContentHeader.transform.childCount; i++)
        {
            townContentSprites.Add(townContentHeader.transform.GetChild(i).gameObject);
        }


        mapText = CameraScript.instance.transform.GetChild(0).GetComponent<SpriteRenderer>();
        fieldItem = GameObject.Find("FieldItem");
        fieldItem.SetActive(false);

        portal = GameObject.Find("Portal");
        portal.SetActive(false);
    }


    public void Setup_2()
    {
        player = PlayerController.instance;
        dialogSystem = DialogSystem.instance;
        princess = FindObjectOfType<Princess>();
    }

    public override void Play()
    {
        if(StageSystem.instance.currentStage == "Tutorial")
            StartCoroutine(Tutorial_1());
        if (StageSystem.instance.currentStage == "Tutorial_2")
            StartCoroutine(Tutorial_11());
    }

    private void Start()
    {
        Play();
    }

    public IEnumerator Tutorial_1()         //바깥부터 걸어오는 것
    {
        Setup();
        player.canControl = false;
        player.transform.position = new Vector3(36.5f, -1f);
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.01f);
            mapText.color = new Color(255,255,255,mapText.color.a+0.01f);
        }
        StartCoroutine(ScreenEffect.instance.FadeOut());

        player.ChangeState(PlayerState.Idle);
        player.ChangeDirection(Direction.Left);
        yield return new WaitForSeconds(3f);
        player.ChangeState(PlayerState.Walk);

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.01f);
            mapText.color = new Color(255, 255, 255, mapText.color.a - 0.01f);
        }

        //여기까지는 되는데 이 다음 언틸이 체크가 안 되는 건지 튜토리얼 2가 안 돼요...
        yield return new WaitUntil(() => player.transform.localPosition.x <= 17);
        StartCoroutine(Tutorial_2());   
    }

    public IEnumerator Tutorial_2()     //재자리 멈추고 다이얼로그
    {
        player.ChangeState(PlayerState.Idle);
        player.Stop();
        yield return new WaitForSeconds(1f);
        dialogSystem.UpdateDialog(999);
    }
    
    public IEnumerator Tutorial_3()     //왕녀랑 부딪치기
    {
        yield return new WaitForSeconds(1f);
        princess.gameObject.SetActive(true);
        SpeechBubbleSystem.instance.SetSpeechBuble(101, princess.transform, new Vector3(0.769999921f, 0.979999959f, 0));
        yield return new WaitForSeconds(0.25f);
        princess.direction = Direction.Right;
        princess.isWalking = true;
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Tutorial_4()     //부딪치고 카메라 쉐이크
    {
        ScreenEffect.instance.Shake(0.5f);
        fieldItem.SetActive(true);
        fieldItem.GetComponent<Rigidbody2D>().AddForce(new Vector3(-2, 2, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(2f);
        SpeechBubbleSystem.instance.SetSpeechBuble(102, player.transform, new Vector3(-1.5f, 3.5f, 0));
        yield return new WaitForSeconds(3f);
        dialogSystem.UpdateDialog(1002);
    }


    public IEnumerator Tutorial_5() //마을 설명
    {
        yield return new WaitForSeconds(1);
        m_Camera.ChangeState(CameraState.cutscene);
        m_Camera.StartCoroutine(m_Camera.MoveToPos(new Vector3(-25, 3.916215f, -10f), Vector2.left));

        yield return new WaitUntil(() => m_Camera.transform.position.x <= 2.5f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.25f);
        townContentSprites[0].SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        Time.timeScale = 1;
        townContentSprites[0].SetActive(false);

        yield return new WaitUntil(() => m_Camera.transform.position.x <= -24.9f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.25f);
        townContentSprites[1].SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        Time.timeScale = 1;
        townContentSprites[1].SetActive(false);

        m_Camera.StartCoroutine(m_Camera.MoveToPos(new Vector3(-24.8f, -8.3f, -10f), new Vector2(0.0097483f,-1)));
        yield return new WaitUntil(() => m_Camera.transform.position.y <= -8.25f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.25f);
        townContentSprites[2].SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        Time.timeScale = 1;
        townContentSprites[2].SetActive(false);

        m_Camera.StartCoroutine(m_Camera.MoveToPos(new Vector3(3, -8.3f, -10f), Vector2.right));

        yield return new WaitUntil(() => m_Camera.transform.position.x >= 2.9f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.25f);
        townContentSprites[3].SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        Time.timeScale = 1;
        townContentSprites[3].SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        m_Camera.ChangeState(CameraState.idle);

        StartCoroutine(Tutorial_3());
    }

    public IEnumerator Tutorial_6()     
    {
        yield return new WaitForSeconds(1f);
        princess.direction = Direction.Left;
        princess.isWalking = true;

        yield return new WaitUntil(() => princess.transform.position.x <= -2.582264f);
        princess.isWalking = false;

        dialogSystem.UpdateDialog(1003);
    }


    public IEnumerator Tutorial_7() //아이템 떨어진 거 발견하고 나서 왕녀로 전환
    {

        yield return new WaitForSeconds(1f);
        ScreenEffect.instance.StartCoroutine(ScreenEffect.instance.FadeIn());
        yield return new WaitForSeconds(1f);
        m_Camera.ChangeTarget(princess.gameObject);
        princess.transform.position = new Vector3(1, -1.07907414f, 0);
        player.transform.position = new Vector3(23.17f, -1.07608473f, 0);
        yield return new WaitForSeconds(1f);
        ScreenEffect.instance.StartCoroutine(ScreenEffect.instance.FadeOut());
        princess.isWalking = true;
        StartCoroutine(Tutorial_10());
    }

    public IEnumerator Tutorial_8() //캐릭터 움직임 풀림
    {
        yield return new WaitForSeconds(1f);
        //화살표 소환 
        player.canControl = true;
    }

    public IEnumerator Tutorial_9()
    {
        player.canControl = false;
        player.ChangeState(PlayerState.Idle);
        player.Stop();
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.I));
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.I));
        yield return new WaitForSeconds(1f);
        dialogSystem.UpdateDialog(998);
    }

    public IEnumerator Tutorial_10()
    {
        yield return new WaitUntil(() => princess.transform.position.x <= -21f);
        princess.isWalking = false;
        princess.Stop();
        princess.gameObject.SetActive(false);
        player.transform.position = new Vector3(16.25f, -1.07608473f, 0);
        yield return new WaitForSeconds(1f);
        m_Camera.ChangeTarget(player.gameObject);
        yield return new WaitForSeconds(1.5f);
        SpeechBubbleSystem.instance.SetSpeechBuble(103,player.transform,new Vector3(-1.02999997f, 3.3599999f, 0));
        yield return new WaitForSeconds(1f);
        //화살표 방향
        portal.SetActive(true);
        player.canControl = true;
    }

    public IEnumerator Tutorial_11()
    {
        Setup_2();
        yield return new WaitForSeconds(1f);
        dialogSystem.UpdateDialog(1005);
    }






}
