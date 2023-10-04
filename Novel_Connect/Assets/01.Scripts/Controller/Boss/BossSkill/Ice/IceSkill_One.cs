using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IceSkill_One : MonoBehaviour
{
    private bool isInit = false;
    private Vector2 target;
    private Vector2 dir;
    private Quaternion rotation;
    private float angle;

    public float angleOffset;
    public float moveSpeed;
    public float rotationSpeed;

    private void Awake()
    {
        isInit = false;
        target = Vector2.zero;
        dir = Vector2.zero;
    }

    public void Init(Transform _target)
    {
        target = _target.position;
        isInit = true;
    }

    private void FixedUpdate()
    {
        if (!isInit) return;
        TrackingTarget();
    }

    private void TrackingTarget()
    {
        dir = target - (Vector2)transform.position;
        angle = MathF.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle + angleOffset, transform.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        transform.position = transform.position += (transform.up * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Managers.Resource.Destroy(gameObject);
        }
    }
}
