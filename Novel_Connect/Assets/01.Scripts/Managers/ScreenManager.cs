using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager
{
    private CameraController cameraController;
    public CameraController CameraController
    {
        get
        {
            if(cameraController == null)
            {
                GameObject go = GameObject.Find("Main Camera");
                if(go == null)
                {
                    go = Managers.Resource.Load<GameObject>("Main Camera");
                }
                cameraController = go.GetOrAddComponent<CameraController>();
            }
            return cameraController;
        }
    }

    public void SetCameraTarget(Transform _target)
    {
        CameraController.SetTarget(_target);
    }

    public void SetCameraOffset(Vector3 _offset)
    {
        CameraController.SetOffset(_offset);
    }
}
