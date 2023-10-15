using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    private new Camera camera;
    public Camera Camera 
    { 
        get
        {
            if (camera == null)
                camera = gameObject.GetOrAddComponent<Camera>();
            return camera;
        }
    }

    private Transform trans;
    public Transform Trans
    {
        get
        {
            if (trans == null)
                trans = gameObject.GetOrAddComponent<Transform>();
            return trans;
        }
    }

    public Transform target;

    public Vector3 offset;
    public Vector2 min, max;
    public float delayTime;

    private Vector3 nextPos;


    private void Awake()
    {
        if (Managers.Screen.CameraController != this)
            Managers.Resource.Destroy(gameObject);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetOffset(Vector3 _offset)
    {
        offset = _offset;
    }

    public void FollowTarget()
    {
        if (target == null) return;
        nextPos = new Vector3(target.transform.position.x + offset.x, target.transform.position.y + offset.y, -10);
        nextPos = Vector3.Lerp(Trans.position, nextPos, delayTime * Time.deltaTime);
        nextPos = new Vector3(Mathf.Clamp(nextPos.x, min.x, max.x), Mathf.Clamp(nextPos.y, min.y, max.y), -10);
        Trans.position = nextPos;
    }

    public void LinearMoveCamera(Vector3 _pos , float _moveTotalTime , Action _callback = null)
    {
        Trans.DOMove(_pos, _moveTotalTime).onComplete += () => 
        {
            _callback?.Invoke();
        };
    }

    private void Update()
    {
        FollowTarget();
    }
}
