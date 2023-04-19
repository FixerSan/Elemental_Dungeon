using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : CutScene
{
    private CameraScript m_Camera;
    private PlayerController player;
    private DialogSystem dialogSystem;

    public override void Play()
    {
        
    }

    public override void Setup()
    {
        m_Camera = CameraScript.instance;
        player = PlayerController.instance;
        dialogSystem = DialogSystem.instance;

        index = 0;
    }
}
