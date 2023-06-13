using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : HitableObejct
{
    public Transform target;
    public Transform nextTarget;
    public float canDistance;
    public Centipede centipede;
    public float rotationSpeed;
    private Vector2 direction;
    public float moveSpeed;

    public Vector3 golePos;

    public bool isCanMove =  true;
    public bool isUp = true;

    public float moveDelay;
    public LineRendererSystem line => FindObjectOfType<LineRendererSystem>();

    private void Update()
    {
        Move(); 
    }

    public void Move()
    {
        if (!target || !isCanMove) return;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position,target.position) > 0.5f)
        {
            transform.position = new Vector3(transform.position.x+direction.x * moveSpeed * Time.deltaTime, transform.position.y + direction.y * moveSpeed* Time.deltaTime);
        }
        else
        {
            StartCoroutine(MoveDelay(moveDelay));
        }
    }

    private void OnEnable()
    {
        isCanMove = false;
        StopAllCoroutines();
        StartCoroutine(MoveDelay(2));
    }

    public void Stop()
    {
        direction = Vector2.zero;
    }

    IEnumerator MoveDelay(float delay)
    {
        isCanMove = false;
        yield return new WaitForSeconds(delay);
        centipede.SetTarget(null);
        line.Play(gameObject.transform.position, nextTarget.position);
        yield return new WaitForSeconds(delay);
        line.Stop();
        target = nextTarget;
        direction = target.position - transform.position;
        dsdsd(direction);
        isCanMove = true;
    }

    public void ChangeTarget(Transform target_)
    {
        nextTarget = target_;
        isUp = !isUp;
    }

    public override void GetDamage(float damage)
    {
        centipede.GetDamage(damage);
    }

    public void dsdsd(Vector2 direction_)
    {
        Vector2 aDir = new Vector2(Mathf.Abs(direction_.x), Mathf.Abs(direction_.y));
        Vector2 cDIr = direction_;
        if (aDir.x < aDir.y)
        {
            cDIr.x = aDir.x / aDir.y;
            cDIr.y = aDir.y / aDir.y;
        }   
        else
        {
            cDIr.x = aDir.x / aDir.x;
            cDIr.y = aDir.y / aDir.x;
        }

        if (direction_.x > 0)
            direction.x = cDIr.x;
        else
            direction.x = cDIr.x * (-1);

        if (direction_.y > 0)
            direction.y = cDIr.y;
        else
            direction.y = cDIr.y * (-1);
    }

    public override void Hit(float damage)
    {
        throw new System.NotImplementedException();
    }
}

