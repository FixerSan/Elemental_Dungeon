using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesxtfedfe : MonoBehaviour
{
    public float jumpPower;
    Rigidbody2D rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

    }

}
