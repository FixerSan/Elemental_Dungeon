using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    protected bool isUsed = false;
    protected bool isCanUse = false;

    public abstract void Interaction();
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isUsed && isCanUse)
        {
            Interaction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCanUse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCanUse = false;
        }
    }
}
