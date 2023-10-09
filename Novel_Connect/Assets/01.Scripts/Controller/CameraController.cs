using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.EventSystems;

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
        nextPos = new Vector3(Mathf.Clamp((target.transform.position.x + offset.x), min.x, max.x), Mathf.Clamp((target.transform.position.y + offset.y), min.y, max.y), -10);
        nextPos = Vector3.Lerp(Trans.position, nextPos, delayTime * Time.deltaTime);
        Trans.position = nextPos;
    }

    private void Update()
    {
        FollowTarget();
    }
}
