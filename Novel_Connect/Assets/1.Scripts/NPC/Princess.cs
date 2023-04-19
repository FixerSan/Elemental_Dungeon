using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : MonoBehaviour
{
    public Rigidbody2D rb;
    public Direction direction;
    public bool isWalking;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Falldown();
            PlayerController.instance.Stop();
        }
    }

    public void Move()
    {
        Vector2 next = new Vector2(1, 0);
        if (!isWalking)
            return;
        if (direction == Direction.Left)
            rb.velocity = new Vector2(-5, rb.velocity.y);
        else
            rb.velocity = new Vector2(5, rb.velocity.y);
    }

    private void Update()
    {
        Move();
    }

    public void Falldown()
    {
        isWalking = false;
        rb.velocity = Vector2.zero;
        CutSceneManager.instance.gameObject.GetComponent<Tutorial>().StartCoroutine(CutSceneManager.instance.gameObject.GetComponent<Tutorial>().Tutorial_4());
    }


}




