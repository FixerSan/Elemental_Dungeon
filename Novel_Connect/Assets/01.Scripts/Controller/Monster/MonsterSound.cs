using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterSound
{
    protected MonsterController monster;
    protected Coroutine moveCoroutine;
    public abstract void PlayMoveSound();
    public abstract void StopMoveSound();
    public abstract void PlayHitSound();
    public abstract void PlayAttackSound();
}
namespace MonsterSounds
{
    public class Ghost_Bat : MonsterSound
    {
        public Ghost_Bat(MonsterController _monster)
        {
            monster = _monster;
            moveCoroutine = null;
        }
        public override void PlayMoveSound()
        {
            moveCoroutine = Managers.Routine.StartCoroutine(PlayMoveSoundRoutine());
        }

        public override void StopMoveSound()
        {
            if(moveCoroutine != null)
                Managers.Routine.StopCoroutine(moveCoroutine);
        }

        public IEnumerator PlayMoveSoundRoutine()
        {
            if(monster != null)
            {
                Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Ghost_Bat_Move);
                yield return new WaitForSeconds(0.5f);
                moveCoroutine = Managers.Routine.StartCoroutine(PlayMoveSoundRoutine());
            }
        }

        public override void PlayHitSound()
        {
            Managers.Sound.PlaySoundEffect(Define.AudioClip_Effect.Ghost_Bat_Hit);
        }

        public override void PlayAttackSound()
        {
            Managers.Sound.PlaySoundEffect(Define.AudioClip_Effect.Ghost_Bat_Attack);
        }
    }

}
