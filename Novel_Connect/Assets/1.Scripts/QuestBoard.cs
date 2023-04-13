using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBoard : ClickableNPC
{
    public GameObject questBoardUI;
    public override void Interaction()
    {
        questBoardUI = CanvasScript.instance.transform.Find("QuestBoardUI").gameObject;
        questBoardUI.SetActive(true);
    }
}
