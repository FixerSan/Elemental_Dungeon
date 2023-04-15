using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    #region Singleton, DontDestroyOnLoad;
    private static BattleSystem Instance;
    public static BattleSystem instance
    {
        get { return Instance; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public float amageMultiplier;

    public void Calculate(Elemental attackerElemental, Elemental hiterElemental, IHitable hiter, float damage)
    {
        float calculatedDamage = damage;

        // 속성에 따라 데미지 계산
        switch (hiterElemental)
        {
            case Elemental.Water:
                if (attackerElemental == Elemental.Wind || attackerElemental == Elemental.Glass || attackerElemental == Elemental.Electric)
                {
                    calculatedDamage *= amageMultiplier;
                }
                break;
            case Elemental.Wind:
                if (attackerElemental == Elemental.Glass || attackerElemental == Elemental.Ice)
                {
                    calculatedDamage *= amageMultiplier;
                }
                break;
            case Elemental.Rock:
                if (attackerElemental == Elemental.Water || attackerElemental == Elemental.Poison || attackerElemental == Elemental.Electric)
                {
                    calculatedDamage *= amageMultiplier;
                }
                break;
            case Elemental.Glass:
                if (attackerElemental == Elemental.Wind || attackerElemental == Elemental.Ice || attackerElemental == Elemental.Poison)
                {
                    calculatedDamage *= amageMultiplier;
                }
                break;
            case Elemental.Electric:
                if (attackerElemental == Elemental.Wind || attackerElemental == Elemental.Ice || attackerElemental == Elemental.Poison)
                {
                    calculatedDamage *= amageMultiplier;
                }
                break;
            case Elemental.Ice:
                if (attackerElemental == Elemental.Rock)
                {
                    calculatedDamage *= amageMultiplier;
                }
                break;
            case Elemental.Poison:
                if (attackerElemental == Elemental.Rock)
                {
                    calculatedDamage *= amageMultiplier;
                }
                break;
            default:
                break;
        }

        // 데미지 적용
        hiter.Hit(calculatedDamage);
    }

    public void SetStatusEffect(IStatusEffect hiter,StatusEffect status, float duration, float damage)
    {
        hiter.SetStatusEffect(status, duration, damage);
    }
}
