using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill_2 : MonoBehaviour
{
    [SerializeField]
    private GameObject skill_2_1;
    [SerializeField]
    private GameObject skill_2_2;
    PlayerControllerV3 player => FindObjectOfType<PlayerControllerV3>();


    void Setup()
    {
        AudioSystem.Instance.PlayOneShotSoundProfile("Main_Character_Transform_Fire", 2);

        transform.eulerAngles = player.transform.eulerAngles;
        transform.position = player.transform.position;

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
        ObjectPool.instance.ReturnSkill(this.gameObject, 1);
    }
}
