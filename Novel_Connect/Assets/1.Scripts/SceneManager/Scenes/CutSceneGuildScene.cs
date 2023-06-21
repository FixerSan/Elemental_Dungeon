using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneGuildScene : BaseScene
{
    public GameObject npcs;
    public GameObject etcObject;
    protected override void Setup()
    {
        CameraScript.instance.GetComponent<Camera>().orthographicSize = 5;
        CameraScript.instance.min = new Vector2(-10.41f, 0f);
        CameraScript.instance.max = new Vector2(15.96f, 0f);
        CameraScript.instance.playerPlusY = 0f;
        npcs = GameObject.Find("NPCs");
        npcs.transform.GetChild(2).gameObject.SetActive(false);
        etcObject = GameObject.Find("ETCObject");
        QuestSystem.instance.AddQuest();
    }

    public override void SceneEvent(int index)
    {
        switch(index)
        {
            case 0:
                StartCoroutine(Event_1());
                break;

            case 1:
                StartCoroutine(Event_2());
                break;

            case 2:
                StartCoroutine(Event_3());
                break;

            case 3:
                StartCoroutine(Event_4());
                break;
        }
    }

    public IEnumerator Event_1()
    {
        CameraScript.instance.ChangeTarget(npcs.transform.GetChild(2).gameObject);
        yield return new WaitForSeconds(2);
        DialogSystem.Instance.UpdateDialog(1046);
    }

    public IEnumerator Event_2()
    {
        CameraScript.instance.ChangeTarget(etcObject.transform.GetChild(0).gameObject);
        CameraScript.instance.ChangeSize(3.5f);
        CameraScript.instance.max = new Vector2(CameraScript.instance.max.x,1.4f);
        yield return new WaitForSeconds(2);
        DialogSystem.Instance.UpdateDialog(1047);
        yield return new WaitForSeconds(2);
        CameraScript.instance.ChangeTarget(GameManager.instance.player.gameObject);
        CameraScript.instance.max = new Vector2(15.96f, 0f);
        CameraScript.instance.ChangeSize(5f);
        npcs.transform.GetChild(2).gameObject.SetActive(true);
    }

    IEnumerator Event_3()
    {
        yield return new WaitUntil(() => !CanvasScript.instance.transform.GetChild(5).gameObject.activeSelf);
        yield return new WaitForSeconds(1);
        DialogSystem.Instance.UpdateDialog(1054);
    }

    IEnumerator Event_4()
    {
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(ScreenEffect.instance.FadeIn(1));
        SceneManager.instance.LoadScene("Cave");
        yield return new WaitForSeconds(1); 
        yield return StartCoroutine(ScreenEffect.instance.FadeOut(1));
    }
}
