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
        player.movement.isWalking = true;
        while (player.movement.isWalking)
        {
            yield return null;
            float animationTime = player.animator.GetCurrentAnimatorStateInfo(0).length;
            player.movement.walkDistance += Time.deltaTime;
            if(player.movement.walkDistance >= animationTime * 0.5f)
            {

                Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Player_Walk);
                player.movement.walkDistance = 0f;
            }
        }
    }
    public virtual void StopWalkSound()
    {
        player.movement.isWalking = false;
        if (walkSoundCoroutine == null) return;
        Managers.Routine.StopCoroutine(walkSoundCoroutine);
        walkSoundCoroutine = null;
    }
    public virtual void PlayRunSound()
    {
        StopRunSound();
        runSoundCoroutine = Managers.Routine.StartCoroutine(PlayRunSoundRoutine());
    }
    protected virtual IEnumerator PlayRunSoundRoutine()
    {
        player.movement.isRuning = true;
        while (player.movement.isRuning)
        {
            yield return null;
            float animationTime = player.animator.GetCurrentAnimatorStateInfo(0).length;
            player.movement.walkDistance += Time.deltaTime;
            if (player.movement.walkDistance >= animationTime * 0.5f)
            {
                Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Player_Run);
                player.movement.walkDistance = 0f;
            }
        }
    }
    public virtual void StopRunSound()
    {
        player.movement.isRuning = false;
        if (runSoundCoroutine == null) return;
        Managers.Routine.StopCoroutine(runSoundCoroutine);
        runSoundCoroutine = null;
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
