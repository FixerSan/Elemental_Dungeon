using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guild : Stage
{
    public override void Setup()
    {
        CameraScript.instance.GetComponent<Camera>().orthographicSize = 5;
        CameraScript.instance.min = new Vector2(- 10.41f, 0f);
        CameraScript.instance.max = new Vector2(15.96f, 0f);
        CameraScript.instance.playerPlusY = 0f;
        MonsterObjectPool.instance.Init(0, 3);
    }

    public override void UpdateStage()
    {
        
    }
}

