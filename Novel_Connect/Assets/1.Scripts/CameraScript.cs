using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector2 min, max;
    public float delayTime;
    private PlayerController player;
    public float playerPlusY;

    public CameraState cameraState;
    private void Awake()
    {
        player = PlayerController.instance;
    }

    private void FixedUpdate()
    {
        if (player == null)
            player = PlayerController.instance;
        switch (cameraState)
        {
            case CameraState.idle :

                Vector3 targetPos = new Vector3(
                    Mathf.Clamp(player.transform.position.x , min.x, max.x), 
                    Mathf.Clamp(player.transform.position.y + playerPlusY, min.y , max.y), 
                    transform.position.z);

                transform.position = Vector3.Lerp(transform.position, targetPos, delayTime);
                break;
            case CameraState.cutscene :
                break;
        }
    }

    public void ChangeState(CameraState state)
    {
        cameraState = state;
    }
}

public enum CameraState
{
    idle, cutscene
}
