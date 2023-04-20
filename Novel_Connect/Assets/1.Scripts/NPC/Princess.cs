using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : MonoBehaviour
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

        if (!isWalking)
        {
            if (animator.GetBool("isWalk"))
                animator.SetBool("isWalk", false);
                return;
        }

        if (!animator.GetBool("isWalk"))
            animator.SetBool("isWalk", true);

        if (animator.GetBool("isFalldown"))
            animator.SetBool("isFalldown", false);

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

    private void Update()
    {
        Move();
    }

    public void Falldown()
    {
        animator.SetBool("isWalk", false);
        animator.SetBool("isFalldown", true);
        isWalking = false;
        rb.velocity = Vector2.zero;
        CutSceneManager.instance.gameObject.GetComponent<Tutorial>().StartCoroutine(CutSceneManager.instance.gameObject.GetComponent<Tutorial>().Tutorial_4());
    }


}




