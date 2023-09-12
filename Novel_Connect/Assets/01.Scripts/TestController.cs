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

        if (Input.GetKeyDown(KeyCode.F1))
            Managers.Dialog.Call(0);

        if (Input.GetKeyDown(KeyCode.F2))
            Managers.Screen.SetCameraTarget(Managers.Object.Player.trans);
    }

    private void OnEnable()
    {
        Managers.Event.OnIntEvent += (_event, _uid) => { if (_event == IntEventType.OnDeadMonster) Debug.Log(_uid); };
    }
}
