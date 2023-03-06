using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rosy : ClickableNPC
{
    public GameObject RosyUIPanel;
    public Dialogue info;


    public override void Interaction()
    {
        RosyUIPanel.SetActive(true);
        var system = FindObjectOfType<DialogueSystem>();
        system.Begin(info);
    }

    private void Start()
    {
        RosyUIPanel.SetActive(false);
    }
}
