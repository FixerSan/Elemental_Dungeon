using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerSound
{
    public PlayerControllerV3 player;

    public void PlayAudioClip()
    {
        //만약 현재 실행중인 애니메이션의 이름이 "이름" 일 때
        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            //오디오 시스템의 실행 명령
            AudioSystem.Instance.PlayOneShotSoundProfile("Main_Character_Attack", 0); //데이터베이스에 있는 프로파일 중 "이름", 몇 번째 배열의 클립을 실행
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