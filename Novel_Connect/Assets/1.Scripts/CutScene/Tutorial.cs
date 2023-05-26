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
    public GameObject[] monsters = new GameObject[3];

    private List<GameObject> tutorialContentSprites = new List<GameObject>();
    public GameObject boss;
    public Knight knight;

    public GameObject arrow;

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

        GameObject tutorialContentHeader = GameObject.Find("TutorialContents");
        for (int i = 0; i < tutorialContentHeader.transform.childCount; i++)
        {
            tutorialContentSprites.Add(tutorialContentHeader.transform.GetChild(i).gameObject);
        }


        mapText = CameraScript.instance.transform.GetChild(0).GetComponent<SpriteRenderer>();
        fieldItem = GameObject.Find("FieldItem");
        fieldItem.SetActive(false);

        portal = GameObject.Find("Portal");
        portal.SetActive(false);

        boss = GameObject.Find("Boss");
        boss.SetActive(false);

        knight = FindObjectOfType<Knight>();
        DontDestroyOnLoad(knight.gameObject);
        knight.gameObject.SetActive(false);
        DontDestroyOnLoad(boss);
        DontDestroyOnLoad(tutorialContentHeader);


        arrow = GameObject.Find("ArrowAnimation");
        arrow.SetActive(false);
    }


    public void Setup_2()
    {
        player = PlayerController.instance;
        dialogSystem = DialogSystem.instance;
        m_Camera = CameraScript.instance;
        princess = FindObjectOfType<Princess>();
        princess.GetComponent<BoxCollider2D>().enabled = false;
        princess.GetComponent<Rigidbody2D>().gravityScale = 0;
        princess.Tied();

        monsters[0] = GameObject.Find("TutorialMonster");
        monsters[1] = GameObject.Find("TutorialMonster (1)");
        monsters[2] = GameObject.Find("TutorialMonster (2)");
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

        yield return new WaitForSeconds(2f); 

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.01f);
            mapText.color = new Color(255, 255, 255, mapText.color.a - 0.01f);
        }

        StartCoroutine(ScreenEffect.instance.FadeOut());

        player.ChangeState(PlayerState.Idle);
        player.ChangeDirection(Direction.Left);
        player.ChangeState(PlayerState.Walk);



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
        player.ChangeState(PlayerState.Sit);
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
        player.ChangeState(PlayerState.Idle);
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
        tutorialContentSprites[4].transform.position = new Vector3(12.5f, 2, 0);
        tutorialContentSprites[4].SetActive(true);
        arrow.transform.position = new Vector3(11.6999998f, 0.439999998f, 0);
        arrow.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftArrow));
        tutorialContentSprites[4].SetActive(false);
    }

    public IEnumerator Tutorial_9()
    {
        arrow.SetActive(false);
        player.canControl = false;
        player.ChangeState(PlayerState.Idle);
        player.Stop();

        tutorialContentSprites[1].SetActive(true);
        tutorialContentSprites[1].transform.position = new Vector3(7.3499999f, 2, 0);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.I));
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.I));
        tutorialContentSprites[1].SetActive(false);
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
        player.Stop();
        m_Camera.ChangeState(CameraState.cutscene);
        m_Camera.transform.position = new Vector3(-0.28f, 0, -10);
        m_Camera.GetComponent<Camera>().orthographicSize = 5.7f;
        player.canControl = false;
        player.transform.position = new Vector3(8.27000046f, -4.94754791f, 0);
        player.ChangeState(PlayerState.Sit);
        yield return new WaitForSeconds(1f);
        dialogSystem.UpdateDialog(1005);
    }

    public IEnumerator Tutorial_12()
    {
        //Time.timeScale = f;
        yield return new WaitForSeconds(1f);
        SpeechBubbleSystem.instance.SetSpeechBuble(104, princess.transform, new Vector3(0.105999999f, 0.63f, 0), 0.7f);
        yield return new WaitForSeconds(0.2f);
        SpeechBubbleSystem.instance.SetSpeechBuble(105, monsters[0].transform, new Vector3(0.254999995f, 0.893999994f, 0), 0.7f);
        yield return new WaitForSeconds(0.2f);
        SpeechBubbleSystem.instance.SetSpeechBuble(105, monsters[1].transform, new Vector3(0.254999995f, 0.893999994f, 0), 0.7f);
        yield return new WaitForSeconds(0.2f);
        SpeechBubbleSystem.instance.SetSpeechBuble(105, monsters[2].transform, new Vector3(-0.223000005f, 0.893999994f, 0), 0.7f);
        yield return new WaitForSeconds(0.2f);
        SpeechBubbleSystem.instance.SetSpeechBuble(108, player.transform, new Vector3(-0.610000014f, 2.16000009f, 0), 0.7f);

        yield return new WaitForSeconds(3f);
        dialogSystem.UpdateDialog(997);
    }

    public IEnumerator Tutorial_13()
    {
        yield return new WaitForSeconds(1f);
        SpeechBubbleSystem.instance.SetSpeechBuble(111, monsters[2].transform, new Vector3(-0.428142935f, 0.88257122f, 0), 0.7f);
        yield return new WaitForSeconds(1f);
        SpeechBubbleSystem.instance.SetSpeechBuble(110, monsters[0].transform, new Vector3(0.270285577f, 0.88257128f, 0), 0.7f);
        yield return new WaitForSeconds(3f);
        dialogSystem.UpdateDialog(996);
    }

    public IEnumerator Tutorial_14()
    {
        yield return new WaitForSeconds(1f);
        monsters[1].GetComponent<Animator>().SetTrigger("FakeAttack");
        yield return new WaitForSeconds(1f);
        player.transform.position = new Vector3(-5.03000021f, -4.94754791f, 0);
        player.ChangeState(PlayerState.Attack);
        yield return new WaitForSeconds(1f);
        SpeechBubbleSystem.instance.SetSpeechBuble(112, monsters[1].transform, new Vector3(0.425000012f, 0.879999995f, 0), 0.7f);

        yield return new WaitForSeconds(3f);
        dialogSystem.UpdateDialog(1011);
    }

    public IEnumerator Tutorial_15()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        tutorialContentSprites[0].SetActive(true);
        CameraScript.instance.ChangeState(CameraState.idle);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.A));
        player.canControl = true;
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].GetComponent<MonsterV2>().isCutScene = false;
            monsters[i].GetComponent<AttackMonster>().stateMachine.ChangeState(monsters[i].GetComponent<AttackMonster>().states[(int)MonsterState.Follow]);
        }
        Time.timeScale = 1f;
        tutorialContentSprites[0].SetActive(false);

        yield return new WaitUntil(() => !monsters[0].activeSelf);
        yield return new WaitUntil(() => !monsters[1].activeSelf);
        yield return new WaitUntil(() => !monsters[2].activeSelf);

        yield return new WaitForSeconds(1f);

        dialogSystem.UpdateDialog(994);
    }

    public IEnumerator Tutorial_16()
    {
        yield return new WaitForSeconds(1f);
        boss.transform.position = new Vector3(-9.85999966f, -4.97399902f, 0);
        boss.SetActive(true);

        yield return new WaitForSeconds(1f);
        dialogSystem.UpdateDialog(993);


    }

    public IEnumerator Tutorial_17()
    {
        yield return new WaitForSeconds(1f);
        dialogSystem.UpdateDialog(1013);
    }

    public IEnumerator Tutorial_18()
    {
        yield return new WaitForSeconds(1f);

        tutorialContentSprites[5].SetActive(true);

        boss.GetComponent<AttackMonster>().isCutScene = false;
        boss.GetComponent<AttackMonster>().stateMachine.ChangeState(boss.GetComponent<AttackMonster>().states[(int)MonsterState.Follow]);
        Time.timeScale = 0f;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F1));
        tutorialContentSprites[5].SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        tutorialContentSprites[2].SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Q));
        tutorialContentSprites[2].SetActive(false);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        tutorialContentSprites[3].SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.W));
        Time.timeScale = 1f;
        tutorialContentSprites[3].SetActive(false);

        yield return new WaitUntil(() => !boss.activeSelf);

        player.canControl = false;
        player.transform.position = new Vector3(-3.79999995f, -4.94754791f, 0);

        yield return new WaitForSeconds(1f);
        dialogSystem.UpdateDialog(1014);
    }

    public IEnumerator Tutorial_19()
    {
        knight.gameObject.SetActive(true);
        knight.transform.position = new Vector3(6.44f, -4.94754791f, 0);
        m_Camera.ChangeTarget(knight.gameObject);
        yield return new WaitForSeconds(1f);
        SpeechBubbleSystem.instance.SetSpeechBuble(113, knight.transform, new Vector3(0.73f, 0.97f, 0), 0.7f);
        yield return new WaitForSeconds(1f);
        knight.isWalking = true;
        yield return new WaitUntil(() => Vector2.Distance(knight.transform.position , new Vector3(-1.23000002f, -4.94754791f, 0)) <= 0.1f);
        knight.isWalking = false;
        yield return new WaitForSeconds(1f);


        dialogSystem.UpdateDialog(1017);
    }

    public IEnumerator Tutorial_20()
    {
        yield return new WaitForSeconds(1f);
        princess.gameObject.SetActive(false);
        knight.GetComponent<Animator>().SetBool("isTaken", true);
        SpeechBubbleSystem.instance.SetSpeechBuble(114, knight.transform, new Vector3(0.73f, 0.97f, 0), 0.7f);
        knight.direction = Direction.Right;
        knight.GetComponent<SpriteRenderer>().flipX = true;
        knight.isWalking = true;
        yield return new WaitUntil(() => Vector2.Distance(knight.transform.position, new Vector3(10.3400002f, -4.94670725f, 0)) <= 0.1f);
        knight.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        m_Camera.ChangeTarget(player.gameObject);
        yield return new WaitForSeconds(1f);

        dialogSystem.UpdateDialog(990);
    }

    public IEnumerator End()
    {
        ScreenEffect.instance.FadeIn();
        yield return new WaitForSeconds(1f);
        Destroy(princess.gameObject);
        Destroy(knight.gameObject);
        Destroy(monsters[0].gameObject);
        Destroy(monsters[1].gameObject);
        Destroy(monsters[2].gameObject);
        Destroy(boss.gameObject);
        foreach (var item in townContentSprites)
        {
            Destroy(item);
        }
        foreach (var item in tutorialContentSprites)
        {
            Destroy(item);
        }
        player.canControl = true;
        Inventory.instance.items = new List<ItemData>();
        MonsterObjectPool.instance.monsterQueues = new Dictionary<int, Queue<GameObject>>();
        StageSystem.instance.ChangeScene("Guild");
    }
}
