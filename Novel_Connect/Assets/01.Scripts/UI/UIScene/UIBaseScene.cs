using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseScene : UIScene
{
    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindSlider(typeof(Sliders));
        BindImage(typeof(Images));
        BindObject(typeof(Objects));


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

        GetImage((int)Images.Image_Skill_OneCoolTime).fillAmount = 0;
        GetImage((int)Images.Image_Skill_TwoCoolTime).fillAmount = 0;

        return true;
    }

    private enum Sliders { Slider_HP, Slider_MP }
    private enum Images { Image_Illust, Image_Skill_One, Image_Skill_OneCoolTime, Image_Skill_Two, Image_Skill_TwoCoolTime }
    private enum Objects { Object_ChangeElemental }

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
}
