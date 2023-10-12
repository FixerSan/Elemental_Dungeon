using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            Managers.Object.Player.SetPosition(new Vector3(31.5799999f, -24.1100006f, 0));

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Managers.Object.Player.SetPosition(new Vector3(2.0999999f, -37.4000015f, 0));
            Managers.Object.SpawnBoss(0, new Vector3(9.43999958f, -37.4000015f, 0));
        }
    }
}
