using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Portal : MonoBehaviour
{
    public int sceneIndex;
    public Vector2 pos;
    public Direction playerDirection;
    [SerializeField]
    private PlayerController player;
    private void Update()
    {
        if(player != null)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(playerDirection == Direction.Left)
                    GameManager.instance.SceneChange(sceneIndex,player,pos,-1);
                else
                    GameManager.instance.SceneChange(sceneIndex, player, pos, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform.parent.parent.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
        }
    }
}
