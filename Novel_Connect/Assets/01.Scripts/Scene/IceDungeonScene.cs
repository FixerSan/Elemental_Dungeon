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
    public override void Init(Action _loadCallback, Action _soundCallback)
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
        _loadCallback?.Invoke();
        Managers.Sound.FadeOutBGM(2, () => 
        {
            Managers.Sound.FadeInBGM(SoundProfile_BGM.IceDungeon, 2, 0, () => 
            {
                _soundCallback?.Invoke();
            });    
        });
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
                Managers.Game.nowCheckPoint = 9;
                Managers.Input.isCanControl = false;
                Managers.Object.Player.ChangeState(PlayerState.IDLE);
                Managers.Object.Player.Stop();
                Managers.Screen.CameraController.min = new Vector2(6, -35f);
                Managers.Screen.CameraController.max = new Vector2(9.1f, -35f);
                Managers.Object.ClearMonsters();
                Managers.Object.Player.ChangeDirection(Direction.Right);
                Managers.Object.Player.GetFullHP();
                Managers.Object.Player.SetPosition(new Vector3(2.0999999f, -37.4000015f, 0));

                Managers.Sound.FadeOutBGM(2, () => 
                {
                    Managers.Sound.FadeInBGM(SoundProfile_BGM.IceDungeon, 2, 2, () => 
                    {
                        Managers.Sound.FadeChangeBGM(SoundProfile_BGM.IceDungeon, 4, 0);
                        Managers.Input.isCanControl = true;
                    });
                });
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
                Managers.Input.isCanControl = false;
                Managers.Object.Player.ChangeState(PlayerState.IDLE);
                Managers.Object.Player.Stop();
                Managers.Sound.FadeOutBGM(2, () => 
                {
                    Managers.Input.isCanControl = true;
                    Managers.Screen.Shake(3,3);
                    Managers.Sound.PlaySoundEffect(SoundProfile_Effect.Centipede, 0, () => 
                    {
                        Managers.Sound.FadeInBGM(SoundProfile_BGM.IceDungeon, 2, 1);
                        CentipedeController centipede = Managers.Resource.Instantiate("Ghost_Centipede", Managers.Object.MonsterTransform).GetOrAddComponent<CentipedeController>();
                        centipede.SetPosition(new Vector3(18.6700001f, -17.1399994f, 0));
                        centipede.Init();
                    });         
                });
                break;

            case 4:
                Managers.Screen.CameraController.Camera.orthographicSize = 4;
                Managers.Screen.CameraController.SetTarget(cameraPoses[1]);
                break;

            case 5:
                Managers.Screen.CameraController.Camera.orthographicSize = 3;
                Managers.Screen.CameraController.SetTarget(Managers.Object.Player.trans);
                Managers.Sound.FadeChangeBGM(SoundProfile_BGM.IceDungeon, 4, 0);
                break;

            case 6:
                Managers.Object.SpawnMonster(new Vector3(2.76546454f, -16.8775959f, 0), Monster.Ghost_Bat);
                break;

            case 7:
                Managers.Object.SpawnItem(1, new Vector3(8.93f, -15, 0));
                break;

            case 8:
                Managers.Sound.FadeChangeBGM(SoundProfile_BGM.IceDungeon, 4, 0);
                break;

            case 9:
                Managers.Game.nowCheckPoint = 9;
                Managers.Input.isCanControl = false;
                Managers.Object.Player.ChangeState(PlayerState.IDLE);
                Managers.Object.Player.Stop();
                Managers.Screen.CameraController.min = new Vector2(6, -35f);
                Managers.Screen.CameraController.max = new Vector2(9.1f, -35f);
                Managers.Object.ClearMonsters();
                Managers.Object.Player.ChangeDirection(Direction.Right);
                Managers.Object.Player.GetFullHP();
                Managers.Object.Player.SetPosition(new Vector3(2.0999999f, -37.4000015f, 0));
                Managers.Sound.FadeChangeBGM(SoundProfile_BGM.IceDungeon, 4, 0);
                Managers.Input.isCanControl = true;
                break;

            case 100:
                Managers.Sound.FadeChangeBGM(SoundProfile_BGM.IceDungeon, 4, 0);
                break;

            case -100:
                Managers.Scene.LoadScene(Scene.Guild);
                break;
        }  
    }

    public void CheckEvent(IntEventType _type, int _value)
    {
        if (_type != IntEventType.OnDeadBoss)
            return;
        if(_value == 0)
            SceneEvent(100);
        if (_value == 1)
            SceneEvent(8);
    }
}
