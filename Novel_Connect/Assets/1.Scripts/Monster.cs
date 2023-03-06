using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int id;
    public float hp;
    public GameObject hitEffect;
    public float currentHp;
    private AudioSource m_AudioSource;
    private Animator m_Animator;
    public string m_name;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        currentHp = hp;

        m_AudioSource = GetComponent<AudioSource>();
        name = m_name;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp <= 0)
            Death();
    }

    public void Death()
    {


        GameManager.instance.onEnenyDeath.Invoke(id);
        Destroy(gameObject);
    }

    public void Hit(float damage)
    {
        m_Animator.SetTrigger("isHitTrigger");
        currentHp -= damage;

        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        StartCoroutine(hitEffectCoroutine());
    }

    IEnumerator hitEffectCoroutine()
    {
        GameObject hitEffect_ = Instantiate(hitEffect, transform);
        hitEffect_.transform.position = transform.position;
        yield return new WaitForSeconds(1f);
        Destroy(hitEffect_);
    }
}
