using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector2 min, max;
    public float delayTime;
    private Player player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(
            Mathf.Clamp(player.transform.position.x , min.x, max.x), 
            Mathf.Clamp(player.transform.position.y , min.y , max.y), 
            transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, delayTime);
    }
}
