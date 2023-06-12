using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFoward : HitableObejct
{
    public Centipede centipede;
    public GameObject target;
    public Vector3 targetPos;
    public float distance;
    public float speed;
    public float rotationSpeed = 25;
    public float slowTime;

    public override void GetDamage(float damage)
    {
        centipede.GetDamage(damage);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Vector2.Distance(transform.position, target.transform.position) > distance)
        //{
        //    transform.position = Vector3.Lerp(transform.position, (Vector2.MoveTowards(transform.position, target.transform.position, 0.5f)), speed * Time.deltaTime);

        //    transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, rotationSpeed * Time.deltaTime);
        //}
        if (targetPos == null) return;
        transform.position = Vector3.Lerp(transform.position, (Vector2.MoveTowards(transform.position, targetPos, 0.5f)), speed * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, rotationSpeed * Time.deltaTime);
        StartCoroutine(Follow());
    }

    IEnumerator Follow()
    {
        Vector3 slowPos = target.transform.position;
        yield return new WaitForSeconds(slowTime);


        targetPos = slowPos;
    }

    public override void Hit(float damage)
    {
        throw new System.NotImplementedException();
    }
}
