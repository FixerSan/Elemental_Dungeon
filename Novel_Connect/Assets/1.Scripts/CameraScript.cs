using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    #region Singleton;
    private static CameraScript Instance;
    public static CameraScript instance
    {
        get
        {
            if (Instance != null)
                return Instance;

            else
                return null;
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        player = PlayerController.instance;
    }
    #endregion

    public Vector2 min, max;
    public float delayTime;
    private PlayerController player;
    public float playerPlusY;

    public CameraState cameraState;


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
