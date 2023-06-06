using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public AnimationSystem anim;
    public InventoryV2 inventory;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            DialogSystem.Instance.UpdateDialog(1008);
        if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.instance.LoadScene("Guild");
        }

    }
}
