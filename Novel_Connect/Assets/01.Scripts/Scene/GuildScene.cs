using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildScene : BaseScene
{
    public override void Init()
    {
        Managers.Object.Player.Init(1, Define.Elemental.Normal.ToString());
        Managers.Object.Player.SetPosition(new Vector3(0, -1 ,0));
        Managers.Object.SpawnMonster(new Vector3(1,0,0), 0);
    }

    public override void Clear()
    {

    }
}
