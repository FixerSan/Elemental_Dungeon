using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public Transform testPos;
    public Transform[] displayPoses;
    private GameObject player;

    private void Awake()
    {
        player = GameManager.instance.player.gameObject;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.transform.position = displayPoses[0].position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.transform.position = displayPoses[1].position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.transform.position = displayPoses[2].position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.transform.position = displayPoses[3].position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            player.transform.position = displayPoses[4].position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.instance.LoadScene("StartScene");
        }
    }

}
