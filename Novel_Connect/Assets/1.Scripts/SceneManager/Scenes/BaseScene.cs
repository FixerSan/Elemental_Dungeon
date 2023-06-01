using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public string sceneName;

    private void Awake()
    {
        Setup();
    }

    protected virtual void Setup()
    {

    }
    protected virtual void Clear()
    {

    }
}
