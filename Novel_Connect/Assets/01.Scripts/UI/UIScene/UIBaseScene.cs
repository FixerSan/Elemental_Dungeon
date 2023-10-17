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
        Managers.Event.OnVoidEvent -= ChangeDashCooltime;
        Managers.Event.OnVoidEvent += ChangeDashCooltime;
        Managers.Event.OnVoidEvent -= DrawElementalUI;
        Managers.Event.OnVoidEvent += DrawElementalUI;
        Managers.Event.OnVoidEvent -= DrawQuestPanel;
        Managers.Event.OnVoidEvent += DrawQuestPanel;

        GetImage((int)Images.Image_Skill_OneCoolTime).fillAmount = 1;
        GetImage((int)Images.Image_Skill_TwoCoolTime).fillAmount = 1;
        GetImage((int)Images.Image_DashCoolTime).fillAmount = 0;
        //GetImage((int)Images.Image_Skill_One).gameObject.SetActive(false);
        //GetImage((int)Images.Image_Skill_Two).gameObject.SetActive(false);

        //GetObject((int)Objects.Panel_FireSkill).SetActive(false);
        DrawQuestPanel(VoidEventType.OnChangeQuest);
        return true;
    }

    private enum Images { Image_HPbar, Image_MPbar, Image_Illust, Image_Skill_One, Image_Skill_OneCoolTime, Image_Skill_Two, Image_Skill_TwoCoolTime , Image_DashCoolTime }
    private enum Objects { Object_ChangeElemental , Panel_FireSkill }
    private enum Texts { Text_State, Text_Title }

    private enum Slots { Slot_QuestPanel_One, Slot_QuestPanel_Two, Slot_QuestPanel_Three }
    public void ChangeHPSlider(VoidEventType _eventType)
    {
        if (_eventType != VoidEventType.OnChangeHP) return;

        PlayerController player = Managers.Object.Player;
        GetImage((int)Images.Image_HPbar).fillAmount = player.status.currentHP/player.status.maxHP;
    }

    public void ChangeMPSlider(VoidEventType _eventType)
    {
        if (_eventType != VoidEventType.OnChangeMP) return;

        PlayerController player = Managers.Object.Player;
        GetImage((int)Images.Image_MPbar).fillAmount = player.status.currentMP / player.status.maxMP;
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
            if (player.skills.Length == 0) 
            {
                GetImage((int)Images.Image_Skill_OneCoolTime).fillAmount = 1;
                return;
            }
            GetImage((int)Images.Image_Skill_OneCoolTime).fillAmount = player.skills[0].currentCoolTime / player.skills[0].coolTime;
        }
    }

    public void ChangeSkill_TwoCooltime(VoidEventType _eventType)
    {
        if (_eventType == VoidEventType.OnChangeSkill_TwoCoolTime || _eventType == VoidEventType.OnChangeElemental)
        {
            PlayerController player = Managers.Object.Player;
            if (player.skills.Length == 0)
            {
                GetImage((int)Images.Image_Skill_TwoCoolTime).fillAmount = 1;
                return;
            }
            GetImage((int)Images.Image_Skill_TwoCoolTime).fillAmount = player.skills[1].currentCoolTime / player.skills[1].coolTime;
        }
    }

    public void ChangeDashCooltime(VoidEventType _eventType)
    {
        if (_eventType == VoidEventType.OnChangeDashTime || _eventType == VoidEventType.OnChangeElemental)
        {
            PlayerController player = Managers.Object.Player;

            GetImage((int)Images.Image_DashCoolTime).fillAmount = player.movement.currentdashCooltime / player.movement.dashCooltime;
        }
    }

    public void DrawElementalUI(VoidEventType _eventType)
    {
        if (_eventType == VoidEventType.OnInput_ElementalKey)
        {
            GetObject((int)Objects.Object_ChangeElemental).SetActive(Managers.Object.Player.elementals.isChangeElemental);
        }

        //if(_eventType == VoidEventType.OnChangeElemental)
        //{
        //    GetObject((int)Objects.Object_ChangeElemental).SetActive(Managers.Object.Player.elementals.isChangeElemental);
        //    if (Managers.Object.Player.elemental == Elemental.Fire)
        //    {
        //        GetImage((int)Images.Image_Skill_One).gameObject.SetActive(true);
        //        GetImage((int)Images.Image_Skill_Two).gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        GetImage((int)Images.Image_Skill_One).gameObject.SetActive(false);
        //        GetImage((int)Images.Image_Skill_Two).gameObject.SetActive(false);
        //    }
        //}
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
        Managers.Event.OnVoidEvent -= ChangeDashCooltime;
        Managers.Event.OnVoidEvent -= DrawElementalUI;
        Managers.Event.OnVoidEvent -= DrawQuestPanel;
    }
}
