using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : OnTriggerEnterPlayer
{
    public int triggerIndex;
    bool isUsed = false;
    public override void Enter(Collider2D collision)
    {
        if (isUsed) return;
        SceneManager.instance.GetCurrentScene().TriggerEffect(triggerIndex);
        isUsed = true; ;
    }
}
