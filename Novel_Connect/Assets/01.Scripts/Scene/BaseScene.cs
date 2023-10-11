using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public Vector3 cameraOffset;
    public virtual void Init()
    {
        Managers.Screen.SetCameraOffset(cameraOffset);
    }
    public abstract void Clear();

    public abstract void SceneEvent(int _eventIndex);
}
