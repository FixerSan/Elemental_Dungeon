using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject camera_;
    public float cameraPosition;
    public float moveSpeed;

    private float change;

    private void Start()
    {
        camera_ = Camera.main.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (cameraPosition != camera_.transform.position.x)
        {
            float nowCameraX = camera_.transform.position.x;
            change = nowCameraX - cameraPosition;
        }

        transform.position = new Vector3(transform.position.x - change*moveSpeed, transform.position.y, transform.position.z);
        change = 0;
        cameraPosition = camera_.transform.position.x;
    }
}
