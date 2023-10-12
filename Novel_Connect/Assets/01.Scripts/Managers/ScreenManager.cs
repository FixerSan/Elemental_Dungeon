using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager
{
    private CameraController cameraController;  // 카메라 컨트롤러 선언
    public CameraController CameraController    // 카메라 컨트롤러 프로퍼티 선언
    {
        get
        {
            if(cameraController == null)
            {
                GameObject go = GameObject.Find("Main Camera");
                if(go == null)
                    go = Managers.Resource.Instantiate("Main Camera");
                Object.DontDestroyOnLoad(go);
                cameraController = go.GetOrAddComponent<CameraController>();
            }
            return cameraController;
        }
    }

    // 카메라 타겟 설정
    public void SetCameraTarget(Transform _target)
    {
        CameraController.SetTarget(_target);
    }

    // 카메라 오프셋 설정
    public void SetCameraOffset(Vector3 _offset)
    {
        CameraController.SetOffset(_offset);
    } 
}
