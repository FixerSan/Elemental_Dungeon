using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public LayerMask clickLayer;
    public GameObject canClickNPC;


    private void Update()
    {
        //if(GameManager.instance.MouseLayCheckUse)
            HoverCheck();
        if (canClickNPC)
            ClickCheck();
    }
    private void HoverCheck()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0 , clickLayer);


        if (hit.collider != null)
        {
            canClickNPC = hit.transform.gameObject;
            canClickNPC.GetComponent<SpriteRenderer>().enabled = true;
        }

        else
        {
            if(canClickNPC)
            {
                canClickNPC.GetComponent<SpriteRenderer>().enabled = false;
                canClickNPC = null;
            }
        }
    }
    private void ClickCheck()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(canClickNPC.GetComponent<ClickableNPC>() != null)
            {
                canClickNPC.GetComponent<ClickableNPC>().Interaction();
                canClickNPC.GetComponent<SpriteRenderer>().enabled = false;
                canClickNPC = null;
                GameManager.instance.MouseLayCheckUse = false;
            }
        }
    }
}
