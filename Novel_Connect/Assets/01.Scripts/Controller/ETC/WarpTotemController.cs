using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTotemController : MonoBehaviour
{
    private Animator animator;
    private bool isCanUse;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isCanUse = false;
    }
    
    private void CheckUse()
    {
        if (!isCanUse) return;
        Managers.Input.CheckInput(Managers.Input.interactionKey, (_inputType) => 
        {
            if (_inputType != InputType.PRESS) return;
            Use();
        });
    }

    private void Use()
    {
        animator.SetBool("isUse", true);
        IceDungeonScene scene = Managers.scene.GetScene<IceDungeonScene>();
        scene.SceneEvent(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
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

    private void Update()
    {
        CheckUse();
    }
}
