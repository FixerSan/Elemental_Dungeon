using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownScene : BaseScene
{
    protected override void Setup()
    {
        CameraScript.instance.GetComponent<Camera>().orthographicSize = 8;
        CameraScript.instance.min = new Vector2(-25.2f, -10.58f);
        CameraScript.instance.max = new Vector2(21.8f, 5.8f);
        CameraScript.instance.playerPlusY = 5f;
    }
}
