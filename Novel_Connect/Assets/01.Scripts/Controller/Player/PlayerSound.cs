using System.Collections;
using System.Collections.Generic;
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
        walkSoundCoroutine = Managers.Routine.StartCoroutine(PlayWalkSoundRoutine());
    }
    protected virtual IEnumerator PlayWalkSoundRoutine()
    {
        yield return new WaitForSeconds(0.05f);
        float animationTime = player.animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationTime * 0.5f - 0.05f);
        Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Player_Walk);
        yield return new WaitForSeconds(animationTime * 0.5f);
        Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Player_Walk);
        walkSoundCoroutine = Managers.Routine.StartCoroutine(PlayWalkSoundRoutine());
    }
    public virtual void StopWalkSound()
    {
        Managers.Routine.StopCoroutine(walkSoundCoroutine);
    }
    public virtual void PlayRunSound()
    {
        runSoundCoroutine = Managers.Routine.StartCoroutine(PlayRunSoundRoutine());
    }
    protected virtual IEnumerator PlayRunSoundRoutine()
    {
        Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Player_Run);
        yield return new WaitForSeconds(0.05f);
        float animationTime = player.animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationTime * 0.5f - 0.05f);
        Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Player_Run);
        yield return new WaitForSeconds(animationTime * 0.5f);
        walkSoundCoroutine = Managers.Routine.StartCoroutine(PlayRunSoundRoutine());
    }
    public virtual void StopRunSound()
    {
        Managers.Routine.StopCoroutine(runSoundCoroutine);
    }
    public virtual void PlayJumpStartSound()
    {

    }
    public virtual void PlayJumpSound()
    {

    }
    public virtual void PlayFallSound()
    {

    }
    public virtual void PlayFallEndSound()
    {

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
        elementalSoundCoroutine = Managers.Routine.StartCoroutine(PlayElemetalSoundRoutine());
    }
    protected virtual IEnumerator PlayElemetalSoundRoutine()
    {
        yield return null;
    }
    public virtual void StopElementalSound()
    {
        Managers.Routine.StopCoroutine(elementalSoundCoroutine);
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
        }
    }
    public class Fire : PlayerSound
    {
        public Fire(PlayerController _player)
        {
            player = _player;
        }
    }
}
