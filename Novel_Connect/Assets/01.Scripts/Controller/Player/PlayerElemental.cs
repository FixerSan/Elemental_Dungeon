using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerElemental
{
    public PlayerController player;
    public bool isHasFire = false;
    public bool isHasIce = false;
    public bool isChangeElemental = false;
    public void CheckChangeElemental()
    {
        Managers.Input.CheckInput(Managers.Input.changeElementalKey, (_inputType) => 
        {
            if (_inputType != InputType.PRESS) return;
            isChangeElemental = !isChangeElemental;
        });

        if (isChangeElemental)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && player.elemental != Elemental.Normal)
            {
                ChangeElemental(Elemental.Normal);
                isChangeElemental = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && player.elemental != Elemental.Fire && isHasFire)
            {
                ChangeElemental(Elemental.Fire);
                isChangeElemental = false;
            }
        }
    }

    public void ChangeElemental(Elemental _elemental, Action _callback = null)
    {
        switch (_elemental)
        {
            case Elemental.Normal:
                player.attack = new PlayerAttacks.Normal(player);
                player.movement = new Playermovements.Normal(player);
                player.sound = new PlayerSounds.Normal(player);
                player.skills = new PlayerSkill[0];
                break;

            case Elemental.Fire:
                player.attack = new PlayerAttacks.Fire(player);
                player.movement = new Playermovements.Fire(player);
                player.sound = new PlayerSounds.Fire(player);
                player.skills = new PlayerSkill[2];
                player.skills[0] = new PlayerSkills.Fire.One(player);
                player.skills[1] = new PlayerSkills.Fire.Two(player);
                break;
        }

        player.elemental = _elemental;
        Managers.Resource.Load<RuntimeAnimatorController>($"Player_{_elemental}", (ac) =>
        {
            player.animator.runtimeAnimatorController = ac;
            isChangeElemental = false;
            Managers.Event.OnVoidEvent?.Invoke(VoidEventType.OnChangeElemental);
            _callback?.Invoke();
        });
    }

    public PlayerElemental(PlayerController _player) 
    {
        player = _player;
        isChangeElemental = false;
        isHasFire = true;
    }
}
