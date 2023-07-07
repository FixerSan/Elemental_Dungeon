using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerSound
{
    public PlayerControllerV3 player;

    public void PlayAudioClip()
    {
        //���� ���� �������� �ִϸ��̼��� �̸��� "�̸�" �� ��
        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            //����� �ý����� ���� ���
            AudioSystem.Instance.PlayOneShotSoundProfile("Main_Character_Attack", 0); //�����ͺ��̽��� �ִ� �������� �� "�̸�", �� ��° �迭�� Ŭ���� ����
        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            AudioSystem.Instance.PlayOneShotSoundProfile("Main_Character_Attack", 1);

        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
            AudioSystem.Instance.PlayOneShotSoundProfile("Main_Character_Attack", 2);

        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack4"))
            AudioSystem.Instance.PlayOneShotSoundProfile("Main_Character_Attack", 3);

        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            AudioSystem.Instance.PlayOneShotSoundProfile("Main_Character_Foot");

        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Start"))
            AudioSystem.Instance.PlayOneShotSoundProfile("Main_Character_Jump_Start");

        if (player.anim.GetCurrentAnimatorStateInfo(1).IsName("HitEffect"))
            AudioSystem.Instance.PlayOneShotSoundProfile("Main_Character_Voice");
    }

    public void Setup(PlayerControllerV3 player_)
    {
        player = player_;
    }
}