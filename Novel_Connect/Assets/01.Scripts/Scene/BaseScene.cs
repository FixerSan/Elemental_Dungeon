using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseScene : MonoBehaviour
{
    public Vector3 cameraOffset;
    public virtual void Init(Action _callback = null)
    {
        Managers.Screen.SetCameraOffset(cameraOffset);
    }
    public abstract void Clear();

    public abstract void SceneEvent(int _eventIndex, Action _callback = null);
}
