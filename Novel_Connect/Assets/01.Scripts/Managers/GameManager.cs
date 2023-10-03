using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera cam;
    private LayerMask layerMask;
    private NPCController npc;

    private void Awake()
    {
        cam = Camera.main;
        layerMask = LayerMask.GetMask("NPC");
    }
    void FixedUpdate()
    {
        CheckMousePointInteraction();
    }

    private void Update()
    {
        CheckInteraction();
    }

    public void CheckMousePointInteraction()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 100, layerMask);
        if (hit)
        {
            if(npc == null)
                npc = hit.transform.GetOrAddComponent<NPCController>();
            npc.EnterHover();
        }
        else
        {
            if(npc != null)
                npc.ExitHover();
            npc = null;
        }
    }

    public void CheckInteraction()
    {
        if (Input.GetMouseButtonDown(0) && npc != null)
        {
            npc.Interaction();
        }
    }
}
