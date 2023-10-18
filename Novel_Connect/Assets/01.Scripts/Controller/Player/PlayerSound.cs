using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerSound 
{
    protected PlayerController player;
    protected Coroutine walkSoundCoroutine;
    protected Coroutine runSoundCoroutine;
    protected Coroutine elementalSoundCoroutine;
    public virtual void PlayAttackSound(int _index)
    {
        Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Player_Attack, _index);
    }

    public virtual void PlayWalkSound()
    {
        StopWalkSound();
        walkSoundCoroutine = Managers.Routine.StartCoroutine(PlayWalkSoundRoutine());
    }

    protected virtual IEnumerator PlayWalkSoundRoutine()
    {
        yield return null;

    }

    public virtual void StopWalkSound()
    {

    }

    public virtual void PlayRunSound()
    {
        StopRunSound();
        runSoundCoroutine = Managers.Routine.StartCoroutine(PlayRunSoundRoutine());
    }

    protected virtual IEnumerator PlayRunSoundRoutine()
    {
        yield return null;
    }

    public virtual void StopRunSound()
    {
        if (runSoundCoroutine == null) return;
        Managers.Routine.StopCoroutine(runSoundCoroutine);
        runSoundCoroutine = null;
    }
    public virtual void PlayJumpStartSound()
    {
        Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Player_ETC,0);
    }

    public virtual void PlayFallEndSound()
    {
        Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Player_ETC);
    }

    public virtual void PlayHitSound()
    {

    }
    public virtual void PlaySkillOneSound()
    {

    }
    public virtual void PlaySkillTwoSound()
    {

    }
    public virtual void PlayElementalSound()
    {
        StopElementalSound();
        elementalSoundCoroutine = Managers.Routine.StartCoroutine(PlayElemetalSoundRoutine());
    }
    protected virtual IEnumerator PlayElemetalSoundRoutine()
    {
        yield return null;
    }
    public virtual void StopElementalSound()
    {
        if (elementalSoundCoroutine == null) return;
        Managers.Routine.StopCoroutine(elementalSoundCoroutine);
        elementalSoundCoroutine = null;
    }
    public virtual void PlayChangeElementalSound()
    {

    }
    public virtual void PlayEndElementalSound()
    {

    }

}
namespace PlayerSounds
{
    public class Normal : PlayerSound
    {
        public Normal(PlayerController _player)
        {
            player = _player;
            StopElementalSound();
            StopRunSound();
            StopWalkSound();
        }
    }
    public class Fire : PlayerSound
    {
        public Fire(PlayerController _player)
        {
            player = _player;
            StopElementalSound();
            StopRunSound();
            StopWalkSound();
        }
    }
}
