using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    PlayerControllerV3 player;
    bool isMove = false;
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    public Direction direction;
    public float moveForce;
    public float stopDelay;

    public Transform[] turnPoints;

    private void Awake()
    {
        for (int i = 0; i < turnPoints.Length; i++)
        {
            turnPoints[i].SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerControllerV3>();
            SetParentPlayer();
        }

        if (collision.transform.name == "TurnLeftPoint")
        {
            StartCoroutine(StopAndTurnLeft());
        }
        else if (collision.transform.name == "TurnRightPoint")
        {
            StartCoroutine(StopAndTurnRifgt());
        }
    }

    public IEnumerator StopAndTurnLeft()
    {
        isMove = false;
        direction = Direction.Left;
        player.playerInput.isCanControl = true;
        yield return new WaitForSeconds(stopDelay);


        isMove = true;
    }

    public IEnumerator StopAndTurnRifgt()
    {
        isMove = false;
        direction = Direction.Right;
        player.playerInput.isCanControl = true;
        yield return new WaitForSeconds(stopDelay);


        isMove = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DeSetParentPlayer();
        }
    }

    void SetParentPlayer()
    {
        player.transform.SetParent(transform);
        isMove = true;
    }

    void DeSetParentPlayer()
    {
        if (!player) return;
        player.transform.SetParent(null);
        DontDestroyOnLoad(player.gameObject);
        player = null;
        isMove = false;
        StopAllCoroutines();
    }

    private void Update()
    {
        if(isMove)
        {
            Move();
        }
    }

    void Move()
    {
        if(direction == Direction.Right)
        {
            rb.velocity = new Vector2(moveForce , 0);
        }

        if (direction == Direction.Left)
        {
            rb.velocity = new Vector2(-moveForce, 0);
        }
        if(player.playerInput.isCanControl)
        {
            player.playerInput.isCanControl = false;
        }
        if(player.state != PlayerState.Idle)
        {
            player.ChangeState(PlayerState.Idle);
            player.playerMovement.Stop();
        }
        player.transform.position = new Vector3(transform.position.x, player.transform.position.y);
    }
}
