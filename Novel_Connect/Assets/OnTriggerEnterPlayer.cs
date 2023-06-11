using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnTriggerEnterPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Enter(collision);
    }

    public abstract void Enter(Collider2D collision);

}
