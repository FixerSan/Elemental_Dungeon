using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BattleManager
{
    public float amageMultiplier;   // ������ ó�� ����

    //�Ӽ� �� ������ ���
    public void DamageCalculate(BaseController _attacker, BaseController _hiter, float _damage)
    {
        float calculatedDamage = _attacker.status.currentAttackForce;

        // �Ӽ��� ���� ������ ���
        switch (_hiter.elemental)
        {
            case Elemental.Water:
                if (_attacker.elemental == Elemental.Wind || _attacker.elemental == Elemental.Glass || _attacker.elemental == Elemental.Electric)
                {
                    calculatedDamage *= amageMultiplier;
                }
                break;
            case Elemental.Wind:
                if (_attacker.elemental == Elemental.Glass || _attacker.elemental == Elemental.Ice)
                {
                    calculatedDamage *= amageMultiplier;
                }
                break;
            case Elemental.Rock:
                if (_attacker.elemental == Elemental.Water || _attacker.elemental == Elemental.Poison || _attacker.elemental == Elemental.Electric)
                {
                    calculatedDamage *= amageMultiplier;
                }
                break;
            case Elemental.Glass:
                if (_attacker.elemental == Elemental.Wind || _attacker.elemental == Elemental.Ice || _attacker.elemental == Elemental.Poison)
                {
                    calculatedDamage *= amageMultiplier;
                }
                break;
            case Elemental.Electric:
                if (_attacker.elemental == Elemental.Wind || _attacker.elemental == Elemental.Ice || _attacker.elemental == Elemental.Poison)
                {
                    calculatedDamage *= amageMultiplier;
                }
                break;
            case Elemental.Ice:
                if (_attacker.elemental == Elemental.Rock)
                {
                    calculatedDamage *= amageMultiplier;
                }
                break;
            case Elemental.Poison:
                if (_attacker.elemental == Elemental.Rock)
                {
                    calculatedDamage *= amageMultiplier;
                }
                break;
            default:
                break;
        }

        // ������ ����
        _hiter.Hit(_attacker.trans, calculatedDamage);
    }


    // Ư�� ȿ�� (����, �����) ����
    public void SetStatusEffect(BaseController _Attacker, BaseController _hiter, StatusEffect _status)
    {
        switch (_status)
        {
            case StatusEffect.BURN:
                _hiter.status.StartBurn(_Attacker.status.currentAttackForce * 0.05f);
                break;

            case StatusEffect.FREEZE:
                _hiter.status.SetFreezeCount();
                break;
        }
    }
}
