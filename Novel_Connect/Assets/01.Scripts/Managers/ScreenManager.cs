using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

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
                    go = Managers.Resource.Instantiate("Main Camera");
                UnityEngine.Object.DontDestroyOnLoad(go);
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

    public void Shake(float _shakeForce, float _time = 0)
    {
        CameraController.shakeForce = _shakeForce;
        CameraController.isShake = !CameraController.isShake;
        if (_time != 0)
            Managers.Routine.StartCoroutine(ShakeRoutine(_time));
    }

    private IEnumerator ShakeRoutine(float _time)
    {
        yield return new WaitForSeconds(_time);
        CameraController.isShake = false;
    }

    public void FadeIn(float _fadeTime, Action _callback = null)
    {
        Managers.Routine.StartCoroutine(CameraController.FadeIn(_fadeTime, () => { _callback?.Invoke(); }));
    }

    public void FadeOut(float _fadeTime, Action _callback = null)
    {
        Managers.Routine.StartCoroutine(CameraController.FadeOut(_fadeTime, () => { _callback?.Invoke(); }));
    }

    public void FadeInOut(float _totalTile, Action _callback = null)
    {
        Managers.Routine.StartCoroutine(CameraController.FadeInOut(_totalTile, () => { _callback?.Invoke(); }));
    }
}
