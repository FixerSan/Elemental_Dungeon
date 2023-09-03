using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            float x = Random.Range(-2, 2);
            Managers.Object.SpawnMonster(new Vector3(x,0,0),0);
        }
    }
}
