using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public Transform triggerHeader;
    private Transform[] triggers;
    private GameObject player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerControllerV3>(true).gameObject;
        triggers = triggerHeader.GetComponentsInChildren<Transform>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            player.transform.position = triggers[0].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.transform.position = triggers[1].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.transform.position = triggers[2].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.transform.position = triggers[3].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.transform.position = triggers[4].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            player.transform.position = triggers[5].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            player.transform.position = triggers[6].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            player.transform.position = triggers[7].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            player.transform.position = triggers[8].position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            player.transform.position = triggers[13].position;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            AudioSystem.Instance.FadeInMusic("BGM",0, 2);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AudioSystem.Instance.FadeInMusic("BGM", 1,2);
        }
    }

}
