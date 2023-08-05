using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObstacle : InteractableObject
{
    public int index;
    public override void Awake()
    {
        base.Awake();
        isRuning = false;
    }
    public override void Interaction()
    {
        switch(index)
        {
            case 0:
                SceneManager.instance.GetCurrentScene().TriggerEffect(27);
                break;
        }
    }
}
