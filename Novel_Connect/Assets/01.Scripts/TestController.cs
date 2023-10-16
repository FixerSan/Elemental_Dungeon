using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public float sdsd;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            Managers.Object.Player.SetPosition(new Vector3(31.5799999f, -24.1100006f, 0));

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Managers.Screen.Shake(sdsd);
        }
    }
}
