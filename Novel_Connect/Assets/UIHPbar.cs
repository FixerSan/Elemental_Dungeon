using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHPbar : MonoBehaviour
{
    public BaseController controller;
    public Image hpImage;
    public TMPro.TextMeshProUGUI text;

    private void Awake()
    {
        gameObject.transform.SetParent(null);
    }

    public void FixedUpdate()
    {
        if (controller == null)
            Managers.Resource.Destroy(gameObject);
        hpImage.fillAmount = controller.status.currentHP / controller.status.maxHP;
        text.text = $"{controller.status.currentHP} / {controller.status.maxHP}";
    }
}
