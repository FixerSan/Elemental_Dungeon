using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStage : Stage
{
    public override void Setup()
    {
        if(CutSceneManager.instance.GetComponent<Tutorial>() == null)
            CutSceneManager.instance.AddCutScene("Tutorial");
    }

    public override void UpdateStage()
    {

    }
}
