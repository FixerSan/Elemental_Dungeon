using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill_2 : MonoBehaviour
{
    [SerializeField]
    private GameObject skill_2_1;
    [SerializeField]
    private GameObject skill_2_2;



    void Setup()
    {
        transform.eulerAngles = PlayerController.instance.transform.eulerAngles;
        transform.position = PlayerController.instance.transform.position;

        skill_2_1.transform.localPosition = Vector3.zero;
        skill_2_1.SetActive(true);

        skill_2_2.SetActive(true);
    }

    private void OnEnable()
    {
        Setup();
    }

    public void Disable()
    {
        SkillObjectPool.instance.ReturnSkill(this.gameObject, 1);
    }
}
