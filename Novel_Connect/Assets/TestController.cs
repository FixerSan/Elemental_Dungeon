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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                player.transform.position = displayPoses[0].position;
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                DialogSystem.Instance.SetAllClose();
                SceneManager.instance.LoadScene("StartScene");
            }
        }
    }

} 
