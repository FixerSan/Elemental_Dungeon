using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GuildScene : BaseScene
{
    public List<Transform> cameraPoses;
    public override void Init()
    {
        base.Init();

        GameObject go = GameObject.Find("@CameraPoses");
        for (int i = 0; i < go.transform.childCount; i++)
        {
            cameraPoses.Add(go.transform.GetChild(i));
        }

        Managers.UI.ShowSceneUI<UIBaseScene>("UIScene_BaseScene");
        Managers.Object.Player.Init(1, Define.Elemental.Normal.ToString());
        Managers.Object.Player.SetPosition(Vector2.zero);
        Managers.Screen.SetCameraTarget(Managers.Object.Player.GetTrans());
        Managers.Screen.CameraController.Camera.orthographicSize = 1.2f;
    }

    public override void Clear()
    {

    }

    public override void SceneEvent(int _eventIndex)
    {
        switch(_eventIndex)
        {
            case 0:
                Managers.Input.isCanControl = false;
                Managers.Screen.SetCameraTarget(null);
                Managers.Screen.CameraController.LinearMoveCamera(cameraPoses[0].position, 2, () => 
                {
                    Managers.Routine.StartCoroutine(SceneEvent_0());
                });
                break;

            case 1:
                Managers.Routine.StartCoroutine(SceneEvent_1());
                break;

            case 2:
                Managers.Routine.StartCoroutine(SceneEvent_2());
                break;
        }
    }

    private IEnumerator SceneEvent_0()
    {
        yield return new WaitForSeconds(1);
        Managers.Dialog.Call(1003);
    }

    private IEnumerator SceneEvent_1()
    {
        yield return new WaitForSeconds(1);
        Managers.Dialog.Call(1009);
    }
    
    private IEnumerator SceneEvent_2()
    {
        yield return new WaitForSeconds(1);
        Managers.scene.LoadScene(Define.Scene.IceDungeon);
        Managers.Quest.AddQuest(Define.QuestType.KILL, 0);
        Managers.Quest.AddQuest(Define.QuestType.KILL, 2);
        Managers.Quest.AddQuest(Define.QuestType.KILL, 1);
    }

    public GuildScene()
    {
        cameraOffset = new Vector3(0,2,-10);
        cameraPoses = new List<Transform>();
    }
}
