using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Portal : MonoBehaviour
{
    public string portName;
    private GameObject portObject;
    private void Update()
    {
        if (portObject)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                PortalSystem.instance.PortOject(portObject, portName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            portObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            portObject = null;
        }
    }
}