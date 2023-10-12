using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class IceDungeonScene : BaseScene
{
    public int nowCheckPoint;
   
    public override void Init()
    {
        cameraOffset = new Vector3(0, 2, -10);
        base.Init();
        nowCheckPoint = 0;
        Managers.UI.ShowSceneUI<UIBaseScene>("UIScene_BaseScene");
        Managers.Object.Player.Init(1, Define.Elemental.Normal.ToString());
        Managers.Object.Player.SetPosition(new Vector3(-5, 1, 0));
        Managers.Screen.SetCameraTarget(Managers.Object.Player.trans);
        Managers.Screen.CameraController.Camera.orthographicSize = 3;
        Managers.Screen.CameraController.min = new Vector2(-1000, -1000f);
        Managers.Screen.CameraController.max = new Vector2(1000, 1000f);
        Transform batSpawnTran = Util.FindChild<Transform>(Managers.Object.MonsterTransform.gameObject, _name: "BatTransforms");

        for (int i = 0; i < batSpawnTran.childCount; i++)
        {
            Managers.Object.SpawnMonster(batSpawnTran.GetChild(i).position, Monster.Ghost_Bat);
        }

        CentipedeController centipede = Managers.Resource.Instantiate("Ghost_Centipede", Managers.Object.MonsterTransform).GetOrAddComponent<CentipedeController>();
        centipede.SetPosition(new Vector3(18.6700001f, -17.1399994f, 0));
        centipede.Init();
    }

    public override void Clear()
    {

    }

    public override void SceneEvent(int _eventIndex)
    {
        switch ((_eventIndex)) 
        {
            case 0:
                Managers.Object.Player.SetPosition(new Vector2(1.5f, -37f));
                break;
        }  
    }

    public void BackToCheckPoint()
    {
        switch ((nowCheckPoint))
        {
            case 0:
                break;
        }
    }
}
