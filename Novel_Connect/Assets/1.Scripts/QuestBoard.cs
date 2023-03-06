using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBoard : ClickableNPC
{
    public GameObject questBoardUI;
    public override void Interaction()
    {
        questBoardUI.SetActive(true);
    }
}
