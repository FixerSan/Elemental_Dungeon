using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBoss : BaseBoss
{
    public void PlaySoundClip()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Appearance"))
            AudioSystem.Instance.PlayOneShotSoundProfile("IceBoss_Effect",0);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1"))
            AudioSystem.Instance.PlayOneShotSoundProfile("IceBoss_Effect", 1);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2"))
            AudioSystem.Instance.PlayOneShotSoundProfile("IceBoss_Effect", 2);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("die"))
            AudioSystem.Instance.PlayOneShotSoundProfile("IceBoss_Effect", 3);
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skill 1"))
            AudioSystem.Instance.PlayOneShotSoundProfile("IceBoss_Effect", 4);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sword_Call"))
            AudioSystem.Instance.PlayOneShotSoundProfile("IceBoss_Effect", 5);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skill 2-1"))
            AudioSystem.Instance.PlayOneShotSoundProfile("IceBoss_Effect", 6);
    }
}
