using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildGuide : NPCController
{
    private Animator anim;
    protected override void Init()
    {
        base.Init();
        anim = GetComponent<Animator>();
    }

    public override void EnterHover()
    {
        base.EnterHover();
        anim.Play("move", 0);
    }

    public override void ExitHover()
    {
        base.ExitHover();
        anim.Play("idle", 0);
    }

    public override void Interaction()
    {
        Managers.Dialog.Call(1001 ,() => 
        {
            Managers.scene.GetScene<GuildScene>().SceneEvent(0);
        });
    }
}
