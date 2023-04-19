using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStage : Stage
{
    public override void Setup()
    {
        CutSceneManager.instance.PlayCutScene("Tutorial");
    }

    public override void UpdateStage()
    {

    }
}
