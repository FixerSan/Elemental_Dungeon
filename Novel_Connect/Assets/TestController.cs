using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public AnimationSystem anim;
    public InventoryV2 inventory;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            anim.PlayAnimation("Idle");
        if (Input.GetKeyDown(KeyCode.W))
            anim.PlayAnimation("Walk");
        if (Input.GetKeyDown(KeyCode.E))
            anim.StopAnimation();

        if (Input.GetKeyDown(KeyCode.R))
            inventory.AddItem(1);

        if (Input.GetKeyDown(KeyCode.T))
            inventory.AddItem(2);
    }
}
