using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public Rigidbody2D rb;
    public Direction direction;
    public bool isWalking;
    Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Move()
    {

        if (!isWalking)
        {
            if (animator.GetBool("isWalk"))
                animator.SetBool("isWalk", false);
            return;
        }

        if (!animator.GetBool("isWalk"))
            animator.SetBool("isWalk", true);


        if (direction == Direction.Left)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void Stop()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void Update()
    {
        Move();
    }


}

