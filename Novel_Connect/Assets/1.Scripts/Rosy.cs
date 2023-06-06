using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rosy : ClickableNPC
{
    public override void Interaction()
    {
        DialogSystem.Instance.UpdateDialog(10000);
    }
}
