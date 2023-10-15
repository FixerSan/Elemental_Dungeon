using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool isOneTime;
    private bool isUsed;
    public int sceneEventIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOneTime)
            if (isUsed)
                return;
        if(collision.CompareTag("Player"))
        {
            Managers.Scene.GetScene<IceDungeonScene>().SceneEvent(sceneEventIndex);
            isUsed = true;
        }
    }
}
