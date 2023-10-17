using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using DG.Tweening;

public class IceDungeonScene : BaseScene
{
    public GameObject endTotem;
    private Transform[] cameraPoses;
    private BaseController boss;
    public override void Init(Action _callback)
    {
        cameraOffset = new Vector3(0, 2, -10);
        base.Init();
        Managers.UI.ShowSceneUI<UIBaseScene>("UIScene_BaseScene");
        Managers.Object.Player.SetPosition(new Vector3(-5, 1, 0));
        Managers.Screen.SetCameraTarget(Managers.Object.Player.trans);
        Managers.Screen.CameraController.Camera.orthographicSize = 3;
        Managers.Screen.CameraController.min = new Vector2(-1000, -1000f);
        Managers.Screen.CameraController.max = new Vector2(1000, 1000f);
        cameraPoses = GameObject.Find("@CameraPoses").GetComponentsInChildren<Transform>();
        Transform batSpawnTran = Util.FindChild<Transform>(Managers.Object.MonsterTransform.gameObject, _name: "BatTransforms_OneStage");
        for (int i = 0; i < batSpawnTran.childCount; i++)
            Managers.Object.SpawnMonster(batSpawnTran.GetChild(i).position, Monster.Ghost_Bat);

        Managers.Event.OnIntEvent += CheckEvent;
        _callback?.Invoke();
    }

    public override void Clear()
    {
        Managers.Object.Monsters.Clear();
    }

    public override void SceneEvent(int _eventIndex, Action _callback = null)
    {
        switch ((_eventIndex)) 
        {
            case 0:
                Managers.Game.nowCheckPoint = 0;
                Managers.Screen.CameraController.min = new Vector2(6, -35f);
                Managers.Screen.CameraController.max = new Vector2(9.1f, -35f);
                Managers.Object.ClearMonsters();
                Managers.Object.Player.ChangeDirection(Direction.Right);
                Managers.Object.Player.GetFullHP();
                Managers.Object.Player.SetPosition(new Vector3(2.0999999f, -37.4000015f, 0));
                boss = Managers.Object.SpawnBoss(0, new Vector3(9.43999958f, -37.4000015f, 0));
                break;
            case 1:
                Managers.Object.SpawnItem(1, new Vector3(5, -5f, 0));
                break;

            case 2:
                Transform batSpawnTran = Util.FindChild<Transform>(Managers.Object.MonsterTransform.gameObject, _name: "BatTransforms_TwoStage");
                for (int i = 0; i < batSpawnTran.childCount; i++)
                    Managers.Object.SpawnMonster(batSpawnTran.GetChild(i).position, Monster.Ghost_Bat);
                break;

            case 3:
                Managers.Game.nowCheckPoint = 3;
                Managers.Object.Player.GetFullHP();
                Managers.Object.ClearMonsters();
                Managers.Object.Player.SetPosition(new Vector3(31.59f, -24.11f, 0));
                CentipedeController centipede = Managers.Resource.Instantiate("Ghost_Centipede", Managers.Object.MonsterTransform).GetOrAddComponent<CentipedeController>();
                centipede.SetPosition(new Vector3(18.6700001f, -17.1399994f, 0));
                centipede.Init();
                break;

            case 4:
                Managers.Screen.CameraController.Camera.orthographicSize = 4;
                Managers.Screen.CameraController.SetTarget(cameraPoses[1]);
                break;

            case 5:
                Managers.Screen.CameraController.Camera.orthographicSize = 3;
                Managers.Screen.CameraController.SetTarget(Managers.Object.Player.trans);
                break;

            case 6:
                Managers.Object.SpawnMonster(new Vector3(2.76546454f, -16.8775959f, 0), Monster.Ghost_Bat);
                break;

            case 7:
                Managers.Object.SpawnItem(1, new Vector3(8.93f, -15, 0));
                break;

            case 100:
                
                break;

            case -100:
                Managers.Scene.LoadScene(Scene.Guild);
                break;
        }  
    }

    public void CheckEvent(IntEventType _type, int _value)
    {
        if (_type != IntEventType.OnDeadBoss || _value != 0)
            return;
        SceneEvent(100);
    }
}
