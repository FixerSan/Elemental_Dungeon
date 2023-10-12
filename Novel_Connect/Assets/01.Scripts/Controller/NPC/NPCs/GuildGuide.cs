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
        if (!Managers.Game.npcFirstDictionary.ContainsKey($"{nameof(GuildGuide)}"))
            Managers.Game.npcFirstDictionary.Add($"{nameof(GuildGuide)}", true);
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
        if (!isHover) return;
        if (Managers.Game.npcFirstDictionary[($"{nameof(GuildGuide)}")])
        {
            Managers.Dialog.Call(1001);
            return;
        }

        else
        {
            if(Managers.Quest.quests.Count > 1 && Managers.Quest.isCanGetReward)
            {

            }

            else
            {
                Managers.Dialog.Call(1000);
            }
        }
    }
}
