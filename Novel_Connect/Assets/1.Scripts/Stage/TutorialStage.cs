using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStage : Stage
{
    public override void Setup()
    {
        if(CutSceneManager.instance.GetComponent<Tutorial>() == null)
        {
            CutSceneManager.instance.AddCutScene("Tutorial");
        }

        else
        {
            CutSceneManager.instance.GetComponent<Tutorial>().Play();
        }

        if(StageSystem.instance.currentStage == "Tutorial_2")
        {
            CameraScript.instance.GetComponent<Camera>().orthographicSize = 5.7f;
            CameraScript.instance.min = new Vector2(-2.2f,-1.5f);
            CameraScript.instance.max = new Vector2(2f,2.14f);
            CameraScript.instance.playerPlusY = 4.5f;
        }
    }

    public override void UpdateStage()
    {

    }
}
