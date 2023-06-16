using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerAttack
{
    public PlayerControllerV3 player;
    public Transform attackPos;
    public LayerMask attackLayer;
    public float canAttackDuration;

    public GameObject fireStartSkill;

    public List<Skill<PlayerControllerV3>> skills = new List<Skill<PlayerControllerV3>>();

    public int attackCount;

    public Coroutine attackCoroutine;
    public void AttackActor()
    {
        Collider2D[] collider2Ds_1 = Physics2D.OverlapBoxAll(attackPos.position, attackPos.localScale, 0, attackLayer);
        AudioSystem.Instance.PlayOneShotSoundProfile("AttackClips", attackCount - 1);
        foreach (Collider2D hitTarget in collider2Ds_1)
        {
            if (hitTarget.CompareTag("Player")) continue;
            #region Actor
            Actor hitActor = hitTarget.GetComponent<Actor>();
            if (hitActor != null)
            {
                
                hitActor.SetTarget(player.gameObject);
                BattleSystem.instance.HitCalculate(player.elemental, hitActor.elemental, hitActor, player.statuses.force);
                switch (player.elemental)
                {
                    case Elemental.Fire:
                        BattleSystem.instance.SetStatusEffect(hitActor, StatusEffect.Burns, 5);
                        break;
                }
                continue;
            }
            #endregion
            #region HitableObejct
            HitableObejct hitableObejct = hitTarget.GetComponent<HitableObejct>();
            if (hitableObejct)
            {
                hitableObejct.GetDamage(player.statuses.force);

            }
            #endregion
        }
    }
    public IEnumerator AttackCoroutine()
    {
        //���� �ܰ迡 ���� �ٸ� ���� ���
        switch (attackCount)
        {
            case 1: //1 Ÿ ���� 
                yield return new WaitForSeconds(0.18f/1.5f);

                AttackActor();
                yield return new WaitForSeconds(0.3f/1.5f);
                break;

            case 2://1.5f Ÿ ����
                yield return new WaitForSeconds(0.21f/1.5f);
                AttackActor();
                yield return new WaitForSeconds(0.35f/1.5f);
                AttackActor();
                yield return new WaitForSeconds(0.35f/1.5f);
                break;

            case 3: //3Ÿ ����
                yield return new WaitForSeconds(0.84f/1.5f);
                AttackActor();
                yield return new WaitForSeconds(0.35f/1.5f);
                break;

            case 4: //3Ÿ ����
                yield return new WaitForSeconds(0.84f/1.5f);
                AttackActor();
                yield return new WaitForSeconds(0.66f/1.5f);
                break;
        }
        player.ChangeState(PlayerState.Idle);

        //������ ���� �ܰ� �ʱ�ȭ �ð� ��ŭ ��ٸ� �� ���� �ܰ� �ʱ�ȭ
        yield return new WaitForSeconds(canAttackDuration);
        attackCoroutine = null;
        attackCount = 0;
        player.anim.SetInteger("AttackCount", attackCount);
    }
    public void Setup(PlayerControllerV3 player_)
    {
        player = player_;
    }
}
