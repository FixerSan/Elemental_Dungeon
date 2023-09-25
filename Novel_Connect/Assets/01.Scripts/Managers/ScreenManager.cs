using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager
{
    private CameraController cameraController;  // ī�޶� ��Ʈ�ѷ� ����
    public CameraController CameraController    // ī�޶� ��Ʈ�ѷ� ������Ƽ ����
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

    // ī�޶� Ÿ�� ����
    public void SetCameraTarget(Transform _target)
    {
        CameraController.SetTarget(_target);
    }

    // ī�޶� ������ ����
    public void SetCameraOffset(Vector3 _offset)
    {
        CameraController.SetOffset(_offset);
    } 
}
