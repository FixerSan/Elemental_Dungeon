using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClickableNPC : MonoBehaviour
{
    public int npcID;
    public abstract void Interaction();
}
