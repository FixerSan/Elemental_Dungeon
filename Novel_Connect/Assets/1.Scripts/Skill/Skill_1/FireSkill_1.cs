using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill_1 : MonoBehaviour
{
    public PlayerControllerV3 player => FindObjectOfType<PlayerControllerV3>();
    public float breathDamage;
    public float burnsDuration;
    public Transform breathPos;
    public Vector2 breathSize;
    public LayerMask attackLayer;
    void Setup()
    {
        transform.eulerAngles = player.transform.eulerAngles;
        if(transform.rotation.y == 0)
        {
            transform.position = player.transform.position + new Vector3(3f, -1.85f, 0) ;
        }
        else
        {
            transform.position = player.transform.position + new Vector3(-3f, -1.85f, 0);
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
            if (item.CompareTag("Monster"))
            {
                Actor hiter = item.GetComponent<Actor>();
                if (hiter != null)
                {
                    hiter.SetTarget(player.gameObject);
                    BattleSystem.instance.HitCalculate(Elemental.Fire, hiter.elemental, hiter, breathDamage);
                    if (item.GetComponent<Actor>() != null)
                        BattleSystem.instance.SetStatusEffect(hiter, StatusEffect.Burns, burnsDuration);
                }
            }
        }
        yield return new WaitForSeconds(endTime - 1f);
        ObjectPool.instance.ReturnSkill(this.gameObject, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(breathPos.position, breathSize);
    }
}
