using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public Actor target;
    private RectTransform rect;
    private Slider slider;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!target)    return;
        SetValue();

        rect.position = Camera.main.WorldToScreenPoint(target.hpBarPos.position);

        if(rect.rotation.y != 0)
        {
            rect.eulerAngles = Vector3.zero;
        }

        if (target.statuses.currentHp == 0)
        {
            ReturnObjectPool();
        }
    }
    public void SetTarget(Actor target_)
    {
        target = target_;
    }
    void SetValue()
    {
        slider.value = target.statuses.currentHp / target.statuses.maxHp;
    }
    void ReturnObjectPool()
    {
        ObjectPool.instance.ReturnHpBar(gameObject);
    }
}
