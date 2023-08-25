using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildScene : BaseScene
{
    public override void Init()
    {
        Managers.Object.Player.Init(1, Define.Elemental.Normal.ToString());
        Managers.Object.Player.SetPosition(Vector3.zero);
    }

    public override void Clear()
    {

    }
}
