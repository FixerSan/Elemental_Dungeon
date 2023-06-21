using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildScene : BaseScene
{
    protected override void Setup()
    {
        CameraScript.instance.GetComponent<Camera>().orthographicSize = 5;
        CameraScript.instance.min = new Vector2(-10.41f, 0f);
        CameraScript.instance.max = new Vector2(15.96f, 0f);
        CameraScript.instance.playerPlusY = 0f;
        GameManager.instance.player.transform.position = new Vector3(0.0500000007f, 0.379999995f, 0);

    }
}
