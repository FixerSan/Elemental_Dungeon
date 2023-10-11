using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildScene : BaseScene
{
    public override void Init()
    {
        base.Init();
        Managers.UI.ShowSceneUI<UIBaseScene>("UIScene_BaseScene");
        Managers.Object.Player.Init(1, Define.Elemental.Normal.ToString());
        Managers.Object.Player.SetPosition(new Vector3(3, -1 ,0));
        Managers.Screen.SetCameraTarget(Managers.Object.Player.GetTrans());  
        Managers.Object.SpawnMonster(new Vector3(-3,0,0), Define.Monster.Ghost_Bat);
        Managers.Object.SpawnBoss(0, new Vector3(2.5f, -2f, 0f));
    }

    public override void Clear()
    {

    }

    public override void SceneEvent(int _eventIndex)
    {

    }

    public GuildScene()
    {
        cameraOffset = new Vector3(0,2,-10);
    }
}
