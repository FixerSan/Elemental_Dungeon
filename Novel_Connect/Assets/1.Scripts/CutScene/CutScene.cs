using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CutScene : MonoBehaviour
{
    public int index;
    public abstract void Setup();
    public abstract void Play();
}
