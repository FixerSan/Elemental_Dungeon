using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildScene : BaseScene
{
    public override void Init()
    {
        base.Init();
        Managers.Object.Player.Init(1, Define.Elemental.Normal.ToString());
        Managers.Object.Player.SetPosition(new Vector3(3, -1 ,0));
        Managers.Screen.SetCameraTarget(Managers.Object.Player.GetTrans());  
        Managers.Object.SpawnMonster(new Vector3(-3,0,0), Define.Monster.Ghost_Bat);
    }

    public override void Clear()
    {

    }

    public GuildScene()
    {
        cameraOffset = new Vector3(0,0,-10);
    }
}
