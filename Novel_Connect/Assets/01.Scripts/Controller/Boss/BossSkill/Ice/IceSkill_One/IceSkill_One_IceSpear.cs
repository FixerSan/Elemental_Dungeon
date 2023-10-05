using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IceSkill_One_IceSpear : MonoBehaviour
{
    private bool isShot = false;
    private Vector2 target;
    private Vector2 dir;
    private Quaternion rotation;
    private float angle;
    private float time;

    public float angleOffset;
    public float moveSpeed;
    public float rotationSpeed;

    private void Init()
    {
        isShot = false;
        target = Vector2.zero;
        dir = Vector2.zero;
        rotation = Quaternion.identity;
        angle = 0;
        time = 0;

        transform.rotation = Quaternion.identity;
    }

    public void Shot(Vector2 _target)
    {
        target = _target;
        isShot = true;
    }

    private void FixedUpdate()
    {
        if (!isShot) return;
        TrackingTarget();
    }

    private void TrackingTarget()
    {
        if (time < 1)
        {
            time += Time.deltaTime * rotationSpeed;
            if (time > 1) time = 1;
        }
        dir = target - (Vector2)transform.position;
        angle = MathF.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle + angleOffset, transform.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, time);
        transform.position = transform.position += (transform.up * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        Init();
    }
}