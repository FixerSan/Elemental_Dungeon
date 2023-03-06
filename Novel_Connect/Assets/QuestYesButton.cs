using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestYesButton : MonoBehaviour
{
    public QuestSlot nowQuest;

    public void Yes()
    {
        nowQuest.AddQuest();
    }
}
