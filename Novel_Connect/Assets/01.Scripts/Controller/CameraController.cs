using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SetTarget(Transform _target)
    {
        target = _target;
        Trans.position = _target.position;
    }

    public void SetOffset(Vector3 _offset)
    {
        offset = _offset;
    }

    public void FollowTarget()
    {
        if (target == null) return;
        Trans.position = target.position + offset;
    }


    private void Update()
    {
        FollowTarget();
    }
}
