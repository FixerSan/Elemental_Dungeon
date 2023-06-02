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
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        target = GameObject.Find("Player");
    }
    #endregion

    public Vector2 min, max;
    public float delayTime;
    public float playerPlusY;

    public GameObject target;

    public CameraState cameraState;

    public bool isMove;
    public float moveSpeed;
    private Vector3 moveDirection;
    public bool isShake;
    public float shakeRange;

    private void FixedUpdate()
    {
        switch (cameraState)
        {
            case CameraState.idle :

                Vector3 targetPos = new Vector3(
                    Mathf.Clamp(target.transform.position.x , min.x, max.x), 
                    Mathf.Clamp(target.transform.position.y + playerPlusY, min.y , max.y), 
                    transform.position.z);

                transform.position = Vector3.Lerp(transform.position, targetPos, delayTime);
                break;

            case CameraState.cutscene :
                break;
        }


        if(isShake)
        {
            float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
            float cameraPosY = Random.value * shakeRange * 2 - shakeRange;

            Vector3 cameraPos = transform.position;
            cameraPos.x += cameraPosX;
            cameraPos.y += cameraPosY;
            transform.position = cameraPos;
        }

        if (isMove)
            Move(moveDirection);
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(direction * Time.fixedDeltaTime * moveSpeed);
    }

    public IEnumerator Shake(float time)
    {
        isShake = true;
        yield return new WaitForSeconds(time);
        isShake = false;
    }

    public IEnumerator MoveToPos(Vector3 pos , Vector3 direction)
    {
        moveDirection = direction;
        isMove = true;
        yield return new WaitUntil(() => Vector3.Distance(transform.position , pos) <= 0.1f);
        isMove = false;
        moveDirection = Vector3.zero;
    }

    public void ChangeState(CameraState state)
    {
        cameraState = state;
    }

    public void ChangeTarget(GameObject target_)
    {
        target = target_;
    }
}

public enum CameraState
{
    idle, cutscene
}
