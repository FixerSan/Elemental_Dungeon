using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    Animator animator => GetComponent<Animator>();
    public int totemIndex;
    bool isUsed = false;
    bool isCanOpen = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isUsed && isCanOpen)
        {
            Use();
        }
    }

    void Use()
    {
        if (!TotemSystem.Instance.CheckCanUse(totemIndex)) return;
        animator.SetTrigger("On");
        isUsed = false;
        TotemSystem.Instance.TotemEvent(totemIndex, this.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
