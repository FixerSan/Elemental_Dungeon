using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class UIBaseScene : UIScene
{
    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindSlider(typeof(Sliders));
        BindImage(typeof(Images));
        BindObject(typeof(Objects));
        BindText(typeof(Texts));
        Bind<UISlot_QuestPanel>(typeof(Slots));

        Managers.Event.OnVoidEvent -= ChangeHPSlider;
        Managers.Event.OnVoidEvent += ChangeHPSlider;
        Managers.Event.OnVoidEvent -= ChangeMPSlider;
        Managers.Event.OnVoidEvent += ChangeMPSlider;
        Managers.Event.OnVoidEvent -= ChangeElemetal;
        Managers.Event.OnVoidEvent += ChangeElemetal;
        Managers.Event.OnVoidEvent -= ChangeSkill_OneCooltime;
        Managers.Event.OnVoidEvent += ChangeSkill_OneCooltime;
        Managers.Event.OnVoidEvent -= ChangeSkill_TwoCooltime;
        Managers.Event.OnVoidEvent += ChangeSkill_TwoCooltime;
        Managers.Event.OnVoidEvent -= DrawElementalUI;
        Managers.Event.OnVoidEvent += DrawElementalUI;
        Managers.Event.OnVoidEvent -= DrawQuestPanel;
        Managers.Event.OnVoidEvent += DrawQuestPanel;

        GetImage((int)Images.Image_Skill_OneCoolTime).fillAmount = 0;
        GetImage((int)Images.Image_Skill_TwoCoolTime).fillAmount = 0;

        GetObject((int)Objects.Object_ChangeElemental).SetActive(Managers.Object.Player.elementals.isChangeElemental);
        DrawQuestPanel(VoidEventType.OnChangeQuest);
        return true;
    }

    private enum Sliders { Slider_HP, Slider_MP }
    private enum Images { Image_Illust, Image_Skill_One, Image_Skill_OneCoolTime, Image_Skill_Two, Image_Skill_TwoCoolTime }
    private enum Objects { Object_ChangeElemental  }
    private enum Texts { Text_State, Text_Title }

    private enum Slots { Slot_QuestPanel_One, Slot_QuestPanel_Two, Slot_QuestPanel_Three }
    public void ChangeHPSlider(VoidEventType _eventType)
    {
        if (_eventType != VoidEventType.OnChangeHP) return;

        PlayerController player = Managers.Object.Player;
        GetSlider((int)Sliders.Slider_HP).value = player.status.currentHP/player.status.maxHP;
    }

    public void ChangeMPSlider(VoidEventType _eventType)
    {
        if (_eventType != VoidEventType.OnChangeMP) return;

        PlayerController player = Managers.Object.Player;
        GetSlider((int)Sliders.Slider_MP).value = player.status.currentMP / player.status.maxMP;
    }

    public void ChangeElemetal(VoidEventType _eventType)
    {
        if (_eventType != VoidEventType.OnChangeElemental) return;
    }

    public void ChangeSkill_OneCooltime(VoidEventType _eventType)
    {
        if (_eventType == VoidEventType.OnChangeSkill_OneCoolTime || _eventType == VoidEventType.OnChangeElemental)
        {
            PlayerController player = Managers.Object.Player;
            if (player.skills.Length == 0) return;
            GetImage((int)Images.Image_Skill_OneCoolTime).fillAmount = player.skills[0].currentCoolTime / player.skills[0].coolTime;
        }
    }

    public void ChangeSkill_TwoCooltime(VoidEventType _eventType)
    {
        if (_eventType == VoidEventType.OnChangeSkill_TwoCoolTime || _eventType == VoidEventType.OnChangeElemental)
        {
            PlayerController player = Managers.Object.Player;
            if (player.skills.Length == 0) return;
            GetImage((int)Images.Image_Skill_TwoCoolTime).fillAmount = player.skills[1].currentCoolTime / player.skills[1].coolTime;
        }
    }

    public void DrawElementalUI(VoidEventType _eventType)
    {
        if (_eventType == VoidEventType.OnInput_ElementalKey || _eventType == VoidEventType.OnChangeElemental)
        {
            GetObject((int)Objects.Object_ChangeElemental).SetActive(Managers.Object.Player.elementals.isChangeElemental);
        }
    }

    public void DrawQuestPanel(VoidEventType _eventType)
    {
        if (_eventType != VoidEventType.OnChangeQuest) return;
        GetText((int)Texts.Text_Title).text = $"수행중인 퀘스트 ({Managers.Quest.quests.Count}/3)";

        for (int i = 0; i < 3; i++)
        {
            Get<UISlot_QuestPanel>(i).Disabled();
        }

        for (int i = 0; i < Managers.Quest.quests.Count; i++)
        {
            Get<UISlot_QuestPanel>(i).DrawQuestInfo(Managers.Quest.quests[i]);
        }
    }

    private void Update()
    {
        GetText((int)Texts.Text_State).text = Managers.Object.Player.state.ToString();
    }

    private void OnDestroy()
    {
        Managers.Event.OnVoidEvent -= ChangeHPSlider;
        Managers.Event.OnVoidEvent -= ChangeMPSlider;
        Managers.Event.OnVoidEvent -= ChangeElemetal;
        Managers.Event.OnVoidEvent -= ChangeSkill_OneCooltime;
        Managers.Event.OnVoidEvent -= ChangeSkill_TwoCooltime;
        Managers.Event.OnVoidEvent -= DrawElementalUI;
        Managers.Event.OnVoidEvent -= DrawQuestPanel;
    }
}
