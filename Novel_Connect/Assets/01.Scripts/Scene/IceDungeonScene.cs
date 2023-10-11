using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public override void Clear()
    {

    }

    public override void SceneEvent(int _eventIndex)
    {
        switch ((_eventIndex)) 
        {
            case 0:
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
