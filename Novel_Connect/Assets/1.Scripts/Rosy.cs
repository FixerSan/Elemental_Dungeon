using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rosy : ClickableNPC
{
    public int dialogIndex;
    public override void Interaction()
    {
        DialogSystem.Instance.UpdateDialog(dialogIndex);
    }
}
