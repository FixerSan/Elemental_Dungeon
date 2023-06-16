using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : OnTriggerEnterPlayer
{
    public int triggerIndex;
    public bool justOneTime = true;
    public float isCanDelay;

    bool isUsed = false;
    float checkTime = 0;
    public override void Enter(Collider2D collision)
    {
        if (isUsed) return;
        SceneManager.instance.GetCurrentScene().TriggerEffect(triggerIndex);
        isUsed = true;
    }

    private void Update()
    {
        if(!justOneTime && isUsed)
        {
            checkTime += Time.deltaTime;
            if(checkTime >= isCanDelay)
            {
                checkTime = 0;
                isUsed = false;
            }
        }
    }
}
