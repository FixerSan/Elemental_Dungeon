using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDungeonScene : BaseScene
{
    private void Awake()
    {
        cameraOffset = new Vector3(0, 2, -10);
    }

    public override void Init()
    {
        base.Init();
        Debug.Log("아이스 던전 켜짐");
        Managers.UI.ShowSceneUI<UIBaseScene>("UIScene_BaseScene");
        Managers.Object.Player.Init(1, Define.Elemental.Normal.ToString());
        Managers.Object.Player.SetPosition(new Vector3(3, -1, 0));
        Managers.Screen.SetCameraTarget(Managers.Object.Player.trans);
    }

    public override void Clear()
    {

    }
}
