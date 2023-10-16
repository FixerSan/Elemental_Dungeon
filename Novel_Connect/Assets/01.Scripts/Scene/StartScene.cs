using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    public override void Init(Action _callback)
    {
        Managers.UI.ShowSceneUI<UIStartScene>("UIScene_StartScene");
    }
    public override void Clear()
    {

    }

    public override void SceneEvent(int _eventIndex, Action _callback = null)
    {

    }
}
