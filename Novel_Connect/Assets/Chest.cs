using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int chestIndex;
    Animator animator;
    bool isCanOpen = false;
    bool isFirst = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isCanOpen && isFirst)
        {
            Open();
        }
    }

    void Open()
    {
        isFirst = false;
        animator.SetTrigger("Open");
        ChestSystem.Instance.ChestEvent(chestIndex,this.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isCanOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCanOpen = false;
        }
    }
}
