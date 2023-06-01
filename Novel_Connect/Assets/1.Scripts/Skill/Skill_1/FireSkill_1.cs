using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill_1 : MonoBehaviour
{
    public float breathDamage;
    public float burnsDuration;
    public Transform breathPos;
    public Vector2 breathSize;
    public LayerMask attackLayer;
    void Setup()
    {
        transform.eulerAngles = PlayerController.instance.transform.eulerAngles;
        if(transform.rotation.y == 0)
        {
            transform.position = PlayerController.instance.transform.position + new Vector3(3f, -1.85f, 0) ;
        }
        else
        {
            transform.position = PlayerController.instance.transform.position + new Vector3(-3f, -1.85f, 0);
        }    
    }

    private void OnEnable()
    {
        Setup();
        StartCoroutine(Breath());
        
    }

    IEnumerator Breath()
    {
        float endTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(1f);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(breathPos.position, breathSize, 0, attackLayer);
        foreach (var item in collider2Ds)
        {
            if (item.CompareTag("Player"))
                break;
            Actor hiter = item.GetComponent<Actor>();
            if (hiter != null)
            {
                BattleSystem.instance.Calculate(Elemental.Fire, hiter.elemental, hiter, breathDamage);
                if (item.GetComponent<IStatusEffect>() != null)
                    BattleSystem.instance.SetStatusEffect(item.GetComponent<IStatusEffect>(), StatusEffect.Burns, burnsDuration, PlayerController.instance.playerData.force * 0.1f);
            }
        }
        yield return new WaitForSeconds(endTime - 1f);
        SkillObjectPool.instance.ReturnSkill(this.gameObject, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(breathPos.position, breathSize);
    }
}
