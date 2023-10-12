using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBoardManager : NPCController
{
    protected override void Init()
    {
        base.Init();
        if (!Managers.Game.npcFirstDictionary.ContainsKey($"{nameof(QuestBoardManager)}"))
            Managers.Game.npcFirstDictionary.Add($"{nameof(QuestBoardManager)}", true);
    }

    public override void Interaction()
    {
        if (!Managers.Game.npcFirstDictionary[$"{nameof(QuestBoardManager)}"]) return;
        if (Managers.Game.npcFirstDictionary[$"{nameof(GuildGuide)}"]) return;
        Managers.Dialog.Call(1004);
    }

    public override void EnterHover()
    {
        if (isHover) return;
        if (!Managers.Game.npcFirstDictionary[$"{nameof(QuestBoardManager)}"]) return;
        if (Managers.Game.npcFirstDictionary[$"{nameof(GuildGuide)}"]) return;
        isHover = true;
        SetOutline();
    }

    public override void ExitHover()
    {
        if (!Managers.Game.npcFirstDictionary[$"{nameof(QuestBoardManager)}"]) return;
        if (Managers.Game.npcFirstDictionary[$"{nameof(GuildGuide)}"]) return;
        if (!isHover) return;
        isHover = false;
        SetOutline();
    }
}
